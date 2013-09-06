using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModGL
{
    public static class ObjectManagerExtensions
    {
        public static async Task<uint> CreateVertexBufferAsync()
        {
            
        }
    }
    public interface IObjectManager
    {
        uint CreateVertexBuffer();

        uint CreateTexture();

        uint CreateIndexBuffer();

        uint CreateVertexArray();
    }
}
