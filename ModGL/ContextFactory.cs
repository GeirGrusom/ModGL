using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using ModGL.Windows;
using Platform.Invoke;

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
        public ILibraryLoader LibraryLoader { get; private set; }
        public ILibraryInterfaceMapper Mapper { get; private set; }
        private readonly PlatformID os;

        public ContextFactory(ILibraryLoader libraryLoader, ILibraryInterfaceMapper mapper, PlatformID os)
        {
            LibraryLoader = libraryLoader;
            Mapper = mapper;
            this.os = os;
        }

        public ContextFactory()
        {
            LibraryLoader = LibraryLoaderFactory.Create();
            Mapper = new LibraryInterfaceMapper(new DelegateTypeBuilder(), new DefaultConstructorBuilder(s => s), new DefaultMethodCallWrapper());
            os = Environment.OSVersion.Platform;
        }

        public static IContextFactory Instance { get { return new ContextFactory(); } }

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
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (os == PlatformID.Win32NT)
            {
                var loader = LibraryLoader;
                var libGL = loader.Load("OpenGL32");
                var gdi32 = loader.Load("GDI32");

                var wgl = Mapper.Implement<IWGL>(new CompositeLibraryProvider(libGL, gdi32));

                var context = new WindowsContext(wgl, null, parameters);
                context.Initialize();
                return context;
            }
            throw new PlatformNotSupportedException();
        }
    }
}
