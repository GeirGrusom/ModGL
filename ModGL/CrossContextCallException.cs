using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModGL
{
    [Serializable]
    public class CrossContextCallException : Exception
    {
        public CrossContextCallException()
            : base("A call was made to an interface produced by a context that is not the current context or belongs to another thread.")
        {
        }
    }
}
