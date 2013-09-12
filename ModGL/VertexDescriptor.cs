﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL.NativeGL;

namespace ModGL
{

    public enum ElementType
    {
        @byte, @short, @int, @long,
        unsigned_byte, unsigned_short, unsigned_int, unsigned_long,
        half, @float, @double
    }

    public class VertexElement
    {
        public int Length { get; private set; }
        public string Name { get; private set; }
        public int Dimensions { get; private set;}
        public ElementType Type { get; private set; }
    }

    public class VertexDescriptor<TElementType>
    {
        public IEnumerable<VertexElement> Elements { get; private set; }
        public void Apply(VertexArray<TElementType> array, IOpenGL30 gl)
        {
            
        }
    }
}
