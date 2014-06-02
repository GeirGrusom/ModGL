using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ModGL.Numerics
{
    public static class ProjectionMatrixHelper
    {
        public static Matrix4f RightHandPerspective(float fovy, float aspectRatio, float near, float far)
        {
            var f = (float) (1/Math.Tan(fovy/2));

            return new Matrix4f(
                new Vector4f(f / aspectRatio, 0, 0, 0),
                new Vector4f(0, f, 0, 0),
                new Vector4f(0, 0, (far + near) / (near - far), 2 * far * near / (near - far)),
                new Vector4f(0, 0, -1, 0));
        }

        public static Matrix4f Orthographic(float top, float bottom, float left, float right, float far, float near)
        {
            return new Matrix4f(
                new Vector4f(1 / ( right - left)),
                new Vector4f(),
                new Vector4f(),
                new Vector4f());
        }
    }
}
