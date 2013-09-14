using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL.VertexInfo;

namespace ModGL
{
    public static class BufferExtensions
    {
        public static void CopyTo<TElementType>(this Buffer<TElementType> buffer, Buffer<ElementType> other, long start, long length, long targetOffset)
            where TElementType : struct 
        {
            Array.Copy(buffer.Data, start, other.Data, targetOffset, length);
        }
        public static void CopyTo<TElementType>(this Buffer<TElementType> buffer, ElementType[] other, long start, long length, long targetOffset)
            where TElementType : struct
        {
            Array.Copy(buffer.Data, start,other,targetOffset, length);
        }

    }
}
