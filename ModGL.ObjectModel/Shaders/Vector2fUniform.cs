using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ModGL.NativeGL;

namespace ModGL.ObjectModel.Shaders
{
    public class Vector2fUniform : Uniform<Vector2f>
    {
        public Vector2fUniform(IOpenGL30 gl, string name, int location) : base(gl, name, location)
        {
        }

        public override void SetData(IOpenGL30 gl)
        {
            gl.Uniform2f(Location, Value.X, Value.Y);
        }
    }
}
