using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ModGL.NativeGL;
using InvalidOperationException = System.InvalidOperationException;

namespace ModGL.ObjectModel.VertexInfo
{
    using Buffers;

    public interface IVertexArray : IGLObject, IBindable, IDisposable
    {
        IEnumerable<IBuffer> Buffers { get; }
    }

    public sealed class VertexArray : IVertexArray
    {
        private readonly IOpenGL30 _gl;

        public IEnumerable<IBuffer> Buffers { get; private set; }

        public VertexArray(IOpenGL30 gl, IEnumerable<IVertexBuffer> buffers, IEnumerable<IVertexDescriptor> descriptors)
        {
            IVertexBuffer[] bufferObjects = buffers.ToArray();
            IVertexDescriptor[] descs = descriptors.ToArray();
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
                    Apply(desc, count);
                    count += desc.Elements.Count();
                }
            }
        }

        private void Apply(IVertexDescriptor descriptor, int indexOffset)
        {
            var openGL41 = _gl as IOpenGL41;
            foreach (var e in descriptor.Elements.Select((e, i) => new { Index = i + indexOffset, Item = e }))
            {
                // Double is supported by glVertexAttribLPointer, which is not implemented in OpenGL 3.0.
                if (e.Item.Type == DataType.Half || e.Item.Type == DataType.Float)
                {
                    _gl.VertexAttribPointer(
                        (uint)e.Index,
                        e.Item.Dimensions,
                        (uint)e.Item.Type,
                        (byte)GLboolean.False,
                        Marshal.SizeOf(descriptor.ElementType),
                        new IntPtr(e.Item.Offset));
                }
                else if (openGL41 != null && e.Item.Type == DataType.Double)
                {
                    openGL41.VertexAttribLPointer(
                        (uint)e.Index,
                        e.Item.Dimensions,
                        (uint)e.Item.Type,
                        Marshal.SizeOf(descriptor.ElementType),
                        new IntPtr(e.Item.Offset));
                }
                else
                {
                    _gl.VertexAttribIPointer(
                        (uint)e.Index,
                        e.Item.Dimensions,
                        (uint)e.Item.Type,
                        Marshal.SizeOf(descriptor.ElementType),
                        new IntPtr(e.Item.Offset));
                }
                _gl.EnableVertexAttribArray((uint)e.Index);
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
