﻿using System;
using System.Diagnostics.Contracts;

using ModGL.NativeGL;
using ModGL.Windows;
using Platform.Invoke;
using Platform.Invoke.Windows;

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
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                var loader = new WindowsLibraryLoader();
                var libGL = loader.Load("OpenGL32");
                var gdi32 = loader.Load("GDI32");

                var wgl = LibraryInterfaceFactory.Implement<IWGL>(new CompositeLibraryProvider(libGL, gdi32));

                var context = new WindowsContext(wgl, null, parameters);
                return context;
            }
            throw new PlatformNotSupportedException();
        }
    }
}
