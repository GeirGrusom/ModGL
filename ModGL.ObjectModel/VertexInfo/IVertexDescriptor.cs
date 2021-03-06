﻿using System;
using System.Collections.Generic;

namespace ModGL.ObjectModel.VertexInfo
{
    public interface IVertexDescriptor
    {
        Type ElementType { get; }
        IEnumerable<VertexElement> Elements { get; } 
    }

    public interface IVertexDescriptor<TElementType> : IVertexDescriptor
    {
    }
}