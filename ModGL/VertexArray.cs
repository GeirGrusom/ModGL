using System;
using System.Collections.Generic;
using System.Linq;

using ModGL.NativeGL;

namespace ModGL
{
    public interface IVertexArray : IGLObject, IBindable
    {
        
    }

    public sealed class VertexArray : IVertexArray
    {
        private readonly IOpenGL30 _gl;

        public IEnumerable<IBuffer> Buffers { get; private set; }

        public VertexArray(IOpenGL30 gl, IEnumerable<IBuffer> buffers, IEnumerable<VertexInfo.VertexDescriptor> descriptors)
        {
            IBuffer[] bufferObjects = buffers.ToArray();
            VertexInfo.VertexDescriptor[] descs = descriptors.ToArray();
            if (descs.Length != bufferObjects.Length)
                throw new InvalidOperationException("Number of buffers and number of descriptors must match.");

            _gl = gl;
            uint[] handles = new uint[1];
            gl.glGenVertexArrays(1, handles);
            Handle = handles.Single();
            Buffers = bufferObjects;
            using (Bind())
            {
                int count = 0;
                for (int index = 0; index < bufferObjects.Length; index++)
                {
                    var buffer = bufferObjects[index];
                    var desc = descs[index];
                    buffer.Bind();
                    desc.Apply(gl, count);
                    count += desc.Elements.Count();
                }
            }
        }

        public uint Handle { get; private set; }

        public BindContext Bind()
        {
            _gl.glBindVertexArray(Handle);
            return new BindContext(() => _gl.glBindVertexArray(0));
        }
    }
}
