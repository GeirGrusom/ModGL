using ModGL.NativeGL;

namespace ModGL.VertexInfo
{
    public sealed class VertexElement
    {
        public int Dimensions { get; private set; }
        public string Name { get; private set; }
        public DataType Type { get; private set; }
        public int Offset { get; private set; }

        public VertexElement(string name, DataType type, int dimensions, int offset)
        {
            Name = name;
            Type = type;
            Dimensions = dimensions;
            Offset = offset;
        }
    }
}