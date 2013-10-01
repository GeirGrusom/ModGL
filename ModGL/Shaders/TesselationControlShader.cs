using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL.NativeGL;

namespace ModGL.Shaders
{
    public sealed class TesselationControlShader : Shader
    {
        public TesselationControlShader(IOpenGL42 gl, string code)
            : base(gl, ShaderType.TesselationControlShader, code)
        {
            
        }
    }

    public sealed class TesselationEvaulationShader : Shader
    {
        public TesselationEvaulationShader(IOpenGL42 gl, string code)
            : base(gl, ShaderType.TesselationEvaluationShader, code)
        {
        }
    }
}
