using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ModGL.NativeGL;

namespace ModGL
{

    public interface IBuffer : IGLObject, IBindable
    {
        long Elements { get; }
        int ElementSize { get; }
        BufferTarget Target { get; }
        Type ElementType { get; }
    }

    public class Buffer<TElementType> : IBuffer
        where TElementType : struct
    {
        internal TElementType[] Data;
        private readonly int _elementSize;
        private readonly IOpenGL30 _gl;

        public bool Released { get; private set; }

        public BufferTarget Target { get; private set; }

        public long Elements { get { return Data.LongLength; } }

        public int ElementSize { get { return _elementSize; } }

        public uint Handle { get; private set; }

        public BindContext Bind()
        {
            _gl.glBindBuffer(Target, Handle);
            return new BindContext(() => _gl.glBindBuffer(Target, 0));
        }

        public BindContext Bind(uint index)
        {
            _gl.glBindBufferBase(Target, index, Handle);
            return new BindContext(() => _gl.glBindBufferBase(Target, index, 0));
        }

        public BindContext Bind(uint index, long startIndex, long elements)
        {
            _gl.glBindBufferRange(Target, index, Handle, new IntPtr(startIndex * _elementSize), new IntPtr(elements * _elementSize));
            return new BindContext(() => _gl.glBindBufferBase(Target, index, 0));
        }

        public void ReleaseClientData()
        {
            Data = null;
            Released = true;
        }

        private void ReleasedConstraint()
        {
            if(Released)
                throw new InvalidOperationException();
        }

        private Buffer(BufferTarget target, IOpenGL30 gl)
        {
            ReleasedConstraint();
            if (gl == null)
                throw new ArgumentNullException("gl");
            _gl = gl;
            Target = target;
            _elementSize = Marshal.SizeOf(typeof(TElementType));
            var names = new uint[1];
            gl.glGenBuffers(1, names);
            Handle = names.Single();
        }

        protected Buffer(BufferTarget target, IEnumerable<TElementType> elements , IOpenGL30 gl)
            : this(target,  gl)
        {
            if(elements == null)
                throw new ArgumentNullException("elements");
            Data = elements.ToArray();
        }

        protected Buffer(BufferTarget target, long size, IOpenGL30 gl)
            : this(target, gl)
        {
            Data = new TElementType[size];
        }

        public void BufferData(BufferUsage usage)
        {
            ReleasedConstraint();
            var handle = GCHandle.Alloc(Data, GCHandleType.Pinned);
            try
            {
                _gl.glBufferData(Target, new IntPtr(Data.LongLength * ElementSize), handle.AddrOfPinnedObject(), usage);
            }
            finally
            {
                handle.Free();
            }
        }

        public Type ElementType { get { return typeof(TElementType); } }

        public void BufferSubData(BufferUsage usage, int offset, int size)
        {
            ReleasedConstraint();
            var handle = GCHandle.Alloc(Data, GCHandleType.Pinned);
            try
            {
                _gl.glBufferSubData(Target, new IntPtr(offset), new IntPtr(size), handle.AddrOfPinnedObject());
            }
            finally
            {
                handle.Free();
            }
        }

        public void BufferSubData<TElement>(BufferUsage usage, System.Linq.Expressions.Expression<Func<TElementType, TElement>> elementProc)
        {
            ReleasedConstraint();
            throw new NotImplementedException();
        }

        public TElementType this[long index]
        {
            get { ReleasedConstraint(); return Data[index]; }
            set { ReleasedConstraint(); Data[index] = value; }
        }
    }
}
