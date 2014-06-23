using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Numerics;

namespace ModGL.Numerics
{
    [ImmutableObject(true)]
    [DebuggerDisplay("{_data[0]}, {_data[1]}, {_data[2]}, {_data[3]}")]
    public sealed class Matrix4f
    {
        public static readonly Matrix4f Identity = new Matrix4f(new Vector4f(1, 0, 0, 0), new Vector4f(0, 1, 0, 0), new Vector4f(0, 0, 1, 0), new Vector4f(0, 0, 0, 1));
        internal readonly Vector4f[] _data;


        /// <summary>
        /// Gets the specified row from the matrix.
        /// </summary>
        /// <param name="index">Row index. Must be between 0 and 3 inclusive.</param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">Thrown in index is less than zero or more than three.</exception>
        [Pure]
        public Vector4f Row(int index)
        {
            Contract.Requires(index >= 0 && index < 4);
            Contract.EndContractBlock();
            if (index < 0 || index > 3)
                throw new IndexOutOfRangeException();
            return _data[index];
        }

        /// <summary>
        /// Gets the specified column from the matrix.
        /// </summary>
        /// <param name="index">Column index. Must be between 0 and 3 inclusive.</param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        [Pure]
        public Vector4f Column(int index)
        {
            Contract.Requires(index >= 0 && index < 4);
            Contract.EndContractBlock();

            if (index == 0)
                return new Vector4f(_data[0].X, _data[1].X, _data[2].X, _data[3].X);
            if (index == 1)
                return new Vector4f(_data[0].Y, _data[1].Y, _data[2].Y, _data[3].Y);
            if (index == 2)
                return new Vector4f(_data[0].Z, _data[1].Z, _data[2].Z, _data[3].Z);
            if (index == 3)
                return new Vector4f(_data[0].W, _data[1].W, _data[2].W, _data[3].W);

            throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// Calculates the determinant of the matrix.
        /// </summary>
        /// <returns></returns>
        [Pure]
        public float Determinant()
        {
            // Gnarly
            return
                _data[0].X * _data[1].Y * _data[2].Z * _data[3].W - _data[0].X * _data[1].Y * _data[2].W * _data[3].Z + _data[0].X * _data[1].Z * _data[2].W * _data[3].Y - _data[0].X * _data[1].Z * _data[2].Y * _data[3].W
              + _data[0].X * _data[1].W * _data[2].Y * _data[3].Z - _data[0].X * _data[1].W * _data[2].Z * _data[3].Y - _data[0].Y * _data[1].Z * _data[2].W * _data[3].X + _data[0].Y * _data[1].Z * _data[2].X * _data[3].W
              - _data[0].Y * _data[1].W * _data[2].X * _data[3].Z + _data[0].Y * _data[1].W * _data[2].Z * _data[3].X - _data[0].Y * _data[1].X * _data[2].Z * _data[3].W + _data[0].Y * _data[1].X * _data[2].W * _data[3].Z
              + _data[0].Z * _data[1].W * _data[2].X * _data[3].Y - _data[0].Z * _data[1].W * _data[2].Y * _data[3].X + _data[0].Z * _data[1].X * _data[2].Y * _data[3].W - _data[0].Z * _data[1].X * _data[2].W * _data[3].Y
              + _data[0].Z * _data[1].Y * _data[2].W * _data[3].X - _data[0].Z * _data[1].Y * _data[2].X * _data[3].W - _data[0].W * _data[1].X * _data[2].Y * _data[3].Z + _data[0].W * _data[1].X * _data[2].Z * _data[3].Y
              - _data[0].W * _data[1].Y * _data[2].Z * _data[3].X + _data[0].W * _data[1].Y * _data[2].X * _data[3].Z - _data[0].W * _data[1].Z * _data[2].X * _data[3].Y + _data[0].W * _data[1].Z * _data[2].Y * _data[3].X;
        }

        private float this[int row, int column]
        {
            get
            {
                var r = _data[row];
                if (column == 0)
                    return r.X;
                if (column == 1)
                    return r.Y;
                if (column == 2)
                    return r.Z;
                if (column == 3)
                    return r.W;
                throw new IndexOutOfRangeException("Column must be between 0 and 3 inclusive.");
            }
        }

        /// <summary>
        /// Calculates the inverse matrix. If no inverse exists, throws InvalidOperationException.
        /// </summary>
        /// <returns>Invertex matrix</returns>
        /// <exception cref="InvalidOperationException">Thrown if no inverse matrix can be calculated.</exception>
        // Taken from Robin Hillard's code on http://stackoverflow.com/questions/2624422/efficient-4x4-matrix-inverse-affine-transform
        // TODO: check for vector operations and inline this[]
        [Pure]
        public Matrix4f Invert()
        {
            var s0 = this[0, 0] * this[1, 1] - this[1, 0] * this[0, 1];
            var s1 = this[0, 0] * this[1, 2] - this[1, 0] * this[0, 2];
            var s2 = this[0, 0] * this[1, 3] - this[1, 0] * this[0, 3];
            var s3 = this[0, 1] * this[1, 2] - this[1, 1] * this[0, 2];
            var s4 = this[0, 1] * this[1, 3] - this[1, 1] * this[0, 3];
            var s5 = this[0, 2] * this[1, 3] - this[1, 2] * this[0, 3];

            var c5 = this[2, 2] * this[3, 3] - this[3, 2] * this[2, 3];
            var c4 = this[2, 1] * this[3, 3] - this[3, 1] * this[2, 3];
            var c3 = this[2, 1] * this[3, 2] - this[3, 1] * this[2, 2];
            var c2 = this[2, 0] * this[3, 3] - this[3, 0] * this[2, 3];
            var c1 = this[2, 0] * this[3, 2] - this[3, 0] * this[2, 2];
            var c0 = this[2, 0] * this[3, 1] - this[3, 0] * this[2, 1];

            
            var invdet = 1.0f / (s0 * c5 - s1 * c4 + s2 * c3 + s3 * c2 - s4 * c1 + s5 * c0);
            if(invdet >= -float.Epsilon && invdet >= float.Epsilon)
                throw new InvalidOperationException("Matrix is not invertible.");

            var b = new Vector4f[4];

            b[0] = new Vector4f(
                ( this[1, 1] * c5 - this[1, 2] * c4 + this[1, 3] * c3) * invdet,
                (-this[0, 1] * c5 + this[0, 2] * c4 - this[0, 3] * c3) * invdet,
                ( this[3, 1] * s5 - this[3, 2] * s4 + this[3, 3] * s3) * invdet,
                (-this[2, 1] * s5 + this[2, 2] * s4 - this[2, 3] * s3) * invdet);

            b[1] = new Vector4f(
                (-this[1, 0] * c5 + this[1, 2] * c2 - this[1, 3] * c1) * invdet,
                ( this[0, 0] * c5 - this[0, 2] * c2 + this[0, 3] * c1) * invdet,
                (-this[3, 0] * s5 + this[3, 2] * s2 - this[3, 3] * s1) * invdet,
                ( this[2, 0] * s5 - this[2, 2] * s2 + this[2, 3] * s1) * invdet);

            b[2] = new Vector4f(
                ( this[1, 0] * c4 - this[1, 1] * c2 + this[1, 3] * c0) * invdet,
                (-this[0, 0] * c4 + this[0, 1] * c2 - this[0, 3] * c0) * invdet,
                ( this[3, 0] * s4 - this[3, 1] * s2 + this[3, 3] * s0) * invdet,
                (-this[2, 0] * s4 + this[2, 1] * s2 - this[2, 3] * s0) * invdet);

            b[3] = new Vector4f(
                (-this[1, 0] * c3 + this[1, 1] * c1 - this[1, 2] * c0) * invdet,
                ( this[0, 0] * c3 - this[0, 1] * c1 + this[0, 2] * c0) * invdet,
                (-this[3, 0] * s3 + this[3, 1] * s1 - this[3, 2] * s0) * invdet,
                ( this[2, 0] * s3 - this[2, 1] * s1 + this[2, 2] * s0) * invdet);

            return new Matrix4f(b[0], b[1], b[2], b[3]);
        }

        [Pure]
        public Matrix4f Multiply(Matrix4f rhs)
        {
            Contract.Requires(rhs != null, "rhs cannot be null.");
            Contract.EndContractBlock();

            if (rhs == null)
                throw new ArgumentNullException("rhs");

            var result = new float[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result[j, i] = System.Numerics.VectorMath.DotProduct(Column(j), rhs.Row(i));
                }
            }
            return new Matrix4f(
                new Vector4f(result[0, 0], result[1, 0], result[2, 0], result[3, 0]),
                new Vector4f(result[0, 1], result[1, 1], result[2, 1], result[3, 1]),
                new Vector4f(result[0, 2], result[1, 2], result[2, 2], result[3, 2]),
                new Vector4f(result[0, 3], result[1, 3], result[2, 3], result[3, 3]));
        }

