using System;

using ModGL.Binding;

namespace ModGL.Unix
{
    public class UnixLibrary : ILibrary
    {
        public Delegate GetProcedure(string procedureName, Type delegateType)
        {
            throw new NotImplementedException();
        }

        public TDelegate GetProcedure<TDelegate>(string procedureName) where TDelegate : class
        {
            throw new NotImplementedException();
        }

        public TDelegate GetProcedure<TDelegate>() where TDelegate : class
        {
            throw new NotImplementedException();
        }

        private readonly IntPtr _so;
        public string Name { get; private set; }

        public UnixLibrary(IntPtr sharedObject, string name)
        {
            _so = sharedObject;
            Name = name;
        }
    }
}