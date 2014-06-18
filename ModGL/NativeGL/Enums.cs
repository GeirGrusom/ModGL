
using System;

namespace ModGL.NativeGL
{
    public enum GLboolean : byte
    {
        True = 1,
        False = 0
    }


    [GLVersion(1, 5)]
    public enum BufferUsage : uint
    {
        StreamDraw = 0x88E0,
        StreamRead = 0x88E1,
        StreamCopy = 0x88E2,
        StaticDraw = 0x88E4,
        StaticRead = 0x88E5,
        StaticCopy = 0x88E6,
        DynamicDraw = 0x88E8,
        DynamicRead = 0x88E9,
        DynamicCopy = 0x88EA
    }

    [GLVersion(1, 5)]
    public enum BufferAccess : uint
    {
        ReadOnly = 0x88B8,
        WriteOnly = 0x88B9,
        ReadWrite = 0x88BA
    }

    [GLVersion(1, 5)]
    public enum BufferTarget : uint
    {
        [GLVersion(1, 5)]
        Array = 0x8892,

        [GLVersion(4, 1)]
        AtomicCounter = 0x92C0,

        [GLVersion(3, 0)]
        CopyRead = 0x8F36,

        [GLVersion(3, 0)]
        CopyWrite = 0x8F37,

        [GLVersion(3, 3)]
        DrawIndirect = 0x8F3F,

        [GLVersion(4, 3)]
        DispatchIndirect = 0x90EE,

        [GLVersion(1, 5)]
        ElementArray = 0x8893,

        [GLVersion(2, 0)]
        PixelPack = 0x88EB,

        [GLVersion(2, 0)]
        PixelUnpack = 0x88EC,

        [GLVersion(4, 4)]
        Query = 0x9192,

        [GLVersion(4, 3)]
        ShaderStorage = 0x90D2,

        [GLVersion(3, 1)]
        Texture = 0x8C2A,

        [GLVersion(3, 0)]
        TransformFeedback = 0x8C8E,

        [GLVersion(3, 1)]
        UniformBuffer = 0x8A11
    }


    public enum FronFaceDirection
    {
        Clockwise = 0x0900,
        CounterClockwise = 0x0901
    }

    public enum DataType
    {
        Byte = 0x1400,
        UnsignedByte = 0x1401,
        Short = 0x1402,
        UnsignedShort = 0x1403,
        Int = 0x1404,
        UnsignedInt = 0x1405,
        Float = 0x1406,
        Double = 0x140A,
        Half = 0x140B
    }

    [Flags]
    public enum ClearTarget : uint
    {
        Depth = 0x00000100,
        Stencil = 0x00000400,
        Color = 0x00004000,
        All = Depth | Stencil | Color
    }

    public enum PixelInternalFormat
    {
        R3G3B2 = 0x2A10,
        Rgb4 = 0x804F,
        Rgb5 = 0x8050,
        Rgb8 = 0x8051,
        Rgb10 = 0x8052,
        Rgb12 = 0x8053,
        Rgb16 = 0x8054,
        Rgba2 = 0x8055,
        Rgba4 = 0x8056,
        Rgb5A1 = 0x8057,
        Rgba8 = 0x8058,
        Rgb10A2 = 0x8059,
        Rgba12 = 0x805A,
        Rgba16 = 0x805B,
    }

    public enum HintValue : uint
    {
        Fastest = 0x1101,
        Nicest = 0x1102,
        DontCare = 0x1100
    }

    public enum Hint : uint
    {
        LineSmooth = 0x0C52,
        PolygonSmooth = 0x0C53,
        TextureCompression = 0x84EF,
        FragmentShaderDerivative = 0x8B8B
    }

    public enum TexParameterName : uint
    {
        DepthStencilTextureMode = 0x90EA,
        TextureBaseLevel = 0x813C,
        TextureCompareFunc = 0x884D,
        TextureCompareMode = 0x884C,
        TextureLodBias = 0x8501,
        TextureMinFilter = 0x2801,
        TextureMagFilter = 0x2800,
        TextureMinLod = 0x813A,
        TextureMaxLod = 0x813B,
        TextureMaxLevel = 0x813D,
        TextureSwizzleR = 0x8E42,
        TextureSwizzleG = 0x8E43,
        TextureSwizzleB = 0x8E44,
        TextureSwizzleA = 0x8E45,
        TextureWrapS = 0x2802,
        TextureWrapT = 0x2803,
        TextureWrapR = 0x8072
    }

