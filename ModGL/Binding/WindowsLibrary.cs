using System;
using System.Runtime.InteropServices;

namespace ModGL.Binding
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

        public string Name { get { return this._moduleName; } }

        [DllImport("kernel32")]
        private static extern IntPtr GetProcAddress(IntPtr module, [In]string procName);

        [DllImport("kernel32")]
        private static extern bool FreeLibrary(IntPtr module);

        private readonly Func<IntPtr, string, IntPtr> _getProcAddress;
        private readonly Func<IntPtr, bool> _freeLibrary; 

        public WindowsLibrary(Func<IntPtr, string, IntPtr> getProc, Func<IntPtr, bool> freeProc, IntPtr module, string moduleName)
        {
            _getProcAddress = getProc;
            _freeLibrary = freeProc;
            _module = module;
            _moduleName = moduleName;
        }

        public IntPtr GetProcedureAddress(string name)
        {
            if(this._isDisposed)
                throw new ObjectDisposedException(this._moduleName);
            return _getProcAddress(this._module, name);
        }

        public void Dispose()
        {
            _freeLibrary(this._module);
            this._isDisposed = true;
        }

        public override string ToString()
        {
            return this._moduleName;
        }

        public WindowsLibrary(IntPtr hModule, string moduleName)
        {
            _module = hModule;
            _moduleName = moduleName;
            _getProcAddress = GetProcAddress;
            _freeLibrary = FreeLibrary;
        }

        public Delegate GetProcedure(string procedureName, Type delegateType)
        {
            if(this._isDisposed)
                throw new ObjectDisposedException(this._moduleName);
            IntPtr proc = _getProcAddress(this._module, procedureName);
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
}