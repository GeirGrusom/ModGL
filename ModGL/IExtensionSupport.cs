using System;

namespace ModGL
{
    public interface IExtensionSupport
    {
        Delegate GetProcedure(string procedureName, Type delegateType);
        TDelegate GetProcedure<TDelegate>(string procedureName);
        TDelegate GetProcedure<TDelegate>();
    }
}