    public enum ShaderParameters : uint
    {
        ShaderType = 0x8B4F,
        DeleteStatus = 0x8B80,
        CompileStatus = 0x8B81,
        ShaderSourceLength = 0x8B88,
        InfoLogLength = 0x8B84
    }

    public enum TexturePixelType : uint
    {
        UnsignedByte = 0x1401,
        Byte = 0x1400,
        UnsignedShort = 0x1403,
        Short = 0x1402,
        UnsignedInt = 0x1405,
        Int = 0x1404,
        Float = 0x1406,
        UnsignedByte_3_3_2 = 0x8032,
        UnsignedByte_2_3_3_Rev = 0x8362,
        UnsignedShort_5_6_5 = 0x8363,
        UnsignedShort_5_6_5_Rev = 0x8364,
        UnsignedShort_4_4_4_4 = 0x8033,
        UnsignedShort_4_4_4_4_Rev = 0x8365,
        UnsignedShort_5_5_5_1 = 0x8034,
        UnsignedShort_1_5_5_5_Rev = 0x8366,
        UnsignedInt_8_8_8_8 = 0x8035,
        UnsignedInt_8_8_8_8_Rev = 0x8367,
        UnsignedInt_10_10_10_2 = 0x8036,
        UnsignedInt_2_10_10_10_Rev = 0x8368
    }

    public enum FramebufferTextureTarget : uint
    {
        None = 0,
        Texture1D = 0x0DE0,
        Texture2D = 0x0DE1,
        Texture3D = 0x806F,
        Texture1DArray = 0x8C18,
        Texture2DArray = 0x8C1A,
        TextureCubeMap = 0x8513,
        TextureCubeMapArray = 0x9009,
        TextureRectangle = 0x84F5,
        TextureBuffer = 0x8C2A,
        Texture2DMultisample = 0x9100,
        Texture2DMultisampleArray = 0x9102,
        TextureCubeMapPositiveX = 0x8515,
        TextureCubeMapNegativeX = 0x8516,
        TextureCubeMapPositiveY = 0x8517,
        TextureCubeMapNegativeY = 0x8518,
        TextureCubeMapPositiveZ = 0x8519,
        TextureCubeMapNegativeZ = 0x851A
    }

    public enum TextureFormat : uint
    {
        R = 0x1903,
        RG = 0x8227,
        RGB = 0x1907,
        BGR = 0x80E0,
        RGBA = 0x1908,
        BGRA = 0x80E1,
        RInteger = 0x8D94,
        RGInteger = 0x8228,
        RGBInteger = 0x8D98,
        BGRInteger = 0x8D9A,
        RGBAInteger = 0x8D99,
        BGRAInteger = 0x8D9B,
        StencilIndex = 0x1901,
        DepthComponent = 0x1902,
        DepthStencil = 0x84F9
    }

    public enum TextureInternalFormat : uint
    {
        R = 0x1903,
        RG = 0x8227,
        RGB = 0x1907,
        BGR = 0x80E0,
        RGBA = 0x1908,
        BGRA = 0x80E1,
        RInteger = 0x8D94,
        RGInteger = 0x8228,
        RGBInteger = 0x8D98,
        BGRInteger = 0x8D9A,
        RGBAInteger = 0x8D99,
        BGRAInteger = 0x8D9B,
        StencilIndex = 0x1901,
        DepthComponent = 0x1902,
        DepthStencil = 0x84F9,
        R8 = 0x8229,
        R16 = 0x822A,
        RG8 = 0x822B,
        RG16 = 0x822C,
        R16F = 0x822D,
        R32F = 0x822E,
        RG16F = 0x822F,
        RG32F = 0x8230,
        R8I = 0x8231,
        R8UI = 0x8232,
        R16I = 0x8233,
        R16UI = 0x8234,
        R32I = 0x8235,
        R32UI = 0x8236,
        RG8I = 0x8237,
        RG8UI = 0x8238,
        RG16I = 0x8239,
        RG16UI = 0x823A,
        RG32I = 0x823B,
        RG32UI = 0x823C,
        RGBA32F = 0x8814,
        RGB32F = 0x8815,
        RGBA16F = 0x881A,
        RGB16F = 0x881B,
        R3_G3_B2 = 0x2A10,
        RGB4 = 0x804F,
        RGB5 = 0x8050,
        RGB8 = 0x8051,
        RGB10 = 0x8052,
        RGB12 = 0x8053,
        RGB16 = 0x8054,
        RGBA2 = 0x8055,
        RGBA4 = 0x8056,
        RGB5_A1 = 0x8057,
        RGBA8 = 0x8058,
        RGB10_A2 = 0x8059,
        RGBA12 = 0x805A,
        RGBA16 = 0x805B,
    }

