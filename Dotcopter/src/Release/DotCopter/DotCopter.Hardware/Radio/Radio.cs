using DotCopter.Physics;

namespace DotCopter.Hardware.Radio
{
    public abstract class Radio
    {
        public float Throttle;
        public Vector3D AngularVelocity;
        public bool Gear;

        public Radio(float throttle, Vector3D angularVelocity, bool gear)
        {
            Throttle = throttle;
            AngularVelocity = angularVelocity;
            Gear = gear;
        }
        public abstract void Update();
    }
}
