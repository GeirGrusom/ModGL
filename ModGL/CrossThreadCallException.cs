using System;

namespace ModGL
{
    [Serializable]
    public class CrossThreadCallException : Exception
    {
        public CrossThreadCallException()
            : base("A call was made on a context from another thread.")
        {
        }
    }
}