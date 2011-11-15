using DotCopter.Physics;

namespace DotCopter.ControlAlgorithms.PID
{
    public struct PIDSettings
    {
        public Vector3D ProportionalGain;
        public Vector3D IntegralGain;
        public Vector3D DerivativeGain;
        public Vector3D WindupLimit;
    }
}
