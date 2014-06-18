using System;
using System.Runtime.InteropServices;

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

        public void BufferData<T>(T[] dataArray)
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

        public void BufferData(IntPtr address)
        {
            _gl.TexImage1D(Target, 0, (int)InternalFormat, Width, 0, (PixelFormat)Format, (PixelType)PixelType, address);
        }
    }
}
