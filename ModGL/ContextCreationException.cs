using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModGL
{
    [Serializable]
    public class ContextCreationException : Exception
    {
        public ContextCreationException(string message, ContextCreationParameters parameters, Exception innerException)
            : base(message, innerException)
        {
            CreationParameters = parameters;
        }

        public ContextCreationException(string message, ContextCreationParameters parameters)
            : base(message)
        {
            CreationParameters = parameters;
        }
        
        public ContextCreationParameters CreationParameters { get; private set; }
    }
}
