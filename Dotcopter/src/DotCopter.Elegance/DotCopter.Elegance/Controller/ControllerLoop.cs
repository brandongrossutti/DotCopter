using System;
using System.Threading;
using DotCopter.Elegance.Avionics;
using DotCopter.Elegance.Commands;
using DotCopter.Elegance.Physics;

namespace DotCopter.Elegance.Controller
{
    public class ControllerLoop
    {
        public ControllerLoop(InertialMeasurement inertialMeasurement, Pilot pilot, VectorTranslationalCommandConsumer vectorTranslationalCommandConsumer, VectorTranslationalCommandProvider vectorTranslationalCommandProvider, LoopSettings loopSettings)
        {
            long previousTime = DateTime.Now.Ticks;
            long lastRadioTime = 0;
            long lastSensorTime = 0;
            long lastControlTime = 0;
            long lastMotorTime = 0;

            inertialMeasurement.Update();
            bool systemArmed = false;

            Thread.Sleep(1000);
            inertialMeasurement.Zero();


            VectorTranslationalCommand vectorTranslationalCommand = null;
            while (true)
            {
                long currentTime = DateTime.Now.Ticks;
                if (currentTime >= (lastRadioTime + loopSettings.RadioLoopPeriod))
                {
                    lastRadioTime = currentTime;
                    pilot.Update();
                    systemArmed = true; // pilot.GetCommand().Throttle > motorSettings.MinimumOutput;
                    //if (!systemArmed)
                      //  logger.Flush();
                }


                currentTime = DateTime.Now.Ticks;
                if (systemArmed && (currentTime >= (lastSensorTime + loopSettings.SensorLoopPeriod)))
                {
                    lastSensorTime = currentTime;
                    inertialMeasurement.Update();
                }

                currentTime = DateTime.Now.Ticks;
                if (systemArmed && (currentTime >= (lastControlTime + loopSettings.ControlAlgorithmPeriod)))
                {
                    lastControlTime = currentTime;
                    vectorTranslationalCommand =  vectorTranslationalCommandProvider.GetCommand(pilot, inertialMeasurement, (float)(currentTime - previousTime) / loopSettings.LoopUnit);
                    previousTime = currentTime;
                }

                currentTime = DateTime.Now.Ticks;
                if (currentTime >= (lastMotorTime + loopSettings.MotorLoopPeriod))
                {
                    if (systemArmed)
                        vectorTranslationalCommandConsumer.ProcessCommand(vectorTranslationalCommand);   //.Update(radio.Throttle, pid.Axes);
                    else
                        //mixer.SetSafe();

                    lastMotorTime = currentTime;
                }
            }
        }
    }
}

