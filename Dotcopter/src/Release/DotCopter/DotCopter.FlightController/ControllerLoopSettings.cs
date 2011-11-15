namespace DotCopter.FlightController
{
    public class ControllerLoopSettings
    {

        public ControllerLoopSettings(int radioLoopFrequency, int controlAlgorithmLoopFrequency, int loopUnit, float minimumThrottle)
        {
            RadioLoopPeriod = loopUnit / (radioLoopFrequency);
            ControlAlgorithmPeriod = loopUnit / (controlAlgorithmLoopFrequency);
            LoopUnit = loopUnit;
            MinimumThrottle = minimumThrottle;
        }

        public readonly int RadioLoopPeriod;
        public readonly int ControlAlgorithmPeriod;
        public readonly int LoopUnit;
        public readonly float MinimumThrottle;

    }
}
