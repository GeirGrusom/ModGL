using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Numerics;

namespace ModGL.Numerics
{
    [ImmutableObject(true)]
    public struct Quaternion : IEquatable<Quaternion>, IEquatable<Vector4f>
    {
        private readonly Vector4f _value;

        public static readonly Quaternion Identity = new Quaternion(0, 0, 0, 1);

        public float X { get { return _value.X; } }
        public float Y { get { return _value.Y; } }
        public float Z { get { return _value.Z; } }
        public float W { get { return _value.W; } }

        public Quaternion(float x, float y, float z, float w)
        {
            _value = new Vector4f(x, y, z, w);
        }

        public Quaternion(Vector4f value)
        {
            _value = value;
        }

        [Pure]
        public Quaternion Normalize()
        {
            return new Quaternion(_value.Normalize());
        }

        [Pure]
        public bool Equals(Quaternion other)
        {
            return _value.Equals(other._value);
        }
        
        [Pure]
        public bool Equals(Vector4f other)
        {
            return other.Equals(_value);
        }

        [Pure]
        public override string ToString()
        {
            return string.Join(", ", _value.X, _value.Y, _value.Z, _value.W);
        }

        [Pure]
        public static Quaternion operator *(Quaternion left, Quaternion right)
        {
            return left.Multiply(right);
        }

        [Pure]
        public Quaternion Multiply(Quaternion other)
        {
            return new Quaternion
                (
                    other.X * _value.X - other.Y * _value.Y - other.Z * _value.Z - other.W * _value.W,
                    other.X * _value.Y + other.Y * _value.X - other.Z * _value.W + other.W * _value.Z,
                    other.X * _value.Z + other.Y * _value.W + other.Z * _value.X - other.W * _value.Y,
                    other.X * _value.W - other.Y * _value.Z + other.Z * _value.Y + other.W * _value.X
                );
        }

        [Pure]
        public Quaternion Conjugate()
        {
            return new Quaternion(-_value.X, -_value.Y, -_value.Z, _value.W);
        }

        [Pure]
        public Matrix4f ToMatrix()
        {
            var qw = _value.X;
            var qx = _value.Y;
            var qy = _value.Z;
            var qz = _value.W;

            return new Matrix4f(
                new Vector4f( 1.0f - 2.0f*qy*qy - 2.0f*qz*qz, 2.0f*qx*qy - 2.0f*qz*qw, 2.0f*qx*qz + 2.0f*qy*qw, 0.0f),
                new Vector4f(2.0f*qx*qy + 2.0f*qz*qw, 1.0f - 2.0f*qx*qx - 2.0f*qz*qz, 2.0f*qy*qz - 2.0f*qx*qw, 0.0f),
                new Vector4f(2.0f*qx*qz - 2.0f*qy*qw, 2.0f*qy*qz + 2.0f*qx*qw, 1.0f - 2.0f*qx*qx - 2.0f*qy*qy, 0.0f),
                new Vector4f(0.0f, 0.0f, 0.0f, 1.0f));
        }
    }
}
