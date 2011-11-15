using System;

//todo: BRANDON THIS NOTE IS FOR YOU 
//we need build time ability to figure out which system we are using and include the correct library
//ie this is not on netduino
using GHIElectronics.NETMF.System; 

namespace DotCopter.Elegance.Physics
{
    /// <summary>
    /// vector of floats with three components (x,y,z)
    /// Adapted from: http://www.codeproject.com/KB/recipes/VectorType.aspx
    /// </summary>
    public struct Vector3
    {
        
        #region Fields

        /// <summary>
        /// The X component of the vector
        /// </summary>
        public double X;

        /// <summary>
        /// The Y component of the vector
        /// </summary>
        public double Y;

        /// <summary>
        /// The Z component of the vector
        /// </summary>
        public double Z;

        #endregion

        #region Constructors

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        
        public Vector3(Vector3 v1)
        {
            X = v1.X;
            Y = v1.Y;
            Z = v1.Z;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Addition of two Vectors
        /// </summary>
        /// <param name="v1">Vector3 to be added to </param>
        /// <param name="v2">Vector3 to be added</param>
        /// <returns>Vector3 representing the sum of two Vectors</returns>
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return (new Vector3(
                v1.X + v2.X,
                v1.Y + v2.Y,
                v1.Z + v2.Z));
        }

        /// <summary>
        /// Subtraction of two Vectors
        /// </summary>
        /// <param name="v1">Vector3 to be subtracted from </param>
        /// <param name="v2">Vector3 to be subtracted</param>
        /// <returns>Vector3 representing the difference of two Vectors</returns>
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return (new Vector3(
                v1.X - v2.X,
                v1.Y - v2.Y,
                v1.Z - v2.Z
                ));
        }

        /// <summary>
        /// Product of a Vector3 and a scalar value
        /// </summary>
        /// <param name="v1">Vector3 to be multiplied </param>
        /// <param name="s2">Scalar value to be multiplied by </param>
        /// <returns>Vector3 representing the product of the vector and scalar</returns>
        public static Vector3 operator *(Vector3 v1, float s2)
        {
            return (new Vector3(
                v1.X*s2,
                v1.Y*s2,
                v1.Z*s2
                ));
        }

        /// <summary>
        /// Product of a Vector3 and a scalar value
        /// </summary>
        /// <param name="s1">Scalar to be multiplied </param>
        /// <param name="v2">Vector3 value to be multiplied by </param>
        /// <returns>Vector3 representing the product of the vector and scalar</returns>
        public static Vector3 operator *(float s1, Vector3 v2)
        {
            return (new Vector3(
                s1 * v2.X,
                s1 * v2.Y,
                s1 * v2.Z
                ));
        }

        /// <summary>
        /// Division of a Vector3 and a scalar value
        /// </summary>
        /// <param name="v1">Vector3 to be divided </param>
        /// <param name="s2">Scalar value to be divided by </param>
        /// <returns>Vector3 representing the division of the vector and scalar</returns>
        public static Vector3 operator /(Vector3 v1, float s2)
        {
            return (new Vector3(
                v1.X/s2,
                v1.Y/s2,
                v1.Z/s2
                ));
        }

        /// <summary>
        /// Negation of a Vector3
        /// Invert the direction of the Vector3
        /// Make Vector3 negative (-vector)
        /// </summary>
        /// <param name="v1">Vector3 to be negated  </param>
        /// <returns>Negated vector</returns>
        public static Vector3 operator -(Vector3 v1)
        {
            return (new Vector3(
                -v1.X,
                -v1.Y,
                -v1.Z
                ));
        }

        /// <summary>
        /// Reinforcement of a Vector3
        /// Make Vector3 positive (+vector)
        /// </summary>
        /// <param name="v1">Vector3 to be reinforced </param>
        /// <returns>Reinforced vector</returns>
        public static Vector3 operator +(Vector3 v1)
        {
            return (new Vector3(
                +v1.X,
                +v1.Y,
                +v1.Z
                ));
        }

        /// <summary>
        /// Compare the magnitude of two Vectors (less than)
        /// </summary>
        /// <param name="v1">Vector3 to be compared </param>
        /// <param name="v2">Vector3 to be compared with</param>
        /// <returns>True if v1 less than v2</returns>
        public static bool operator <(Vector3 v1, Vector3 v2)
        {
            return SumComponentSqrs(v1) < SumComponentSqrs(v2);
        }

