using System;

namespace DotCopter.Configurator.Settings
{
    [Serializable]
    public class ControllerLoopSettings
    {
        public int RadioLoopFrequency { get; set; }
        public int SensorLoopFrequency { get; set; }
        public int ControlAlgorithmLoopFrequency { get; set; }
        public int MotorLoopFrequency { get; set; }
        public int TelemetryLoopFrequency { get; set; }
        public int LoopUnit { get; set; }

        public ControllerLoopSettings(int radioLoopFrequency, int sensorLoopFrequency, int controlAlgorithmLoopFrequency, int motorLoopFrequency, int telemetryLoopFrequency, int loopUnit)
        {
            RadioLoopFrequency = radioLoopFrequency;
            SensorLoopFrequency = sensorLoopFrequency;
            ControlAlgorithmLoopFrequency = controlAlgorithmLoopFrequency;
            MotorLoopFrequency = motorLoopFrequency;
            TelemetryLoopFrequency = telemetryLoopFrequency;
            LoopUnit = loopUnit;
        }

        public ControllerLoopSettings()
        {
            
        }
    }
}