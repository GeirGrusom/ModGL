using ModGL.NativeGL;

namespace ModGL.Shaders
{
    public class FloatUniform : Uniform<float>
    {
        public FloatUniform(IOpenGL30 gl, string name, int location)
            : base(gl, name, location)
        {
        }

        public override void SetData(IOpenGL30 gl)
        {
            gl.glUniform1f(this.Location, this.Value);
        }
    }
}