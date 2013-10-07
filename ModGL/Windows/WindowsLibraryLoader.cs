using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.InteropServices;

using ModGL.Binding;

namespace ModGL.Windows
{
    /// <summary>
    /// Loads windows Dynamic Link Libraries. This is used by the extension mechanism.
    /// </summary>
    public class WindowsLibraryLoader : ILibraryLoader
    {
        [DllImport("kernel32")]
        private static extern IntPtr LoadLibrary([In]string libraryName);

        private readonly Func<string, IntPtr> _loadFunc;

        /// <summary>
        /// Overload used to override LoadLibrary. Intended for internal use.
        /// </summary>
        /// <param name="loadFunc"></param>
        public WindowsLibraryLoader(Func<string, IntPtr> loadFunc)
        {
            _loadFunc = loadFunc;
        }

        public WindowsLibraryLoader()
        {
            _loadFunc = LoadLibrary;
        }

        /// <summary>
        /// Loads a dynamic link library.
        /// </summary>
        /// <param name="moduleName">Name of the module to load. Note that the .dll extension can be omitted.</param>
        /// <returns>Library function loader.</returns>
        /// <remarks>Note that this functions uses the default library search paths used by the operating system. Supplying absolute or relative paths here will most likely not work.</remarks>
        [Pure]
        public ILibrary Load(string moduleName)
        {
            string filename;
            if (!moduleName.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase))
                filename = moduleName + ".dll";
            else
                filename = moduleName;

            var hModule = this._loadFunc(filename);

            if(hModule == IntPtr.Zero)
                throw new FileNotFoundException(string.Format("Unable to locate library '{0}'.", filename));

            return new WindowsLibrary(hModule, moduleName);
        }
    }
}