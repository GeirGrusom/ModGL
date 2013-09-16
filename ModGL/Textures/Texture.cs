using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using ModGL.NativeGL;

namespace ModGL.Textures
{
    public abstract class Texture : IGLObject, IBindable
    {
        public TextureTarget Target { get; private set; }

        protected readonly IOpenGL30 _gl;

        public uint Handle { get; set; }

        protected Texture(IOpenGL30 gl, TextureTarget target)
        {
            Target = target;
            uint[] newHandles = new uint[1];
            this._gl = gl;
            gl.glGenTextures(1, newHandles);
            Handle = newHandles.Single();
        }

        public BindContext Bind()
        {
            _gl.glBindTexture(Target, Handle);
            return new BindContext(() => _gl.glBindTexture(Target, 0));
        }

        public void BufferData(byte[] dataArray)
        {
            var handle = GCHandle.Alloc(dataArray, GCHandleType.Pinned);
            try
            {
                BufferData(handle.AddrOfPinnedObject());
            }
            finally
            {
                handle.Free();
            }
        }

        public abstract void BufferData(IntPtr address);

        public void BufferData(System.IO.Stream source)
        {
            throw new NotImplementedException();
        }

    }
}
