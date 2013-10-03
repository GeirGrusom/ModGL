using System;
using System.Reflection;
using System.Runtime.InteropServices;

using ModGL.Binding;

using GLenum = System.UInt32;
using GLbitfield = System.UInt32;
using GLchar = System.Byte;
using GLbyte = System.SByte;
using GLshort = System.Int16;
using GLint = System.Int32;
using GLsizei = System.Int32;
using GLubyte = System.Byte;
using GLushort = System.UInt16;
using GLuint = System.UInt32;
using GLfloat = System.Single;
using GLdouble = System.Double;
using GLintptr = System.IntPtr;
using GLsizeiptr = System.IntPtr;
using GLsync = System.IntPtr;
using GLuint64 = System.UInt64;
using GLint64 = System.Int64;

namespace ModGL.NativeGL
{
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

    public enum TextureTarget : uint
    {
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

    public enum DrawMode : uint
    {
        Points = 0x0000,
        Lines = 0x0001,
        LineLoop = 0x0002,
        LineStrip = 0x0003,
        Triangles = 0x0004,
        TriangleStrip = 0x0005,
        TriangleFan = 0x0006,
        LinesAdjacency = 0x000A,
        LineStripAdjacency = 0x000B,
        TrianglesAdjacency = 0x000C,
        TriangleStripAdjacency = 0x000D,
        Patches = 0x000E
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

    public interface IBufferObjects
    {
        void BindBuffer(BufferTarget target, GLuint buffer);
        void DeleteBuffers(GLsizei n, [In]GLuint[] buffers);
        void GenBuffers(GLsizei n, [Out]GLuint[] buffers);
        GLboolean IsBuffer(GLuint buffer);
        void BufferData(BufferTarget target, GLsizeiptr size, IntPtr data, BufferUsage usage);
        void BufferSubData(BufferTarget target, GLintptr offset, GLsizeiptr size, IntPtr data);
        void GetBufferSubData(GLenum target, GLintptr offset, GLsizeiptr size, IntPtr data);
        IntPtr MapBuffer(BufferTarget target, BufferAccess access);
        GLboolean UnmapBuffer(BufferTarget target);
        void GetBufferParameteriv(GLenum target, GLenum pname, [Out]GLint[] @params);
        void GetBufferPointerv(GLenum target, GLenum pname, [Out]IntPtr[] @params);
    }

    public class ConstStringReturnMarshaller : ICustomMarshaler
    {
        private static readonly ICustomMarshaler instance;
        public static ICustomMarshaler GetInstance(string what)
        {
            return new ConstStringReturnMarshaller();
        }
        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            return Marshal.PtrToStringAnsi(pNativeData);
        }


        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            throw new NotImplementedException();
        }


        public void CleanUpNativeData(IntPtr pNativeData)
        {

        }


        public void CleanUpManagedData(object ManagedObj)
        {

        }


        public int GetNativeDataSize()
        {
            return 0;
        }
    }

