using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

using ModGL.Binding;

namespace ModGL.Windows
{
    /// <summary>
    /// This class represents a Windows Dynamic Link Library and is used to retrieve function pointers from it.
    /// For more information see Marshalling on MSDN.
    /// </summary>
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

        private readonly Func<IntPtr, string, IntPtr> _getProcAddress;
        private readonly Func<IntPtr, bool> _freeLibrary; 

        /// <summary>
        /// Overload used to override GetProcAddress and FreeLibrary. Intended for internal use.
        /// </summary>
        /// <param name="getProc">Override function for GetProcAddress.</param>
        /// <param name="freeProc">Override function for FreeLibrary.</param>
        /// <param name="module">Module for loaded library.</param>
        /// <param name="moduleName">Name of the module supplied by LibraryLoader.</param>
        public WindowsLibrary(Func<IntPtr, string, IntPtr> getProc, Func<IntPtr, bool> freeProc, IntPtr module, string moduleName)
        {
            _getProcAddress = getProc;
            _freeLibrary = freeProc;
            _module = module;
            _moduleName = moduleName;
        }

        /// <summary>
        /// Creates a new instance of WindowsLibrary for the module specified by hModule and modulename.
        /// </summary>
        /// <param name="hModule">Module handle supplied by WindowsLibraryLoader.</param>
        /// <param name="moduleName">Name of module.</param>
        public WindowsLibrary(IntPtr hModule, string moduleName)
        {
            _module = hModule;
            _moduleName = moduleName;
            _getProcAddress = GetProcAddress;
            _freeLibrary = FreeLibrary;
        }


        [Pure]
        public IntPtr GetProcedureAddress(string name)
        {
            if(_isDisposed)
                throw new ObjectDisposedException(_moduleName);
            return _getProcAddress(this._module, name);
        }

        public void Dispose()
        {
            _freeLibrary(this._module);
            _isDisposed = true;
        }

        [Pure]
        public Delegate GetProcedure(string procedureName, Type delegateType)
        {
            if(_isDisposed)
                throw new ObjectDisposedException(this._moduleName);
            IntPtr proc = _getProcAddress(this._module, procedureName);
            if (proc == IntPtr.Zero)
                return null;
            return (Delegate)Convert.ChangeType(Marshal.GetDelegateForFunctionPointer(proc, delegateType), delegateType);
        }

        [Pure]
        public TDelegate GetProcedure<TDelegate>(string procedureName)
            where TDelegate : class
        {
            return (TDelegate)Convert.ChangeType(GetProcedure(procedureName, typeof(TDelegate)), typeof(TDelegate));
        }

        [Pure]
        public TDelegate GetProcedure<TDelegate>()
            where TDelegate : class
        {
            return GetProcedure<TDelegate>(typeof(TDelegate).Name);
        }
    }
}