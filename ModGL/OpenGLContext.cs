﻿using System;

namespace ModGL
{
    public interface IContext
    {
        void MakeCurrent();
    }

    public interface IExtensionSupport
    {
        TDelegate GetExtension<TDelegate>(string extensionName);
        TDelegate GetExtension<TDelegate>();
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

        public abstract TDelegate GetExtension<TDelegate>(string extensionName);

        public NativeGL.IOpenGL30 GetOpenGL(OpenGLVersion desiredVersion)
        {
            throw new NotImplementedException();
        }

        public TDelegate GetExtension<TDelegate>()
        {
            return GetExtension<TDelegate>(typeof (TDelegate).Name);
        }

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
