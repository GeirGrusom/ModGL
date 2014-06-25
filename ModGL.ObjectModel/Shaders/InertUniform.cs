using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModGL.NativeGL;

namespace ModGL.ObjectModel.Shaders
{
    public class InertUniform<TValue> : Uniform<TValue>
    {
        public InertUniform(IOpenGL30 gl, string name, int location) 
            : base(gl, name, location)
        {
        }


        public override void SetData(IOpenGL30 gl)
        {
        }
    }
}
