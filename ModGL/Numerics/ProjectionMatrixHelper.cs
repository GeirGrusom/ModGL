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
                new Vector4f(0, 0, (far + near) / (near - far), -1),
                new Vector4f(0, 0, 2 * far * near / (near - far), 0));
        }
    }
}
