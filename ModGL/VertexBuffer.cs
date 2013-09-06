using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModGL
{
    public interface IVertexBuffer : IGLObject
    {
        long Elements { get; }
        int ElementSize { get; }
    }

    public class VertexBuffer<TElementType> : IVertexBuffer, IDisposable
        where TElementType : struct
    {
        private readonly TElementType[] data;
        private readonly IObjectManager manager;

        public long Elements { get { return data.LongLength; } }
        public int ElementSize { get { throw new NotImplementedException(); } }
        public uint Handle { get; protected internal set; }
        

        public VertexBuffer(IEnumerable<TElementType> elements, IObjectManager manager)
        {
            this.manager = manager;
            Handle = await manager.CreateVertexBuffer();
            data = elements.ToArray();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public TElementType this[long index]
        {
            get { return data[index]; }
            set { data[index] = value; }
        }
    }
}