    public interface IOpenGL
    {
        // 1.0
        void CullFace(Face mode);
        void FrontFace(FronFaceDirection mode);
        void Hint(Hint target, HintValue mode);
        void LineWidth(GLfloat width);
        void PointSize(GLfloat size);
        void PolygonMode(Face face, PolygonMode mode);
        void Scissor(GLint x, GLint y, GLsizei width, GLsizei height);
        void TexParameterf(TextureTarget target, TexParameterName pname, GLfloat param);
        void TexParameterfv(TextureTarget target, TexParameterName pname, [In]GLfloat[] @params);
        void TexParameteri(TextureTarget target, TexParameterName pname, GLint param);
        void TexParameteriv(TextureTarget target, TexParameterName pname, [In]GLint[] @params);
        void TexImage1D(TextureTarget target, GLint level, TextureInternalFormat internalformat, GLsizei width, GLint border, TextureFormat format, TexturePixelType type, IntPtr pixels);
        void TexImage2D(TextureTarget target, GLint level, TextureInternalFormat internalformat, GLsizei width, GLsizei height, GLint border, TextureFormat format, TexturePixelType type, IntPtr pixels);
        void DrawBuffer(GLenum mode);
        void Clear(ClearTarget mask);
        void ClearColor(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);
        void ClearStencil(GLint s);
        void ClearDepth(GLdouble depth);
        void StencilMask(GLuint mask);
        void ColorMask(GLboolean red, GLboolean green, GLboolean blue, GLboolean alpha);
        void DepthMask(GLboolean flag);
        void Disable(StateCaps cap);
        void Enable(StateCaps cap);
        void Finish();
        void Flush();
        void BlendFunc(GLenum sfactor, GLenum dfactor);
        void LogicOp(GLenum opcode);
        void StencilFunc(GLenum func, GLint @ref, GLuint mask);
        void StencilOp(GLenum fail, GLenum zfail, GLenum zpass);
        void DepthFunc(GLenum func);
        void PixelStoref(GLenum pname, GLfloat param);
        void PixelStorei(GLenum pname, GLint param);
        void ReadBuffer(GLenum mode);
        void ReadPixels(GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, IntPtr pixels);
        void GetBooleanv(GLenum pname, [Out]GLboolean[] @params);
        void GetDoublev(GLenum pname, [Out]GLdouble[] @params);
        GLenum GetError();
        void GetFloatv(GLenum pname, [Out]GLfloat[] @params);
        void GetIntegerv(GLenum pname, [Out]GLint[] @params);

        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstStringReturnMarshaller))]
        string GetString(GetStringNames name);
        void GetTexImage(GLenum target, GLint level, GLenum format, GLenum type, IntPtr pixels);
        void GetTexParameterfv(GLenum target, GLenum pname, [Out]GLfloat[] @params);
        void GetTexParameteriv(GLenum target, GLenum pname, [Out]GLint[] @params);
        void GetTexLevelParameterfv(GLenum target, GLint level, GLenum pname, [Out]GLfloat[] @params);
        void GetTexLevelParameteriv(GLenum target, GLint level, GLenum pname, [Out]GLint[] @params);
        GLboolean IsEnabled(StateCaps cap);
        void DepthRange(GLdouble near, GLdouble far);
        void Viewport(GLint x, GLint y, GLsizei width, GLsizei height);

        // 1.1
        void DrawArrays(DrawMode mode, GLint first, GLsizei count);
        void DrawElements(DrawMode mode, GLsizei count, ElementBufferItemType type, IntPtr indices);
        void GetPointerv(GLenum pname, [Out]IntPtr[] @params);
        void PolygonOffset(GLfloat factor, GLfloat units);
        void CopyTexImage1D(GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLint border);
        void CopyTexImage2D(GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLsizei height, GLint border);
        void CopyTexSubImage1D(GLenum target, GLint level, GLint xoffset, GLint x, GLint y, GLsizei width);
        void CopyTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint x, GLint y, GLsizei width, GLsizei height);
        void TexSubImage1D(GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLenum type, IntPtr pixels);
        void TexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLenum type, IntPtr pixels);
        void BindTexture(TextureTarget target, GLuint texture);
        void DeleteTextures(GLsizei n, [Out]GLuint[] textures);
        void GenTextures(GLsizei n, [Out]GLuint[] textures);
        GLboolean IsTexture(GLuint texture);

    }

    [GLVersion(3, 0)]
    public interface IOpenGL30 : IOpenGL, IBufferObjects
    {
        // 3.0
        GLboolean IsRenderbuffer(GLuint renderbuffer);
        void BindRenderbuffer(GLenum target, GLuint renderbuffer);
        void DeleteRenderbuffers(GLsizei n, [In]GLuint[] renderbuffers);
        void GenRenderbuffers(GLsizei n, [Out]GLuint[] renderbuffers);
        void RenderbufferStorage(GLenum target, GLenum internalformat, GLsizei width, GLsizei height);
        void GetRenderbufferParameteriv(GLenum target, GLenum pname, [Out]GLint[] @params);
        GLboolean IsFramebuffer(GLuint framebuffer);
        void BindFramebuffer(FramebufferTarget target, GLuint framebuffer);
        void DeleteFramebuffers(GLsizei n, [In]GLuint[] framebuffers);
        void GenFramebuffers(GLsizei n, [Out]GLuint[] framebuffers);
        GLenum CheckFramebufferStatus(GLenum target);
        void FramebufferTexture1D(FramebufferTarget target, FramebufferAttachment attachment, FramebufferTextureTarget textarget, GLuint texture, GLint level);
        void FramebufferTexture2D(FramebufferTarget target, FramebufferAttachment attachment, FramebufferTextureTarget textarget, GLuint texture, GLint level);
        void FramebufferTexture3D(FramebufferTarget target, FramebufferAttachment attachment, FramebufferTextureTarget textarget, GLuint texture, GLint level, GLint zoffset);
        void FramebufferRenderbuffer(GLenum target, GLenum attachment, GLenum renderbuffertarget, GLuint renderbuffer);
        void GetFramebufferAttachmentParameteriv(GLenum target, GLenum attachment, GLenum pname, [Out]GLint[] @params);
        void GenerateMipmap(GLenum target);
        void BlitFramebuffer(GLint srcX0, GLint srcY0, GLint srcX1, GLint srcY1, GLint dstX0, GLint dstY0, GLint dstX1, GLint dstY1, GLbitfield mask, GLenum filter);
        void RenderbufferStorageMultisample(GLenum target, GLsizei samples, GLenum internalformat, GLsizei width, GLsizei height);
        void FramebufferTextureLayer(GLenum target, GLenum attachment, GLuint texture, GLint level, GLint layer);
        IntPtr MapBufferRange(GLenum target, GLintptr offset, GLsizeiptr length, GLbitfield access);
        void FlushMappedBufferRange(GLenum target, GLintptr offset, GLsizeiptr length);
        void BindVertexArray(GLuint array);
        void DeleteVertexArrays(GLsizei n, [In]GLuint[] arrays);
        void GenVertexArrays(GLsizei n, [Out]GLuint[] arrays);
        GLboolean IsVertexArray(GLuint array);

        void ColorMaski(GLuint index, GLboolean r, GLboolean g, GLboolean b, GLboolean a);
        void GetBooleani_v(GLenum target, GLuint index, [Out]GLboolean[] data);
        void GetIntegeri_v(GLenum target, GLuint index, [Out]GLint[] data);
        void Enablei(GLenum target, GLuint index);
        void Disablei(GLenum target, GLuint index);
        GLboolean IsEnabledi(GLenum target, GLuint index);
        void BeginTransformFeedback(GLenum primitiveMode);
        void EndTransformFeedback();
        void BindBufferRange(BufferTarget target, GLuint index, GLuint buffer, GLintptr offset, GLsizeiptr size);
        void BindBufferBase(BufferTarget target, GLuint index, GLuint buffer);
        void TransformFeedbackVaryings(GLuint program, GLsizei count, [In]string[] varyings, GLenum bufferMode);
        void GetTransformFeedbackVarying(GLuint program, GLuint index, GLsizei bufSize, [Out]out GLsizei length, [Out]out GLsizei size, [Out]out GLenum type, [Out]GLchar[] name);
        void ClampColor(GLenum target, GLenum clamp);
        void BeginConditionalRender(GLuint id, GLenum mode);
        void EndConditionalRender();
        void VertexAttribIPointer(GLuint index, GLint size, DataType type, GLsizei stride, IntPtr pointer);
        void GetVertexAttribIiv(GLuint index, GLenum pname, [Out]GLint[] @params);
        void GetVertexAttribIuiv(GLuint index, GLenum pname, [Out]GLuint[] @params);
        void VertexAttribI1i(GLuint index, GLint x);
        void VertexAttribI2i(GLuint index, GLint x, GLint y);
        void VertexAttribI3i(GLuint index, GLint x, GLint y, GLint z);
        void VertexAttribI4i(GLuint index, GLint x, GLint y, GLint z, GLint w);
        void VertexAttribI1ui(GLuint index, GLuint x);
        void VertexAttribI2ui(GLuint index, GLuint x, GLuint y);
        void VertexAttribI3ui(GLuint index, GLuint x, GLuint y, GLuint z);
        void VertexAttribI4ui(GLuint index, GLuint x, GLuint y, GLuint z, GLuint w);
        void VertexAttribI1iv(GLuint index, [In]GLint[] v);
        void VertexAttribI2iv(GLuint index, [In]GLint[] v);
        void VertexAttribI3iv(GLuint index, [In]GLint[] v);
        void VertexAttribI4iv(GLuint index, [In]GLint[] v);
        void VertexAttribI1uiv(GLuint index, [In]GLuint[] v);
        void VertexAttribI2uiv(GLuint index, [In]GLuint[] v);
        void VertexAttribI3uiv(GLuint index, [In]GLuint[] v);
        void VertexAttribI4uiv(GLuint index, [In]GLuint[] v);
        void VertexAttribI4bv(GLuint index, [In]GLbyte[] v);
        void VertexAttribI4sv(GLuint index, [In]GLshort[] v);
        void VertexAttribI4ubv(GLuint index, [In]GLubyte[] v);
        void VertexAttribI4usv(GLuint index, [In]GLushort[] v);
        void GetUniformuiv(GLuint program, GLint location, [Out]GLuint[] @params);
        void BindFragDataLocation(GLuint program, GLuint color, [In]string name);
        GLint GetFragDataLocation(GLuint program, [In]string name);
        void Uniform1ui(GLint location, GLuint v0);
        void Uniform2ui(GLint location, GLuint v0, GLuint v1);
        void Uniform3ui(GLint location, GLuint v0, GLuint v1, GLuint v2);
        void Uniform4ui(GLint location, GLuint v0, GLuint v1, GLuint v2, GLuint v3);
        void Uniform1uiv(GLint location, GLsizei count, [In]GLuint[] value);
        void Uniform2uiv(GLint location, GLsizei count, [In]GLuint[] value);
        void Uniform3uiv(GLint location, GLsizei count, [In]GLuint[] value);
        void Uniform4uiv(GLint location, GLsizei count, [In]GLuint[] value);
        void TexParameterIiv(GLenum target, GLenum pname, [In]GLint[] @params);
        void TexParameterIuiv(GLenum target, GLenum pname, [In]GLuint[] @params);
        void GetTexParameterIiv(GLenum target, GLenum pname, [Out]GLint[] @params);
        void GetTexParameterIuiv(GLenum target, GLenum pname, [Out]GLuint[] @params);
        void ClearBufferiv(GLenum buffer, GLint drawbuffer, [In]GLint[] value);
        void ClearBufferuiv(GLenum buffer, GLint drawbuffer, [In]GLuint[] value);
        void ClearBufferfv(GLenum buffer, GLint drawbuffer, [In]GLfloat[] value);
        void ClearBufferfi(GLenum buffer, GLint drawbuffer, GLfloat depth, GLint stencil);

        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstStringReturnMarshaller))]
        string GetStringi(GLenum name, GLuint index);
        void UniformMatrix2x3fv(GLint location, GLsizei count, GLboolean transpose, [In]GLfloat[] value);
        void UniformMatrix3x2fv(GLint location, GLsizei count, GLboolean transpose, [In]GLfloat[] value);
        void UniformMatrix2x4fv(GLint location, GLsizei count, GLboolean transpose, [In]GLfloat[] value);
        void UniformMatrix4x2fv(GLint location, GLsizei count, GLboolean transpose, [In]GLfloat[] value);
        void UniformMatrix3x4fv(GLint location, GLsizei count, GLboolean transpose, [In]GLfloat[] value);
        void UniformMatrix4x3fv(GLint location, GLsizei count, GLboolean transpose, [In]GLfloat[] value);
        // 2.0
        void BlendEquationSeparate(GLenum modeRGB, GLenum modeAlpha);
        void DrawBuffers(GLsizei n, [In]GLenum[] bufs);
        void StencilOpSeparate(GLenum face, GLenum sfail, GLenum dpfail, GLenum dppass);
        void StencilFuncSeparate(GLenum face, GLenum func, GLint @ref, GLuint mask);
        void StencilMaskSeparate(GLenum face, GLuint mask);
        void AttachShader(GLuint program, GLuint shader);
        void BindAttribLocation(GLuint program, GLuint index, string name);
        void CompileShader(GLuint shader);
        GLuint CreateProgram();
        GLuint CreateShader(GLenum type);
        void DeleteProgram(GLuint program);
        void DeleteShader(GLuint shader);
        void DetachShader(GLuint program, GLuint shader);
        void DisableVertexAttribArray(GLuint index);
        void EnableVertexAttribArray(GLuint index);
        void GetActiveAttrib(GLuint program, GLuint index, GLsizei bufSize, [Out]out GLsizei length, [Out]out GLint size, [Out]out GLenum type, [Out]GLchar[] name);
        void GetActiveUniform(GLuint program, GLuint index, GLsizei bufSize, [Out]out GLsizei length, [Out]out GLint size, [Out]out GLenum type, [Out]GLchar[] name);
        void GetAttachedShaders(GLuint program, GLsizei maxCount, [Out]out GLsizei count, [Out]GLuint[] obj);
        GLint GetAttribLocation(GLuint program, string name);
        void GetProgramiv(GLuint program, ProgramParameters pname, [Out]GLint[] @params);
        void GetProgramInfoLog(GLuint program, GLsizei bufSize, out GLsizei length, [Out]GLchar[] infoLog);
        void GetShaderiv(GLuint shader, ShaderParameters pname, [Out]GLint[] @params);
        void GetShaderInfoLog(GLuint shader, GLsizei bufSize, out GLsizei length, [Out]GLchar[] infoLog);
        void GetShaderSource(GLuint shader, GLsizei bufSize, out GLsizei length, [Out]GLchar[] source);
        GLint GetUniformLocation(GLuint program, string name);
        void GetUniformfv(GLuint program, GLint location, [Out] out GLfloat @params);
        void GetUniformiv(GLuint program, GLint location, [Out]out GLint @params);
        void GetVertexAttribdv(GLuint index, GLenum pname, [Out]out GLdouble @params);
        void GetVertexAttribfv(GLuint index, GLenum pname, [Out]out GLfloat @params);
        void GetVertexAttribiv(GLuint index, GLenum pname, [Out]out GLint @params);
        void GetVertexAttribPointerv(GLuint index, GLenum pname, [Out]out IntPtr pointer);
        GLboolean IsProgram(GLuint program);
        GLboolean IsShader(GLuint shader);
        void LinkProgram(GLuint program);
        void ShaderSource(GLuint shader, GLsizei count, [In]string[] @string, [In]GLint[] length);
        void UseProgram(GLuint program);
        void Uniform1f(GLint location, GLfloat v0);
        void Uniform2f(GLint location, GLfloat v0, GLfloat v1);
        void Uniform3f(GLint location, GLfloat v0, GLfloat v1, GLfloat v2);
        void Uniform4f(GLint location, GLfloat v0, GLfloat v1, GLfloat v2, GLfloat v3);
        void Uniform1i(GLint location, GLint v0);
        void Uniform2i(GLint location, GLint v0, GLint v1);
        void Uniform3i(GLint location, GLint v0, GLint v1, GLint v2);
        void Uniform4i(GLint location, GLint v0, GLint v1, GLint v2, GLint v3);
        void Uniform1fv(GLint location, GLsizei count, [In]GLfloat[] value);
        void Uniform2fv(GLint location, GLsizei count, [In]GLfloat[] value);
        void Uniform3fv(GLint location, GLsizei count, [In]GLfloat[] value);
        void Uniform4fv(GLint location, GLsizei count, [In]GLfloat[] value);
        void Uniform1iv(GLint location, GLsizei count, [In]GLint[] value);
        void Uniform2iv(GLint location, GLsizei count, [In]GLint[] value);
        void Uniform3iv(GLint location, GLsizei count, [In]GLint[] value);
        void Uniform4iv(GLint location, GLsizei count, [In]GLint[] value);
        void UniformMatrix2fv(GLint location, GLsizei count, GLboolean transpose, [In]IntPtr value);
        void UniformMatrix3fv(GLint location, GLsizei count, GLboolean transpose, [In]IntPtr value);
        void UniformMatrix4fv(GLint location, GLsizei count, GLboolean transpose, [In]IntPtr value);
        void ValidateProgram(GLuint program);
        void VertexAttrib1d(GLuint index, GLdouble x);
        void VertexAttrib1dv(GLuint index, [In]GLdouble[] v);
        void VertexAttrib1f(GLuint index, GLfloat x);
        void VertexAttrib1fv(GLuint index, [In]GLfloat[] v);
        void VertexAttrib1s(GLuint index, GLshort x);
        void VertexAttrib1sv(GLuint index, [In]GLshort[] v);
        void VertexAttrib2d(GLuint index, GLdouble x, GLdouble y);
        void VertexAttrib2dv(GLuint index, [In]GLdouble[] v);
        void VertexAttrib2f(GLuint index, GLfloat x, GLfloat y);
        void VertexAttrib2fv(GLuint index, [In]GLfloat[] v);
        void VertexAttrib2s(GLuint index, GLshort x, GLshort y);
        void VertexAttrib2sv(GLuint index, [In]GLshort[] v);
        void VertexAttrib3d(GLuint index, GLdouble x, GLdouble y, GLdouble z);
        void VertexAttrib3dv(GLuint index, [In]GLdouble[] v);
        void VertexAttrib3f(GLuint index, GLfloat x, GLfloat y, GLfloat z);
        void VertexAttrib3fv(GLuint index, [In]GLfloat[] v);
        void VertexAttrib3s(GLuint index, GLshort x, GLshort y, GLshort z);
        void VertexAttrib3sv(GLuint index, [In]GLshort[] v);
        void VertexAttrib4Nbv(GLuint index, [In]GLbyte[] v);
        void VertexAttrib4Niv(GLuint index, [In]GLint[] v);
        void VertexAttrib4Nsv(GLuint index, [In]GLshort[] v);
        void VertexAttrib4Nub(GLuint index, GLubyte x, GLubyte y, GLubyte z, GLubyte w);
        void VertexAttrib4Nubv(GLuint index, [In] GLubyte[] v);
        void VertexAttrib4Nuiv(GLuint index, [In] GLuint[] v);
        void VertexAttrib4Nusv(GLuint index, [In] GLushort[] v);
        void VertexAttrib4bv(GLuint index, [In]GLbyte[] v);
        void VertexAttrib4d(GLuint index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);
        void VertexAttrib4dv(GLuint index, [In]GLdouble[] v);
        void VertexAttrib4f(GLuint index, GLfloat x, GLfloat y, GLfloat z, GLfloat w);
        void VertexAttrib4fv(GLuint index, [In]GLfloat[] v);
        void VertexAttrib4iv(GLuint index, [In]GLint[] v);
        void VertexAttrib4s(GLuint index, GLshort x, GLshort y, GLshort z, GLshort w);
        void VertexAttrib4sv(GLuint index, [In]GLshort[] v);
        void VertexAttrib4ubv(GLuint index, [In]GLubyte[] v);
        void VertexAttrib4uiv(GLuint index, [In]GLuint[] v);
        void VertexAttrib4usv(GLuint index, [In]GLushort[] v);
        void VertexAttribPointer(GLuint index, GLint size, DataType type, GLboolean normalized, GLsizei stride, IntPtr pointer);
        // 1.5
        void GenQueries(GLsizei n, [Out]GLuint[] ids);
        void DeleteQueries(GLsizei n, [In]GLuint[] ids);
        GLboolean IsQuery(GLuint id);
        void BeginQuery(GLenum target, GLuint id);
        void EndQuery(GLenum target);
        void GetQueryiv(GLenum target, GLenum pname, [Out]GLint[] @params);
        void GetQueryObjectiv(GLuint id, GLenum pname, [Out]GLint[] @params);
        void GetQueryObjectuiv(GLuint id, GLenum pname, [Out]GLuint[] @params);
        // 1.4
        void BlendFuncSeparate(GLenum sfactorRGB, GLenum dfactorRGB, GLenum sfactorAlpha, GLenum dfactorAlpha);
        void MultiDrawArrays(GLenum mode, [In]GLint[] first, [In]GLsizei[] count, GLsizei drawcount);
        void MultiDrawElements(GLenum mode, GLsizei[] count, GLenum type, [In]IntPtr[] indices, GLsizei drawcount);
        void PointParameterf(GLenum pname, GLfloat param);
        void PointParameterfv(GLenum pname, [In]GLfloat[] @params);
        void PointParameteri(GLenum pname, GLint param);
        void PointParameteriv(GLenum pname, [In]GLint[] @params);
        // 1.3
        void ActiveTexture(ActiveTexture texture);
        void SampleCoverage(GLfloat value, GLboolean invert);
        void CompressedTexImage3D(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLsizei imageSize, IntPtr data);
        void CompressedTexImage2D(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLint border, GLsizei imageSize, IntPtr data);
        void CompressedTexImage1D(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLint border, GLsizei imageSize, IntPtr data);
        void CompressedTexSubImage3D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLsizei imageSize, IntPtr data);
        void CompressedTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLsizei imageSize, IntPtr data);
        void CompressedTexSubImage1D(GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLsizei imageSize, IntPtr data);
        void GetCompressedTexImage(GLenum target, GLint level, IntPtr img);
        void BlendColor(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);
        void BlendEquation(GLenum mode);
        void DrawRangeElements(GLenum mode, GLuint start, GLuint end, GLsizei count, GLenum type, IntPtr indices);
        void TexImage3D(TextureTarget target, GLint level, TextureInternalFormat internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, TextureFormat format, TexturePixelType type, IntPtr pixels);
        void TexSubImage3D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLenum type, IntPtr pixels);
        void CopyTexSubImage3D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLint x, GLint y, GLsizei width, GLsizei height);
    }

    [GLVersion(3, 1)]
    public interface IOpenGL31 : IOpenGL30
    {
        void DrawArraysInstanced(GLenum mode, GLint first, GLsizei count, GLsizei instancecount);
        void DrawElementsInstanced(GLenum mode, GLsizei count, GLenum type, IntPtr indices, GLsizei instancecount);
        void TexBuffer(BufferTarget target, TextureInternalFormat internalformat, GLuint buffer);
        void PrimitiveRestartIndex(GLuint index);
        void CopyBufferSubData(GLenum readTarget, GLenum writeTarget, GLintptr readOffset, GLintptr writeOffset, GLsizeiptr size);
        void GetUniformIndices(GLuint program, GLsizei uniformCount, [In]string[] uniformNames, [Out]GLuint[] uniformIndices);
        void GetActiveUniformsiv(GLuint program, GLsizei uniformCount, GLuint[] uniformIndices, GLenum pname, GLint[] @params);
        void GetActiveUniformName(GLuint program, GLuint uniformIndex, GLsizei bufSize, out GLsizei length, string uniformName);
        GLuint GetUniformBlockIndex(GLuint program, string uniformBlockName);
        void GetActiveUniformBlockiv(GLuint program, GLuint uniformBlockIndex, GLenum pname, [Out]GLint[] @params);
        void GetActiveUniformBlockName(GLuint program, GLuint uniformBlockIndex, GLsizei bufSize, [Out]out GLsizei length, string uniformBlockName);
        void UniformBlockBinding(GLuint program, GLuint uniformBlockIndex, GLuint uniformBlockBinding);

    }

    [GLVersion(3, 2)]
    public interface IOpenGL32 : IOpenGL31
    {
        void DrawElementsBaseVertex(GLenum mode, GLsizei count, GLenum type, IntPtr indices, GLint basevertex);
        void DrawRangeElementsBaseVertex(GLenum mode, GLuint start, GLuint end, GLsizei count, GLenum type, IntPtr indices, GLint basevertex);
        void DrawElementsInstancedBaseVertex(GLenum mode, GLsizei count, GLenum type, IntPtr indices, GLsizei instancecount, GLint basevertex);
        void MultiDrawElementsBaseVertex(GLenum mode, [In]GLsizei[] count, GLenum type, [In]IntPtr[] indices, GLsizei drawcount, [In]GLint[] basevertex);
        void ProvokingVertex(GLenum mode);
        GLsync FenceSync(GLenum condition, GLbitfield flags);
        GLboolean IsSync(GLsync sync);
        void DeleteSync(GLsync sync);
        GLenum ClientWaitSync(GLsync sync, GLbitfield flags, GLuint64 timeout);
        void WaitSync(GLsync sync, GLbitfield flags, GLuint64 timeout);
        void GetInteger64v(GLenum pname, [Out]GLint64[] @params);
        void GetSynciv(GLsync sync, GLenum pname, GLsizei bufSize, [Out]GLsizei[] length, [Out]GLint[] values);
        void GetInteger64i_v(GLenum target, GLuint index, [Out]GLint64[] data);
        void GetBufferParameteri64v(GLenum target, GLenum pname, GLint64[] @params);
        void FramebufferTexture(GLenum target, GLenum attachment, GLuint texture, GLint level);
        void TexImage2DMultisample(GLenum target, GLsizei samples, GLint internalformat, GLsizei width, GLsizei height, GLboolean fixedsamplelocations);
        void TexImage3DMultisample(GLenum target, GLsizei samples, GLint internalformat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedsamplelocations);
        void GetMultisamplefv(GLenum pname, GLuint index, [Out]GLfloat[] val);
        void SampleMaski(GLuint index, GLbitfield mask);
    }

    [GLVersion(3, 3)]
    public interface IOpenGL33 : IOpenGL32
    {
        void BindFragDataLocationIndexed(GLuint program, GLuint colorNumber, GLuint index, string name);
        GLint GetFragDataIndex(GLuint program, [In]string name);
        void GenSamplers(GLsizei count, GLuint[] samplers);
        void DeleteSamplers(GLsizei count, GLuint[] samplers);
        GLboolean IsSampler(GLuint sampler);
        void BindSampler(GLuint unit, GLuint sampler);
        void SamplerParameteri(GLuint sampler, GLenum pname, [In]GLint param);
        void SamplerParameteriv(GLuint sampler, GLenum pname, [In]GLint[] param);
        void SamplerParameterf(GLuint sampler, GLenum pname, GLfloat param);
        void SamplerParameterfv(GLuint sampler, GLenum pname, [In]GLfloat[] param);
        void SamplerParameterIiv(GLuint sampler, GLenum pname, [In]GLint[] param);
        void SamplerParameterIuiv(GLuint sampler, GLenum pname, [In]GLuint[] param);
        void GetSamplerParameteriv(GLuint sampler, GLenum pname, [Out]GLint[] @params);
        void GetSamplerParameterIiv(GLuint sampler, GLenum pname, [Out]GLint[] @params);
        void GetSamplerParameterfv(GLuint sampler, GLenum pname, [Out]GLfloat[] @params);
        void GetSamplerParameterIuiv(GLuint sampler, GLenum pname, [Out]GLuint[] @params);
        void QueryCounter(GLuint id, GLenum target);
        void GetQueryObjecti64v(GLuint id, GLenum pname, GLint64[] @params);
        void GetQueryObjectui64v(GLuint id, GLenum pname, GLuint64[] @params);
        void VertexAttribDivisor(GLuint index, GLuint divisor);
        void VertexAttribP1ui(GLuint index, GLenum type, GLboolean normalized, GLuint value);
        void VertexAttribP1uiv(GLuint index, GLenum type, GLboolean normalized, [In]GLuint[] value);
        void VertexAttribP2ui(GLuint index, GLenum type, GLboolean normalized, GLuint value);
        void VertexAttribP2uiv(GLuint index, GLenum type, GLboolean normalized, [In]GLuint[] value);
        void VertexAttribP3ui(GLuint index, GLenum type, GLboolean normalized, GLuint value);
        void VertexAttribP3uiv(GLuint index, GLenum type, GLboolean normalized, [In]GLuint[] value);
        void VertexAttribP4ui(GLuint index, GLenum type, GLboolean normalized, GLuint value);
        void VertexAttribP4uiv(GLuint index, GLenum type, GLboolean normalized, [In]GLuint[] value);
    }

    [GLVersion(4, 0)]
    public interface IOpenGL40 : IOpenGL33
    {
        void MinSampleShading(GLfloat value);
        void BlendEquationi(GLuint buf, GLenum mode);
        void BlendEquationSeparatei(GLuint buf, GLenum modeRGB, GLenum modeAlpha);
        void BlendFunci(GLuint buf, GLenum src, GLenum dst);
        void BlendFuncSeparatei(GLuint buf, GLenum srcRGB, GLenum dstRGB, GLenum srcAlpha, GLenum dstAlpha);
        void DrawArraysIndirect(GLenum mode, IntPtr indirect);
        void DrawElementsIndirect(GLenum mode, GLenum type, [In]IntPtr indirect);
        void Uniform1d(GLint location, GLdouble x);
        void Uniform2d(GLint location, GLdouble x, GLdouble y);
        void Uniform3d(GLint location, GLdouble x, GLdouble y, GLdouble z);
        void Uniform4d(GLint location, GLdouble x, GLdouble y, GLdouble z, GLdouble w);
        void Uniform1dv(GLint location, GLsizei count, [In] GLdouble[] value);
        void Uniform2dv(GLint location, GLsizei count, [In] GLdouble[] value);
        void Uniform3dv(GLint location, GLsizei count, [In] GLdouble[] value);
        void Uniform4dv(GLint location, GLsizei count, [In] GLdouble[] value);
        void UniformMatrix2dv(GLint location, GLsizei count, GLboolean transpose, [In] GLdouble[] value);
        void UniformMatrix3dv(GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
        void UniformMatrix4dv(GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
        void UniformMatrix2x3dv(GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
        void UniformMatrix2x4dv(GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
        void UniformMatrix3x2dv(GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
        void UniformMatrix3x4dv(GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
        void UniformMatrix4x2dv(GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
        void UniformMatrix4x3dv(GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
        void GetUniformdv(GLuint program, GLint location, [Out]GLdouble @params);
        GLint GetSubroutineUniformLocation(GLuint program, GLenum shadertype, [In] string name);
        GLuint GetSubroutineIndex(GLuint program, GLenum shadertype, [In] string name);
        void GetActiveSubroutineUniformiv(GLuint program, GLenum shadertype, GLuint index, GLenum pname, [Out]GLint[] values);
        void GetActiveSubroutineUniformName(GLuint program, GLenum shadertype, GLuint index, GLsizei bufsize, [Out]GLsizei[] length, [Out]GLchar[] name);
        void GetActiveSubroutineName(GLuint program, GLenum shadertype, GLuint index, GLsizei bufsize, [Out] out GLsizei length, [Out] string name);
        void UniformSubroutinesuiv(GLenum shadertype, GLsizei count, [In]GLuint[] indices);
        void GetUniformSubroutineuiv(GLenum shadertype, GLint location, [Out]GLuint[] @params);
        void GetProgramStageiv(GLuint program, GLenum shadertype, GLenum pname, [Out]GLint[] values);
        void PatchParameteri(GLenum pname, GLint value);
        void PatchParameterfv(GLenum pname, [In]GLfloat[] values);
        void BindTransformFeedback(GLenum target, GLuint id);
        void DeleteTransformFeedbacks(GLsizei n, [In] GLuint[] ids);
        void GenTransformFeedbacks(GLsizei n, [Out]GLuint[] ids);
        GLboolean IsTransformFeedback(GLuint id);
        void PauseTransformFeedback();
        void ResumeTransformFeedback();
        void DrawTransformFeedback(GLenum mode, GLuint id);
        void DrawTransformFeedbackStream(GLenum mode, GLuint id, GLuint stream);
        void BeginQueryIndexed(GLenum target, GLuint index, GLuint id);
        void EndQueryIndexed(GLenum target, GLuint index);
        void GetQueryIndexediv(GLenum target, GLuint index, GLenum pname, [Out]GLint[] @params);
    }

    [GLVersion(4, 1)]
    public interface IOpenGL41 : IOpenGL40
    {
        void ReleaseShaderCompiler();
        void ShaderBinary(GLsizei count, [In] GLuint[] shaders, GLenum binaryformat, [In] byte[] binary, GLsizei length);
        void GetShaderPrecisionFormat(GLenum shadertype, GLenum precisiontype, GLint[] range, GLint[] precision);
        void DepthRangef(GLfloat n, GLfloat f);
        void ClearDepthf(GLfloat d);
        void GetProgramBinary(GLuint program, GLsizei bufSize, out GLsizei length, out GLenum binaryFormat, [Out]byte[] binary);
        void ProgramBinary(GLuint program, GLenum binaryFormat, [In]byte[] binary, GLsizei length);
        void ProgramParameteri(GLuint program, GLenum pname, GLint value);
        void UseProgramStages(GLuint pipeline, GLbitfield stages, GLuint program);
        void ActiveShaderProgram(GLuint pipeline, GLuint program);
        GLuint CreateShaderProgramv(GLenum type, GLsizei count, [In]string[] strings);
        void BindProgramPipeline(GLuint pipeline);
        void DeleteProgramPipelines(GLsizei n, [In]GLuint[] pipelines);
        void GenProgramPipelines(GLsizei n, [Out]GLuint[] pipelines);
        GLboolean IsProgramPipeline(GLuint pipeline);
        void GetProgramPipelineiv(GLuint pipeline, GLenum pname, GLint[] @params);
        void ProgramUniform1i(GLuint program, GLint location, GLint v0);
        void ProgramUniform1iv(GLuint program, GLint location, GLsizei count, [In]GLint[] value);
        void ProgramUniform1f(GLuint program, GLint location, GLfloat v0);
        void ProgramUniform1fv(GLuint program, GLint location, GLsizei count, [In]GLfloat[] value);
        void ProgramUniform1d(GLuint program, GLint location, GLdouble v0);
        void ProgramUniform1dv(GLuint program, GLint location, GLsizei count, [In]GLdouble[] value);
        void ProgramUniform1ui(GLuint program, GLint location, GLuint v0);
        void ProgramUniform1uiv(GLuint program, GLint location, GLsizei count, [In]GLuint[] value);
        void ProgramUniform2i(GLuint program, GLint location, GLint v0, GLint v1);
        void ProgramUniform2iv(GLuint program, GLint location, GLsizei count, [In] GLint[] value);
        void ProgramUniform2f(GLuint program, GLint location, GLfloat v0, GLfloat v1);
        void ProgramUniform2fv(GLuint program, GLint location, GLsizei count, [In] GLfloat[] value);
        void ProgramUniform2d(GLuint program, GLint location, GLdouble v0, GLdouble v1);
        void ProgramUniform2dv(GLuint program, GLint location, GLsizei count, [In] GLdouble[] value);
        void ProgramUniform2ui(GLuint program, GLint location, GLuint v0, GLuint v1);
        void ProgramUniform2uiv(GLuint program, GLint location, GLsizei count, [In] GLuint[] value);
        void ProgramUniform3i(GLuint program, GLint location, GLint v0, GLint v1, GLint v2);
        void ProgramUniform3iv(GLuint program, GLint location, GLsizei count, [In] GLint[] value);
        void ProgramUniform3f(GLuint program, GLint location, GLfloat v0, GLfloat v1, GLfloat v2);
        void ProgramUniform3fv(GLuint program, GLint location, GLsizei count, [In] GLfloat[] value);
        void ProgramUniform3d(GLuint program, GLint location, GLdouble v0, GLdouble v1, GLdouble v2);
        void ProgramUniform3dv(GLuint program, GLint location, GLsizei count, [In] GLdouble[] value);
        void ProgramUniform3ui(GLuint program, GLint location, GLuint v0, GLuint v1, GLuint v2);
        void ProgramUniform3uiv(GLuint program, GLint location, GLsizei count, [In] GLuint[] value);
        void ProgramUniform4i(GLuint program, GLint location, GLint v0, GLint v1, GLint v2, GLint v3);
        void ProgramUniform4iv(GLuint program, GLint location, GLsizei count, [In] GLint[] value);
        void ProgramUniform4f(GLuint program, GLint location, GLfloat v0, GLfloat v1, GLfloat v2, GLfloat v3);
        void ProgramUniform4fv(GLuint program, GLint location, GLsizei count, [In] GLfloat[] value);
        void ProgramUniform4d(GLuint program, GLint location, GLdouble v0, GLdouble v1, GLdouble v2, GLdouble v3);
        void ProgramUniform4dv(GLuint program, GLint location, GLsizei count, [In] GLdouble[] value);
        void ProgramUniform4ui(GLuint program, GLint location, GLuint v0, GLuint v1, GLuint v2, GLuint v3);
        void ProgramUniform4uiv(GLuint program, GLint location, GLsizei count, [In]GLuint[] value);
        void ProgramUniformMatrix2fv(GLuint program, GLint location, GLsizei count, GLboolean transpose, [In]GLfloat[] value);
        void ProgramUniformMatrix3fv(GLuint program, GLint location, GLsizei count, GLboolean transpose, [In]GLfloat[] value);
        void ProgramUniformMatrix4fv(GLuint program, GLint location, GLsizei count, GLboolean transpose, [In]GLfloat[] value);
        void ProgramUniformMatrix2dv(GLuint program, GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
        void ProgramUniformMatrix3dv(GLuint program, GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
        void ProgramUniformMatrix4dv(GLuint program, GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
        void ProgramUniformMatrix2x3fv(GLuint program, GLint location, GLsizei count, GLboolean transpose, [In]GLfloat[] value);
        void ProgramUniformMatrix3x2fv(GLuint program, GLint location, GLsizei count, GLboolean transpose, [In]GLfloat[] value);
        void ProgramUniformMatrix2x4fv(GLuint program, GLint location, GLsizei count, GLboolean transpose, [In]GLfloat[] value);
        void ProgramUniformMatrix4x2fv(GLuint program, GLint location, GLsizei count, GLboolean transpose, [In]GLfloat[] value);
        void ProgramUniformMatrix3x4fv(GLuint program, GLint location, GLsizei count, GLboolean transpose, [In]GLfloat[] value);
        void ProgramUniformMatrix4x3fv(GLuint program, GLint location, GLsizei count, GLboolean transpose, [In]GLfloat[] value);
        void ProgramUniformMatrix2x3dv(GLuint program, GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
        void ProgramUniformMatrix3x2dv(GLuint program, GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
        void ProgramUniformMatrix2x4dv(GLuint program, GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
        void ProgramUniformMatrix4x2dv(GLuint program, GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
        void ProgramUniformMatrix3x4dv(GLuint program, GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
        void ProgramUniformMatrix4x3dv(GLuint program, GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
        void ValidateProgramPipeline(GLuint pipeline);
        void GetProgramPipelineInfoLog(GLuint pipeline, GLsizei bufSize, out GLsizei length, [Out]GLchar[] infoLog);
        void VertexAttribL1d(GLuint index, GLdouble x);
        void VertexAttribL2d(GLuint index, GLdouble x, GLdouble y);
        void VertexAttribL3d(GLuint index, GLdouble x, GLdouble y, GLdouble z);
        void VertexAttribL4d(GLuint index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);
        void VertexAttribL1dv(GLuint index, [In]GLdouble[] v);
        void VertexAttribL2dv(GLuint index, [In]GLdouble[] v);
        void VertexAttribL3dv(GLuint index, [In]GLdouble[] v);
        void VertexAttribL4dv(GLuint index, [In]GLdouble[] v);
        void VertexAttribLPointer(GLuint index, GLint size, DataType type, GLsizei stride, IntPtr pointer);
        void GetVertexAttribLdv(GLuint index, GLenum pname, [Out]GLdouble[] @params);
        void ViewportArrayv(GLuint first, GLsizei count, [In]GLfloat[] v);
        void ViewportIndexedf(GLuint index, GLfloat x, GLfloat y, GLfloat w, GLfloat h);
        void ViewportIndexedfv(GLuint index, [In]GLfloat[] v);
        void ScissorArrayv(GLuint first, GLsizei count, [In]GLint[] v);
        void ScissorIndexed(GLuint index, GLint left, GLint bottom, GLsizei width, GLsizei height);
        void ScissorIndexedv(GLuint index, [In]GLint[] v);
        void DepthRangeArrayv(GLuint first, GLsizei count, [In]GLdouble[] v);
        void DepthRangeIndexed(GLuint index, GLdouble n, GLdouble f);
        void GetFloati_v(GLenum target, GLuint index, [Out]GLfloat[] data);
        void GetDoublei_v(GLenum target, GLuint index, [Out]GLdouble[] data);
    }

    [GLVersion(4, 2)]
    public interface IOpenGL42 : IOpenGL41
    {
        void DrawArraysInstancedBaseInstance(GLenum mode, GLint first, GLsizei count, GLsizei instancecount, GLuint baseinstance);
        void DrawElementsInstancedBaseInstance(GLenum mode, GLsizei count, GLenum type, [In]IntPtr[] indices, GLsizei instancecount, GLuint baseinstance);
        void DrawElementsInstancedBaseVertexBaseInstance(GLenum mode, GLsizei count, GLenum type, [In]IntPtr[] indices, GLsizei instancecount, GLint basevertex, GLuint baseinstance);
        void GetInternalformativ(GLenum target, GLenum internalformat, GLenum pname, GLsizei bufSize, [Out]GLint[] @params);
        void GetActiveAtomicCounterBufferiv(GLuint program, GLuint bufferIndex, GLenum pname, [Out]GLint[] @params);
        void BindImageTexture(GLuint unit, GLuint texture, GLint level, GLboolean layered, GLint layer, GLenum access, GLenum format);
        void MemoryBarrier(GLbitfield barriers);
        void TexStorage1D(GLenum target, GLsizei levels, GLenum internalformat, GLsizei width);
        void TexStorage2D(GLenum target, GLsizei levels, GLenum internalformat, GLsizei width, GLsizei height);
        void TexStorage3D(GLenum target, GLsizei levels, GLenum internalformat, GLsizei width, GLsizei height, GLsizei depth);
        void DrawTransformFeedbackInstanced(GLenum mode, GLuint id, GLsizei instancecount);
        void DrawTransformFeedbackStreamInstanced(GLenum mode, GLuint id, GLuint stream, GLsizei instancecount);
    }

    [GLVersion(4, 3)]
    public interface IOpenGL43 : IOpenGL42
    {

    }

    [GLVersion(4, 4)]
    public interface IOpenGL44 : IOpenGL43
    {

    }

    public enum GLboolean : byte
    {
        True = 1,
        False = 1
    }

    public enum AttribMask
    {
        DepthBufferBit = 0x00000100,
        StencilBufferBit = 0x00000400,
        ColorBufferBit = 0x00004000
    }

    public enum PrimitiveType
    {
        Points,
        Lines,
        LineLoop,
        LineStrip,
        Triangles,
        TriangleStrip,
        TriangleFan,
        Quads
    }

    public enum AlphaFunction
    {
        Never = 0x0200,
        Less = 0x0201,
        Equal = 0x0202,
        LessThanOrEqual = 0x0203,
        Greater = 0x0204,
        NotEqual = 0x0205,
        GreaterThanOrEqual = 0x0206,
        Always = 0x0207
    }

    public enum BlendingFactorDest
    {
        Zero = 0,
        One = 1,
        SrcColor = 0x0300,
        OneMinusSrcColor = 0x0301,
        SrcAlpha = 0x0302,
        OneMinusSrcAlpha = 0x0303,
        DstAlpha = 0x0304,
        OneMinusDstAlpha = 0x0305
    }

    public enum BlendingFactorSrc
    {
        DstColor = 0x0306,
        OneMinusDstColor = 0x0307,
        SrcAlphaSaturate = 0x0308
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

    public enum DrawBufferMode
    {
        None = 0,
        FrontLeft = 0x0400,
        FrontRight = 0x0401,
        BackLeft = 0x0402,
        BackRight = 0x0403,
        Front = 0x0404,
        Back = 0x0405,
        Left = 0x0406,
        Right = 0x0407,
        FrontAndBack = 0x0408
    }

    public enum ErrorCode
    {
        NoError = 0,
        InvalidEnum = 0x0500,
        InvalidValue = 0x0501,
        InvalidOperation = 0x0502,
        StackOverflow = 0x0503,
        StackUnderflow = 0x0504,
        OutOfMemory = 0x0505
    }

    public enum FronFaceDirection
    {
        Clockwise = 0x0900,
        CounterClockwise = 0x0901
    }

    public enum GetPName
    {
        PointSize = 0x0B11,
        PointSizeRange = 0x0B12,
        PointSizeGranularity = 0x0B13,
        LineSmooth = 0x0B20,
        LineWidth = 0x0B21,
        LineWidthRange = 0x0B22,
        LineWidthGranularity = 0x0B23,
        PolygonMode = 0x0B40,
        PolygonSmooth = 0x0B41,
        CullFace = 0x0B44,
        CullFaceMode = 0x0B45,
        FrontFace = 0x0B46,
        DepthRange = 0x0B70,
        DepthTest = 0x0B71,
        DepthWritemask = 0x0B72,
        DepthClearValue = 0x0B73,
        DepthFunc = 0x0B74,
        StencilTest = 0x0B90,
        StencilClearValue = 0x0B91,
        StencilFunc = 0x0B92,
        StencilValueMask = 0x0B93,
        StencilFail = 0x0B94,
        StencilPassDepthFail = 0x0B95,
        StencilPassDepthPass = 0x0B96,
        StencilRef = 0x0B97,
        StencilWritemask = 0x0B98,
        Viewport = 0x0BA2,
        Dither = 0x0BD0,
        BlendDst = 0x0BE0,
        BlendSrc = 0x0BE1,
        Blend = 0x0BE2,
        LogicOpMode = 0x0BF0,
        ColorLogicOp = 0x0BF2,
        DrawBuffer = 0x0C01,
        ReadBuffer = 0x0C02,
        ScissorBox = 0x0C10,
        ScissorTest = 0x0C11,
        ColorClearValue = 0x0C22,
        ColorWritemask = 0x0C23,
        Doublebuffer = 0x0C32,
        Stereo = 0x0C33,
        LineSmoothHint = 0x0C52,
        PolygonSmoothHint = 0x0C53,
        UnpackSwapBytes = 0x0CF0,
        UnpackLsbFirst = 0x0CF1,
        UnpackRowLength = 0x0CF2,
        UnpackSkipRows = 0x0CF3,
        UnpackSkipPixels = 0x0CF4,
        UnpackAlignment = 0x0CF5,
        PackSwapBytes = 0x0D00,
        PackLsbFirst = 0x0D01,
        PackRowLength = 0x0D02,
        PackSkipRows = 0x0D03,
        PackSkipPixels = 0x0D04,
        PackAlignment = 0x0D05,
        MaxTextureSize = 0x0D33,
        MaxViewportDims = 0x0D3A,
        SubpixelBits = 0x0D50,
        Texture1D = 0x0DE0,
        Texture2D = 0x0DE1,
        PolygonOffsetUnits = 0x2A00,
        PolygonOffsetPoint = 0x2A01,
        PolygonOffsetLine = 0x2A02,
        PolygonOffsetFill = 0x8037,
        PolygonOffsetFactor = 0x8038,
        TextureBinding_1D = 0x8068,
        TextureBinding_2D = 0x8069,
    }

    public enum GetTextureParameter
    {
        TextureWidth = 0x1000,
        TextureHeight = 0x1001,
        TextureInternalFormat = 0x1003,
        TextureBorderColor = 0x1004,
        TextureRedSize = 0x805C,
        TextureGreenSize = 0x805D,
        TextureBlueSize = 0x805E,
        TextureAlphaSize = 0x805F,
    }

    public enum HitMode
    {
        DontCare = 0x1100,
        Fastest = 0x1101,
        Nicest = 0x1102,
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

    public enum LogicOp
    {
        Clear = 0x1500,
        And = 0x1501,
        AndReverse = 0x1502,
        Copy = 0x1503,
        AndInverted = 0x1504,
        Noop = 0x1505,
        Xor = 0x1506,
        Or = 0x1507,
        Nor = 0x1508,
        Equiv = 0x1509,
        Invert = 0x150A,
        OrReverse = 0x150B,
        CopyInverted = 0x150C,
        OrInverted = 0x150D,
        Nand = 0x150E,
        Set = 0x150F,
    }

    /// <summary>
    /// Used for FBO
    /// </summary>
    public enum MatrixMode
    {
        Texture = 0x1702
    }

    public enum PixelCopyType
    {
        Color = 0x1800,
        Depth = 0x1801,
        Stencil = 0x1802,
    }

    public enum PixelFormat
    {
        StencilIndex = 0x1901,
        DepthComponent = 0x1902,
        Red = 0x1903,
        Green = 0x1904,
        Blue = 0x1905,
        Alpha = 0x1906,
        Rgb = 0x1907,
        Rgba = 0x1908,
    }

    public enum PolygonMode
    {
        Point = 0x1B00,
        Line = 0x1B01,
        Fill = 0x1B02,
    }

    public enum StencilOp
    {
        Keep = 0x1E00,
        Replace = 0x1E01,
        Increment = 0x1E02,
        Decrement = 0x1E03,
    }

    public enum StringName
    {
        Vendor = 0x1F00,
        Renderer = 0x1F01,
        Version = 0x1F02,
        Extensions = 0x1F03
    }

    public enum TextureMagFilter
    {
        Nearest = 0x2600,
        Linear = 0x2601
    }

    public enum TextureMinFilter
    {
        NearestMipmapNearest = 0x2700,
        LinearMipmapNearest = 0x2701,
        NearestMipmapLinear = 0x2702,
        LinearMipmapLinear = 0x2703,
    }

    public enum TextureParameterName
    {
        TextureMagFilter = 0x2800,
        TextureMinFilter = 0x2801,
        TextureWrapS = 0x2802,
        TextureWrapT = 0x2803,
    }

    public enum TextureWrapMode
    {
        Repeat = 0x2901
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

    public static class GL
    {
        [DllImport(GLLibraryName, EntryPoint = "glCullFace")]
        public static extern void CullFace(Face mode);

        [DllImport(GLLibraryName, EntryPoint = "glFrontFace")]
        public static extern void FrontFace(FronFaceDirection mode);

        [DllImport(GLLibraryName, EntryPoint = "glHint")]
        public static extern void Hint(Hint target, HintValue mode);

        [DllImport(GLLibraryName, EntryPoint = "glLineWidth")]
        public static extern void LineWidth(GLfloat width);

        [DllImport(GLLibraryName, EntryPoint = "glPointSize")]
        public static extern void PointSize(GLfloat size);

        [DllImport(GLLibraryName, EntryPoint = "glPolygonMode")]
        public static extern void PolygonMode(Face face, PolygonMode mode);

        [DllImport(GLLibraryName, EntryPoint = "glScissor")]
        public static extern void Scissor(GLint x, GLint y, GLsizei width, GLsizei height);

        [DllImport(GLLibraryName, EntryPoint = "glTexParameterf")]
        public static extern void TexParameterf(TextureTarget target, TexParameterName pname, GLfloat param);

        [DllImport(GLLibraryName, EntryPoint = "glTexParameterfv")]
        public static extern void TexParameterfv(TextureTarget target, TexParameterName pname, [In]GLfloat[] @params);

        [DllImport(GLLibraryName, EntryPoint = "glTexParameteri")]
        public static extern void TexParameteri(TextureTarget target, TexParameterName pname, GLint param);

        [DllImport(GLLibraryName, EntryPoint = "glTexParameteriv")]
        public static extern void TexParameteriv(TextureTarget target, TexParameterName pname, [In]GLint[] @params);

        [DllImport(GLLibraryName, EntryPoint = "glTexImage1D")]
        public static extern void TexImage1D(TextureTarget target, GLint level, TextureInternalFormat internalformat, GLsizei width, GLint border, TextureFormat format, TexturePixelType type, IntPtr pixels);

        [DllImport(GLLibraryName, EntryPoint = "glTexImage2D")]
        public static extern void TexImage2D(TextureTarget target, GLint level, TextureInternalFormat internalformat, GLsizei width, GLsizei height, GLint border, TextureFormat format, TexturePixelType type, IntPtr pixels);

        [DllImport(GLLibraryName, EntryPoint = "glDrawBuffer")]
        public static extern void DrawBuffer(GLenum mode);

        [DllImport(GLLibraryName, EntryPoint = "glClear")]
        public static extern void Clear(ClearTarget mask);

        [DllImport(GLLibraryName, EntryPoint = "glClearColor")]
        public static extern void ClearColor(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);

        [DllImport(GLLibraryName, EntryPoint = "glClearStencil")]
        public static extern void ClearStencil(GLint s);

        [DllImport(GLLibraryName, EntryPoint = "glClearDepth")]
        public static extern void ClearDepth(GLdouble depth);

        [DllImport(GLLibraryName, EntryPoint = "glStencilMask")]
        public static extern void StencilMask(GLuint mask);

        [DllImport(GLLibraryName, EntryPoint = "glColorMask")]
        public static extern void ColorMask(GLboolean red, GLboolean green, GLboolean blue, GLboolean alpha);

        [DllImport(GLLibraryName, EntryPoint = "glDepthMask")]
        public static extern void DepthMask(GLboolean flag);

        [DllImport(GLLibraryName, EntryPoint = "glDisable")]
        public static extern void Disable(StateCaps cap);

        [DllImport(GLLibraryName, EntryPoint = "glEnable")]
        public static extern void Enable(StateCaps cap);

        [DllImport(GLLibraryName, EntryPoint = "glFinish")]
        public static extern void Finish();

        [DllImport(GLLibraryName, EntryPoint = "glFlush")]
        public static extern void Flush();

        [DllImport(GLLibraryName, EntryPoint = "glBlendFunc")]
        public static extern void BlendFunc(GLenum sfactor, GLenum dfactor);

        [DllImport(GLLibraryName, EntryPoint = "glLogicOp")]
        public static extern void LogicOp(GLenum opcode);

        [DllImport(GLLibraryName, EntryPoint = "glStencilFunc")]
        public static extern void StencilFunc(GLenum func, GLint @ref, GLuint mask);

        [DllImport(GLLibraryName, EntryPoint = "glStencilOp")]
        public static extern void StencilOp(GLenum fail, GLenum zfail, GLenum zpass);

        [DllImport(GLLibraryName, EntryPoint = "glDepthFunc")]
        public static extern void DepthFunc(GLenum func);

        [DllImport(GLLibraryName, EntryPoint = "glPixelStoref")]
        public static extern void PixelStoref(GLenum pname, GLfloat param);

        [DllImport(GLLibraryName, EntryPoint = "glPixelStorei")]
        public static extern void PixelStorei(GLenum pname, GLint param);

        [DllImport(GLLibraryName, EntryPoint = "glReadBuffer")]
        public static extern void ReadBuffer(GLenum mode);

        [DllImport(GLLibraryName, EntryPoint = "glReadPixels")]
        public static extern void ReadPixels(GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, IntPtr pixels);

        [DllImport(GLLibraryName, EntryPoint = "glGetBooleanv")]
        public static extern void GetBooleanv(GLenum pname, [Out]GLboolean[] @params);

        [DllImport(GLLibraryName, EntryPoint = "glGetDoublev")]
        public static extern void GetDoublev(GLenum pname, [Out]GLdouble[] @params);

        [DllImport(GLLibraryName, EntryPoint = "glGetError")]
        public static extern GLenum GetError();

        [DllImport(GLLibraryName, EntryPoint = "glGetFloatv")]
        public static extern void GetFloatv(GLenum pname, [Out]GLfloat[] @params);

        [DllImport(GLLibraryName, EntryPoint = "glGetIntegerv")]
        public static extern void GetIntegerv(GLenum pname, [Out]GLint[] @params);

        [DllImport(GLLibraryName, EntryPoint = "glGetString")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstStringReturnMarshaller))]
        public static extern string GetString(GetStringNames name);

        [DllImport(GLLibraryName, EntryPoint = "glGetTexImage")]
        public static extern void GetTexImage(GLenum target, GLint level, GLenum format, GLenum type, IntPtr pixels);

        [DllImport(GLLibraryName, EntryPoint = "glGetTexParameterfv")]
        public static extern void GetTexParameterfv(GLenum target, GLenum pname, [Out]GLfloat[] @params);

        [DllImport(GLLibraryName, EntryPoint = "glGetTexParameteriv")]
        public static extern void GetTexParameteriv(GLenum target, GLenum pname, [Out]GLint[] @params);

        [DllImport(GLLibraryName, EntryPoint = "glGetTexLevelParameterfv")]
        public static extern void GetTexLevelParameterfv(GLenum target, GLint level, GLenum pname, [Out]GLfloat[] @params);

        [DllImport(GLLibraryName, EntryPoint = "glGetTexLevelParameteriv")]
        public static extern void GetTexLevelParameteriv(GLenum target, GLint level, GLenum pname, [Out]GLint[] @params);

        [DllImport(GLLibraryName, EntryPoint = "glIsEnabled")]
        public static extern GLboolean IsEnabled(StateCaps cap);

        [DllImport(GLLibraryName, EntryPoint = "glDepthRange")]
        public static extern void DepthRange(GLdouble near, GLdouble far);

        [DllImport(GLLibraryName, EntryPoint = "glViewport")]
        public static extern void Viewport(GLint x, GLint y, GLsizei width, GLsizei height);

        [DllImport(GLLibraryName, EntryPoint = "glDrawArrays")]
        public static extern void DrawArrays(DrawMode mode, GLint first, GLsizei count);

        [DllImport(GLLibraryName, EntryPoint = "glDrawElements")]
        public static extern void DrawElements(DrawMode mode, GLsizei count, ElementBufferItemType type, IntPtr indices);

        [DllImport(GLLibraryName, EntryPoint = "glGetPointerv")]
        public static extern void GetPointerv(GLenum pname, IntPtr[] @params);

        [DllImport(GLLibraryName, EntryPoint = "glPolygonOffset")]
        public static extern void PolygonOffset(GLfloat factor, GLfloat units);

        [DllImport(GLLibraryName, EntryPoint = "glCopyTexImage1D")]
        public static extern void CopyTexImage1D(GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLint border);

        [DllImport(GLLibraryName, EntryPoint = "glCopyTexImage2D")]
        public static extern void CopyTexImage2D(GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLsizei height, GLint border);

        [DllImport(GLLibraryName, EntryPoint = "glCopyTexSubImage1D")]
        public static extern void CopyTexSubImage1D(GLenum target, GLint level, GLint xoffset, GLint x, GLint y, GLsizei width);

        [DllImport(GLLibraryName, EntryPoint = "glCopyTexSubImage2D")]
        public static extern void CopyTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint x, GLint y, GLsizei width, GLsizei height);

        [DllImport(GLLibraryName, EntryPoint = "glTexSubImage1D")]
        public static extern void TexSubImage1D(GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLenum type, IntPtr pixels);

        [DllImport(GLLibraryName, EntryPoint = "glTexSubImage2D")]
        public static extern void TexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLenum type, IntPtr pixels);

        [DllImport(GLLibraryName, EntryPoint = "glBindTexture")]
        public static extern void BindTexture(TextureTarget target, GLuint texture);

        [DllImport(GLLibraryName, EntryPoint = "glDeleteTextures")]
        public static extern void DeleteTextures(GLsizei n, GLuint[] textures);

        [DllImport(GLLibraryName, EntryPoint = "glGenTextures")]
        public static extern void GenTextures(GLsizei n, GLuint[] textures);

        [DllImport(GLLibraryName, EntryPoint = "glIsTexture")]
        public static extern GLboolean IsTexture(GLuint texture);


        [System.Diagnostics.DebuggerHidden]
        [System.Diagnostics.DebuggerStepThrough]
        public static void HandleOpenGLError()
        {
            GLenum error = GetError();
            if (error == 0)
                return;
            switch ((ErrorCode)error)
            {
                case ErrorCode.InvalidEnum:
                    throw new OpenGLInvalidEnumException();
                case ErrorCode.InvalidOperation:
                    throw new OpenGLInvalidOperationException();
                case ErrorCode.InvalidValue:
                    throw new OpenGLInvalidValueException();
                case ErrorCode.StackOverflow:
                    throw new OpenGLStackOverflowException();
                case ErrorCode.StackUnderflow:
                    throw new OpenGLStackUnderflowException();
                default:
                    throw new OpenGLException((ErrorCode)error);
            }
        }

        public static void FlushOpenGLError()
        {
            GetError();
        }

        public static readonly ErrorHandling OpenGLErrorFunctions = new ErrorHandling
        {
            FlushError = typeof(GL).GetMethod("FlushOpenGLError", BindingFlags.Public | BindingFlags.Static),
            CheckErrorState = typeof(GL).GetMethod("HandleOpenGLError", BindingFlags.Public | BindingFlags.Static)
        };

        public const string GLLibraryName = "opengl32";
    }
}
