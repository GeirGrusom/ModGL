using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using ModGL.NativeGL;

namespace ModGL.ObjectModel.Textures
{
    public sealed class CubeMap : Texture
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public TextureFormat Format { get; private set; }
        public TextureInternalFormat InternalFormat { get; private set; }
        public TexturePixelType PixelType { get; private set; }

        public CubeMap(IOpenGL30 gl, int width, int height, TextureFormat format, TextureInternalFormat internalFormat, TexturePixelType type)
            : base(gl, (TextureTarget)Constants.TextureCubeMap)
        {
            Width = width;
            Height = height;
            Format = format;
            InternalFormat = internalFormat;
            PixelType = type;
        }

        public void BufferData<T>(
            T[] positiveX, T[] negativeX, T[] positiveY, T[] negativeY, T[] positiveZ, T[] negativeZ)
        {
            var lpx = GCHandle.Alloc(positiveX, GCHandleType.Pinned);
            var lnx = GCHandle.Alloc(negativeX, GCHandleType.Pinned);
            var lpy = GCHandle.Alloc(positiveY, GCHandleType.Pinned);
            var lny = GCHandle.Alloc(negativeY, GCHandleType.Pinned);
            var lpz = GCHandle.Alloc(positiveZ, GCHandleType.Pinned);
            var lnz = GCHandle.Alloc(negativeZ, GCHandleType.Pinned);
            try
            {
                BufferData(
                    lpx.AddrOfPinnedObject(),
                    lnx.AddrOfPinnedObject(),
                    lpy.AddrOfPinnedObject(),
                    lny.AddrOfPinnedObject(),
                    lpz.AddrOfPinnedObject(),
                    lnz.AddrOfPinnedObject());
            }
            finally
            {
                lpx.Free();
                lnx.Free();
                lpy.Free();
                lny.Free();
                lpz.Free();
                lnz.Free();
            }
        }

        public void BufferData(IntPtr positiveX, IntPtr negativeX, IntPtr positiveY, IntPtr negativeY, IntPtr positiveZ, IntPtr negativeZ)
        {
            _gl.TexImage2D((TextureTarget)Constants.TextureCubeMapPositiveX, 0, (int)InternalFormat, Width, Height, 0, (PixelFormat)Format, (PixelType)PixelType, positiveX);
            _gl.TexImage2D((TextureTarget)Constants.TextureCubeMapPositiveY, 0, (int)InternalFormat, Width, Height, 0, (PixelFormat)Format, (PixelType)PixelType, positiveY);
            _gl.TexImage2D((TextureTarget)Constants.TextureCubeMapPositiveZ, 0, (int)InternalFormat, Width, Height, 0, (PixelFormat)Format, (PixelType)PixelType, positiveZ);
            _gl.TexImage2D((TextureTarget)Constants.TextureCubeMapNegativeX, 0, (int)InternalFormat, Width, Height, 0, (PixelFormat)Format, (PixelType)PixelType, negativeX);
            _gl.TexImage2D((TextureTarget)Constants.TextureCubeMapNegativeY, 0, (int)InternalFormat, Width, Height, 0, (PixelFormat)Format, (PixelType)PixelType, negativeY);
            _gl.TexImage2D((TextureTarget)Constants.TextureCubeMapNegativeZ, 0, (int)InternalFormat, Width, Height, 0, (PixelFormat)Format, (PixelType)PixelType, negativeZ);
        }

    }
}
