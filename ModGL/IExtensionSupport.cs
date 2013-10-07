using System;
using System.Collections.Generic;
using System.Linq;

namespace ModGL
{
    public interface IExtensionSupport
    {
        Delegate GetProcedure(string procedureName, Type delegateType);
        TDelegate GetProcedure<TDelegate>(string procedureName)
            where TDelegate : class;
        
        TDelegate GetProcedure<TDelegate>()
            where TDelegate : class;
    }
    public class CompundExtensionProvider : IExtensionSupport
    {
        private readonly IList<IExtensionSupport> _providers;

        public CompundExtensionProvider(params IExtensionSupport[] providers)
        {
            _providers = providers.ToList();
        }


        public Delegate GetProcedure(string procedureName, Type delegateType)
        {
            return _providers.Select(p => p.GetProcedure(procedureName, delegateType)).FirstOrDefault(p => p != null);
        }


        public TDelegate GetProcedure<TDelegate>(string procedureName)
            where TDelegate : class
        {
            return _providers.Select(p => p.GetProcedure<TDelegate>(procedureName)).FirstOrDefault(p => p != null);
        }


        public TDelegate GetProcedure<TDelegate>()
            where TDelegate : class
        {
            return _providers.Select(p => p.GetProcedure<TDelegate>()).FirstOrDefault(p => p != null);
        }
    }
}