using ModGL.NativeGL;
using ModGL.Textures;

namespace ModGL.Shaders
{
    public class TextureUniform : Uniform<Texture>
    {
        public TextureUniform(IOpenGL30 gl, string name, int location) 
            : base(gl, name, location)
        {
        }

        public override void SetData(IOpenGL30 gl)
        {
            gl.Uniform1i(this.Location, (int)this.Value.Handle);
        }
    }
}