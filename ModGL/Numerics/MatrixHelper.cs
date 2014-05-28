using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ModGL.Numerics
{
    public static class MatrixHelper
    {
        public static Matrix4f Translate(Vector3f location)
        {
            return new Matrix4f(new Vector4f(1, 0, 0, 0), new Vector4f(0, 1, 0, 0), new Vector4f(0, 0, 1, 0), new Vector4f(location.X, location.Y, location.Z, 1));
        }

        public static Matrix4f Translate(float x, float y, float z)
        {
            return new Matrix4f(new Vector4f(1, 0, 0, 0), new Vector4f(0, 1, 0, 0), new Vector4f(0, 0, 1, 0), new Vector4f(x, y, z, 1));
        }

        public static Matrix4f Scale(Vector3f scale)
        {
            return new Matrix4f(new Vector4f(scale.X, 0, 0, 0), new Vector4f(0, scale.Y, 0, 0), new Vector4f(0, 0, scale.Z, 0), new Vector4f(0, 0, 0, 1));
        }

        public static Matrix4f Scale(float x, float y, float z)
        {
            return new Matrix4f(new Vector4f(x, 0, 0, 0), new Vector4f(0, y, 0, 0), new Vector4f(0, 0, z, 0), new Vector4f(0, 0, 0, 1));
        }
    }
}
