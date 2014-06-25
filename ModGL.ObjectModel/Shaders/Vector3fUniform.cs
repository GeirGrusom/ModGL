using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ModGL.NativeGL;

namespace ModGL.ObjectModel.Shaders
{
    public class Vector3fUniform : Uniform<Vector3f>
    {
        public Vector3fUniform(IOpenGL30 gl, string name, int location) : base(gl, name, location)
        {
        }

        public override void SetData(IOpenGL30 gl)
        {
            gl.Uniform3f(Location, Value.X, Value.Y, Value.Z);
        }
    }
}
