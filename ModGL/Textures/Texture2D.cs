using System;
using System.Runtime.InteropServices;

using ModGL.NativeGL;

namespace ModGL.Textures
{
    public sealed class Texture2D : Texture
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public TextureFormat Format { get; private set; }
        public TextureInternalFormat InternalFormat { get; private set; }
        public TexturePixelType PixelType { get; private set; }

        public Texture2D(IOpenGL30 gl, int width, int height, TextureFormat format, TextureInternalFormat internalFormat, TexturePixelType type)
            : base(gl, TextureTarget.Texture2d)
        {
            Format = format;
            InternalFormat = internalFormat;
            PixelType = type;
            Width = width;
            Height = height;
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
            _gl.TexImage2D(Target, 0, (int)InternalFormat, Width, Height, 0, (PixelFormat)Format, (PixelType)PixelType, address);
        }
    }
}