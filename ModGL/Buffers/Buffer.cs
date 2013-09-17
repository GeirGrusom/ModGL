using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

using ModGL.NativeGL;

namespace ModGL.Buffers
{
    public interface IBuffer : IGLObject, IBindable
    {
        long Elements { get; }
        int ElementSize { get; }
        BufferTarget Target { get; }
        Type ElementType { get; }
    }

    public class MappedBuffer : IDisposable
    {
        private BindContext _bufferBindingContext;
        public IBuffer Buffer { get; private set; }
        public UnmanagedMemoryAccessor Accessor { get; private set; }

        internal MappedBuffer(IBuffer buffer, BindContext context, UnmanagedMemoryAccessor accessor)
        {
            this.Buffer = buffer;
            this._bufferBindingContext = context;
            this.Accessor = accessor;
        }

        public void Dispose()
        {
            this.Accessor.Dispose();
            this._bufferBindingContext.Dispose();
        }
    }

    public class SafeMapBuffer : SafeBuffer
    {
        internal SafeMapBuffer(IntPtr handle)
            : base(false)
        {
            this.handle = handle;
        }

        protected override bool ReleaseHandle()
        {
            // This class cannot reliably release the handle.
            return true;
        }
    }

    public class Buffer<TElementType> : IBuffer
        where TElementType : struct
    {
        internal TElementType[] Data;
        internal readonly long length;
        private readonly int _elementSize;
        private readonly IOpenGL30 _gl;

        public bool Released { get; private set; }

        public BufferTarget Target { get; private set; }

        public long Elements { get { return this.length; } }

        public int ElementSize { get { return this._elementSize; } }

        public uint Handle { get; private set; }

        public BindContext Bind()
        {
            this._gl.glBindBuffer(this.Target, this.Handle);
            return new BindContext(() => this._gl.glBindBuffer(this.Target, 0));
        }

        public BindContext Bind(uint index)
        {
            this._gl.glBindBufferBase(this.Target, index, this.Handle);
            return new BindContext(() => this._gl.glBindBufferBase(this.Target, index, 0));
        }

        public BindContext Bind(uint index, long startIndex, long elements)
        {
            this._gl.glBindBufferRange(this.Target, index, this.Handle, new IntPtr(startIndex * this._elementSize), new IntPtr(elements * this._elementSize));
            return new BindContext(() => this._gl.glBindBufferBase(this.Target, index, 0));
        }

        public void ReleaseClientData()
        {
            this.Data = null;
            this.Released = true;
        }

        private void ReleasedConstraint()
        {
            if(this.Released)
                throw new InvalidOperationException();
        }

        private Buffer(BufferTarget target, IOpenGL30 gl)
        {
            if (gl == null)
                throw new ArgumentNullException("gl");
            var names = new uint[1];
            gl.glGenBuffers(1, names);
            Handle = names.Single();
            if(Handle == 0)
                throw new NoHandleCreatedException();

            _gl = gl;
            Target = target;
            _elementSize = Marshal.SizeOf(typeof(TElementType));
        }

        protected Buffer(BufferTarget target, IEnumerable<TElementType> elements , IOpenGL30 gl)
            : this(target,  gl)
        {
            if(elements == null)
                throw new ArgumentNullException("elements");
            this.Data = elements.ToArray();
            this.length = this.Data.LongLength;
        }

        protected Buffer(BufferTarget target, long size, IOpenGL30 gl)
            : this(target, gl)
        {
            this.Data = new TElementType[size];
            this.length = size;
        }

        public void BufferData(BufferUsage usage)
        {
            ReleasedConstraint();
            var handle = GCHandle.Alloc(this.Data, GCHandleType.Pinned);
            try
            {
                this._gl.glBufferData(this.Target, new IntPtr(this.Data.LongLength * this.ElementSize), handle.AddrOfPinnedObject(), usage);
            }
            finally
            {
                handle.Free();
            }
        }

        /// <summary>
        /// Maps a buffer to memory.
        /// </summary>
        /// <param name="access">Map access</param>
        /// <returns>A mapped buffer handle. This object must be disposed after use.</returns>
        public MappedBuffer MapBuffer(BufferAccess access)
        {
            // TODO: Add a check constrain if the object this is called on is the currently bound object.
            var bindContext = new BindContext(() => this._gl.glUnmapBuffer(this.Target) );

            // Note: glMapBuffer seem to have a relatively stupid implementation.
            var ptr = this._gl.glMapBuffer(this.Target, access);
            var accessor = new UnmanagedMemoryAccessor(
                new SafeMapBuffer(ptr),
                0,
                this.Elements * this.ElementSize,
                access == BufferAccess.ReadOnly
                    ? FileAccess.Read
                    : access == BufferAccess.WriteOnly 
                        ? FileAccess.Write 
                        : FileAccess.ReadWrite);

            return new MappedBuffer(this, bindContext, accessor);
        }

        public Type ElementType { get { return typeof(TElementType); } }

        public void BufferSubData(BufferUsage usage, int offset, int size)
        {
            ReleasedConstraint();
            var handle = GCHandle.Alloc(this.Data, GCHandleType.Pinned);
            try
            {
                this._gl.glBufferSubData(this.Target, new IntPtr(offset), new IntPtr(size), handle.AddrOfPinnedObject());
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
            get { ReleasedConstraint(); return this.Data[index]; }
            set { ReleasedConstraint(); this.Data[index] = value; }
        }
    }
}
