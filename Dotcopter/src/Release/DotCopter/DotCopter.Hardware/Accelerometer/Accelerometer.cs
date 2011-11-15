using DotCopter.Physics;

namespace DotCopter.Hardware.Accelerometer
{
    public abstract class Accelerometer
    {
        /// <summary>
        /// Last measured acceleration in units of milli g's.
        /// </summary>
        public readonly Vector3D Acceleration;
        public abstract void Update();
        public abstract void Zero();

        public Accelerometer(Vector3D acceleration)
        {
            Acceleration = acceleration;
        }

    }
}
