using System;
using System.IO.Ports;
using DotCopter.Commons.Logging;
using DotCopter.ControlAlgorithms.Mixing;
using DotCopter.Hardware.Accelerometer;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Motor;
using DotCopter.Hardware.Radio;
using Microsoft.SPOT;

namespace DotCopter.FlightController
{
    public class ControllerWithGyroDebug
    {
        public ControllerWithGyroDebug(MotorMixer mixer, AxesController pid, Gyro gyro, Accelerometer accelerometer,Radio radio, ControllerLoopSettings loopSettings, MotorSettings motorSettings, ILogger logger)
        {
            long lastRadioTime = 0;
            long lastSensorTime = 0;
            long lastControlTime = 0;
            long lastMotorTime = 0;
            long lastTelemetryTime = 0;
            bool systemArmed = false;
            
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
                    Debug.Print(
                    //byte[] buffer = System.Text.Encoding.UTF8.GetBytes( //"\r" + currentTime +
                        //"\tR:" + radio.Axes.Pitch +);
                        "\rP:" + gyro.Axes.Pitch +
                        "\rR:" + gyro.Axes.Roll +
                        "\rY:" + gyro.Axes.Yaw
                        //"\tP" + pid.Axes.Pitch
                        );
                    //serialPort.Write(buffer, 0, buffer.Length);
                    lastTelemetryTime = currentTime;
                }
            }
        }
    }
}

