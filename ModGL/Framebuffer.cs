using System;

using ModGL.NativeGL;

namespace ModGL
{
    public interface IRenderBuffer : IGLObject, IDisposable, IBindable
    {
        int Width { get; }
        int Height { get; }
        PixelInternalFormat InternalFormat { get; }
    }


    public class RenderBuffer : IRenderBuffer
    {
        public uint Handle { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public PixelInternalFormat InternalFormat { get; private set; }
        private readonly IOpenGL30 _gl;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public RenderBuffer(IOpenGL30 gl, int width, int height, PixelInternalFormat internalFormat, int samples)
        {
            if(gl == null)
                throw new ArgumentNullException("gl");

            if(width <= 0)
                throw new ArgumentException("Width must be greater than 0", "width");

            if(height <= 0)
                throw new ArgumentException("Height must be greater than 0.", "height");

            uint handle = gl.GenRenderBuffer();
            if(handle == 0)
                throw new NoHandleCreatedException();

            Handle = handle;
            Width = width;
            Height = height;
            InternalFormat = internalFormat;

            gl.BindRenderbuffer(Constants.Renderbuffer, Handle);
            gl.RenderbufferStorage(Constants.Renderbuffer, (uint)InternalFormat, Width, Height);
            gl.BindRenderbuffer(Constants.Renderbuffer, 0);
        }

        public BindContext Bind()
        {
            _gl.BindRenderbuffer(Constants.Renderbuffer, Handle);
            return new BindContext(() => _gl.BindRenderbuffer(Constants.Renderbuffer, 0));
        }
    }

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
            _gl.BindFramebuffer((uint)_target, Handle);
            return new BindContext(() => _gl.BindFramebuffer((uint)_target, 0));
        }

        public void Attach(Textures.Texture2D texture, FramebufferTarget target, FramebufferAttachment attachment, FramebufferTextureTarget textureTarget)
        {
            _gl.FramebufferTexture2D((uint)target, (uint)attachment, (uint) textureTarget, texture.Handle, 0);   
        }
    }
}
