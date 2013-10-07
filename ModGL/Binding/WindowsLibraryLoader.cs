using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ModGL.Binding
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
            _loadFunc = loadFunc;
        }

        public WindowsLibraryLoader()
        {
            _loadFunc = LoadLibrary;
        }

        public ILibrary Load(string moduleName)
        {
            string filename;
            if (!moduleName.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase))
                filename = moduleName + ".dll";
            else
                filename = moduleName;

            var hModule = _loadFunc(filename);

            if(hModule == IntPtr.Zero)
                throw new FileNotFoundException(string.Format("Unable to locate library '{0}'.", filename));

            return new WindowsLibrary(hModule, moduleName);
        }
    }
}