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
        private IOpenGL33 gl33;
        protected TextureBuffer(IEnumerable<TPixelType> elements, IOpenGL33 gl) : base(BufferTarget.Texture, elements, gl)
        {
            gl33 = gl;
        }

        protected TextureBuffer(long size, IOpenGL33 gl) : base(BufferTarget.Texture, size, gl)
        {
            gl33 = gl;
        }
    }
}
