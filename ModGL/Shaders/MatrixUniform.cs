using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ModGL.NativeGL;
using ModGL.Numerics;

namespace ModGL.Shaders
{
    public class MatrixUniform : Uniform<Matrix4f>
    {
        public MatrixUniform(IOpenGL30 gl, string name, int location) : base(gl, name, location)
        {
        }

        public override void SetData(IOpenGL30 gl)
        {
            var fix = GCHandle.Alloc(Value._data, GCHandleType.Pinned);
            gl.UniformMatrix4fv(Location, 1, transpose: GLboolean.False, value: fix.AddrOfPinnedObject());
            fix.Free();
        }
    }
}
