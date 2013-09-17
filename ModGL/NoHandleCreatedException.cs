using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModGL
{
    /// <summary>
    /// Thrown if a system was unable to create a required handle.
    /// </summary>
    [Serializable]
    public class NoHandleCreatedException : Exception
    {
        public NoHandleCreatedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NoHandleCreatedException(string message)
            : base(message)
        {
        }

        public NoHandleCreatedException(Exception innerException)
            : base("Unable to create handle.", innerException)
        {
        }

        public NoHandleCreatedException()
            : base("Unable to create handle.")
        {
        }
    }
}
