using System;

namespace DotCopter.Configurator.Settings
{
    [Serializable]

    public class Vector3D
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3D()
        {
            
        }
    }
}