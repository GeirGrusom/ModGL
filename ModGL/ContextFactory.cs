using System;
using System.Diagnostics.Contracts;

using ModGL.Binding;
using ModGL.NativeGL;
using ModGL.Unix;
using ModGL.Windows;

namespace ModGL
{
    public interface IContextFactory
    {
        IContext Create(ContextCreationParameters parameters);
    }

    /// <summary>
    /// This class is used to create a context for the current platform.
    /// </summary>
    public class ContextFactory : IContextFactory
    {
        /// <summary>
        /// Creates a context based on the current platform.
        /// </summary>
        /// <param name="parameters">Parameters to create context from.</param>
        /// <returns>An OpenGL context defined by <see cref="parameters"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if parameters is null.</exception>
        /// <exception cref="PlatformNotSupportedException">Thrown if the current platform is not supported.</exception>
        [Pure]
        public IContext Create(ContextCreationParameters parameters)
        {
            if(parameters == null)
                throw new ArgumentNullException("parameters");
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

    /// <summary>
    /// This class is used to create a library loader for the current platform.
    /// </summary>
    public class LibraryLoaderFactory : ILibraryLoaderFactory
    {
        /// <summary>
        /// Creates a library loader for the current platform.
        /// </summary>
        /// <returns>Library loader for the current platform.</returns>
        /// <exception cref="PlatformNotSupportedException">Thrown if the current platform is not supported.</exception>
        [Pure]
        public ILibraryLoader Create()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                return new WindowsLibraryLoader();

            if(Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
                return new UnixLibraryLoader();

            throw new PlatformNotSupportedException();
        }
    }

    /// <summary>
    /// This class is used to create a interface implementation for the current platform.
    /// </summary>
    public class InterfaceFactory
    {

        /// <summary>
        /// Builds a interface and a context for the current platoform if supported.
        /// </summary>
        /// <typeparam name="TInterface">Interface type to implement.</typeparam>
        /// <param name="parameters">Context creation parameters.</param>
        /// <param name="throwOnError">Set to true if failed OpenGL calls should throw exceptions.</param>
        /// <param name="context">The created context.</param>
        /// <returns>Implementation of the specified interface for the context.</returns>
        /// <exception cref="PlatformNotSupportedException">Thrown if the platform is not supported by the implementation.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <see cref="parameters"/> is null.</exception>
        [Pure]
        public TInterface CreateInterface<TInterface>(ContextCreationParameters parameters, bool throwOnError, out IContext context)
            where TInterface : IOpenGL // Must be at least an OpenGL 1.1 interface.
        {
            return CreateInterface<TInterface>(
                parameters, new ContextFactory(), new LibraryLoaderFactory(), throwOnError, out context);
        }

        /// <summary>
        /// Builds a interface and a context for the current platoform if supported.
        /// </summary>
        /// <typeparam name="TInterface">Interface type to implement.</typeparam>
        /// <param name="parameters">Context creation parameters.</param>
        /// <param name="context">The created context.</param>
        /// <returns>Implementation of the specified interface for the context.</returns>
        /// <exception cref="PlatformNotSupportedException">Thrown if the platform is not supported by the implementation.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <see cref="parameters"/> is null.</exception>
        [Pure]
        public TInterface CreateInterface<TInterface>(ContextCreationParameters parameters, out IContext context)
            where TInterface : IOpenGL // Must be at least an OpenGL 1.1 interface.
        {
            return CreateInterface<TInterface>(
                parameters, new ContextFactory(), new LibraryLoaderFactory(), false, out context);
        }

        /// <summary>
        /// Builds a interface and a context for the current platoform if supported.
        /// </summary>
        /// <typeparam name="TInterface">Interface type to implement.</typeparam>
        /// <param name="parameters">Context creation parameters.</param>
        /// <param name="contextFactory">Context factory. Normally a new instance of <see cref="ContextFactory"/>.</param>
        /// <param name="libraryLoaderFactory">Library loader factory. Nomrally a new instance of <see cref="LibraryLoaderFactory"/>.</param>
        /// <param name="throwOnError">Set to true if failed OpenGL calls should throw exceptions.</param>
        /// <param name="context">The created context.</param>
        /// <returns>Implementation of the specified interface for the context.</returns>
        /// <exception cref="PlatformNotSupportedException">Thrown if the platform is not supported by the implementation.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <see cref="parameters"/>, <see cref="contextFactory"/> or <see cref="libraryLoaderFactory"/> is null.</exception>
        [Pure]
        public TInterface CreateInterface<TInterface>(ContextCreationParameters parameters, IContextFactory contextFactory, ILibraryLoaderFactory libraryLoaderFactory,  bool throwOnError, out IContext context)
            where TInterface : IOpenGL // Must be at least an OpenGL 1.1 interface.
        {
            if(contextFactory == null)
                throw new ArgumentNullException("contextFactory");
            if(libraryLoaderFactory == null)
                throw new ArgumentNullException("libraryLoaderFactory");
            if(parameters == null)
                throw new ArgumentNullException("parameters");

            context = contextFactory.Create(parameters);
            var lib = libraryLoaderFactory.Create();

            ILibrary glLib;
            
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                glLib = lib.Load("OpenGL32");
            else
                glLib = lib.Load("libgl");

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
        /// <summary>
        /// Builds a interface and a context for the current platoform if supported.
        /// </summary>
        /// <typeparam name="TInterface">Interface type to implement.</typeparam>
        /// <param name="parameters">Context creation parameters.</param>
        /// <param name="contextFactory">Context factory. Normally a new instance of <see cref="ContextFactory"/>.</param>
        /// <param name="libraryLoaderFactory">Library loader factory. Nomrally a new instance of <see cref="LibraryLoaderFactory"/>.</param>
        /// <param name="context">The created context.</param>
        /// <returns>Implementation of the specified interface for the context.</returns>
        /// <exception cref="PlatformNotSupportedException">Thrown if the platform is not supported by the implementation.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <see cref="parameters"/>, <see cref="contextFactory"/> or <see cref="libraryLoaderFactory"/> is null.</exception>
        [Pure]
        public TInterface CreateInterface<TInterface>(ContextCreationParameters parameters, IContextFactory contextFactory, ILibraryLoaderFactory libraryLoaderFactory, out IContext context)
            where TInterface : IOpenGL // Must be at least an OpenGL 1.1 interface.
        {
            return CreateInterface<TInterface>(parameters, contextFactory, libraryLoaderFactory, false, out context);
        }
    }
}
