using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ModGL.NativeGL;

namespace ModGL.ObjectModel.Shaders
{
    public class Vector4fUniform : Uniform<Vector4f>
    {
        public Vector4fUniform(IOpenGL30 gl, string name, int location) : base(gl, name, location)
        {
        }

        public override void SetData(IOpenGL30 gl)
        {
            gl.Uniform4f(Location, Value.X, Value.Y, Value.Z, Value.W);
        }
    }
}
