﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL.NativeGL;

namespace ModGL.Rendering
{
    public class Renderer
    {
        private readonly IOpenGL30 _gl;

        public Renderer(IOpenGL30 gl)
        {
            _gl = gl;
        }

        public void DrawElements(IElementArray elements, DrawMode mode)
        {
            using (elements.Bind())
            {
                _gl.glDrawElements(mode, (int)elements.Elements, elements.Type, IntPtr.Zero);
            }
        }

        public void Draw(DrawMode mode, IVertexBuffer buffer)
        {
            using (buffer.Bind())
            {
                _gl.glDrawArrays(mode, 0, (int)buffer.Elements);
            }
        }
    }
}
