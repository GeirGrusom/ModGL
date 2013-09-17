using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL.NativeGL;

namespace ModGL.Buffers
{
    public class TextureBuffer<TPixelType> : Buffer<TPixelType>
        where TPixelType : struct 
    {
        private IOpenGL42 gl42;
        protected TextureBuffer(IEnumerable<TPixelType> elements, IOpenGL42 gl) : base(BufferTarget.Texture, elements, gl)
        {
            gl42 = gl;
        }

        protected TextureBuffer(long size, IOpenGL42 gl) : base(BufferTarget.Texture, size, gl)
        {
            gl42 = gl;
        }
    }
}
