using System;

namespace ModGL
{
    public interface IBindable
    {
        BindContext Bind();
    }

    public struct BindContext : IDisposable
    {
        private readonly Action _disposeAction;
        internal BindContext(Action dispose)
        {
            _disposeAction = dispose;
        }

        public void Dispose()
        {
            _disposeAction();
        }
    }
}
