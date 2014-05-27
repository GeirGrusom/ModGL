using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Platform.Invoke;

namespace ModGL
{
    /// <summary>
    /// This class provides a extension mechanism where it will check for functions in all
    /// extension providers registered. The first one to give a non-null value will be returned.
    /// </summary>
    public class CompositeLibraryProvider : ILibrary
    {
        private readonly IList<ILibrary> _providers;

        public string Name { get { return string.Join(", ", _providers.Select(p => p.Name)); } }

        public void Dispose()
        {
            foreach(var provider in _providers)
                provider.Dispose();
        }

        public CompositeLibraryProvider(params ILibrary[] providers)
        {
            _providers = providers.ToList();
        }

        [Pure]
        public Delegate GetProcedure(Type delegateType, string procedureName)
        {
            return _providers.Select(p => p.GetProcedure(delegateType, procedureName)).FirstOrDefault(p => p != null);
        }


        [Pure]
        public TDelegate GetProcedure<TDelegate>(string procedureName)
            where TDelegate : class
        {
            return _providers.Select(p => p.GetProcedure<TDelegate>(procedureName)).FirstOrDefault(p => p != null);
        }
    }
}