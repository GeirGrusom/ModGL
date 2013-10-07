using System;
using System.Diagnostics.Contracts;

using ModGL.Binding;
using ModGL.NativeGL;

namespace ModGL
{
    public interface IContext : IDisposable, IExtensionSupport
    {
        IntPtr Handle { get; }
        BindContext Bind();
        void SwapBuffers();
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

    public abstract class Context : IContext
    {
        public IntPtr Handle { get; protected set; }

        [ThreadStatic]
        private static IContext currentContext;

        public abstract BindContext Bind();

        public abstract void SwapBuffers();

        public abstract void Dispose();

        [Pure]
        public TOpenGLInterface GetOpenGL<TOpenGLInterface>(IInterfaceBindingFactory bindingFactory, bool debug = false)
            where TOpenGLInterface : class
        {
            return bindingFactory
                .CreateBinding<TOpenGLInterface>
                (
                    context: this,
                    errorHandling: debug ? GL.OpenGLErrorFunctions : null, extensionMethodPrefix: "gl"
                );
        }

        [Pure]
        public TDelegate GetProcedure<TDelegate>(string procedureName)
            where TDelegate : class
        {
            return (TDelegate)Convert.ChangeType(GetProcedure(procedureName, typeof(TDelegate)), typeof(TDelegate));
        }

        [Pure]
        public TDelegate GetProcedure<TDelegate>()
            where TDelegate : class
        {
            return (TDelegate)Convert.ChangeType(GetProcedure(typeof(TDelegate).Name, typeof (TDelegate)), typeof(TDelegate));
        }

        public abstract Delegate GetProcedure(string name, Type delegateType);

        public static IContext Current
        {
            get
            {
                return currentContext;
            } 
            set
            {
                currentContext = value; 
                if(value != null)
                    currentContext.Bind();
            }
        }
    }
}
