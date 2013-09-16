﻿using System;

using ModGL.NativeGL;

namespace ModGL.Textures
{
    public sealed class Texture3D : Texture
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Depth { get; private set; }

        public TextureFormat Format { get; private set; }
        public TextureInternalFormat InternalFormat { get; private set; }
        public TexturePixelType PixelType { get; private set; }

        public Texture3D(IOpenGL30 gl, TextureTarget target, int width, int height, int depth, TextureFormat format, TextureInternalFormat internalFormat, TexturePixelType type)
            : base(gl, target)
        {
            Format = format;
            InternalFormat = internalFormat;
            PixelType = type;
            Width = width;
            Height = height;
            Depth = depth;

        }

        public override void BufferData(IntPtr address)
        {
            this._gl.glTexImage3D(Target, 0, InternalFormat, Width, Height, Depth, 0, Format, PixelType, address);
        }
    }
}