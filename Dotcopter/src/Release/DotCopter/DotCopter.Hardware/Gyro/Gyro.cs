using DotCopter.Physics;

namespace DotCopter.Hardware.Gyro
{
    public abstract class Gyro
    {
        /// <summary>
        /// Last measured angular velocity in units of degrees per second.
        /// </summary>
        public Vector3D AngularVelocity;
        public abstract void Update();
        public abstract void Zero();

        public Gyro(Vector3D angularVelocity)
        {
            AngularVelocity = angularVelocity;
        }
    }
}
