using ModGL.NativeGL;

namespace ModGL.ObjectModel.Shaders
{
    public class GeometryShader : Shader
    {
        public GeometryShader(IOpenGL32 gl, ShaderType shaderType, string code) 
            : base(gl, shaderType, code)
        {
        }
    }
}