        [Pure]
        public static Matrix4f operator *(Matrix4f lhs, Matrix4f rhs)
        {
            Contract.Requires(lhs != null);
            Contract.Requires(rhs != null);
            Contract.EndContractBlock();
            return lhs.Multiply(rhs);
        }

        [Pure]
        public static Vector4f operator *(Matrix4f lhs, Vector4f rhs)
        {
            Contract.Requires(lhs != null);
            Contract.EndContractBlock();
            return lhs.Multiply(rhs);
        }

        [Pure]
        public Vector4f Multiply(Vector4f vector)
        {
            return new Vector4f(
                System.Numerics.VectorMath.DotProduct(vector, Row(0)),
                System.Numerics.VectorMath.DotProduct(vector, Row(1)),
                System.Numerics.VectorMath.DotProduct(vector, Row(2)),
                System.Numerics.VectorMath.DotProduct(vector, Row(3)));
        }

        [Pure]
        public Vector3f Multiply(Vector3f vector)
        {
            var vec = new Vector4f(vector.X, vector.Y, vector.Z, 1);
            var result = new Vector4f(
                System.Numerics.VectorMath.DotProduct(vec, Row(0)),
                System.Numerics.VectorMath.DotProduct(vec, Row(1)),
                System.Numerics.VectorMath.DotProduct(vec, Row(2)),
                System.Numerics.VectorMath.DotProduct(vec, Row(3)));

            return new Vector3f(result.X, result.Y, result.Z);
        }

        public Matrix4f()
        {
            Contract.Ensures(_data != null);
            Contract.Ensures(_data.Length == 4);
            Contract.EndContractBlock();
            _data = new Vector4f[4];
        }

        public Matrix4f(Vector4f row0, Vector4f row1, Vector4f row2, Vector4f row3)
        {
            Contract.Ensures(_data != null);
            Contract.Ensures(_data.Length == 4);
            Contract.EndContractBlock();
            _data = new[] { row0, row1, row2, row3 };
        }

        [Pure]
        public Matrix4f Transpose()
        {
            return new Matrix4f(
                new Vector4f(_data[0].X, _data[1].X, _data[2].X, _data[3].X),
                new Vector4f(_data[0].Y, _data[1].Y, _data[2].Y, _data[3].Y),
                new Vector4f(_data[0].Z, _data[1].Z, _data[2].Z, _data[3].Z),
                new Vector4f(_data[0].W, _data[1].W, _data[2].W, _data[3].W));
        }
    }
}
