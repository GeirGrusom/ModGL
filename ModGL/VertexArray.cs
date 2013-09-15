using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL.NativeGL;

namespace ModGL
{
    public interface IVertexArray : IGLObject
    {
        
    }

    public class VertexArray<TElementType> : IVertexArray, IBindable
    {
        private readonly IOpenGL30 _gl;
        public VertexArray(IOpenGL30 gl)
        {
            _gl = gl;
            uint[] handles = new uint[1];
            gl.glGenVertexArrays(1, handles);
            Handle = handles.Single();
        }

        public uint Handle { get; private set; }

        public BindContext Bind()
        {
            _gl.glBindVertexArray(Handle);
            return new BindContext(() => _gl.glBindVertexArray(0));
        }
    }
}