        /// <summary>
        /// Compare the magnitude of two Vectors (greater than)
        /// </summary>
        /// <param name="v1">Vector3 to be compared </param>
        /// <param name="v2">Vector3 to be compared with</param>
        /// <returns>True if v1 greater than v2</returns>
        public static bool operator >(Vector3 v1, Vector3 v2)
        {
            return SumComponentSqrs(v1) > SumComponentSqrs(v2);
        }

        /// <summary>
        /// Compare the magnitude of two Vectors (less than or equal to)
        /// </summary>
        /// <param name="v1">Vector3 to be compared </param>
        /// <param name="v2">Vector3 to be compared with</param>
        /// <returns>True if v1 less than or equal to v2</returns>
        public static bool operator <=(Vector3 v1, Vector3 v2)
        {
            return SumComponentSqrs(v1) <= SumComponentSqrs(v2);
        }

        /// <summary>
        /// Compare the magnitude of two Vectors (greater than or equal to)
        /// </summary>
        /// <param name="v1">Vector3 to be compared </param>
        /// <param name="v2">Vector3 to be compared with</param>
        /// <returns>True if v1 greater than or equal to v2</returns>
        public static bool operator >=(Vector3 v1, Vector3 v2)
        {
            return SumComponentSqrs(v1) >= SumComponentSqrs(v2);
        }

