using System;

namespace DotCopter.ControlAlgorithms.PID
{
    public abstract class PID
    {
        public PIDSettings Settings;
        public float Output;
        public abstract void Update(float setPoint, float actualPosition, float deltaTimeGain);
        public abstract void ZeroIntegral();

        public PID(PIDSettings settings)
        {
            Settings = settings;
        }

    }
}
