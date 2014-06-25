using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModGL.NativeGL;

namespace ModGL.Textures
{
    public class Sampler : IGLObject//, IBindable
    {
        private readonly IOpenGL33 _gl;

        public uint Handle { get; private set; }

        public Sampler(IOpenGL33 gl)
        {
            _gl = gl;
            var handle = _gl.GenSampler();
            if(handle == 0)
                throw new NoHandleCreatedException();
            Handle = handle;
        }

        public BindContext Bind(uint textureUnit)
        {
            _gl.BindSampler(textureUnit, Handle);
            return new BindContext(() => _gl.BindSampler(textureUnit, 0));
        }
    }
}
