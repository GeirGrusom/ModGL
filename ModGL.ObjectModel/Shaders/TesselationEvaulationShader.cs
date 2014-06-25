using ModGL.NativeGL;

namespace ModGL.ObjectModel.Shaders
{
    public sealed class TesselationEvaulationShader : Shader
    {
        public TesselationEvaulationShader(IOpenGL40 gl, string code)
            : base(gl, ShaderType.TesselationEvaluationShader, code)
        {
        }
    }
}