using System;

namespace ModGL
{
    [Serializable]
    public class CrossContextCallException : Exception
    {
        public CrossContextCallException()
            : base("A call was made to an interface produced by a context that is not the current context.")
        {
        }
    }
}
