using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL.NativeGL;

namespace ModGL
{
    public struct VertexArrayBindContext : IDisposable
    {
        private readonly IOpenGL30 gl;
        public BufferBindContext(IOpenGL30 gl, BufferTarget target)
        {
            this.gl = gl;
        }
        public void Dispose()
        {
            gl.glBindVertexArray
        }        
    }

    public struct BufferBindContext : IDisposable
    {
        private readonly BufferTarget target;
        private readonly IOpenGL30 gl;
        public BufferBindContext(IOpenGL30 gl, BufferTarget target)
        {
            this.gl = gl;
            this.target = target;
        }
        public void Dispose()
        {
            gl.glBindBuffer(target, 0);
        }
    }
}
