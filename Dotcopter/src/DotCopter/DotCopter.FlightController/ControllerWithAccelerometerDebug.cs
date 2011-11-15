using System;
using System.IO.Ports;
using System.Threading;
using DotCopter.Commons.Logging;
using DotCopter.Commons.Utilities;
using DotCopter.ControlAlgorithms.Mixing;
using DotCopter.Hardware.Accelerometer;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Motor;
using DotCopter.Hardware.Radio;

namespace DotCopter.FlightController
{
    public class ControllerWithAccelerometerDebug
    {
        public ControllerWithAccelerometerDebug(MotorMixer mixer, AxesController pid, Gyro gyro, Accelerometer accelerometer, Radio radio, ControllerLoopSettings loopSettings, MotorSettings motorSettings, ILogger logger)
        {
            long lastRadioTime = 0;
            long lastSensorTime = 0;
            long lastControlTime = 0;
            long lastMotorTime = 0;
            long lastTelemetryTime = 0;
            bool systemArmed = false;
            
            Thread.Sleep(1000);
            gyro.Zero();
            accelerometer.Zero();

            var serialPort = new SerialPort("COM1",115200);
            serialPort.Open();

            while (true)
            {
                long currentTime = DateTime.Now.Ticks;
                if (currentTime >= (lastRadioTime + loopSettings.RadioLoopPeriod))
                {
                    radio.Update();
                    systemArmed = radio.Throttle > motorSettings.MinimumOutput;
                    if (!systemArmed)
                        logger.Flush();
                    lastRadioTime = currentTime;
                }

               
                currentTime = DateTime.Now.Ticks;
                if (currentTime >= (lastSensorTime + loopSettings.SensorLoopPeriod))
                {
                    gyro.Update();
                    accelerometer.Update();
                    lastSensorTime = currentTime;
                }

                currentTime = DateTime.Now.Ticks;
                if (currentTime >= (lastControlTime + loopSettings.ControlAlgorithmPeriod))
                {
                    if (systemArmed)
                        pid.Update(radio.Axes, gyro.Axes, (float)(currentTime - lastControlTime) / loopSettings.LoopUnit);
                    lastControlTime = currentTime;
                }

                currentTime = DateTime.Now.Ticks;
                if (currentTime >= (lastMotorTime + loopSettings.MotorLoopPeriod))
                {
                    if (systemArmed)
                        mixer.Update(radio.Throttle, pid.Axes);
                    else
                        mixer.SetSafe();
                    
                    lastMotorTime = currentTime;
                }

                currentTime = DateTime.Now.Ticks;
                if (currentTime >= (lastTelemetryTime + loopSettings.TelemetryLoopPeriod))
                {
                    byte[] buffer= new byte[12];

                    BitConverter.ToBytes(ref buffer, 0, accelerometer.Axes.Pitch);
                    BitConverter.ToBytes(ref buffer, 4, accelerometer.Axes.Roll);
                    BitConverter.ToBytes(ref buffer, 8, accelerometer.Axes.Yaw);

                    serialPort.Write(buffer, 0, buffer.Length);
                    lastTelemetryTime = currentTime;
                }
            }
        }
    }
}

