using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
            Buffer = buffer;
            _bufferBindingContext = context;
            Accessor = accessor;
        }

        public void Dispose()
        {
            Accessor.Dispose();
            _bufferBindingContext.Dispose();
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
        internal readonly long Length;
        private readonly int _elementSize;
        private readonly IOpenGL30 _gl;

        [Pure]
        public bool Released { get; private set; }

        [Pure]
        public BufferTarget Target { get; private set; }

        [Pure]
        public long Elements { get { return Length; } }

        [Pure]
        public int ElementSize { get { return _elementSize; } }

        [Pure]
        public uint Handle { get; private set; }

        public BindContext Bind()
        {
            _gl.BindBuffer(Target, Handle);
            return new BindContext(() => _gl.BindBuffer(Target, 0));
        }

        public BindContext Bind(uint index)
        {
            _gl.BindBufferBase(Target, index, Handle);
            return new BindContext(() => _gl.BindBufferBase(Target, index, 0));
        }

        public BindContext Bind(uint index, long startIndex, long elements)
        {
            _gl.BindBufferRange(Target, index, Handle, new IntPtr(startIndex * _elementSize), new IntPtr(elements * _elementSize));
            return new BindContext(() => _gl.BindBufferBase(Target, index, 0));
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
            if (gl == null)
                throw new ArgumentNullException("gl");
            var names = new uint[1];
            gl.GenBuffers(1, names);
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
            Data = elements.ToArray();
            Length = Data.LongLength;
        }

        protected Buffer(BufferTarget target, long size, IOpenGL30 gl)
            : this(target, gl)
        {
            Data = new TElementType[size];
            Length = size;
        }

        public void BufferData(BufferUsage usage)
        {
            ReleasedConstraint();
            var handle = GCHandle.Alloc(Data, GCHandleType.Pinned);
            try
            {
                _gl.BufferData(Target, new IntPtr(Data.LongLength * ElementSize), handle.AddrOfPinnedObject(), usage);
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
            var bindContext = new BindContext(() => _gl.UnmapBuffer(Target) );

            // Note: glMapBuffer seem to have a relatively stupid implementation.
            var ptr = _gl.MapBuffer(Target, access);
            var accessor = new UnmanagedMemoryAccessor(
                new SafeMapBuffer(ptr),
                0,
                Elements * ElementSize,
                access == BufferAccess.ReadOnly
                    ? FileAccess.Read
                    : access == BufferAccess.WriteOnly 
                        ? FileAccess.Write 
                        : FileAccess.ReadWrite);

            return new MappedBuffer(this, bindContext, accessor);
        }

        [Pure]
        public Type ElementType { get { return typeof(TElementType); } }

        public void BufferSubData(BufferUsage usage, int offset, int size)
        {
            ReleasedConstraint();
            var handle = GCHandle.Alloc(Data, GCHandleType.Pinned);
            try
            {
                _gl.BufferSubData(Target, new IntPtr(offset), new IntPtr(size), handle.AddrOfPinnedObject());
            }
            finally
            {
                handle.Free();
            }
        }

        public void BufferSubData<TElement>(BufferUsage usage, Expression<Func<TElementType, TElement>> elementProc)
        {
            ReleasedConstraint();
            var member = (FieldInfo)((MemberExpression) elementProc.Body).Member;
            int fieldSize;// = System.Runtime.InteropServices.Marshal.SizeOf(member.FieldType);

            var fieldOffset = member.GetCustomAttribute<FieldOffsetAttribute>();
            int offset = fieldOffset.Value;
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                BindingFlags.ExactBinding;
                
            var preFields = member.DeclaringType.GetFields(bindingFlags).OrderBy(m => m.MetadataToken).ToList();

            var index = preFields.IndexOf(member);

            if (index == preFields.Count - 1)
            {
                fieldSize = Marshal.SizeOf(member.DeclaringType) - offset;
            }
            else
            {
                var next = preFields[index + 1];
                var nextOffset = next.GetCustomAttribute<FieldOffsetAttribute>().Value;
                fieldSize = nextOffset - offset;
            }

            var handle = GCHandle.Alloc(Data, GCHandleType.Pinned);
            try
            {
                _gl.BufferSubData(Target, new IntPtr(offset), new IntPtr(fieldSize), handle.AddrOfPinnedObject());
            }
            finally
            {
                handle.Free();
            }
        }

        public TElementType this[long index]
        {
            get { ReleasedConstraint(); return Data[index]; }
            set { ReleasedConstraint(); Data[index] = value; }
        }
    }
}
