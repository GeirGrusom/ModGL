using System;
using System.Collections.Generic;
using System.Numerics;
using ModGL.NativeGL;

namespace ModGL.VertexInfo
{
    internal static class ElementTypeHelper
    {
        internal class ElementDescription
        {
            public DataType DataType { get; private set; }
            public int Dimensions { get; private set; }

            internal ElementDescription(DataType dataType, int dimensions = 1)
            {
                DataType = dataType;
                Dimensions = dimensions;
            }

        }
        internal static readonly Dictionary<Type, ElementDescription> TypeConversionTable = new Dictionary<Type, ElementDescription>
        {
            { typeof(byte), new ElementDescription(DataType.UnsignedByte) },
            { typeof(sbyte), new ElementDescription(DataType.Byte) },
            { typeof(short), new ElementDescription(DataType.Short) },
            { typeof(ushort), new ElementDescription(DataType.UnsignedShort) },
            { typeof(int), new ElementDescription(DataType.Int) },
            { typeof(uint), new ElementDescription(DataType.UnsignedInt) },
            { typeof(float), new ElementDescription(DataType.Float) },
            { typeof(double), new ElementDescription(DataType.Double) },
            { typeof(Vector2f), new ElementDescription(DataType.Float, 2) },
            { typeof(Vector3f), new ElementDescription(DataType.Float, 3) },
            { typeof(Vector4f), new ElementDescription(DataType.Float, 4) }
        };
    }
}