using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using ModGL.NativeGL;

namespace ModGL.Textures
{
    public abstract class Texture : IGLObject, IBindable, IDisposable
    {
        public TextureTarget Target { get; private set; }

        protected readonly IOpenGL30 _gl;

        public uint Handle { get; set; }

        public void Dispose()
        {
            uint[] handles = new [] { Handle };
            _gl.DeleteTextures(1, handles);
        }

        protected Texture(IOpenGL30 gl, TextureTarget target)
        {
            var newHandle = gl.GenTexture();
            if(newHandle == 0u)
                throw new NoHandleCreatedException();

            Target = target;
            this._gl = gl;
            Handle = newHandle;
        }

        public BindContext Bind()
        {
            _gl.BindTexture(Target, Handle);
            return new BindContext(() => _gl.BindTexture(Target, 0));
        }
    }
}
