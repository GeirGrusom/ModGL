using ModGL.NativeGL;

namespace ModGL.ObjectModel.Shaders
{
    public class FragmentShader : Shader
    {
        public FragmentShader(IOpenGL30 gl, string code)
            : base(gl, ShaderType.FragmentShader,  code)
        {
        }
    }
}