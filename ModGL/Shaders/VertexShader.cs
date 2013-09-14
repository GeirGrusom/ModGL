using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL.NativeGL;

namespace ModGL.Shaders
{
    public class VertexShader : Shader
    {
        public VertexShader(IOpenGL30 gl, string code) 
            : base(gl, ShaderType.VertexShader, code)
        {
        }
    }
}
