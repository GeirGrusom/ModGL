using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ModGL.Numerics
{
    public static class VectorMath
    {
        public static Vector3f Cross(this Vector3f lhs, Vector3f rhs)
        {
            return new Vector3f(lhs.Y * rhs.Z - lhs.Z * rhs.Y, lhs.Z * rhs.X - lhs.X * rhs.Z, lhs.X * rhs.Y - lhs.Y * rhs.X);
        }

        public static float Length(this Vector3f vec)
        {
            return (float) Math.Sqrt(System.Numerics.VectorMath.DotProduct(vec, vec));
        }

        public static Vector3f Normalize(this Vector3f vec)
        {
            var length = new Vector3f((float)Math.Sqrt(System.Numerics.VectorMath.DotProduct(vec, vec)));
            return vec/length;
        }

        public static Vector3f PlaneNormal(Vector3f a, Vector3f b, Vector3f c)
        {
            return (c - a).Cross(b - a);
        }
    }
}
