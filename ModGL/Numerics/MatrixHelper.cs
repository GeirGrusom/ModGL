using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ModGL.Numerics
{
    public static class MatrixHelper
    {
        /// <summary>
        /// Creates a translaion matrix.
        /// </summary>
        /// <param name="location"></param>
        /// <returns>Translation matrix.</returns>
        [Pure]
        public static Matrix4f Translate(Vector3f location)
        {
            return new Matrix4f(new Vector4f(1, 0, 0, location.X), new Vector4f(0, 1, 0, location.Y), new Vector4f(0, 0, 1, location.Z), new Vector4f(0, 0, 0, 1));
        }

        /// <summary>
        /// Creates a translation matrix.
        /// </summary>
        /// <param name="x">Amount to translate in the X axis.</param>
        /// <param name="y">Amount to translate in the Y axis.</param>
        /// <param name="z">Amount to translate in the Z axis.</param>
        /// <returns>Translation matrix.</returns>
        [Pure]
        public static Matrix4f Translate(float x, float y, float z)
        {
            return new Matrix4f(new Vector4f(1, 0, 0, x), new Vector4f(0, 1, 0, y), new Vector4f(0, 0, 1, z), new Vector4f(0, 0, 0, 1));
        }

        /// <summary>
        /// Creates a scale matrix.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns>Scale matrix.</returns>
        [Pure]
        public static Matrix4f Scale(Vector3f scale)
        {
            return new Matrix4f(new Vector4f(scale.X, 0, 0, 0), new Vector4f(0, scale.Y, 0, 0), new Vector4f(0, 0, scale.Z, 0), new Vector4f(0, 0, 0, 1));
        }

        /// <summary>
        /// Creates a scale matrix.
        /// </summary>
        /// <param name="x">Scale on the X axis.</param>
        /// <param name="y">Scale on the Y axis.</param>
        /// <param name="z">Scale on the Z axis.</param>
        /// <returns>Scale matrix.</returns>
        [Pure]
        public static Matrix4f Scale(float x, float y, float z)
        {
            return new Matrix4f(new Vector4f(x, 0, 0, 0), new Vector4f(0, y, 0, 0), new Vector4f(0, 0, z, 0), new Vector4f(0, 0, 0, 1));
        }

        /// <summary>
        /// Creates a rotation matrix around the X axis.
        /// </summary>
        /// <param name="angleInRadians">Angle of rotation specified in radians.</param>
        /// <returns>Rotation matrix.</returns>
        [Pure]
        public static Matrix4f RotateX(float angleInRadians)
        {
            var cs = (float)Math.Cos(angleInRadians);
            var ss = (float)Math.Sin(angleInRadians);
            return new Matrix4f(
                new Vector4f(1, 0, 0, 0), 
                new Vector4f(0, cs, -ss, 0), 
                new Vector4f(0, ss, cs, 0), 
                new Vector4f(0, 0, 0, 1));
        }

        /// <summary>
        /// Creates a rotation matrix around the Y axis.
        /// </summary>
        /// <param name="angleInRadians">Angle of rotation specified in radians.</param>
        /// <returns>Rotation matrix.</returns>
        [Pure]
        public static Matrix4f RotateY(float angleInRadians)
        {
            var cs = (float)Math.Cos(angleInRadians);
            var ss = (float)Math.Sin(angleInRadians);
            return new Matrix4f(
                new Vector4f(cs,  0, ss, 0), 
                new Vector4f(0,   1, 0,  0), 
                new Vector4f(-ss, 0, cs, 0), 
                new Vector4f(0,   0, 0,  1));
        }

        /// <summary>
        /// Creates a rotation matrix around the Z axis.
        /// </summary>
        /// <param name="angleInRadians">The angle of rotation specified in radians.</param>
        /// <returns>Rotation matrix.</returns>
        [Pure]
        public static Matrix4f RotateZ(float angleInRadians)
        {
            var cs = (float)Math.Cos(angleInRadians);
            var ss = (float)Math.Sin(angleInRadians);
            return new Matrix4f(
                new Vector4f( cs, -ss, 0, 0), 
                new Vector4f(ss, cs, 0, 0), 
                new Vector4f(0, 0, 1, 0), 
                new Vector4f(0, 0, 0, 1));
        }

        /// <summary>
        /// Creates an axis angle rotation matrix.
        /// </summary>
        /// <param name="axis">The axis around which to rotate. This should be a normalized vector.</param>
        /// <param name="angleInRadians">The angle of rotation specified in radians.</param>
        /// <returns>Rotation matrix.</returns>
        [Pure]
        public static Matrix4f Rotate(Vector3f axis, float angleInRadians)
        {
            var cs = (float)Math.Cos(angleInRadians);
            var ss = (float)Math.Sin(angleInRadians);
            return new Matrix4f(
                new Vector4f(cs + axis.X * axis.X * (1 - cs), axis.Y * axis.X * (1 - cs) + axis.Z * ss, axis.Z * axis.X * (1 - cs) - axis.Y * ss, 0),
                new Vector4f(axis.X * axis.Y * (1- cs) - axis.Z * ss, cs + axis.Y * axis.Y * (1 - cs), axis.Z * axis.Y * (1 - cs) + axis.X*ss, 0),
                new Vector4f(axis.X * axis.Z * (1 - cs) + axis.Y * ss, axis.Y * axis.Z * (1 - cs) - axis.X * ss, cs + axis.Z * axis.Z * (1 - cs), 0),
                new Vector4f(0, 0, 0, 1)
                );
        }
    }
}
