using System;
using DotCopter.ControlAlgorithms;
using DotCopter.ControlAlgorithms.Mixing;
using DotCopter.ControlAlgorithms.PID;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Radio;
using Microsoft.SPOT.Hardware;

namespace DotCopter.FlightController
{
    public class Controller
    {
        public Controller(MotorMixer mixer, PID pid, Gyro gyro, Radio radio, ControllerLoopSettings loopSettings)
        {
            long currentTime = DateTime.Now.Ticks;
            long previousTime = DateTime.Now.Ticks;
            long lastRadioTime = 0;
            long lastControlTime = 0;
            bool systemArmed = false;
            
            while (true)
            {
                if (currentTime >= (lastRadioTime + loopSettings.RadioLoopPeriod))
                {
                    lastRadioTime = currentTime;

                    radio.Update();
                    systemArmed = radio.Throttle > loopSettings.MinimumThrottle;

                    currentTime = DateTime.Now.Ticks;
                }

                if (currentTime >= (lastControlTime + loopSettings.ControlAlgorithmPeriod))
                {
                    lastControlTime = currentTime;
                    
                    if (systemArmed)
                    {
                        gyro.Update();
                        pid.Update(radio.AngularVelocity, gyro.AngularVelocity, (float) (currentTime - previousTime)/loopSettings.LoopUnit);
                        mixer.Update(radio.Throttle, pid.Output);
                    }
                    else
                    {
                        mixer.SetSafe();
                    }

                    previousTime = currentTime;
                }
                currentTime = DateTime.Now.Ticks;
            }
        }
    }
}

