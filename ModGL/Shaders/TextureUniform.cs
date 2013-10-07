using ModGL.NativeGL;
using ModGL.Textures;

namespace ModGL.Shaders
{
    public class TextureUniform : Uniform<Texture>
    {
        public ActiveTexture Slot { get; set; }

        public TextureUniform(IOpenGL30 gl, string name, int location) 
            : base(gl, name, location)
        {
        }

        public override void SetData(IOpenGL30 gl)
        {
            gl.ActiveTexture(Slot);
            Value.Bind();
            gl.Uniform1i(Location, (int)Slot);
        }
    }
}