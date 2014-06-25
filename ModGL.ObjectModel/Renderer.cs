using System;
using ModGL.NativeGL;

namespace ModGL.ObjectModel
{
    using Buffers;

    public struct Renderer
    {
        private readonly IOpenGL30 _gl;

        public Renderer(IOpenGL30 gl)
        {
            _gl = gl;
        }

        public void Draw(PrimitiveType mode, IElementArray elements)
        {
            using (elements.Bind())
            {
                _gl.DrawElements(mode, (int)elements.Elements, (uint)elements.Type, IntPtr.Zero);
            }
        }

        public void Draw(PrimitiveType mode, IVertexBuffer buffer)
        {
            _gl.DrawArrays(mode, 0, (int)buffer.Elements);
        }
    }
}