    public enum FramebufferTarget : uint
    {
        /// <summary>
        /// Binds the framebuffer for writing. This is functionally the same as <see cref="DrawFramebuffer"/>.
        /// </summary>
        Framebuffer = 0x8D40,
        /// <summary>
        /// Binds the framebuffer for reading
        /// </summary>
        ReadFramebuffer = 0x8CA8,

        /// <summary>
        /// Binds the framebuffer for writing. This is functionally the same as <see cref="Framebuffer"/>.
        /// </summary>
        DrawFramebuffer = 0x8CA9
    }

    public enum StateCaps : uint
    {
        /// <summary>
        /// If enabled, blend the computed fragment color values with the values in the color buffers. See <see cref="IOpenGL.BlendFunc"/>.
        /// </summary>
        Blend = 0x0BE2,
        /// <summary>
        /// If enabled, clip geometry against user-defined half space. Add index for seperate clip dstances.
        /// </summary>
        ClipDistance = 0x3000,

        /// <summary>
        /// If enabled, apply the currently selected logical operation to the computed fragment color and color buffer values. See <see cref="IOpenGL.LogicOp"/>.
        /// </summary>
        ColorLogicOperation = 0x0BF2,

        /// <summary>
        /// If enabled, cull polygons based on their winding in window coordinates. See <see cref="IOpenGL.CullFace"/>.
        /// </summary>
        CullFace = 0x0B44,
        /// <summary>
        /// If enabled, debug messages are produced by a debug context. When disabled, the debug message log is silenced. Note that in a non-debug context, very few, if any messages might be produced, even when DebugOutput is enabled.
        /// </summary>
        DebugOutput = 0x92E0,

        /// <summary>
        /// If enabled, debug messages are produced synchronously by a debug context. If disabled, debug messages may be produced asynchronously. In particular, they may be delayed relative to the execution of GL commands, and the debug callback function may be called from a thread other than that in which the commands are executed. See glDebugMessageCallback.
        /// </summary>
        DebugOutputSynchronous = 0x8242,

        /// <summary>
        /// If enabled, the -wc≤zc≤wc plane equation is ignored by view volume clipping (effectively, there is no near or far plane clipping). See <see cref="IOpenGL.DepthRange"/>.
        /// </summary>
        DepthClamp = 0x864F,

        /// <summary>
        /// If enabled, do depth comparisons and update the depth buffer. Note that even if the depth buffer exists and the depth mask is non-zero, the depth buffer is not updated if the depth test is disabled. See <see cref="IOpenGL.DepthFunc"/> and <see cref="IOpenGL.DepthRange"/>.
        /// </summary>
        DepthTest = 0x0B71,

        /// <summary>
        /// If enabled, dither color components or indices before they are written to the color buffer.
        /// </summary>
        Dither = 0x0BD0,

        /// <summary>
        /// If enabled and the value of GL_FRAMEBUFFER_ATTACHMENT_COLOR_ENCODING for the framebuffer attachment corresponding to the destination buffer is GL_SRGB, the R, G, and B destination color values (after conversion from fixed-point to floating-point) are considered to be encoded for the sRGB color space and hence are linearized prior to their use in blending.
        /// </summary>
        FramebufferSRGB = 0x8DB9,
        /// <summary>
        /// If enabled, draw lines with correct filtering. Otherwise, draw aliased lines. See <see cref="IOpenGL.LineWidth"/>.
        /// </summary>
        LineSmooth = 0x0B20,

