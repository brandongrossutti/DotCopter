namespace DotCopter.FlightController
{
    public class ControllerLoopSettings
    {

        public ControllerLoopSettings(int radioLoopFrequency, int sensorLoopFrequency, int controlAlgorithmLoopFrequency, int motorLoopFrequency, int telemetryLoopFrequency, int loopUnit)
        {
            RadioLoopPeriod = loopUnit / (RadioLoopFrequency = radioLoopFrequency);
            SensorLoopPeriod = loopUnit / (SensorLoopFrequency = sensorLoopFrequency);
            ControlAlgorithmPeriod = loopUnit / (ControlAlgorithmLoopFrequency = controlAlgorithmLoopFrequency);
            MotorLoopPeriod = loopUnit / (MotorLoopFrequency = motorLoopFrequency);
            TelemetryLoopPeriod = loopUnit / (TelemetryLoopFrequency = telemetryLoopFrequency);
            LoopUnit = loopUnit;
        }

        public int RadioLoopFrequency;
        public readonly int RadioLoopPeriod;

        public int SensorLoopFrequency;
        public readonly int SensorLoopPeriod;

        public int ControlAlgorithmLoopFrequency;
        public readonly int ControlAlgorithmPeriod;

        public int MotorLoopFrequency;
        public readonly int MotorLoopPeriod;

        public int TelemetryLoopFrequency;
        public readonly int TelemetryLoopPeriod;

        public readonly int LoopUnit;
    }
}
