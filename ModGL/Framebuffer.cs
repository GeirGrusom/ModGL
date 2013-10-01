using System;

using ModGL.NativeGL;

namespace ModGL
{
    public class Framebuffer : IGLObject, IDisposable, IBindable
    {
        public uint Handle { get; private set; }
        private readonly IOpenGL30 _gl;
        private readonly FramebufferTarget _target;

        public Framebuffer(IOpenGL30 gl, FramebufferTarget target)
        {
            var handle = gl.GenFramebuffer();
            if(handle == 0)
                throw new NoHandleCreatedException();
            _gl = gl;
            _target = target;
            Handle = handle;
        }

        public void Dispose()
        {
            _gl.DeleteFrameBuffers(Handle);
        }

        public BindContext Bind()
        {
            _gl.BindFramebuffer(_target, Handle);
            return new BindContext(() => _gl.BindFramebuffer(_target, 0));
        }

        public void Attach(Textures.Texture2D texture, FramebufferTarget target, FramebufferAttachment attachment, FramebufferTextureTarget textureTarget)
        {
            _gl.FramebufferTexture2D(target, attachment, textureTarget, texture.Handle, 0);   
        }
    }
}