        /// <summary>
        /// If enabled, use multiple fragment samples in computing the final color of a pixel. See <see cref="IOpenGL30.SampleCoverage"/>.
        /// </summary>
        MultiSample = 0x809D,

        /// <summary>
        /// If enabled, and if the polygon is rendered in GL_FILL mode, an offset is added to depth values of a polygon's fragments before the depth comparison is performed. See <see cref="IOpenGL.PolygonOffset"/>.
        /// </summary>
        PolygonOffsetPoint = 0x2A01,

        /// <summary>
        /// If enabled, and if the polygon is rendered in GL_LINE mode, an offset is added to depth values of a polygon's fragments before the depth comparison is performed. See <see cref="IOpenGL.PolygonOffset" />.
        /// </summary>
        PolygonOffsetLine = 0x2A02,
        /// <summary>
        /// If enabled, an offset is added to depth values of a polygon's fragments before the depth comparison is performed, if the polygon is rendered in GL_POINT mode. See <see cref="IOpenGL.PolygonOffset"/>.
        /// </summary>
        PolygonOffsetFill = 0x8037,

        /// <summary>
        /// If enabled, draw polygons with proper filtering. Otherwise, draw aliased polygons. For correct antialiased polygons, an alpha buffer is needed and the polygons must be sorted front to back.
        /// </summary>
        PolygonSmooth = 0x0B41,

        /// <summary>
        /// Enables primitive restarting. If enabled, any one of the draw commands which transfers a set of generic attribute array elements to the GL will restart the primitive when the index of the vertex is equal to the primitive restart index. See <see cref="IOpenGL31.PrimitiveRestartIndex"/>.
        /// </summary>
        PrimitiveRestart = 0x8F9D,

        /// <summary>
        /// Enables primitive restarting with a fixed index. If enabled, any one of the draw commands which transfers a set of generic attribute array elements to the GL will restart the primitive when the index of the vertex is equal to the fixed primitive index for the specified index type. The fixed index is equal to 2n−1 where n is equal to 8 for GL_UNSIGNED_BYTE, 16 for GL_UNSIGNED_SHORT and 32 for GL_UNSIGNED_INT.
        /// </summary>
        PrimiticeRestartFixedIndex = 0x8D69,

        /// <summary>
        /// If enabled, primitives are discarded after the optional transform feedback stage, but before rasterization. Furthermore, when enabled, <see cref="IOpenGL.Clear"/>, glClearBufferData, glClearBufferSubData, glClearTexImage, and glClearTexSubImage are ignored.
        /// </summary>
        RasterizerDiscard = 0x8C89,

        /// <summary>
        /// If enabled, compute a temporary coverage value where each bit is determined by the alpha value at the corresponding sample location. The temporary coverage value is then ANDed with the fragment coverage value.
        /// </summary>
        SampleAlphaToCoverage = 0x809E,

        /// <summary>
        /// If enabled, each sample alpha value is replaced by the maximum representable alpha value.
        /// </summary>
        SampleAlphaToOne = 0x809F,

        /// <summary>
        /// If enabled, the fragment's coverage is ANDed with the temporary coverage value. If GL_SAMPLE_COVERAGE_INVERT is set to GL_TRUE, invert the coverage value. See <see cref="IOpenGL30.SampleCoverage"/>.
        /// </summary>
        SampleCoverage = 0x80A0,

        /// <summary>
        /// If enabled, the active fragment shader is run once for each covered sample, or at fraction of this rate as determined by the current value of GL_MIN_SAMPLE_SHADING_VALUE. See glMinSampleShading. />.
        /// </summary>
        SampleShading = 0x8C36,

        /// <summary>
        /// If enabled, the sample coverage mask generated for a fragment during rasterization will be ANDed with the value of GL_SAMPLE_MASK_VALUE before shading occurs. See glSampleMaski.
        /// </summary>
        SampleMask = 0x8E51,

