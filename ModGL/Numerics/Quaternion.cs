using System;
using System.Numerics;

namespace ModGL.Numerics
{
    public struct Quaternion : IEquatable<Quaternion>, IEquatable<Vector4f>
    {
        private readonly Vector4f value;

        public float X { get { return value.X; } }
        public float Y { get { return value.Y; } }
        public float Z { get { return value.Z; } }
        public float W { get { return value.W; } }

        public Quaternion(float x, float y, float z, float w)
        {
            value = new Vector4f(x, y, z, w);
        }

        public Quaternion(Vector4f value)
        {
            this.value = value;
        }

        public Quaternion Normalize()
        {
            return new Quaternion(value.Normalize());
        }

        public bool Equals(Quaternion other)
        {
            return value.Equals(other.value);
        }
        
        public bool Equals(Vector4f other)
        {
            return value.Equals(other);
        }

        public override string ToString()
        {
            return string.Join(", ", value.X, value.Y, value.Z, value.W);
        }

        public static Quaternion operator *(Quaternion left, Quaternion right)
        {
            return left.Multiply(right);
        }

        public Quaternion Multiply(Quaternion other)
        {
            return new Quaternion
                (
                    other.X * value.X - other.Y * value.Y - other.Z * value.Z - other.W * value.W,
                    other.X * value.Y + other.Y * value.X - other.Z * value.W + other.W * value.Z,
                    other.X * value.Z + other.Y * value.W + other.Z * value.X - other.W * value.Y,
                    other.X * value.W - other.Y * value.Z + other.Z * value.Y + other.W * value.X
                );
        }

        public Quaternion Conjugate()
        {
            return new Quaternion(-value.X, -value.Y, -value.Z, value.W);
        }

        public Matrix4f ToMatrix()
        {
            float qw = value.X;
            float qx = value.Y;
            float qy = value.Z;
            float qz = value.W;

            return new Matrix4f(
                new Vector4f( 1.0f - 2.0f*qy*qy - 2.0f*qz*qz, 2.0f*qx*qy - 2.0f*qz*qw, 2.0f*qx*qz + 2.0f*qy*qw, 0.0f),
                new Vector4f(2.0f*qx*qy + 2.0f*qz*qw, 1.0f - 2.0f*qx*qx - 2.0f*qz*qz, 2.0f*qy*qz - 2.0f*qx*qw, 0.0f),
                new Vector4f(2.0f*qx*qz - 2.0f*qy*qw, 2.0f*qy*qz + 2.0f*qx*qw, 1.0f - 2.0f*qx*qx - 2.0f*qy*qy, 0.0f),
                new Vector4f(0.0f, 0.0f, 0.0f, 1.0f));
        }
    }
}
