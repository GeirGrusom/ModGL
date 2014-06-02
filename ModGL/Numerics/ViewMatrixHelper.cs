using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using Vector = System.Numerics.VectorMath; 
namespace ModGL.Numerics
{
    public static class ViewMatrixHelper
    {
        public static Matrix4f LookAt(Vector3f eye, Vector3f up, Vector3f target)
        {
            var zaxis = (eye - target).Normalize();
            var xaxis = up.Cross(zaxis).Normalize();
            var yaxis = zaxis.Cross(xaxis);

            var dotx = - Vector.DotProduct(xaxis, eye);
            var doty = -Vector.DotProduct(yaxis, eye);
            var dotz = -Vector.DotProduct(zaxis, eye);

            return new Matrix4f(
                new Vector4f(xaxis.X, xaxis.Y, xaxis.Z, dotx),
                new Vector4f(yaxis.X, yaxis.Y, yaxis.Z, doty),
                new Vector4f(zaxis.X, zaxis.Y, zaxis.Z, dotz),
                new Vector4f(0, 0, 0, 1)
                );
        }
    }
}
