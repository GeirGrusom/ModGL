﻿using ModGL.NativeGL;

namespace ModGL.ObjectModel.Shaders
{
    public sealed class TesselationControlShader : Shader
    {
        public TesselationControlShader(IOpenGL40 gl, string code)
            : base(gl, ShaderType.TesselationControlShader, code)
        {
            
        }
    }
}
