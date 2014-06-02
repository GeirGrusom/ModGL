using System;
using System.Numerics;

namespace ModGL.Numerics
{
    public class Matrix4f
    {
        public static readonly Matrix4f Identity = new Matrix4f(new Vector4f(1, 0, 0, 0), new Vector4f(0, 1, 0, 0), new Vector4f(0, 0, 1, 0), new Vector4f(0, 0, 0, 1));
        internal readonly Vector4f[] _data;

        public Vector4f Row(int index)
        {
            if(index < 0 || index > 3)
                throw new IndexOutOfRangeException();
            return _data[index];
        }

        public Vector4f Column(int index)
        {
            if(index == 0)
                return new Vector4f(_data[0].X, _data[1].X, _data[2].X, _data[3].X);
            if(index == 1)
                return new Vector4f(_data[0].Y, _data[1].Y, _data[2].Y, _data[3].Y);
            if(index == 2)
                return new Vector4f(_data[0].Z, _data[1].Z, _data[2].Z, _data[3].Z);
            if(index  == 3)
                return new Vector4f(_data[0].W, _data[1].W, _data[2].W, _data[3].W);

            throw new IndexOutOfRangeException();
        }

        public Matrix4f Invert()
        {
            
        }

        public Matrix4f Multiply(Matrix4f rhs)
        {
            if(rhs == null)
                throw new ArgumentNullException("rhs");

            var result = new float[4,4];
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

        public static Matrix4f operator *(Matrix4f lhs, Matrix4f rhs)
        {
            return lhs.Multiply(rhs);
        }

        public Vector4f Multiply(Vector4f vector)
        {
            return new Vector4f(
                System.Numerics.VectorMath.DotProduct(vector, Row(0)),
                System.Numerics.VectorMath.DotProduct(vector, Row(1)),
                System.Numerics.VectorMath.DotProduct(vector, Row(2)),
                System.Numerics.VectorMath.DotProduct(vector, Row(3)));
        }

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
            _data = new Vector4f[4];
        }

        public Matrix4f(Vector4f row0, Vector4f row1, Vector4f row2, Vector4f row3)
        {
            _data = new [] { row0, row1, row2, row3 };
        }

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
