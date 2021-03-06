﻿using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Threading;
using ModGL.NativeGL;
using Platform.Invoke;
using InvalidOperationException = ModGL.NativeGL.InvalidOperationException;
using OutOfMemoryException = ModGL.NativeGL.OutOfMemoryException;

namespace ModGL
{
    [Flags]
    public enum InterfaceFlags
    {
        Debug = 0,
        Release = 1,
    }
    public interface IContext : ILibrary
    {
        IntPtr Handle { get; }
        BindContext Bind();
        void Initialize();
        void SwapBuffers();
        TOpenGLInterface CreateInterface<TOpenGLInterface>(InterfaceFlags flags)
            where TOpenGLInterface : class;
    }

    public enum OpenGLVersion
    {
        OpenGL30 = 0x300,
        OpenGL31 = 0x310,
        OpenGL32 = 0x320,
        OpenGL40 = 0x400,
        OpenGL41 = 0x410,
        OpenGL42 = 0x420,
        DontCare = 0xffff,
    }

    public interface IOpenGLGetError
    {
        [Platform.Invoke.Attributes.SkipProbe]
        ErrorCode GetError();
    }

    public abstract class Context : IContext
    {
        public IntPtr Handle { get; protected set; }

        [ThreadStatic]
        protected static IContext CurrentContext;

        public abstract BindContext Bind();

        public abstract void SwapBuffers();

        public abstract void Dispose();

        public abstract void Initialize();

        private readonly Lazy<IOpenGLGetError> _error;

        protected Context()
        {
            _error = new Lazy<IOpenGLGetError>(() => LibraryInterfaceFactory.Implement<IOpenGLGetError>(this, f => "gl" + f), LazyThreadSafetyMode.None);
        }

        [Pure]
        public TOpenGLInterface CreateInterface<TOpenGLInterface>(InterfaceFlags flags)
            where TOpenGLInterface : class
        {
            if ((flags & InterfaceFlags.Debug) == InterfaceFlags.Debug)
                return CreateDebugInterface<TOpenGLInterface>();
            return LibraryInterfaceFactory.Implement<TOpenGLInterface>(this, f => "gl" + f);
        }

        public class DebugProbe<TOpenGLInterface> : IMethodCallProbe<TOpenGLInterface>
            where TOpenGLInterface : class
        {
            private readonly IOpenGLGetError _error;
            private readonly IContext _owner;
            private readonly Thread _ownerThread;

            public DebugProbe(IOpenGLGetError error, IContext owner)
            {
                _error = error;
                _owner = owner;
                _ownerThread = Thread.CurrentThread;
            }

            public void OnBeginInvoke(MethodInfo method, TOpenGLInterface reference)
            {
                if (Thread.CurrentThread != _ownerThread) 
                    // A call to this implementation has been made by a thread that does not own it.
                    throw new CrossThreadCallException();
                if(Current != _owner) 
                    // A call has been made to this implementation, but its context is not current.
                    throw new CrossContextCallException();
                _error.GetError(); // Clear error state
            }

            public void OnEndInvoke(MethodInfo method, TOpenGLInterface reference)
            {
                var errorCode = _error.GetError();
                switch (errorCode)
                {
                    case ErrorCode.InvalidEnum:
                        throw new InvalidEnumException();
                    case ErrorCode.InvalidOperation:
                        throw new InvalidOperationException();
                    case ErrorCode.InvalidValue:
                        throw new InvalidValueException();
                    case ErrorCode.OutOfMemory:
                        throw new OutOfMemoryException();
                    case ErrorCode.InvalidFramebufferOperation:
                        throw new InvalidFramebufferOperationException();
                }
            }
        }

        private TOpenGLInterface CreateDebugInterface<TOpenGLInterface>()
            where TOpenGLInterface : class
        {
            var delegateTypeBuilder = new DelegateTypeBuilder();
            var constructorBuilder = new ProbingConstructorBuilder(f => "gl" + f);
            var methodBuilder = new ProbingMethodCallWrapper(() => constructorBuilder.ProbeField);
            var interfaceFactory = new LibraryInterfaceMapper(delegateTypeBuilder, constructorBuilder,
                methodBuilder);
            var probe = new DebugProbe<TOpenGLInterface>(_error.Value, this);
            return interfaceFactory.Implement<TOpenGLInterface>(this, probe);
        }

        public abstract Delegate GetProcedure(Type delegateType, string name);

        [Pure]
        public TDelegate GetProcedure<TDelegate>(string procedureName)
            where TDelegate : class
        {
            return GetProcedure(typeof(TDelegate), procedureName) as TDelegate;
        }

        public string Name { get { return "OpenGL"; } }

        [Pure]
        public TDelegate GetProcedure<TDelegate>()
            where TDelegate : class
        {
            return (TDelegate)Convert.ChangeType(GetProcedure(typeof(TDelegate), typeof(TDelegate).Name), typeof(TDelegate));
        }

        public static IContext Current
        {
            get
            {
                return CurrentContext;
            } 
        }
    }
}
