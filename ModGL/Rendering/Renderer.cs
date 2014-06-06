using System;
using ModGL.Buffers;
using ModGL.NativeGL;

namespace ModGL.Rendering
{
    public struct Renderer
    {
        private readonly IOpenGL30 _gl;

        public Renderer(IOpenGL30 gl)
        {
            _gl = gl;
        }

        public void Draw(DrawMode mode, IElementArray elements)
        {
            using (elements.Bind())
            {
                _gl.DrawElements(mode, (int)elements.Elements, elements.Type, IntPtr.Zero);
            }
        }

        public void Draw(DrawMode mode, IVertexBuffer buffer)
        {
            _gl.DrawArrays(mode, 0, (int)buffer.Elements);
        }
    }
}
