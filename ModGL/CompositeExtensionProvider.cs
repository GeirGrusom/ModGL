using System;
using System.Collections.Generic;
using System.Linq;

namespace ModGL
{
    public class CompositeExtensionProvider : IExtensionSupport
    {
        private readonly IList<IExtensionSupport> _providers;

        public CompositeExtensionProvider(params IExtensionSupport[] providers)
        {
            this._providers = providers.ToList();
        }


        public Delegate GetProcedure(string procedureName, Type delegateType)
        {
            return this._providers.Select(p => p.GetProcedure(procedureName, delegateType)).FirstOrDefault(p => p != null);
        }


        public TDelegate GetProcedure<TDelegate>(string procedureName)
            where TDelegate : class
        {
            return this._providers.Select(p => p.GetProcedure<TDelegate>(procedureName)).FirstOrDefault(p => p != null);
        }


        public TDelegate GetProcedure<TDelegate>()
            where TDelegate : class
        {
            return this._providers.Select(p => p.GetProcedure<TDelegate>()).FirstOrDefault(p => p != null);
        }
    }
}