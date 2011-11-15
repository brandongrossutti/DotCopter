using System;
using System.Threading;
using DotCopter.Commons.Logging;
using DotCopter.Commons.Serialization;
using DotCopter.ControlAlgorithms.Mixing;
using DotCopter.Hardware.Accelerometer;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Motor;
using DotCopter.Hardware.Radio;
using Microsoft.SPOT;

namespace DotCopter.FlightController
{
    public class Controller
    {
        public Controller(MotorMixer mixer, AxesController pid, Gyro gyro, Accelerometer accelerometer, Radio radio, ControllerLoopSettings loopSettings, MotorSettings motorSettings, ILogger logger)
        {
            TelemetryData telemetryData = new TelemetryData();
            long previousTime = DateTime.Now.Ticks;
            long lastRadioTime = 0;
            long lastSensorTime = 0;
            long lastControlTime = 0;
            long lastMotorTime = 0;
            long lastTelemetryTime = 0;

            bool systemArmed = false;
            
            Thread.Sleep(1000);
            gyro.Zero();
            accelerometer.Zero();

            while (true)
            {
                long currentTime = DateTime.Now.Ticks;
                if (currentTime >= (lastRadioTime + loopSettings.RadioLoopPeriod))
                {
                    lastRadioTime = currentTime;
                    radio.Update();
                    systemArmed = radio.Throttle > motorSettings.MinimumOutput;
                    if (!systemArmed)
                        logger.Flush();
                }

               
                currentTime = DateTime.Now.Ticks;
                if (systemArmed && (currentTime >= (lastSensorTime + loopSettings.SensorLoopPeriod)))
                {
                    lastSensorTime = currentTime;
                    gyro.Update();
                    accelerometer.Update();
                }

                currentTime = DateTime.Now.Ticks;
                if (systemArmed && (currentTime >= (lastControlTime + loopSettings.ControlAlgorithmPeriod)))
                {
                    lastControlTime = currentTime;
                    pid.Update(radio.Axes, gyro.Axes, (float) (currentTime - previousTime)/loopSettings.LoopUnit);
                    previousTime = currentTime;
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
                if (systemArmed && (currentTime >= (lastTelemetryTime + loopSettings.TelemetryLoopPeriod)))
                {
                    telemetryData.Update(gyro.Axes, radio.Axes, pid.Axes, currentTime);
                    lastTelemetryTime = currentTime;
                    logger.Write(telemetryData);
                }

                if (!systemArmed)
                    pid.ZeroIntegral();
            }
        }
    }
}

