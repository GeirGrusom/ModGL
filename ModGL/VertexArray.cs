using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModGL
{
    public interface IVertexArray : IGLObject
    {
        
    }

    public class VertexArray<TElementType> : IVertexArray, IBindable
    {
        public uint Handle { get; private set; }

        public BindContext Bind()
        {
            throw new NotImplementedException();
        }
    }
}