        /// <summary>
        /// If enabled, discard fragments that are outside the scissor rectangle. See <see cref="IOpenGL.Scissor"/>.
        /// </summary>
        ScissorTest = 0x0C11,

        /// <summary>
        /// If enabled, do stencil testing and update the stencil buffer. See <see cref="IOpenGL.StencilFunc"/> and <see cref="IOpenGL.StencilOp"/>.
        /// </summary>
        StencilTest = 0x0B90,

        /// <summary>
        /// If enabled, cubemap textures are sampled such that when linearly sampling from the border between two adjacent faces, texels from both faces are used to generate the final sample value. When disabled, texels from only a single face are used to construct the final sample value.
        /// </summary>
        TextureCubeMapSeamless = 0x884F,

        /// <summary>
        /// If enabled and a vertex or geometry shader is active, then the derived point size is taken from the (potentially clipped) shader builtin gl_PointSize and clamped to the implementation-dependent point size range.
        /// </summary>
        ProgramPointSize = 0x8642

    }

    public enum ActiveTexture : uint
    {
        Texture0 = 0x84C0,
        Texture1 = 0x84C1,
        Texture2 = 0x84C2,
        Texture3 = 0x84C3,
        Texture4 = 0x84C4,
        Texture5 = 0x84C5,
        Texture6 = 0x84C6,
        Texture7 = 0x84C7,
        Texture8 = 0x84C8,
        Texture9 = 0x84C9,
        Texture10 = 0x84CA,
        Texture11 = 0x84CB,
        Texture12 = 0x84CC,
        Texture13 = 0x84CD,
        Texture14 = 0x84CE,
        Texture15 = 0x84CF,
        Texture16 = 0x84D0,
        Texture17 = 0x84D1,
        Texture18 = 0x84D2,
        Texture19 = 0x84D3,
        Texture20 = 0x84D4,
        Texture21 = 0x84D5,
        Texture22 = 0x84D6,
        Texture23 = 0x84D7,
        Texture24 = 0x84D8,
        Texture25 = 0x84D9,
        Texture26 = 0x84DA,
        Texture27 = 0x84DB,
        Texture28 = 0x84DC,
        Texture29 = 0x84DD,
        Texture30 = 0x84DE,
        Texture31 = 0x84DF
    }


    public enum GetStringNames : uint
    {
        Vendor = 0x1F00,
        Renderer = 0x1F01,
        Version = 0x1F02,
        ShadingLanguageVersion = 0x8B8C
    }

    public enum FramebufferAttachment : uint
    {
        ColorAttachment0 = 0x8CE0,
        ColorAttachment1 = 0x8CE1,
        ColorAttachment2 = 0x8CE2,
        ColorAttachment3 = 0x8CE3,
        ColorAttachment4 = 0x8CE4,
        ColorAttachment5 = 0x8CE5,
        ColorAttachment6 = 0x8CE6,
        ColorAttachment7 = 0x8CE7,
        ColorAttachment8 = 0x8CE8,
        ColorAttachment9 = 0x8CE9,
        ColorAttachment10 = 0x8CEA,
        ColorAttachment11 = 0x8CEB,
        ColorAttachment12 = 0x8CEC,
        ColorAttachment13 = 0x8CED,
        ColorAttachment14 = 0x8CEE,
        ColorAttachment15 = 0x8CEF,
        DepthAttachment = 0x8D00,
        StencilAttachment = 0x8D20
    }

    public enum ElementBufferItemType
    {
        UnsignedByte = 0x1401,
        UnsignedShort = 0x1403,
        UnsignedInt = 0x1405
    }

    public enum ProgramParameters : uint
    {
        DeleteStatus = 0x8B80,
        LinkStatus = 0x8B82,
        ValidateStatus = 0x8B83,
        InfoLogLength = 0x8B84,
        AttachedShaders = 0x8B85,
        ActiveUniforms = 0x8B86,
        ActiveUniformMaxLength = 0x8B87,
        ActiveAttributes = 0x8B89,
        ActiveAttributeMaxLength = 0x8B8A
    }

    public enum Face : uint
    {
        Front = 0x0404,
        Back = 0x0405,
        FrontAndBack = 0x0408
    }
}
