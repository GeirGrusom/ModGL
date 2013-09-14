using System;
using System.Collections.Generic;

using ModGL.Binding;
using ModGL.NativeGL;

namespace ModGL
{
    public interface IContext : IDisposable
    {
        BindContext MakeCurrent();
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

    public abstract class Context : IContext, IGLObject, IExtensionSupport
    {
        public uint Handle { get; protected internal set; }

        private static IContext _currentContext;

        public abstract BindContext MakeCurrent();

        public abstract void SwapBuffers();

        public abstract void Dispose();

        public TOpenGLInterface GetOpenGL<TOpenGLInterface>(bool debug = false)
            where TOpenGLInterface : class
        {
            var bindingFactory = new InterfaceBindingFactory();
            return bindingFactory
                .CreateBinding<TOpenGLInterface>
                (
                    context: this,
                    interfaceMap: new Dictionary<Type, Type> { {typeof(IOpenGL), typeof(GL)} }, 
                    errorHandling: debug ? GL.OpenGLErrorFunctions : null
                );
        }

        public TDelegate GetProcedure<TDelegate>(string procedureName)
        {
            return (TDelegate)Convert.ChangeType(GetProcedure(procedureName, typeof(TDelegate)), typeof(TDelegate));
        }


        public TDelegate GetProcedure<TDelegate>()
        {
            return (TDelegate)Convert.ChangeType(GetProcedure(typeof(TDelegate).Name, typeof (TDelegate)), typeof(TDelegate));
        }

        public abstract Delegate GetProcedure(string name, Type delegateType);

        public static IContext Current
        {
            get
            {
                return _currentContext;
            } 
            set
            {
                _currentContext = value; 
                if(value != null)
                    _currentContext.MakeCurrent();
            }
        }
    }
}
