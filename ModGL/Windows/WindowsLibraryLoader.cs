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

        public WindowsLibraryLoader(Func<string, IntPtr> loadFunc)
        {
            this._loadFunc = loadFunc;
        }

        public WindowsLibraryLoader()
        {
            this._loadFunc = LoadLibrary;
        }

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