using System;

namespace ModGL
{
    /// <summary>
    /// A bindable object is one that binds to the OpenGL state machine. This interface enables such a binding to occur within a using() scope.
    /// </summary>
    /// <example>using(someBindable.Bind())
    /// {
    ///    DoSomething();
    /// }</example>
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
