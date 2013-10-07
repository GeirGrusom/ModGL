using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ModGL.Binding
{
    public interface ILibrary : IExtensionSupport
    {
        string Name { get; }
    }
    public interface ILibraryLoader
    {
        ILibrary Load(string moduleName);
    }

    public class WindowsLibrary : ILibrary, IDisposable
    {
        private readonly IntPtr _module;
        private readonly string _moduleName;
        private bool _isDisposed;

        public string Name { get { return _moduleName; } }

        [DllImport("kernel32")]
        private static extern IntPtr GetProcAddress(IntPtr module, [In]string procName);

        [DllImport("kernel32")]
        private static extern bool FreeLibrary(IntPtr module);

        public IntPtr GetProcedureAddress(string name)
        {
            if(_isDisposed)
                throw new ObjectDisposedException(_moduleName);
            return GetProcAddress(_module, name);
        }

        public void Dispose()
        {
            FreeLibrary(_module);
            _isDisposed = true;
        }

        public override string ToString()
        {
            return _moduleName;
        }

        internal WindowsLibrary(IntPtr hModule, string moduleName)
        {
            _module = hModule;
            _moduleName = moduleName;
        }

        public Delegate GetProcedure(string procedureName, Type delegateType)
        {
            if(_isDisposed)
                throw new ObjectDisposedException(_moduleName);
            IntPtr proc = GetProcAddress(_module, procedureName);
            if (proc == IntPtr.Zero)
                return null;
            return (Delegate)Convert.ChangeType(Marshal.GetDelegateForFunctionPointer(proc, delegateType), delegateType);
        }

        public TDelegate GetProcedure<TDelegate>(string procedureName)
            where TDelegate : class
        {
            return (TDelegate)Convert.ChangeType(GetProcedure(procedureName, typeof(TDelegate)), typeof(TDelegate));
        }

        public TDelegate GetProcedure<TDelegate>()
            where TDelegate : class
        {
            return GetProcedure<TDelegate>(typeof(TDelegate).Name);
        }
    }
    public class WindowsLibraryLoader : ILibraryLoader
    {
        [DllImport("kernel32")]
        private static extern IntPtr LoadLibrary([In]string libraryName);

        public ILibrary Load(string moduleName)
        {
            string filename;
            if (!moduleName.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase))
                filename = moduleName + ".dll";
            else
                filename = moduleName;

            var hModule = LoadLibrary(filename);

            if(hModule == IntPtr.Zero)
                throw new FileNotFoundException(string.Format("Unable to locate library '{0}'.", filename));

            return new WindowsLibrary(hModule, moduleName);
        }
    }
}
