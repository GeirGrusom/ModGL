using ModGL.NativeGL;

namespace ModGL.ObjectModel.Shaders
{
    public class IntUniform : Uniform<int>
    {
        public IntUniform(IOpenGL30 gl, string name, int location) : base(gl, name, location)
        {
        }

        public override void SetData(IOpenGL30 gl)
        {
            gl.Uniform1i(this.Location, this.Value);
        }
    }
}