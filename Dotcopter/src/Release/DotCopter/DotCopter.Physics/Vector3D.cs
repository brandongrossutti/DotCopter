using DotCopter.Commons.Utilities;

namespace DotCopter.Physics
{
    public class Vector3D
    {
        public float X;
        public float Y;
        public float Z;

        public static void Add(Vector3D vector3D1, Vector3D vector3D2, Vector3D vector3Dout)
        {
            vector3Dout.X = vector3D1.X + vector3D2.X;
            vector3Dout.Y = vector3D1.Y + vector3D2.Y;
            vector3Dout.Z = vector3D1.Z + vector3D2.Z;
        }

        public static void Subtract(Vector3D vector3D1, Vector3D vector3D2, Vector3D vector3Dout)
        {
            vector3Dout.X = vector3D1.X - vector3D2.X;
            vector3Dout.Y = vector3D1.Y - vector3D2.Y;
            vector3Dout.Z = vector3D1.Z - vector3D2.Z;
        }

        public static void Subtract(float scalar, Vector3D vector3D2, Vector3D vector3Dout)
        {
            vector3Dout.X = scalar - vector3D2.X;
            vector3Dout.Y = scalar - vector3D2.Y;
            vector3Dout.Z = scalar - vector3D2.Z;
        }

        public static void Subtract(Vector3D vector3D1, float scalar, Vector3D vector3Dout)
        {
            vector3Dout.X = vector3D1.X - scalar;
            vector3Dout.Y = vector3D1.Y - scalar;
            vector3Dout.Z = vector3D1.Z - scalar;
        }

        public static void Multiply(Vector3D vector3D1, Vector3D vector3D2, Vector3D vector3Dout)
        {
            vector3Dout.X = vector3D1.X * vector3D2.X;
            vector3Dout.Y = vector3D1.Y * vector3D2.Y;
            vector3Dout.Z = vector3D1.Z * vector3D2.Z;
        }

        public static void Multiply(Vector3D vector3D1, float scalar, Vector3D vector3Dout)
        {
            vector3Dout.X = vector3D1.X * scalar;
            vector3Dout.Y = vector3D1.Y * scalar;
            vector3Dout.Z = vector3D1.Z * scalar;
        }

        public static void Divide(Vector3D vector3D1, float scalar, Vector3D vector3Dout)
        {
            vector3Dout.X = vector3D1.X / scalar;
            vector3Dout.Y = vector3D1.Y / scalar;
            vector3Dout.Z = vector3D1.Z / scalar;
        }

        public static void Constrain(Vector3D vector3D, Vector3D min, Vector3D max)
        {
            vector3D.X = Logic.Constrain(vector3D.X, min.X, max.X);
            vector3D.Y = Logic.Constrain(vector3D.Y, min.Y, max.Y);
            vector3D.Z = Logic.Constrain(vector3D.Z, min.Z, max.Z);
        }

        public static void Copy(Vector3D vector3D, Vector3D vector3Dout)
        {
            vector3Dout.X = vector3D.X;
            vector3Dout.Y = vector3D.Y;
            vector3Dout.Z = vector3D.Z;
        }

        public void ApplyScale(Scale scale)
        {
            X = scale.Calculate(X);
            Y = scale.Calculate(Y);
            Z = scale.Calculate(Z);
        }
    }
}