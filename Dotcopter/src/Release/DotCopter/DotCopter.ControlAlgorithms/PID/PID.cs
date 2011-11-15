using System;
using DotCopter.Physics;

namespace DotCopter.ControlAlgorithms.PID
{
    public abstract class PID
    {
        public PIDSettings Settings;
        public Vector3D Output = new Vector3D();
        public abstract void Update(Vector3D setPoint, Vector3D actualPosition, float deltaTimeGain);
        public abstract void ZeroIntegral();

        public PID(PIDSettings settings)
        {
            Settings = settings;
        }

    }
}
