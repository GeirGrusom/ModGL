using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModGL
{
    public interface IObjectManager
    {
        uint CreateVertexBuffer();

        uint CreateTexture();

        uint CreateIndexBuffer();

        uint CreateVertexArray();
    }
}
