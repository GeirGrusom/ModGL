using ModGL.NativeGL;

namespace ModGL.Shaders
{
    public sealed class TesselationEvaulationShader : Shader
    {
        public TesselationEvaulationShader(IOpenGL40 gl, string code)
            : base(gl, ShaderType.TesselationEvaluationShader, code)
        {
        }
    }
}