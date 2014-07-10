using System.ComponentModel;
using System.Numerics;
using ModGL.NativeGL;

namespace ModGL.ObjectModel.Textures
{
    public enum MagnificationInterpolationMode
    {
        Nearest = (int)Constants.Nearest,
        Linear = (int)Constants.Linear
    }

    public enum TextureWrap
    {
        ClampToEdge = (int)Constants.ClampToEdge,
        MirroredRepeat = (int)Constants.MirroredRepeat,
        Repeat = (int)Constants.Repeat,
        MirrorClampToEdge = (int)Constants.MirrorClampToEdge,
        ClampToBorder = (int)Constants.ClampToBorder
    }

    public enum TextureCompareMode
    {
        None = 0,
        CompareRefToTexture = (int)Constants.CompareRefToTexture,
    }

    public enum MinificationInterpolationMode 
    {
        Nearest = (int)Constants.Nearest,
        Linear = (int)Constants.Linear,
        NearestMipmapLinear = (int)Constants.NearestMipmapLinear,
        LinearMipmapLinear = (int)Constants.LinearMipmapLinear,
        LinearMipmapNearest = (int)Constants.LinearMipmapNearest
    }

    public enum TextureCompareFunc
    {
        LessOrEqual = (int)Constants.Lequal,
        GreaterOrEqual = (int)Constants.Gequal,
        Less = (int)Constants.Less,
        Greater = (int)Constants.Greater,
        Equal = (int)Constants.Equal,
        NotEqual = (int)Constants.Notequal,
        Always = (int)Constants.Always,
        Never = (int)Constants.Never
    }

    public class Sampler : IGLObject//, IBindable
    {
        private readonly IOpenGL33 _gl;

        public uint Handle { get; private set; }

        public Sampler(IOpenGL33 gl)
        {
            _gl = gl;
            var handle = _gl.GenSampler();
            if(handle == 0)
                throw new NoHandleCreatedException();
            Handle = handle;
            
        }

        private int GetValue(uint name)
        {
            var values = new int[1];
            _gl.GetSamplerParameteriv(Handle, name, values);
            return values[0];
        }

        [DefaultValue(Constants.Linear)]
        public MagnificationInterpolationMode Magnification
        {
            get { return (MagnificationInterpolationMode)GetValue(Constants.TextureMagFilter); }
            set
            {
                _gl.SamplerParameteri(Handle, Constants.TextureMagFilter, (int)value);
            }
        }

        [DefaultValue(Constants.Linear)]
        public MinificationInterpolationMode Minification
        {
            get { return (MinificationInterpolationMode) GetValue(Constants.TextureMinFilter); }
            set
            {
                _gl.SamplerParameteri(Handle, Constants.TextureMinFilter, (int)value);
            }
        }

        [DefaultValue(-1000)]
        public float MinLod
        {
            get
            {
                var values = new float[1];
                _gl.GetSamplerParameterfv(Handle, Constants.TextureMinLod, values);
                return values[0];
            }
            set
            {
                _gl.SamplerParameterf(Handle, Constants.TextureMinLod, value);
            }
        }

        [DefaultValue(1000)]
        public float MaxLod
        {
            get
            {
                var values = new float[1];
                _gl.GetSamplerParameterfv(Handle, Constants.TextureMaxLod, values);
                return values[0];
            }
            set
            {
                _gl.SamplerParameterf(Handle, Constants.TextureMaxLod, value);
            }
        }

        [DefaultValue(Constants.Repeat)]
        public TextureWrap WrapR
        {
            get { return (TextureWrap) GetValue(Constants.TextureWrapR); }
            set
            {
                _gl.SamplerParameteri(Handle, Constants.TextureWrapR, (int)value);
            }
        }

        [DefaultValue(Constants.Repeat)]
        public TextureWrap WrapS
        {
            get { return (TextureWrap) GetValue(Constants.TextureWrapS); }
            set
            {
                _gl.SamplerParameteri(Handle, Constants.TextureWrapS, (int)value);
            }
        }

        [DefaultValue(Constants.Repeat)]
        public TextureWrap WrapT
        {
            get { return (TextureWrap) GetValue(Constants.TextureWrapT); }
            set
            {
                _gl.SamplerParameteri(Handle, Constants.TextureWrapT, (int)value);
            }
        }

        public Vector4f BorderColor
        {
            get
            {
                var values = new float[4];
                _gl.GetSamplerParameterfv(Handle, Constants.TextureBorderColor, values);
                return new Vector4f(values[0], values[1], values[2], values[3]);
            }
            set
            {
                float[] values =  {value.X, value.Y, value.Z, value.W};
                _gl.SamplerParameterfv(Handle, Constants.TextureBorderColor, values);
            }
        }

        public TextureCompareMode CompareMode
        {
            get { return (TextureCompareMode) GetValue(Constants.TextureCompareMode); }
            set {  _gl.SamplerParameteri(Handle, Constants.TextureCompareMode, (int)value);}
        }

        public TextureCompareFunc CompareFunc 
        { 
            get
            {
                return (TextureCompareFunc) GetValue(Constants.TextureCompareFunc);
            }
            set
            {
                _gl.SamplerParameteri(Handle, Constants.TextureCompareFunc, (int)value);                
            }
        }
        public BindContext Bind(uint textureUnit)
        {
            _gl.BindSampler(textureUnit, Handle);
            return new BindContext(() => _gl.BindSampler(textureUnit, 0));
        }
    }
}
