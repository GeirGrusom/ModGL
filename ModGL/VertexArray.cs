using System;
using System.Collections.Generic;
using System.Linq;

using ModGL.Buffers;
using ModGL.NativeGL;
using InvalidOperationException = System.InvalidOperationException;

namespace ModGL
{
    using VertexInfo;
    public interface IVertexArray : IGLObject, IBindable, IDisposable
    {
        IEnumerable<IBuffer> Buffers { get; }
    }

    public sealed class VertexArray : IVertexArray
    {
        private readonly IOpenGL30 _gl;

        public IEnumerable<IBuffer> Buffers { get; private set; }

        public VertexArray(IOpenGL30 gl, IEnumerable<IVertexBuffer> buffers, IEnumerable<VertexDescriptor> descriptors)
        {
            IVertexBuffer[] bufferObjects = buffers.ToArray();
            VertexDescriptor[] descs = descriptors.ToArray();
            if (descs.Length != bufferObjects.Length)
                throw new InvalidOperationException("Number of buffers and number of descriptors must match.");

            
            uint[] handles = new uint[1];
            gl.GenVertexArrays(1, handles);
            if(handles[0] == 0u)
                throw new NoHandleCreatedException();

            _gl = gl;
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
            _gl.BindVertexArray(Handle);
            return new BindContext(() => _gl.BindVertexArray(0));
        }

        public void Dispose()
        {
            _gl.DeleteVertexArrays(1, new [] { Handle });
        }
    }
}
