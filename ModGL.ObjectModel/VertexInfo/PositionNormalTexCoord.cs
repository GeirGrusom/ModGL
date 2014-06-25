using System.Numerics;
using System.Runtime.InteropServices;

namespace ModGL.ObjectModel.VertexInfo
{
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public struct PositionNormalTexCoord
    {
        public static readonly IVertexDescriptor<PositionNormalTexCoord> Descriptor = VertexDescriptor.Create<PositionNormalTexCoord>();
        [FieldOffset(0)]
        public Vector3f Position;
        [FieldOffset(12)]
        public Vector3f Normal;
        [FieldOffset(24)]
        public Vector2f TexCoord;
    }
}