using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL.Binding;
using ModGL.NativeGL;
using ModGL.Windows;

namespace ModGL
{
    public interface IContextFactory
    {
        IContext Create(ContextCreationParameters parameters);
    }

    public class ContextFactory : IContextFactory
    {
        public IContext Create(ContextCreationParameters parameters)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                var loader = new WindowsLibraryLoader();
                var libGL = loader.Load("OpenGL32");
                var gdi32 = loader.Load("GDI32");

                var interfaceFactory = new InterfaceBindingFactory();
                var wgl = interfaceFactory.CreateBinding<IWGL>(new CompositeExtensionProvider(libGL, gdi32));
                
                var context = new WindowsContext(wgl, null, parameters);
                return context;
            }
            throw new PlatformNotSupportedException();
        }
    }

    public interface ILibraryLoaderFactory
    {
        ILibraryLoader Create();
    }

    public class LibraryLoaderFactory : ILibraryLoaderFactory
    {
        public ILibraryLoader Create()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                return new WindowsLibraryLoader();
            }
            throw new PlatformNotSupportedException();
        }
    }

    public class InterfaceFactory
    {
        public TInterface CreateInterface<TInterface>(ContextCreationParameters parameters, ContextFactory contextFactory, LibraryLoaderFactory libraryLoaderFactory,  bool throwOnError, out IContext context)
            where TInterface : IOpenGL // Must be at least an OpenGL 1.1 interface.
        {
            context = contextFactory.Create(parameters);
            var lib = libraryLoaderFactory.Create();

            ILibrary glLib;
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                glLib = lib.Load("OpenGL32");
            else 
                throw new PlatformNotSupportedException();

            var bindingFactory = new InterfaceBindingFactory();
            if (throwOnError)
            {
                var result = bindingFactory.CreateBinding<TInterface>(new CompositeExtensionProvider(context, glLib), GL.OpenGLErrorFunctions, "gl");
                GL.RegisterOpenGLInterface(result);
                return result;
            }
            return bindingFactory.CreateBinding<TInterface>(
                context, errorHandling : null, extensionMethodPrefix : "gl");
        }
    }
}
