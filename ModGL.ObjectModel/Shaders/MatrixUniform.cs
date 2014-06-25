using System.Runtime.InteropServices;
using ModGL.NativeGL;
using ModGL.Numerics;

namespace ModGL.ObjectModel.Shaders
{
    public class MatrixUniform : Uniform<Matrix4f>
    {
        public MatrixUniform(IOpenGL30 gl, string name, int location) : base(gl, name, location)
        {
        }

        public override void SetData(IOpenGL30 gl)
        {
            var fix = GCHandle.Alloc(Value._data, GCHandleType.Pinned);
            gl.UniformMatrix4fv(Location, 1, transpose: (byte) GLboolean.False, value: fix.AddrOfPinnedObject());
            fix.Free();
        }
    }
}
