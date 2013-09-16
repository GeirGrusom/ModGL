using System;

namespace ModGL.Buffers
{
    public static class BufferExtensions
    {
        /// <summary>
        /// Copies a buffer into another buffer.
        /// </summary>
        /// <typeparam name="TElementType">Buffer element type.</typeparam>
        /// <param name="buffer"></param>
        /// <param name="other">Buffer to overwrite.</param>
        /// <param name="start">Copy start index.</param>
        /// <param name="length">Copy length.</param>
        /// <param name="targetOffset">Offset into target buffer.</param>
        public static void CopyTo<TElementType>(this Buffer<TElementType> buffer, Buffer<TElementType> other, long start, long length, long targetOffset)
            where TElementType : struct 
        {
            Array.Copy(buffer.Data, start, other.Data, targetOffset, length);
        }

        /// <summary>
        /// Copies a buffer into an array of the same type.
        /// </summary>
        /// <typeparam name="TElementType">Type of elements in buffer.</typeparam>
        /// <param name="buffer"></param>
        /// <param name="other">Array to overwrite.</param>
        /// <param name="start">Copy start index.</param>
        /// <param name="length">Copy length.</param>
        /// <param name="targetOffset">Offset into target array.</param>
        public static void CopyTo<TElementType>(this Buffer<TElementType> buffer, TElementType[] other, long start, long length, long targetOffset)
            where TElementType : struct
        {
            Array.Copy(buffer.Data, start,other,targetOffset, length);
        }
    }
}
