using System;

using ModGL.NativeGL;

namespace ModGL.Textures
{
    public sealed class Texture1D : Texture
    {
        public int Width { get; private set; }

        public TextureFormat Format { get; private set; }
        public TextureInternalFormat InternalFormat { get; private set; }
        public TexturePixelType PixelType { get; private set; }

        public Texture1D(IOpenGL30 gl, TextureTarget target, int width, TextureFormat format, TextureInternalFormat internalFormat, TexturePixelType type) : base(gl, target)
        {
            Format = format;
            InternalFormat = internalFormat;
            PixelType = type;
            Width = width;
        }

        public override void BufferData(IntPtr address)
        {
            _gl.glTexImage1D(Target, 0, InternalFormat, Width, 0, Format, PixelType, address);
        }
    }
}
