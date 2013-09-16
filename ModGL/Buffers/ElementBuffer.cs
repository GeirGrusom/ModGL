using System;
using System.Collections.Generic;

using ModGL.NativeGL;

namespace ModGL.Buffers
{
    public sealed class ElementBuffer<TElementType> : Buffer<TElementType>, IElementArray
        where TElementType : struct
    {
        public ElementBufferItemType Type { get; private set; }

        private ElementBufferItemType DetermineType()
        {
            var type = typeof(TElementType);
            if(type == typeof(int) || type == typeof(uint))
                return ElementBufferItemType.UnsignedInt;
            if(type == typeof(short) || type == typeof(ushort))
                return ElementBufferItemType.UnsignedShort;
            if(type == typeof(byte) || type == typeof(sbyte))
                return ElementBufferItemType.UnsignedByte;
            throw new ArgumentException("Generic argument TElementType must be either unsigned or signed byte, short or int.", "TElementType");
        }

        public ElementBuffer(IEnumerable<TElementType> elements, IOpenGL30 gl)
            : base(BufferTarget.ElementArray, elements, gl)
        {
            Type = DetermineType();
        }
        public ElementBuffer(long size, IOpenGL30 gl)
            : base(BufferTarget.ElementArray, size, gl)
        {
            Type = DetermineType();
        }
    }
}