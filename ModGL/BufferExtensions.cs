using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModGL
{
    public static class BufferExtensions
    {
        public static void CopyTo<TElementType>(this Buffer<TElementType> buffer, Buffer<ElementType> other, long start, long length, long targetOffset)
            where TElementType : struct 
        {
            Array.Copy(buffer.data, start, other.data, targetOffset, length);
        }
        public static void CopyTo<TElementType>(this Buffer<TElementType> buffer, ElementType[] other, long start, long length, long targetOffset)
            where TElementType : struct
        {
            Array.Copy(buffer.data, start,other,targetOffset, length);
        }

    }
}
