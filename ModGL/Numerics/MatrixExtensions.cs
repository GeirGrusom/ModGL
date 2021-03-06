﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ModGL.Numerics
{
    public static class MatrixExtensions
    {
        [Pure]
        public static Matrix4f Translate(this Matrix4f mat, Vector3f offset)
        {
            return MatrixHelper.Translate(offset).Multiply(mat);
        }

        [Pure]
        public static Matrix4f Translate(this Matrix4f mat, float x, float y, float z)
        {
            return MatrixHelper.Translate(x, y, z).Multiply(mat);
        }

        [Pure]
        public static Matrix4f Scale(this Matrix4f mat, Vector3f scale)
        {
            return MatrixHelper.Scale(scale).Multiply(mat);
        }

        [Pure]
        public static Matrix4f Scale(this Matrix4f mat, float x, float y, float z)
        {
            return MatrixHelper.Scale(x, y, z).Multiply(mat);
        }

        [Pure]
        public static Matrix4f RotateX(this Matrix4f mat, float angleInRadians)
        {
            return MatrixHelper.RotateX(angleInRadians).Multiply(mat);
        }

        [Pure]
        public static Matrix4f RotateY(this Matrix4f mat, float angleInRadians)
        {
            return MatrixHelper.RotateY(angleInRadians).Multiply(mat);
        }

        [Pure]
        public static Matrix4f RotateZ(this Matrix4f mat, float angleInRadians)
        {
            return MatrixHelper.RotateZ(angleInRadians).Multiply(mat);
        }
    }
}
