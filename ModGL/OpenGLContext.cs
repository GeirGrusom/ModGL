using System;

namespace ModGL
{
    public interface IContext
    {
        void MakeCurrent();
    }

    public interface IExtensionSupport
    {
        Delegate GetProcedure(string procedureName, Type delegateType);
        TDelegate GetProcedure<TDelegate>(string procedureName);
        TDelegate GetProcedure<TDelegate>();
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

        public abstract void MakeCurrent();

        public NativeGL.IOpenGL30 GetOpenGL(OpenGLVersion desiredVersion)
        {
            throw new NotImplementedException();
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
