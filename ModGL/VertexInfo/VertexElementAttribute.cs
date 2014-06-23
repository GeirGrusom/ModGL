using System;
using ModGL.NativeGL;

namespace ModGL.VertexInfo
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Struct)]
    public class VertexElementAttribute : Attribute
    {
        public VertexElementAttribute(DataType type, int dimensions)
        {
            DataType = type;
            Dimensions = dimensions;
        }

        public VertexElementAttribute(DataType type)
        {
            DataType = type;
            Dimensions = 1;
        }

        public DataType DataType;
        public int Dimensions;
    }
}