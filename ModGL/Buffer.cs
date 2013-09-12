using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using ModGL.NativeGL;

namespace ModGL
{

    public interface IBuffer : IGLObject
    {
        long Elements { get; }
        int ElementSize { get; }
        BufferTarget Target { get; }
        Type ElementType { get; }
    }

    public class Buffer<TElementType> : IBuffer
        where TElementType : struct
    {
        internal TElementType[] data;
        private readonly int elementSize;
        private readonly IOpenGL30 gl;

        public BufferTarget Target { get; private set; }

        public long Elements { get { return data.LongLength; } }

        public int ElementSize { get { return elementSize; } }

        public uint Handle { get; private set; }

        protected Buffer(BufferTarget target, IEnumerable<TElementType> elements , IOpenGL30 gl)
        {
            if(gl == null)
                throw new ArgumentNullException("gl");
            this.gl = gl;
            data = elements.ToArray();
            Target = target;
            elementSize = Marshal.SizeOf(typeof(TElementType));
            uint[] names = new uint[1];
            gl.glGenBuffers(1, names);
            Handle = names.Single();
        }

        protected Buffer(BufferTarget target, long size, IOpenGL30 gl)
        {
            if (gl == null)
                throw new ArgumentNullException("gl");
            this.gl = gl;
            data = new TElementType[size];
            Target = target;
            elementSize = Marshal.SizeOf(typeof(TElementType));
            uint[] names = new uint[1];
            gl.glGenBuffers(1, names);
            Handle = names.Single();
        }

        public void BufferData(BufferUsage usage)
        {
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                gl.glBufferData(Target, new IntPtr(data.LongLength * ElementSize), handle.AddrOfPinnedObject(), usage);
            }
            finally
            {
                handle.Free();
            }
        }

        public Type ElementType { get { return typeof(TElementType); } }

        public void BufferSubData(BufferUsage usage)
        {
            throw new NotImplementedException();
        }

        public void BufferSubData<TElement>(BufferUsage usage, System.Linq.Expressions.Expression<Func<TElementType, TElement>> elementProc)
        {
            throw new NotImplementedException();
        }

        public TElementType this[long index]
        {
            get { return data[index]; }
            set { data[index] = value; }
        }
    }
}