        /// <summary>
        /// Compare two Vectors for equality.
        /// Are two Vectors equal.
        /// </summary>
        /// <param name="v1">Vector3 to be compared for equality </param>
        /// <param name="v2">Vector3 to be compared to </param>
        /// <returns>Boolean decision (truth for equality)</returns>
        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            return (
                      ((v1.X - v2.X) <= float.Epsilon) && ((v2.X - v1.X) <= float.Epsilon) &&
                      ((v1.Y - v2.Y) <= float.Epsilon) && ((v2.Y - v1.Y) <= float.Epsilon) &&
                      ((v1.Z - v2.Z) <= float.Epsilon) && ((v2.Z - v1.Z) <= float.Epsilon)
                   );
        }

        /// <summary>
        /// Negative comparator of two Vectors.
        /// Are two Vectors different.
        /// </summary>
        /// <param name="v1">Vector3 to be compared for in-equality </param>
        /// <param name="v2">Vector3 to be compared to </param>
        /// <returns>Boolean decision (truth for in-equality)</returns>
        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return !(v1 == v2);
        }

        /// <summary>
        /// Implplicit conversion of a Vector3 to a float
        /// Represents the magnitude of the vector
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public static implicit operator float(Vector3 v1)
        {
            return (float)Math.Pow((v1.X*v1.X + v1.Y*v1.Y + v1.Z*v1.Z), 0.5);
        }

        #endregion

        #region Functions

        /// <summary>
        /// Length or absolute value of a Vector3
        /// </summary>
        public static float Magnitude(Vector3 v1)
        {
            return (float)MathEx.Sqrt(SumComponentSqrs(v1));
        }

        /// <summary>
        /// Determine the cross product of two Vectors
        /// Determine the vector product
        /// Determine the normal vector (Vector3 90° to the plane)
        /// </summary>
        /// <param name="v1">The vector to multiply</param>
        /// <param name="v2">The vector to multiply by</param>
        /// <returns>Vector3 representing the cross product of the two vectors</returns>
        public static Vector3 CrossProduct(Vector3 v1, Vector3 v2)
        {
            return (new Vector3(
                v1.Y*v2.Z - v1.Z*v2.Y,
                v1.Z*v2.X - v1.X*v2.Z,
                v1.X*v2.Y - v1.Y*v2.X
                ));
        }

        /// <summary>
        /// Determine the dot product of two Vectors
        /// </summary>
        /// <param name="v1">The vector to multiply</param>
        /// <param name="v2">The vector to multiply by</param>
        /// <returns>Scalar representing the dot product of the two vectors</returns>
        public static float DotProduct(Vector3 v1, Vector3 v2)
        {
            return(
                v1.X * v2.X +
                v1.Y * v2.Y +
                v1.Z * v2.Z
            );
        }

        /// <summary>
        /// Determine the mixed product of three Vectors
        /// Determine volume (with sign precision) of parallelepiped spanned on given vectors
        /// Determine the scalar triple product of three vectors
        /// </summary>
        /// <param name="v1">The first vector</param>
        /// <param name="v2">The second vector</param>
        /// <param name="v3">The third vector</param>
        /// <returns>Scalar representing the mixed product of the three vectors</returns>
        public static float MixedProduct(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            return DotProduct(CrossProduct(v1, v2), v3);
        }

        /// <summary>
        /// Get the normalized vector
        /// Get the unit vector
        /// Scale the Vector3 so that the magnitude is 1
        /// </summary>
        /// <param name="v1">The vector to be normalized</param>
        /// <returns>The normalized Vector3</returns>
        public static Vector3 Normalize(Vector3 v1)
        {
            // Check for divide by zero errors
            if (v1 == 0F)
                return new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            // find the inverse of the vectors magnitude
            float inverse = 1F/v1;
            return (new Vector3(
                v1.X*inverse,
                v1.Y*inverse,
                v1.Z*inverse
                ));
        }

        /// <summary>
        /// Take an interpolated value from between two Vectors or an extrapolated value if allowed
        /// </summary>
        /// <param name="v1">The Vector3 to interpolate from (where control == 0)</param>
        /// <param name="v2">The Vector3 to interpolate to (where control == 1)</param>
        /// <param name="control">The interpolated point between the two vectors to retrieve (fraction between 0 and 1), or an extrapolated point if allowed</param>
        /// <param name="allowExtrapolation">True if the control may represent a point not on the vertex between v1 and v2</param>
        /// <returns>The value at an arbitrary distance (interpolation) between two vectors or an extrapolated point on the extended virtex</returns>
        public static Vector3 Interpolate(Vector3 v1, Vector3 v2, float control, bool allowExtrapolation)
        {
            if (!allowExtrapolation)
            {
                if (control > 1)
                    return v2;
                if (control < 0)
                    return v1;
            }
            return (new Vector3(
                v1.X*(1 - control) + v2.X*control,
                v1.Y*(1 - control) + v2.Y*control,
                v1.Z*(1 - control) + v2.Z*control
                ));
        }

        /// <summary>
        /// Find the distance between two Vectors
        /// Pythagoras theorem on two Vectors
        /// </summary>
        /// <param name="v1">The Vector3 to find the distance from </param>
        /// <param name="v2">The Vector3 to find the distance to </param>
        /// <returns>The distance between two Vectors</returns>
        public static float Distance(Vector3 v1, Vector3 v2)
        {
            return ((float) Math.Pow(
                (v1.X - v2.X)*(v1.X - v2.X) +
                (v1.Y - v2.Y)*(v1.Y - v2.Y) +
                (v1.Z - v2.Z)*(v1.Z - v2.Z),
                0.5
                                ));
        }
        
        /// <summary>
        /// Find the angle between two Vectors
        /// </summary>
        /// <param name="v1">The Vector3 to discern the angle from </param>
        /// <param name="v2">The Vector3 to discern the angle to</param>
        /// <returns>The angle between two positional Vectors</returns>
        public static double Angle(Vector3 v1, Vector3 v2)
        {
            return (MathEx.Acos(
                DotProduct(
                    Normalize(v1),
                    Normalize(v2))));
        }
        
        /// <summary>
        /// compares the magnitude of two Vectors and returns the greater Vector3
        /// </summary>
        /// <param name="v1">The vector to compare</param>
        /// <param name="v2">The vector to compare with</param>
        /// <returns>
        /// The greater of the two Vectors (based on magnitude)
        /// </returns>
        public static Vector3 Max(Vector3 v1, Vector3 v2)
        {
            return v1 < v2 ? v2 : v1;
        }
        
        /// <summary>
        /// compares the magnitude of two Vectors and returns the lesser Vector3
        /// </summary>
        /// <param name="v1">The vector to compare</param>
        /// <param name="v2">The vector to compare with</param>
        /// <returns>
        /// The lesser of the two Vectors (based on magnitude)
        /// </returns>
        public static Vector3 Min(Vector3 v1, Vector3 v2)
        {
            return v1 > v2 ? v2 : v1;
        }
        

        /// <summary>
        /// Rotates a Vector3 around the X axis
        /// Change the pitch of a Vector3
        /// </summary>
        /// <param name="v1">The Vector3 to be rotated</param>
        /// <param name="degree">The angle to rotate the Vector3 around in degrees</param>
        /// <returns>Vector3 representing the rotation around the X axis</returns>
        public static Vector3 RotateX(Vector3 v1, float degree)
        {
            return new Vector3(
                v1.X, 
                (v1.Y * (float)MathEx.Cos(degree)) - (v1.Z * (float)MathEx.Sin(degree)), 
                (v1.Y * (float)MathEx.Sin(degree)) + (v1.Z * (float)MathEx.Cos(degree)));
        }

        /// <summary>
        /// Rotates a Vector3 around the Y axis
        /// Change the yaw of a Vector3
        /// </summary>
        /// <param name="v1">The Vector3 to be rotated</param>
        /// <param name="degree">The angle to rotate the Vector3 around in degrees</param>
        /// <returns>Vector3 representing the rotation around the Y axis</returns>
        public static Vector3 RotateY(Vector3 v1, float degree)
        {
            return new Vector3(
                (v1.Z * (float)MathEx.Sin(degree)) + (v1.X * (float)MathEx.Cos(degree)),
                v1.Y,
                (v1.Z * (float)MathEx.Cos(degree)) - (v1.X * (float)MathEx.Sin(degree)));
        }

        /// <summary>
        /// Rotates a Vector3 around the Z axis
        /// Change the roll of a Vector3
        /// </summary>
        /// <param name="v1">The Vector3 to be rotated</param>
        /// <param name="degree">The angle to rotate the Vector3 around in degrees</param>
        /// <returns>Vector3 representing the rotation around the Z axis</returns>
        public static Vector3 RotateZ(Vector3 v1, float degree)
        {
            return new Vector3(
                (v1.X * (float)MathEx.Cos(degree)) - (v1.Y * (float)MathEx.Sin(degree)),
                (v1.X * (float)MathEx.Sin(degree)) + (v1.Y * (float)MathEx.Cos(degree)),
                v1.Z);
        }
        #endregion

        #region Component Operations

        /// <summary>
        /// The sum of a Vector3's components
        /// </summary>
        /// <param name="v1">The vector whose scalar components to sum</param>
        /// <returns>The sum of the Vectors X, Y and Z components</returns>
        public static float SumComponents(Vector3 v1)
        {
            return (v1.X + v1.Y + v1.Z);
        }

        /// <summary>
        /// The sum of a Vector3's squared components
        /// </summary>
        /// <param name="v1">The vector whose scalar components to square and sum</param>
        /// <returns>The sum of the Vectors X^2, Y^2 and Z^2 components</returns>
        public static float SumComponentSqrs(Vector3 v1)
        {
            return SumComponents(SqrComponents(v1));
        }


        /// <summary>
        /// The individual multiplication to a power of a Vector3's components
        /// </summary>
        /// <param name="v1">The vector whose scalar components to multiply by a power</param>
        /// <param name="power">The power by which to multiply the components</param>
        /// <returns>The multiplied Vector3</returns>
        public static Vector3 PowComponents(Vector3 v1, float power)
        {
            return (new Vector3(
                (float)Math.Pow(v1.X, power),
                (float)Math.Pow(v1.Y, power),
                (float)Math.Pow(v1.Z, power)));
        }
        
        /// <summary>
        /// The individual square root of a Vector3's components
        /// </summary>
        /// <param name="v1">The vector whose scalar components to square root</param>
        /// <returns>The rooted Vector3</returns>
        public static Vector3 SqrtComponents(Vector3 v1)
        {
            return (new Vector3(
                (float)MathEx.Sqrt(v1.X),
                (float)MathEx.Sqrt(v1.Y),
                (float)MathEx.Sqrt(v1.Z)));
        }
        
        /// <summary>
        /// The Vector3's components squared
        /// </summary>
        /// <param name="v1">The vector whose scalar components are to square</param>
        /// <returns>The squared Vector3</returns>
        public static Vector3 SqrComponents(Vector3 v1)
        {
            return (new Vector3(
                v1.X*v1.X,
                v1.Y*v1.Y,
                v1.Z*v1.Z));
        }
        
        #endregion

        public bool Equals(Vector3 other)
        {
            return other == this;
        }
        
        public override bool Equals(object other)
        {
            if (!(other is Vector3))
            {
                return false;
            }
            return (Vector3) other == this;
        }

        public override int GetHashCode()
        {
            return
            (
                (int)((X + Y + Z) % Int32.MaxValue)
            );
        }
    }



}