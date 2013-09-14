using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL.NativeGL;

namespace ModGL
{
    public interface IVertexBuffer : IBuffer
    {
    }

    public interface IElementArray : IBuffer
    {
        ElementBufferItemType Type { get; }
    }

    public sealed class VertexBuffer<TElementType> : Buffer<TElementType>, IVertexBuffer
        where TElementType : struct
    {
        public VertexBuffer(IEnumerable<TElementType> elements, IOpenGL30 gl)
            : base(BufferTarget.Array, elements, gl)
        {
        }

        public VertexBuffer(long size, IOpenGL30 gl)
            : base(BufferTarget.Array, size, gl)
        {
        }
   }
}
