using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ModGL
{
    /// <summary>
    /// This class provides a extension mechanism where it will check for functions in all
    /// extension providers registered. The first one to give a non-null value will be returned.
    /// </summary>
    public class CompositeExtensionProvider : IExtensionSupport
    {
        private readonly IList<IExtensionSupport> _providers;

        public CompositeExtensionProvider(params IExtensionSupport[] providers)
        {
            _providers = providers.ToList();
        }

        [Pure]
        public Delegate GetProcedure(string procedureName, Type delegateType)
        {
            return _providers.Select(p => p.GetProcedure(procedureName, delegateType)).FirstOrDefault(p => p != null);
        }


        [Pure]
        public TDelegate GetProcedure<TDelegate>(string procedureName)
            where TDelegate : class
        {
            return _providers.Select(p => p.GetProcedure<TDelegate>(procedureName)).FirstOrDefault(p => p != null);
        }

        [Pure]
        public TDelegate GetProcedure<TDelegate>()
            where TDelegate : class
        {
            return _providers.Select(p => p.GetProcedure<TDelegate>()).FirstOrDefault(p => p != null);
        }
    }
}