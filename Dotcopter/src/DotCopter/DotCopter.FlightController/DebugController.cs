using System;
using System.IO.Ports;
using DotCopter.Commons.Logging;
using DotCopter.ControlAlgorithms.Mixing;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Motor;
using DotCopter.Hardware.Radio;

namespace DotCopter.FlightController
{
    public class DebugController
    {
        public DebugController(MotorMixer mixer, AxesController pid, Gyro gyro, Radio radio, ControllerLoopSettings loopSettings, MotorSettings motorSettings, ILogger logger)
        {
            long lastRadioTime = 0;
            long lastSensorTime = 0;
            long lastControlTime = 0;
            long lastMotorTime = 0;
            long lastTelemetryTime = 0;

            float currentRadioFrequency = 0;
            float currentSensorFrequency = 0;
            float currentControlFrequency = 0;
            float currentMotorFrequency = 0;

            bool systemArmed = false;
            
            SerialPort serialPort = new SerialPort("COM1",115200);
            serialPort.Open();

            while (true)
            {
                long currentTime = DateTime.Now.Ticks;
                if (currentTime >= (lastRadioTime + loopSettings.RadioLoopPeriod))
                {
                    currentRadioFrequency = loopSettings.LoopUnit / (float)(currentTime - lastRadioTime);
                    radio.Update();
                    systemArmed = radio.Throttle > motorSettings.MinimumOutput;
                    if (!systemArmed)
                        logger.Flush();
                    lastRadioTime = currentTime;
                }

               
                currentTime = DateTime.Now.Ticks;
                if (currentTime >= (lastSensorTime + loopSettings.SensorLoopPeriod))
                {
                    currentSensorFrequency = loopSettings.LoopUnit / (float)(currentTime - lastSensorTime);
                    gyro.Update();
                    lastSensorTime = currentTime;
                }

                currentTime = DateTime.Now.Ticks;
                if (currentTime >= (lastControlTime + loopSettings.ControlAlgorithmPeriod))
                {
                    currentControlFrequency = loopSettings.LoopUnit / (float)(currentTime - lastControlTime);
                    if (systemArmed)
                        pid.Update(radio.Axes, gyro.Axes, (float)(currentTime - lastControlTime) / loopSettings.LoopUnit);
                    lastControlTime = currentTime;
                }

                currentTime = DateTime.Now.Ticks;
                if (currentTime >= (lastMotorTime + loopSettings.MotorLoopPeriod))
                {
                    currentMotorFrequency = loopSettings.LoopUnit / (float)(currentTime - lastMotorTime);
                    if (systemArmed)
                        mixer.Update(radio.Throttle, pid.Axes);
                    else
                        mixer.SetSafe();
                    
                    lastMotorTime = currentTime;
                }

                currentTime = DateTime.Now.Ticks;
                if (currentTime >= (lastTelemetryTime + loopSettings.TelemetryLoopPeriod))
                {
                    float currentTelemetryFrequency = loopSettings.LoopUnit / (float)(currentTime - lastTelemetryTime);

                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes( "\r" + currentTime +
                        "\tR: " + currentRadioFrequency +
                        "\tS: " + currentSensorFrequency +
                        "\tC: " + currentControlFrequency +
                        "\tM: " + currentMotorFrequency +
                        "\tT: " + currentTelemetryFrequency + "\t");
                    serialPort.Write(buffer, 0, buffer.Length);

                    lastTelemetryTime = currentTime;
                }
            }
        }
    }
}

