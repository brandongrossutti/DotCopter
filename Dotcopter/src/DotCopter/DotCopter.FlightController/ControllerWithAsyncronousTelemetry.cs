using System;
using System.IO.Ports;
using System.Threading;
using DotCopter.Commons.Logging;
using DotCopter.Commons.Serialization;
using DotCopter.Commons.Utilities;
using DotCopter.ControlAlgorithms.Mixing;
using DotCopter.Hardware.Accelerometer;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Motor;
using DotCopter.Hardware.Radio;
using Microsoft.SPOT;

namespace DotCopter.FlightController
{
    public class ControllerWithAsyncronousTelemetry
    {
        private TelemetryData _telemetryData = new TelemetryData();
        private long _previousTime = DateTime.Now.Ticks;
        private long _lastRadioTime;
        private long _lastSensorTime;
        private long _lastControlTime;
        private long _lastMotorTime;
        private long _lastTelemetryTime;
        private bool _systemArmed;
        private SerialPort _serialPort = new SerialPort("COM1", 115200);
        private MotorMixer _mixer;
        private AxesController _pid;
        private Gyro _gyro;
        private Accelerometer _accelerometer;
        private Radio _radio;
        private ControllerLoopSettings _loopSettings;
        private MotorSettings _motorSettings;
        private ILogger _logger;

        public ControllerWithAsyncronousTelemetry(
            MotorMixer mixer, 
            AxesController pid, 
            Gyro gyro, 
            Accelerometer accelerometer, 
            Radio radio, 
            ControllerLoopSettings loopSettings, 
            MotorSettings motorSettings, 
            ILogger logger)
        {
            _mixer = mixer;
            _pid = pid;
            _gyro = gyro;
            _accelerometer = accelerometer;
            _radio = radio;
            _loopSettings = loopSettings;
            _motorSettings = motorSettings;
            _logger = logger;
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
            _serialPort.Open();

            Thread.Sleep(1000);
            gyro.Zero();
            accelerometer.Zero();
            Start();
        }

        private void Start()
        {
            while (true)
            {
                long currentTime = DateTime.Now.Ticks;
                if (currentTime >= (_lastRadioTime + _loopSettings.RadioLoopPeriod))
                {
                    _lastRadioTime = currentTime;
                    _radio.Update();
                    _systemArmed = _radio.Throttle > _motorSettings.MinimumOutput;
                    if (!_systemArmed)
                        _logger.Flush();
                }


                currentTime = DateTime.Now.Ticks;
                if (_systemArmed && (currentTime >= (_lastSensorTime + _loopSettings.SensorLoopPeriod)))
                {
                    _lastSensorTime = currentTime;
                    _gyro.Update();
                    _accelerometer.Update();
                }

                currentTime = DateTime.Now.Ticks;
                if (_systemArmed && (currentTime >= (_lastControlTime + _loopSettings.ControlAlgorithmPeriod)))
                {
                    _lastControlTime = currentTime;
                    _pid.Update(_radio.Axes, _gyro.Axes, (float)(currentTime - _previousTime) / _loopSettings.LoopUnit);
                    _previousTime = currentTime;
                }

                currentTime = DateTime.Now.Ticks;
                if (currentTime >= (_lastMotorTime + _loopSettings.MotorLoopPeriod))
                {
                    if (_systemArmed)
                        _mixer.Update(_radio.Throttle, _pid.Axes);
                    else
                        _mixer.SetSafe();

                    _lastMotorTime = currentTime;
                }

                currentTime = DateTime.Now.Ticks;
                if (_systemArmed && (currentTime >= (_lastTelemetryTime + _loopSettings.TelemetryLoopPeriod)))
                {
                    byte[] buffer = new byte[12];

                    BitConverter.ToBytes(ref buffer, 0, _gyro.Axes.Pitch);
                    BitConverter.ToBytes(ref buffer, 4, _gyro.Axes.Roll);
                    BitConverter.ToBytes(ref buffer, 8, _gyro.Axes.Yaw);

                    _serialPort.Write(buffer, 0, buffer.Length);
                    _lastTelemetryTime = currentTime;
                }

                if(!_systemArmed)
                    _pid.ZeroIntegral();
            }
        }

        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            const int frameSize = 5;
            /* Message Frame
             * Command -    1 byte
             * Data -       4 bytes
             * Total -      5 bytes */

            /* Available Commands
             * 0x01 - Pitch Proportional Gain
             * 0x02 - Pitch Integral Gain
             * 0x03 - Pitch Derivative Gain */

            if (_serialPort.BytesToRead < frameSize) return;

            while (_serialPort.BytesToRead >= frameSize)
            {
                byte[] buffer = new byte[frameSize];
                _serialPort.Read(buffer, 0, frameSize);

                byte command = buffer[0];

                switch(command)
                {
                    case 0x01:
                        _pid.RollController.Settings.ProportionalGain = BitConverter.ToFloat(buffer, 1);
                        break;
                    case 0x02:
                        _pid.RollController.Settings.IntegralGain = BitConverter.ToFloat(buffer, 1);
                        break;
                    case 0x03:
                        _pid.RollController.Settings.DerivativeGain = BitConverter.ToFloat(buffer, 1);
                        break;
                }

            }
        }
    }
}
