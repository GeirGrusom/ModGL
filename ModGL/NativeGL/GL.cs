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
        Unsigned_Byte_3_3_2 = 0x8032,
        Unsigned_Byte_2_3_3_REV = 0x8362,
        Unsigned_Short_5_6_5 = 0x8363,
        Unsigned_Short_5_6_5_REV = 0x8364,
        Unsigned_Short_4_4_4_4 = 0x8033,
        Unsigned_Short_4_4_4_4_REV = 0x8365,
        Unsigned_Short_5_5_5_1 = 0x8034,
        Unsigned_Short_1_5_5_5_REV = 0x8366,
        Unsigned_Int_8_8_8_8 = 0x8035,
        Unsigned_Int_8_8_8_8_REV = 0x8367,
        Unsigned_Int_10_10_10_2 = 0x8036,
        Unsigned_Int_2_10_10_10_REV = 0x8368
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
        Texture2DMultisampleArray = 0x9102
    }

    public enum TextureFormat : uint
    {
        Red = 0x1903,
        RedGreen = 0x8227,
        RedGreenBlue = 0x1907,
        BlueGreenRed = 0x80E0,
        RedGreenBlueAlpha = 0x1908,
        BlueGreenRedAlpha = 0x80E1,
        RedInteger = 0x8D94,
        RedGreenInteger = 0x8228,
        RedGreenBlueInteger = 0x8D98,
        BlueGreenRedInteger = 0x8D9A,
        RedGreenBlueAlphaInteger = 0x8D99,
        BlueGreenRedAlphaInteger = 0x8D9B,
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

    public enum StateCaps : uint
    {
        /// <summary>
        /// If enabled, blend the computed fragment color values with the values in the color buffers. See <see cref="IOpenGL.glBlendFunc"/>.
        /// </summary>
        Blend = 0x0BE2,
        /// <summary>
        /// If enabled, clip geometry against user-defined half space. Add index for seperate clip dstances.
        /// </summary>
        ClipDistance = 0x3000,

        /// <summary>
        /// If enabled, apply the currently selected logical operation to the computed fragment color and color buffer values. See <see cref="IOpenGL.glLogicOp"/>.
        /// </summary>
        ColorLogicOperation = 0x0BF2,

        /// <summary>
        /// If enabled, cull polygons based on their winding in window coordinates. See <see cref="IOpenGL.glCullFace"/>.
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
        /// If enabled, the -wc≤zc≤wc plane equation is ignored by view volume clipping (effectively, there is no near or far plane clipping). See <see cref="IOpenGL.glDepthRange"/>.
        /// </summary>
        DepthClamp = 0x864F,

        /// <summary>
        /// If enabled, do depth comparisons and update the depth buffer. Note that even if the depth buffer exists and the depth mask is non-zero, the depth buffer is not updated if the depth test is disabled. See <see cref="IOpenGL.glDepthFunc"/> and <see cref="IOpenGL.glDepthRange"/>.
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
        /// If enabled, draw lines with correct filtering. Otherwise, draw aliased lines. See <see cref="IOpenGL.glLineWidth"/>.
        /// </summary>
        LineSmooth = 0x0B20,

        /// <summary>
        /// If enabled, use multiple fragment samples in computing the final color of a pixel. See <see cref="IOpenGL30.glSampleCoverage"/>.
        /// </summary>
        MultiSample = 0x809D,

        /// <summary>
        /// If enabled, and if the polygon is rendered in GL_FILL mode, an offset is added to depth values of a polygon's fragments before the depth comparison is performed. See <see cref="IOpenGL.glPolygonOffset"/>.
        /// </summary>
        PolygonOffsetPoint = 0x2A01,

        /// <summary>
        /// If enabled, and if the polygon is rendered in GL_LINE mode, an offset is added to depth values of a polygon's fragments before the depth comparison is performed. See <see cref="IOpenGL.glPolygonOffset" />.
        /// </summary>
        PolygonOffsetLine = 0x2A02,
        /// <summary>
        /// If enabled, an offset is added to depth values of a polygon's fragments before the depth comparison is performed, if the polygon is rendered in GL_POINT mode. See <see cref="IOpenGL.glPolygonOffset"/>.
        /// </summary>
        PolygonOffsetFill = 0x8037,

        /// <summary>
        /// If enabled, draw polygons with proper filtering. Otherwise, draw aliased polygons. For correct antialiased polygons, an alpha buffer is needed and the polygons must be sorted front to back.
        /// </summary>
        PolygonSmooth = 0x0B41,

        /// <summary>
        /// Enables primitive restarting. If enabled, any one of the draw commands which transfers a set of generic attribute array elements to the GL will restart the primitive when the index of the vertex is equal to the primitive restart index. See <see cref="IOpenGL30.glPrimitiveRestartIndex"/>.
        /// </summary>
        PrimitiveRestart = 0x8F9D,

        /// <summary>
        /// Enables primitive restarting with a fixed index. If enabled, any one of the draw commands which transfers a set of generic attribute array elements to the GL will restart the primitive when the index of the vertex is equal to the fixed primitive index for the specified index type. The fixed index is equal to 2n−1 where n is equal to 8 for GL_UNSIGNED_BYTE, 16 for GL_UNSIGNED_SHORT and 32 for GL_UNSIGNED_INT.
        /// </summary>
        PrimiticeRestartFixedIndex = 0x8D69,

        /// <summary>
        /// If enabled, primitives are discarded after the optional transform feedback stage, but before rasterization. Furthermore, when enabled, <see cref="IOpenGL.glClear"/>, glClearBufferData, glClearBufferSubData, glClearTexImage, and glClearTexSubImage are ignored.
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
        /// If enabled, the fragment's coverage is ANDed with the temporary coverage value. If GL_SAMPLE_COVERAGE_INVERT is set to GL_TRUE, invert the coverage value. See <see cref="IOpenGL30.glSampleCoverage"/>.
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
        /// If enabled, discard fragments that are outside the scissor rectangle. See <see cref="IOpenGL.glScissor"/>.
        /// </summary>
        ScissorTest = 0x0C11,

        /// <summary>
        /// If enabled, do stencil testing and update the stencil buffer. See <see cref="IOpenGL.glStencilFunc"/> and <see cref="IOpenGL.glStencilOp"/>.
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

    public interface IOpenGL
    {
        // 1.0
        void glCullFace(GLenum mode);
        void glFrontFace(GLenum mode);
        void glHint(GLenum target, GLenum mode);
        void glLineWidth(GLfloat width);
        void glPointSize(GLfloat size);
        void glPolygonMode(GLenum face, GLenum mode);
        void glScissor(GLint x, GLint y, GLsizei width, GLsizei height);
        void glTexParameterf(GLenum target, GLenum pname, GLfloat param);
        void glTexParameterfv(GLenum target, GLenum pname, GLfloat[] @params);
        void glTexParameteri(GLenum target, GLenum pname, GLint param);
        void glTexParameteriv(GLenum target, GLenum pname, GLint[] @params);
        void glTexImage1D(TextureTarget target, GLint level, TextureInternalFormat internalformat, GLsizei width, GLint border, TextureFormat format, TexturePixelType type, IntPtr pixels);
        void glTexImage2D(TextureTarget target, GLint level, TextureInternalFormat internalformat, GLsizei width, GLsizei height, GLint border, TextureFormat format, TexturePixelType type, IntPtr pixels);
        void glDrawBuffer(GLenum mode);
        void glClear(ClearTarget mask);
        void glClearColor(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);
        void glClearStencil(GLint s);
        void glClearDepth(GLdouble depth);
        void glStencilMask(GLuint mask);
        void glColorMask(GLboolean red, GLboolean green, GLboolean blue, GLboolean alpha);
        void glDepthMask(GLboolean flag);
        void glDisable(StateCaps cap);
        void glEnable(StateCaps cap);
        void glFinish();
        void glFlush();
        void glBlendFunc(GLenum sfactor, GLenum dfactor);
        void glLogicOp(GLenum opcode);
        void glStencilFunc(GLenum func, GLint @ref, GLuint mask);
        void glStencilOp(GLenum fail, GLenum zfail, GLenum zpass);
        void glDepthFunc(GLenum func);
        void glPixelStoref(GLenum pname, GLfloat param);
        void glPixelStorei(GLenum pname, GLint param);
        void glReadBuffer(GLenum mode);
        void glReadPixels(GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, IntPtr pixels);
        void glGetBooleanv(GLenum pname, GLboolean[] @params);
        void glGetDoublev(GLenum pname, GLdouble[] @params);
        GLenum glGetError();
        void glGetFloatv(GLenum pname, GLfloat[] @params);
        void glGetIntegerv(GLenum pname, GLint[] @params);
        string glGetString(GLenum name);
        void glGetTexImage(GLenum target, GLint level, GLenum format, GLenum type, IntPtr pixels);
        void glGetTexParameterfv(GLenum target, GLenum pname, GLfloat[] @params);
        void glGetTexParameteriv(GLenum target, GLenum pname, GLint[] @params);
        void glGetTexLevelParameterfv(GLenum target, GLint level, GLenum pname, GLfloat[] @params);
        void glGetTexLevelParameteriv(GLenum target, GLint level, GLenum pname, GLint[] @params);
        GLboolean glIsEnabled(GLenum cap);
        void glDepthRange(GLdouble near, GLdouble far);
        void glViewport(GLint x, GLint y, GLsizei width, GLsizei height);

        // 1.1
        void glDrawArrays(DrawMode mode, GLint first, GLsizei count);
        void glDrawElements(DrawMode mode, GLsizei count, ElementBufferItemType type, IntPtr indices);
        void glGetPointerv(GLenum pname, IntPtr[] @params);
        void glPolygonOffset(GLfloat factor, GLfloat units);
        void glCopyTexImage1D(GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLint border);
        void glCopyTexImage2D(GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLsizei height, GLint border);
        void glCopyTexSubImage1D(GLenum target, GLint level, GLint xoffset, GLint x, GLint y, GLsizei width);
        void glCopyTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint x, GLint y, GLsizei width, GLsizei height);
        void glTexSubImage1D(GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLenum type, IntPtr pixels);
        void glTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLenum type, IntPtr pixels);
        void glBindTexture(TextureTarget target, GLuint texture);
        void glDeleteTextures(GLsizei n, GLuint[] textures);
        void glGenTextures(GLsizei n, GLuint[] textures);
        GLboolean glIsTexture(GLuint texture);

    }

    [GLVersion(3, 0)]
    public interface IOpenGL30 : IOpenGL
    {
        // 3.0
        GLboolean glIsRenderbuffer(GLuint renderbuffer);
        void glBindRenderbuffer(GLenum target, GLuint renderbuffer);
        void glDeleteRenderbuffers(GLsizei n, GLuint[] renderbuffers);
        void glGenRenderbuffers(GLsizei n, GLuint[] renderbuffers);
        void glRenderbufferStorage(GLenum target, GLenum internalformat, GLsizei width, GLsizei height);
        void glGetRenderbufferParameteriv(GLenum target, GLenum pname, GLint[] @params);
        GLboolean glIsFramebuffer(GLuint framebuffer);
        void glBindFramebuffer(GLenum target, GLuint framebuffer);
        void glDeleteFramebuffers(GLsizei n, GLuint[] framebuffers);
        void glGenFramebuffers(GLsizei n, GLuint[] framebuffers);
        GLenum glCheckFramebufferStatus(GLenum target);
        void glFramebufferTexture1D(GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level);
        void glFramebufferTexture2D(GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level);
        void glFramebufferTexture3D(GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level, GLint zoffset);
        void glFramebufferRenderbuffer(GLenum target, GLenum attachment, GLenum renderbuffertarget, GLuint renderbuffer);
        void glGetFramebufferAttachmentParameteriv(GLenum target, GLenum attachment, GLenum pname, GLint[] @params);
        void glGenerateMipmap(GLenum target);
        void glBlitFramebuffer(GLint srcX0, GLint srcY0, GLint srcX1, GLint srcY1, GLint dstX0, GLint dstY0, GLint dstX1, GLint dstY1, GLbitfield mask, GLenum filter);
        void glRenderbufferStorageMultisample(GLenum target, GLsizei samples, GLenum internalformat, GLsizei width, GLsizei height);
        void glFramebufferTextureLayer(GLenum target, GLenum attachment, GLuint texture, GLint level, GLint layer);
        IntPtr glMapBufferRange(GLenum target, GLintptr offset, GLsizeiptr length, GLbitfield access);
        void glFlushMappedBufferRange(GLenum target, GLintptr offset, GLsizeiptr length);
        void glBindVertexArray(GLuint array);
        void glDeleteVertexArrays(GLsizei n, uint[] arrays);
        void glGenVertexArrays(GLsizei n, uint[] arrays);
        GLboolean glIsVertexArray(GLuint array);

        void glColorMaski(GLuint index, GLboolean r, GLboolean g, GLboolean b, GLboolean a);
        void glGetBooleani_v(GLenum target, GLuint index, GLboolean[] data);
        void glGetIntegeri_v(GLenum target, GLuint index, GLint[] data);
        void glEnablei(GLenum target, GLuint index);
        void glDisablei(GLenum target, GLuint index);
        GLboolean glIsEnabledi(GLenum target, GLuint index);
        void glBeginTransformFeedback(GLenum primitiveMode);
        void glEndTransformFeedback();
        void glBindBufferRange(BufferTarget target, GLuint index, GLuint buffer, GLintptr offset, GLsizeiptr size);
        void glBindBufferBase(BufferTarget target, GLuint index, GLuint buffer);
        void glTransformFeedbackVaryings(GLuint program, GLsizei count, string[] varyings, GLenum bufferMode);
        void glGetTransformFeedbackVarying(GLuint program, GLuint index, GLsizei bufSize, GLsizei[] length, GLsizei[] size, GLenum[] type, GLchar[] name);
        void glClampColor(GLenum target, GLenum clamp);
        void glBeginConditionalRender(GLuint id, GLenum mode);
        void glEndConditionalRender();
        void glVertexAttribIPointer(GLuint index, GLint size, DataType type, GLsizei stride, IntPtr pointer);
        void glGetVertexAttribIiv(GLuint index, GLenum pname, GLint[] @params);
        void glGetVertexAttribIuiv(GLuint index, GLenum pname, GLuint[] @params);
        void glVertexAttribI1i(GLuint index, GLint x);
        void glVertexAttribI2i(GLuint index, GLint x, GLint y);
        void glVertexAttribI3i(GLuint index, GLint x, GLint y, GLint z);
        void glVertexAttribI4i(GLuint index, GLint x, GLint y, GLint z, GLint w);
        void glVertexAttribI1ui(GLuint index, GLuint x);
        void glVertexAttribI2ui(GLuint index, GLuint x, GLuint y);
        void glVertexAttribI3ui(GLuint index, GLuint x, GLuint y, GLuint z);
        void glVertexAttribI4ui(GLuint index, GLuint x, GLuint y, GLuint z, GLuint w);
        void glVertexAttribI1iv(GLuint index, GLint[] v);
        void glVertexAttribI2iv(GLuint index, GLint[] v);
        void glVertexAttribI3iv(GLuint index, GLint[] v);
        void glVertexAttribI4iv(GLuint index, GLint[] v);
        void glVertexAttribI1uiv(GLuint index, GLuint[] v);
        void glVertexAttribI2uiv(GLuint index, GLuint[] v);
        void glVertexAttribI3uiv(GLuint index, GLuint[] v);
        void glVertexAttribI4uiv(GLuint index, GLuint[] v);
        void glVertexAttribI4bv(GLuint index, GLbyte[] v);
        void glVertexAttribI4sv(GLuint index, GLshort[] v);
        void glVertexAttribI4ubv(GLuint index, GLubyte[] v);
        void glVertexAttribI4usv(GLuint index, GLushort[] v);
        void glGetUniformuiv(GLuint program, GLint location, GLuint[] @params);
        void glBindFragDataLocation(GLuint program, GLuint color, string name);
        GLint glGetFragDataLocation(GLuint program, string name);
        void glUniform1ui(GLint location, GLuint v0);
        void glUniform2ui(GLint location, GLuint v0, GLuint v1);
        void glUniform3ui(GLint location, GLuint v0, GLuint v1, GLuint v2);
        void glUniform4ui(GLint location, GLuint v0, GLuint v1, GLuint v2, GLuint v3);
        void glUniform1uiv(GLint location, GLsizei count, GLuint[] value);
        void glUniform2uiv(GLint location, GLsizei count, GLuint[] value);
        void glUniform3uiv(GLint location, GLsizei count, GLuint[] value);
        void glUniform4uiv(GLint location, GLsizei count, GLuint[] value);
        void glTexParameterIiv(GLenum target, GLenum pname, GLint[] @params);
        void glTexParameterIuiv(GLenum target, GLenum pname, GLuint[] @params);
        void glGetTexParameterIiv(GLenum target, GLenum pname, GLint[] @params);
        void glGetTexParameterIuiv(GLenum target, GLenum pname, GLuint[] @params);
        void glClearBufferiv(GLenum buffer, GLint drawbuffer, GLint[] value);
        void glClearBufferuiv(GLenum buffer, GLint drawbuffer, GLuint[] value);
        void glClearBufferfv(GLenum buffer, GLint drawbuffer, GLfloat[] value);
        void glClearBufferfi(GLenum buffer, GLint drawbuffer, GLfloat depth, GLint stencil);
        string glGetStringi(GLenum name, GLuint index);
        void glUniformMatrix2x3fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
        void glUniformMatrix3x2fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
        void glUniformMatrix2x4fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
        void glUniformMatrix4x2fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
        void glUniformMatrix3x4fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
        void glUniformMatrix4x3fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
        // 2.0
        void glBlendEquationSeparate(GLenum modeRGB, GLenum modeAlpha);
        void glDrawBuffers(GLsizei n, GLenum[] bufs);
        void glStencilOpSeparate(GLenum face, GLenum sfail, GLenum dpfail, GLenum dppass);
        void glStencilFuncSeparate(GLenum face, GLenum func, GLint @ref, GLuint mask);
        void glStencilMaskSeparate(GLenum face, GLuint mask);
        void glAttachShader(GLuint program, GLuint shader);
        void glBindAttribLocation(GLuint program, GLuint index, string name);
        void glCompileShader(GLuint shader);
        GLuint glCreateProgram();
        GLuint glCreateShader(GLenum type);
        void glDeleteProgram(GLuint program);
        void glDeleteShader(GLuint shader);
        void glDetachShader(GLuint program, GLuint shader);
        void glDisableVertexAttribArray(GLuint index);
        void glEnableVertexAttribArray(GLuint index);
        void glGetActiveAttrib(GLuint program, GLuint index, GLsizei bufSize, GLsizei[] length, GLint[] size, GLenum[] type, GLchar[] name);
        void glGetActiveUniform(GLuint program, GLuint index, GLsizei bufSize, GLsizei[] length, GLint[] size, GLenum[] type, GLchar[] name);
        void glGetAttachedShaders(GLuint program, GLsizei maxCount, GLsizei[] count, GLuint[] obj);
        GLint glGetAttribLocation(GLuint program, string name);
        void glGetProgramiv(GLuint program, ProgramParameters pname, GLint[] @params);
        void glGetProgramInfoLog(GLuint program, GLsizei bufSize, out GLsizei length, GLchar[] infoLog);
        void glGetShaderiv(GLuint shader, ShaderParameters pname, GLint[] @params);
        void glGetShaderInfoLog(GLuint shader, GLsizei bufSize, out GLsizei length, GLchar[] infoLog);
        void glGetShaderSource(GLuint shader, GLsizei bufSize, out GLsizei length, GLchar[] source);
        GLint glGetUniformLocation(GLuint program, string name);
        void glGetUniformfv(GLuint program, GLint location, GLfloat[] @params);
        void glGetUniformiv(GLuint program, GLint location, GLint[] @params);
        void glGetVertexAttribdv(GLuint index, GLenum pname, GLdouble[] @params);
        void glGetVertexAttribfv(GLuint index, GLenum pname, GLfloat[] @params);
        void glGetVertexAttribiv(GLuint index, GLenum pname, GLint[] @params);
        void glGetVertexAttribPointerv(GLuint index, GLenum pname, IntPtr[] pointer);
        GLboolean glIsProgram(GLuint program);
        GLboolean glIsShader(GLuint shader);
        void glLinkProgram(GLuint program);
        void glShaderSource(GLuint shader, GLsizei count, string[] @string, GLint[] length);
        void glUseProgram(GLuint program);
        void glUniform1f(GLint location, GLfloat v0);
        void glUniform2f(GLint location, GLfloat v0, GLfloat v1);
        void glUniform3f(GLint location, GLfloat v0, GLfloat v1, GLfloat v2);
        void glUniform4f(GLint location, GLfloat v0, GLfloat v1, GLfloat v2, GLfloat v3);
        void glUniform1i(GLint location, GLint v0);
        void glUniform2i(GLint location, GLint v0, GLint v1);
        void glUniform3i(GLint location, GLint v0, GLint v1, GLint v2);
        void glUniform4i(GLint location, GLint v0, GLint v1, GLint v2, GLint v3);
        void glUniform1fv(GLint location, GLsizei count, GLfloat[] value);
        void glUniform2fv(GLint location, GLsizei count, GLfloat[] value);
        void glUniform3fv(GLint location, GLsizei count, GLfloat[] value);
        void glUniform4fv(GLint location, GLsizei count, GLfloat[] value);
        void glUniform1iv(GLint location, GLsizei count, GLint[] value);
        void glUniform2iv(GLint location, GLsizei count, GLint[] value);
        void glUniform3iv(GLint location, GLsizei count, GLint[] value);
        void glUniform4iv(GLint location, GLsizei count, GLint[] value);
        void glUniformMatrix2fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
        void glUniformMatrix3fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
        void glUniformMatrix4fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
        void glValidateProgram(GLuint program);
        void glVertexAttrib1d(GLuint index, GLdouble x);
        void glVertexAttrib1dv(GLuint index, GLdouble[] v);
        void glVertexAttrib1f(GLuint index, GLfloat x);
        void glVertexAttrib1fv(GLuint index, GLfloat[] v);
        void glVertexAttrib1s(GLuint index, GLshort x);
        void glVertexAttrib1sv(GLuint index, GLshort[] v);
        void glVertexAttrib2d(GLuint index, GLdouble x, GLdouble y);
        void glVertexAttrib2dv(GLuint index, GLdouble[] v);
        void glVertexAttrib2f(GLuint index, GLfloat x, GLfloat y);
        void glVertexAttrib2fv(GLuint index, GLfloat[] v);
        void glVertexAttrib2s(GLuint index, GLshort x, GLshort y);
        void glVertexAttrib2sv(GLuint index, GLshort[] v);
        void glVertexAttrib3d(GLuint index, GLdouble x, GLdouble y, GLdouble z);
        void glVertexAttrib3dv(GLuint index, GLdouble[] v);
        void glVertexAttrib3f(GLuint index, GLfloat x, GLfloat y, GLfloat z);
        void glVertexAttrib3fv(GLuint index, GLfloat[] v);
        void glVertexAttrib3s(GLuint index, GLshort x, GLshort y, GLshort z);
        void glVertexAttrib3sv(GLuint index, GLshort[] v);
        void glVertexAttrib4Nbv(GLuint index, GLbyte[] v);
        void glVertexAttrib4Niv(GLuint index, GLint[] v);
        void glVertexAttrib4Nsv(GLuint index, GLshort[] v);
        void glVertexAttrib4Nub(GLuint index, GLubyte x, GLubyte y, GLubyte z, GLubyte w);
        void glVertexAttrib4Nubv(GLuint index, GLubyte[] v);
        void glVertexAttrib4Nuiv(GLuint index, GLuint[] v);
        void glVertexAttrib4Nusv(GLuint index, GLushort[] v);
        void glVertexAttrib4bv(GLuint index, GLbyte[] v);
        void glVertexAttrib4d(GLuint index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);
        void glVertexAttrib4dv(GLuint index, GLdouble[] v);
        void glVertexAttrib4f(GLuint index, GLfloat x, GLfloat y, GLfloat z, GLfloat w);
        void glVertexAttrib4fv(GLuint index, GLfloat[] v);
        void glVertexAttrib4iv(GLuint index, GLint[] v);
        void glVertexAttrib4s(GLuint index, GLshort x, GLshort y, GLshort z, GLshort w);
        void glVertexAttrib4sv(GLuint index, GLshort[] v);
        void glVertexAttrib4ubv(GLuint index, GLubyte[] v);
        void glVertexAttrib4uiv(GLuint index, GLuint[] v);
        void glVertexAttrib4usv(GLuint index, GLushort[] v);
        void glVertexAttribPointer(GLuint index, GLint size, DataType type, GLboolean normalized, GLsizei stride, IntPtr pointer);
        // 1.5
        void glGenQueries(GLsizei n, GLuint[] ids);
        void glDeleteQueries(GLsizei n, GLuint[] ids);
        GLboolean glIsQuery(GLuint id);
        void glBeginQuery(GLenum target, GLuint id);
        void glEndQuery(GLenum target);
        void glGetQueryiv(GLenum target, GLenum pname, GLint[] @params);
        void glGetQueryObjectiv(GLuint id, GLenum pname, GLint[] @params);
        void glGetQueryObjectuiv(GLuint id, GLenum pname, GLuint[] @params);
        void glBindBuffer(BufferTarget target, GLuint buffer);
        void glDeleteBuffers(GLsizei n, GLuint[] buffers);
        void glGenBuffers(GLsizei n, GLuint[] buffers);
        GLboolean glIsBuffer(GLuint buffer);
        void glBufferData(BufferTarget target, GLsizeiptr size, IntPtr data, BufferUsage usage);
        void glBufferSubData(BufferTarget target, GLintptr offset, GLsizeiptr size, IntPtr data);
        void glGetBufferSubData(GLenum target, GLintptr offset, GLsizeiptr size, IntPtr data);
        IntPtr glMapBuffer(BufferTarget target, BufferAccess access);
        GLboolean glUnmapBuffer(BufferTarget target);
        void glGetBufferParameteriv(GLenum target, GLenum pname, GLint[] @params);
        void glGetBufferPointerv(GLenum target, GLenum pname, IntPtr[] @params);
        // 1.4
        void glBlendFuncSeparate(GLenum sfactorRGB, GLenum dfactorRGB, GLenum sfactorAlpha, GLenum dfactorAlpha);
        void glMultiDrawArrays(GLenum mode, GLint[] first, GLsizei[] count, GLsizei drawcount);
        void glMultiDrawElements(GLenum mode, GLsizei[] count, GLenum type, IntPtr[] indices, GLsizei drawcount);
        void glPointParameterf(GLenum pname, GLfloat param);
        void glPointParameterfv(GLenum pname, GLfloat[] @params);
        void glPointParameteri(GLenum pname, GLint param);
        void glPointParameteriv(GLenum pname, GLint[] @params);
        // 1.3
        void glActiveTexture(GLenum texture);
        void glSampleCoverage(GLfloat value, GLboolean invert);
        void glCompressedTexImage3D(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLsizei imageSize, IntPtr data);
        void glCompressedTexImage2D(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLint border, GLsizei imageSize, IntPtr data);
        void glCompressedTexImage1D(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLint border, GLsizei imageSize, IntPtr data);
        void glCompressedTexSubImage3D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLsizei imageSize, IntPtr data);
        void glCompressedTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLsizei imageSize, IntPtr data);
        void glCompressedTexSubImage1D(GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLsizei imageSize, IntPtr data);
        void glGetCompressedTexImage(GLenum target, GLint level, IntPtr img);
        void glBlendColor(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);
        void glBlendEquation(GLenum mode);
        void glDrawRangeElements(GLenum mode, GLuint start, GLuint end, GLsizei count, GLenum type, IntPtr indices);
        void glTexImage3D(TextureTarget target, GLint level, TextureInternalFormat internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, TextureFormat format, TexturePixelType type, IntPtr pixels);
        void glTexSubImage3D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLenum type, IntPtr pixels);
        void glCopyTexSubImage3D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLint x, GLint y, GLsizei width, GLsizei height);
    }

    [GLVersion(3, 1)]
    public interface IOpenGL31 : IOpenGL30
    {
        void glDrawArraysInstanced(GLenum mode, GLint first, GLsizei count, GLsizei instancecount);
        void glDrawElementsInstanced(GLenum mode, GLsizei count, GLenum type, IntPtr indices, GLsizei instancecount);
        void glTexBuffer(GLenum target, GLenum internalformat, GLuint buffer);
        void glPrimitiveRestartIndex(GLuint index);
        void glCopyBufferSubData(GLenum readTarget, GLenum writeTarget, GLintptr readOffset, GLintptr writeOffset, GLsizeiptr size);
        void glGetUniformIndices(GLuint program, GLsizei uniformCount, string[] uniformNames, GLuint[] uniformIndices);
        void glGetActiveUniformsiv(GLuint program, GLsizei uniformCount, GLuint[] uniformIndices, GLenum pname, GLint[] @params);
        void glGetActiveUniformName(GLuint program, GLuint uniformIndex, GLsizei bufSize, out GLsizei length, string uniformName);
        GLuint glGetUniformBlockIndex(GLuint program, string uniformBlockName);
        void glGetActiveUniformBlockiv(GLuint program, GLuint uniformBlockIndex, GLenum pname, GLint[] @params);
        void glGetActiveUniformBlockName(GLuint program, GLuint uniformBlockIndex, GLsizei bufSize, GLsizei[] length, string uniformBlockName);
        void glUniformBlockBinding(GLuint program, GLuint uniformBlockIndex, GLuint uniformBlockBinding);

    }

    [GLVersion(3, 2)]
    public interface IOpenGL32 : IOpenGL31
    {
        void glDrawElementsBaseVertex(GLenum mode, GLsizei count, GLenum type, IntPtr indices, GLint basevertex);
        void glDrawRangeElementsBaseVertex(GLenum mode, GLuint start, GLuint end, GLsizei count, GLenum type, IntPtr indices, GLint basevertex);
        void glDrawElementsInstancedBaseVertex(GLenum mode, GLsizei count, GLenum type, IntPtr indices, GLsizei instancecount, GLint basevertex);
        void glMultiDrawElementsBaseVertex(GLenum mode, GLsizei[] count, GLenum type, IntPtr[] indices, GLsizei drawcount, GLint[] basevertex);
        void glProvokingVertex(GLenum mode);
        GLsync glFenceSync(GLenum condition, GLbitfield flags);
        GLboolean glIsSync(GLsync sync);
        void glDeleteSync(GLsync sync);
        GLenum glClientWaitSync(GLsync sync, GLbitfield flags, GLuint64 timeout);
        void glWaitSync(GLsync sync, GLbitfield flags, GLuint64 timeout);
        void glGetInteger64v(GLenum pname, GLint64[] @params);
        void glGetSynciv(GLsync sync, GLenum pname, GLsizei bufSize, GLsizei[] length, GLint[] values);
        void glGetInteger64i_v(GLenum target, GLuint index, GLint64[] data);
        void glGetBufferParameteri64v(GLenum target, GLenum pname, GLint64[] @params);
        void glFramebufferTexture(GLenum target, GLenum attachment, GLuint texture, GLint level);
        void glTexImage2DMultisample(GLenum target, GLsizei samples, GLint internalformat, GLsizei width, GLsizei height, GLboolean fixedsamplelocations);
        void glTexImage3DMultisample(GLenum target, GLsizei samples, GLint internalformat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedsamplelocations);
        void glGetMultisamplefv(GLenum pname, GLuint index, GLfloat[] val);
        void glSampleMaski(GLuint index, GLbitfield mask);
    }

    [GLVersion(3, 3)]
    public interface IOpenGL33 : IOpenGL32
    {
        void glBindFragDataLocationIndexed(GLuint program, GLuint colorNumber, GLuint index, string name);
        GLint glGetFragDataIndex(GLuint program, string name);
        void glGenSamplers(GLsizei count, GLuint[] samplers);
        void glDeleteSamplers(GLsizei count, GLuint[] samplers);
        GLboolean glIsSampler(GLuint sampler);
        void glBindSampler(GLuint unit, GLuint sampler);
        void glSamplerParameteri(GLuint sampler, GLenum pname, [In]GLint param);
        void glSamplerParameteriv(GLuint sampler, GLenum pname, [In]GLint[] param);
        void glSamplerParameterf(GLuint sampler, GLenum pname, GLfloat param);
        void glSamplerParameterfv(GLuint sampler, GLenum pname, [In]GLfloat[] param);
        void glSamplerParameterIiv(GLuint sampler, GLenum pname, [In]GLint[] param);
        void glSamplerParameterIuiv(GLuint sampler, GLenum pname, [In]GLuint[] param);
        void glGetSamplerParameteriv(GLuint sampler, GLenum pname, [Out]GLint[] @params);
        void glGetSamplerParameterIiv(GLuint sampler, GLenum pname, [Out]GLint[] @params);
        void glGetSamplerParameterfv(GLuint sampler, GLenum pname, [Out]GLfloat[] @params);
        void glGetSamplerParameterIuiv(GLuint sampler, GLenum pname, [Out]GLuint[] @params);
        void glQueryCounter(GLuint id, GLenum target);
        void glGetQueryObjecti64v(GLuint id, GLenum pname, GLint64[] @params);
        void glGetQueryObjectui64v(GLuint id, GLenum pname, GLuint64[] @params);
        void glVertexAttribDivisor(GLuint index, GLuint divisor);
        void glVertexAttribP1ui(GLuint index, GLenum type, GLboolean normalized, GLuint value);
        void glVertexAttribP1uiv(GLuint index, GLenum type, GLboolean normalized, [In]GLuint[] value);
        void glVertexAttribP2ui(GLuint index, GLenum type, GLboolean normalized, GLuint value);
        void glVertexAttribP2uiv(GLuint index, GLenum type, GLboolean normalized, [In]GLuint[] value);
        void glVertexAttribP3ui(GLuint index, GLenum type, GLboolean normalized, GLuint value);
        void glVertexAttribP3uiv(GLuint index, GLenum type, GLboolean normalized, [In]GLuint[] value);
        void glVertexAttribP4ui(GLuint index, GLenum type, GLboolean normalized, GLuint value);
        void glVertexAttribP4uiv(GLuint index, GLenum type, GLboolean normalized, [In]GLuint[] value);
    }

    [GLVersion(4, 0)]
    public interface IOpenGL40 : IOpenGL33
    {
void glMinSampleShading (GLfloat value);
void glBlendEquationi (GLuint buf, GLenum mode);
void glBlendEquationSeparatei (GLuint buf, GLenum modeRGB, GLenum modeAlpha);
void glBlendFunci (GLuint buf, GLenum src, GLenum dst);
void glBlendFuncSeparatei (GLuint buf, GLenum srcRGB, GLenum dstRGB, GLenum srcAlpha, GLenum dstAlpha);
void glDrawArraysIndirect (GLenum mode, IntPtr indirect);
void glDrawElementsIndirect (GLenum mode, GLenum type, [In]IntPtr indirect);
void glUniform1d (GLint location, GLdouble x);
void glUniform2d (GLint location, GLdouble x, GLdouble y);
void glUniform3d (GLint location, GLdouble x, GLdouble y, GLdouble z);
void glUniform4d (GLint location, GLdouble x, GLdouble y, GLdouble z, GLdouble w);
void glUniform1dv (GLint location, GLsizei count, [In] GLdouble[] value);
void glUniform2dv (GLint location, GLsizei count, [In] GLdouble[] value);
void glUniform3dv (GLint location, GLsizei count, [In] GLdouble[] value);
void glUniform4dv (GLint location, GLsizei count, [In] GLdouble[] value);
void glUniformMatrix2dv (GLint location, GLsizei count, GLboolean transpose, [In] GLdouble[] value);
void glUniformMatrix3dv (GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
void glUniformMatrix4dv (GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
void glUniformMatrix2x3dv (GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
void glUniformMatrix2x4dv (GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
void glUniformMatrix3x2dv (GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
void glUniformMatrix3x4dv (GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
void glUniformMatrix4x2dv (GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
void glUniformMatrix4x3dv (GLint location, GLsizei count, GLboolean transpose, [In]GLdouble[] value);
void glGetUniformdv (GLuint program, GLint location, [Out]GLdouble @params);
GLint glGetSubroutineUniformLocation (GLuint program, GLenum shadertype, [In] string name);
GLuint glGetSubroutineIndex (GLuint program, GLenum shadertype, [In] string name);
void glGetActiveSubroutineUniformiv (GLuint program, GLenum shadertype, GLuint index, GLenum pname, [Out]GLint[] values);
void glGetActiveSubroutineUniformName (GLuint program, GLenum shadertype, GLuint index, GLsizei bufsize, [Out]GLsizei[] length, [Out]GLchar[] name);
void glGetActiveSubroutineName (GLuint program, GLenum shadertype, GLuint index, GLsizei bufsize, [Out] out GLsizei length, [Out] string name);
void glUniformSubroutinesuiv (GLenum shadertype, GLsizei count, [In]GLuint[] indices);
void glGetUniformSubroutineuiv (GLenum shadertype, GLint location, [Out]GLuint[] @params);
void glGetProgramStageiv (GLuint program, GLenum shadertype, GLenum pname, [Out]GLint[] values);
void glPatchParameteri (GLenum pname, GLint value);
void glPatchParameterfv (GLenum pname, [In]GLfloat[] values);
void glBindTransformFeedback (GLenum target, GLuint id);
void glDeleteTransformFeedbacks (GLsizei n, [In] GLuint[] ids);
void glGenTransformFeedbacks (GLsizei n, [Out]GLuint[] ids);
GLboolean glIsTransformFeedback (GLuint id);
void glPauseTransformFeedback ();
void glResumeTransformFeedback ();
void glDrawTransformFeedback (GLenum mode, GLuint id);
void glDrawTransformFeedbackStream (GLenum mode, GLuint id, GLuint stream);
void glBeginQueryIndexed (GLenum target, GLuint index, GLuint id);
void glEndQueryIndexed (GLenum target, GLuint index);
void glGetQueryIndexediv (GLenum target, GLuint index, GLenum pname, [Out]GLint[] @params);
    }

    [GLVersion(4, 1)]
    public interface IOpenGL41 : IOpenGL40
    {

    }

    [GLVersion(4, 2)]
    public interface IOpenGL42 : IOpenGL41
    {

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

    public static class GL
    {
        [DllImport(GLLibraryName)]
        public static extern void glCullFace(GLenum mode);
        [DllImport(GLLibraryName)]
        public static extern void glFrontFace(GLenum mode);
        [DllImport(GLLibraryName)]
        public static extern void glHint(GLenum target, GLenum mode);
        [DllImport(GLLibraryName)]
        public static extern void glLineWidth(GLfloat width);
        [DllImport(GLLibraryName)]
        public static extern void glPointSize(GLfloat size);
        [DllImport(GLLibraryName)]
        public static extern void glPolygonMode(GLenum face, GLenum mode);
        [DllImport(GLLibraryName)]
        public static extern void glScissor(GLint x, GLint y, GLsizei width, GLsizei height);
        [DllImport(GLLibraryName)]
        public static extern void glTexParameterf(GLenum target, GLenum pname, GLfloat param);
        [DllImport(GLLibraryName)]
        public static extern void glTexParameterfv(GLenum target, GLenum pname, GLfloat[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glTexParameteri(GLenum target, GLenum pname, GLint param);
        [DllImport(GLLibraryName)]
        public static extern void glTexParameteriv(GLenum target, GLenum pname, GLint[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glTexImage1D(TextureTarget target, GLint level, TextureInternalFormat internalformat, GLsizei width, GLint border, TextureFormat format, TexturePixelType type, IntPtr pixels);
        [DllImport(GLLibraryName)]
        public static extern void glTexImage2D(TextureTarget target, GLint level, TextureInternalFormat internalformat, GLsizei width, GLsizei height, GLint border, TextureFormat format, TexturePixelType type, IntPtr pixels);
        [DllImport(GLLibraryName)]
        public static extern void glDrawBuffer(GLenum mode);
        [DllImport(GLLibraryName)]
        public static extern void glClear(ClearTarget mask);
        [DllImport(GLLibraryName)]
        public static extern void glClearColor(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);
        [DllImport(GLLibraryName)]
        public static extern void glClearStencil(GLint s);
        [DllImport(GLLibraryName)]
        public static extern void glClearDepth(GLdouble depth);
        [DllImport(GLLibraryName)]
        public static extern void glStencilMask(GLuint mask);
        [DllImport(GLLibraryName)]
        public static extern void glColorMask(GLboolean red, GLboolean green, GLboolean blue, GLboolean alpha);
        [DllImport(GLLibraryName)]
        public static extern void glDepthMask(GLboolean flag);
        [DllImport(GLLibraryName)]
        public static extern void glDisable(StateCaps cap);
        [DllImport(GLLibraryName)]
        public static extern void glEnable(StateCaps cap);
        [DllImport(GLLibraryName)]
        public static extern void glFinish();
        [DllImport(GLLibraryName)]
        public static extern void glFlush();
        [DllImport(GLLibraryName)]
        public static extern void glBlendFunc(GLenum sfactor, GLenum dfactor);
        [DllImport(GLLibraryName)]
        public static extern void glLogicOp(GLenum opcode);
        [DllImport(GLLibraryName)]
        public static extern void glStencilFunc(GLenum func, GLint @ref, GLuint mask);
        [DllImport(GLLibraryName)]
        public static extern void glStencilOp(GLenum fail, GLenum zfail, GLenum zpass);
        [DllImport(GLLibraryName)]
        public static extern void glDepthFunc(GLenum func);
        [DllImport(GLLibraryName)]
        public static extern void glPixelStoref(GLenum pname, GLfloat param);
        [DllImport(GLLibraryName)]
        public static extern void glPixelStorei(GLenum pname, GLint param);
        [DllImport(GLLibraryName)]
        public static extern void glReadBuffer(GLenum mode);
        [DllImport(GLLibraryName)]
        public static extern void glReadPixels(GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, IntPtr pixels);
        [DllImport(GLLibraryName)]
        public static extern void glGetBooleanv(GLenum pname, GLboolean[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glGetDoublev(GLenum pname, GLdouble[] @params);
        [DllImport(GLLibraryName)]
        public static extern GLenum glGetError();
        [DllImport(GLLibraryName)]
        public static extern void glGetFloatv(GLenum pname, GLfloat[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glGetIntegerv(GLenum pname, GLint[] @params);
        [DllImport(GLLibraryName)]
        public static extern string glGetString(GLenum name);
        [DllImport(GLLibraryName)]
        public static extern void glGetTexImage(GLenum target, GLint level, GLenum format, GLenum type, IntPtr pixels);
        [DllImport(GLLibraryName)]
        public static extern void glGetTexParameterfv(GLenum target, GLenum pname, GLfloat[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glGetTexParameteriv(GLenum target, GLenum pname, GLint[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glGetTexLevelParameterfv(GLenum target, GLint level, GLenum pname, GLfloat[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glGetTexLevelParameteriv(GLenum target, GLint level, GLenum pname, GLint[] @params);
        [DllImport(GLLibraryName)]
        public static extern GLboolean glIsEnabled(GLenum cap);
        [DllImport(GLLibraryName)]
        public static extern void glDepthRange(GLdouble near, GLdouble far);
        [DllImport(GLLibraryName)]
        public static extern void glViewport(GLint x, GLint y, GLsizei width, GLsizei height);

        /*
        [DllImport(GLLibraryName)]
        public static extern void glColorMaski(GLuint index, GLboolean r, GLboolean g, GLboolean b, GLboolean a);
        [DllImport(GLLibraryName)]
        public static extern void glGetBooleani_v(GLenum target, GLuint index, GLboolean[] data);
        [DllImport(GLLibraryName)]
        public static extern void glGetIntegeri_v(GLenum target, GLuint index, GLint[] data);
        [DllImport(GLLibraryName)]
        public static extern void glEnablei(GLenum target, GLuint index);
        [DllImport(GLLibraryName)]
        public static extern void glDisablei(GLenum target, GLuint index);
        [DllImport(GLLibraryName)]
        public static extern GLboolean glIsEnabledi(GLenum target, GLuint index);
        [DllImport(GLLibraryName)]
        public static extern void glBeginTransformFeedback(GLenum primitiveMode);
        [DllImport(GLLibraryName)]
        public static extern void glEndTransformFeedback();
        [DllImport(GLLibraryName)]
        public static extern void glBindBufferRange(GLenum target, GLuint index, GLuint buffer, GLintptr offset, GLsizeiptr size);
        [DllImport(GLLibraryName)]
        public static extern void glBindBufferBase(GLenum target, GLuint index, GLuint buffer);
        [DllImport(GLLibraryName)]
        public static extern void glTransformFeedbackVaryings(GLuint program, GLsizei count, string[] varyings, GLenum bufferMode);
        [DllImport(GLLibraryName)]
        public static extern void glGetTransformFeedbackVarying(GLuint program, GLuint index, GLsizei bufSize, GLsizei[] length, GLsizei[] size, GLenum[] type, GLchar[] name);
        [DllImport(GLLibraryName)]
        public static extern void glClampColor(GLenum target, GLenum clamp);
        [DllImport(GLLibraryName)]
        public static extern void glBeginConditionalRender(GLuint id, GLenum mode);
        [DllImport(GLLibraryName)]
        public static extern void glEndConditionalRender();
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribIPointer(GLuint index, GLint size, GLenum type, GLsizei stride, IntPtr pointer);
        [DllImport(GLLibraryName)]
        public static extern void glGetVertexAttribIiv(GLuint index, GLenum pname, GLint[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glGetVertexAttribIuiv(GLuint index, GLenum pname, GLuint[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI1i(GLuint index, GLint x);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI2i(GLuint index, GLint x, GLint y);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI3i(GLuint index, GLint x, GLint y, GLint z);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI4i(GLuint index, GLint x, GLint y, GLint z, GLint w);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI1ui(GLuint index, GLuint x);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI2ui(GLuint index, GLuint x, GLuint y);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI3ui(GLuint index, GLuint x, GLuint y, GLuint z);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI4ui(GLuint index, GLuint x, GLuint y, GLuint z, GLuint w);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI1iv(GLuint index, GLint[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI2iv(GLuint index, GLint[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI3iv(GLuint index, GLint[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI4iv(GLuint index, GLint[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI1uiv(GLuint index, GLuint[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI2uiv(GLuint index, GLuint[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI3uiv(GLuint index, GLuint[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI4uiv(GLuint index, GLuint[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI4bv(GLuint index, GLbyte[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI4sv(GLuint index, GLshort[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI4ubv(GLuint index, GLubyte[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribI4usv(GLuint index, GLushort[] v);
        [DllImport(GLLibraryName)]
        public static extern void glGetUniformuiv(GLuint program, GLint location, GLuint[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glBindFragDataLocation(GLuint program, GLuint color, string name);
        [DllImport(GLLibraryName)]
        public static extern GLint glGetFragDataLocation(GLuint program, string name);
        [DllImport(GLLibraryName)]
        public static extern void glUniform1ui(GLint location, GLuint v0);
        [DllImport(GLLibraryName)]
        public static extern void glUniform2ui(GLint location, GLuint v0, GLuint v1);
        [DllImport(GLLibraryName)]
        public static extern void glUniform3ui(GLint location, GLuint v0, GLuint v1, GLuint v2);
        [DllImport(GLLibraryName)]
        public static extern void glUniform4ui(GLint location, GLuint v0, GLuint v1, GLuint v2, GLuint v3);
        [DllImport(GLLibraryName)]
        public static extern void glUniform1uiv(GLint location, GLsizei count, GLuint[] value);
        [DllImport(GLLibraryName)]
        public static extern void glUniform2uiv(GLint location, GLsizei count, GLuint[] value);
        [DllImport(GLLibraryName)]
        public static extern void glUniform3uiv(GLint location, GLsizei count, GLuint[] value);
        [DllImport(GLLibraryName)]
        public static extern void glUniform4uiv(GLint location, GLsizei count, GLuint[] value);
        [DllImport(GLLibraryName)]
        public static extern void glTexParameterIiv(GLenum target, GLenum pname, GLint[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glTexParameterIuiv(GLenum target, GLenum pname, GLuint[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glGetTexParameterIiv(GLenum target, GLenum pname, GLint[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glGetTexParameterIuiv(GLenum target, GLenum pname, GLuint[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glClearBufferiv(GLenum buffer, GLint drawbuffer, GLint[] value);
        [DllImport(GLLibraryName)]
        public static extern void glClearBufferuiv(GLenum buffer, GLint drawbuffer, GLuint[] value);
        [DllImport(GLLibraryName)]
        public static extern void glClearBufferfv(GLenum buffer, GLint drawbuffer, GLfloat[] value);
        [DllImport(GLLibraryName)]
        public static extern void glClearBufferfi(GLenum buffer, GLint drawbuffer, GLfloat depth, GLint stencil);
        [DllImport(GLLibraryName)]
        public static extern string glGetStringi(GLenum name, GLuint index);
        [DllImport(GLLibraryName)]
        public static extern void glUniformMatrix2x3fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
        [DllImport(GLLibraryName)]
        public static extern void glUniformMatrix3x2fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
        [DllImport(GLLibraryName)]
        public static extern void glUniformMatrix2x4fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
        [DllImport(GLLibraryName)]
        public static extern void glUniformMatrix4x2fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
        [DllImport(GLLibraryName)]
        public static extern void glUniformMatrix3x4fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
        [DllImport(GLLibraryName)]
        public static extern void glUniformMatrix4x3fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
        [DllImport(GLLibraryName)]
        public static extern void glBlendEquationSeparate(GLenum modeRGB, GLenum modeAlpha);
        [DllImport(GLLibraryName)]
        public static extern void glDrawBuffers(GLsizei n, GLenum[] bufs);
        [DllImport(GLLibraryName)]
        public static extern void glStencilOpSeparate(GLenum face, GLenum sfail, GLenum dpfail, GLenum dppass);
        [DllImport(GLLibraryName)]
        public static extern void glStencilFuncSeparate(GLenum face, GLenum func, GLint @ref, GLuint mask);
        [DllImport(GLLibraryName)]
        public static extern void glStencilMaskSeparate(GLenum face, GLuint mask);
        [DllImport(GLLibraryName)]
        public static extern void glAttachShader(GLuint program, GLuint shader);
        [DllImport(GLLibraryName)]
        public static extern void glBindAttribLocation(GLuint program, GLuint index, string name);
        [DllImport(GLLibraryName)]
        public static extern void glCompileShader(GLuint shader);
        [DllImport(GLLibraryName)]
        public static extern GLuint glCreateProgram();
        [DllImport(GLLibraryName)]
        public static extern GLuint glCreateShader(GLenum type);
        [DllImport(GLLibraryName)]
        public static extern void glDeleteProgram(GLuint program);
        [DllImport(GLLibraryName)]
        public static extern void glDeleteShader(GLuint shader);
        [DllImport(GLLibraryName)]
        public static extern void glDetachShader(GLuint program, GLuint shader);
        [DllImport(GLLibraryName)]
        public static extern void glDisableVertexAttribArray(GLuint index);
        [DllImport(GLLibraryName)]
        public static extern void glEnableVertexAttribArray(GLuint index);
        [DllImport(GLLibraryName)]
        public static extern void glGetActiveAttrib(GLuint program, GLuint index, GLsizei bufSize, GLsizei[] length, GLint[] size, GLenum[] type, GLchar[] name);
        [DllImport(GLLibraryName)]
        public static extern void glGetActiveUniform(GLuint program, GLuint index, GLsizei bufSize, GLsizei[] length, GLint[] size, GLenum[] type, GLchar[] name);
        [DllImport(GLLibraryName)]
        public static extern void glGetAttachedShaders(GLuint program, GLsizei maxCount, GLsizei[] count, GLuint[] obj);
        [DllImport(GLLibraryName)]
        public static extern GLint glGetAttribLocation(GLuint program, string name);
        [DllImport(GLLibraryName)]
        public static extern void glGetProgramiv(GLuint program, GLenum pname, GLint[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glGetProgramInfoLog(GLuint program, GLsizei bufSize, GLsizei[] length, GLchar[] infoLog);
        [DllImport(GLLibraryName)]
        public static extern void glGetShaderiv(GLuint shader, GLenum pname, GLint[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glGetShaderInfoLog(GLuint shader, GLsizei bufSize, GLsizei[] length, string infoLog);
        [DllImport(GLLibraryName)]
        public static extern void glGetShaderSource(GLuint shader, GLsizei bufSize, GLsizei[] length, string source);
        [DllImport(GLLibraryName)]
        public static extern GLint glGetUniformLocation(GLuint program, string name);
        [DllImport(GLLibraryName)]
        public static extern void glGetUniformfv(GLuint program, GLint location, GLfloat[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glGetUniformiv(GLuint program, GLint location, GLint[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glGetVertexAttribdv(GLuint index, GLenum pname, GLdouble[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glGetVertexAttribfv(GLuint index, GLenum pname, GLfloat[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glGetVertexAttribiv(GLuint index, GLenum pname, GLint[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glGetVertexAttribPointerv(GLuint index, GLenum pname, IntPtr[] pointer);
        [DllImport(GLLibraryName)]
        public static extern GLboolean glIsProgram(GLuint program);
        [DllImport(GLLibraryName)]
        public static extern GLboolean glIsShader(GLuint shader);
        [DllImport(GLLibraryName)]
        public static extern void glLinkProgram(GLuint program);
        [DllImport(GLLibraryName)]
        public static extern void glShaderSource(GLuint shader, GLsizei count, string[] @string, GLint[] length);
        [DllImport(GLLibraryName)]
        public static extern void glUseProgram(GLuint program);
        [DllImport(GLLibraryName)]
        public static extern void glUniform1f(GLint location, GLfloat v0);
        [DllImport(GLLibraryName)]
        public static extern void glUniform2f(GLint location, GLfloat v0, GLfloat v1);
        [DllImport(GLLibraryName)]
        public static extern void glUniform3f(GLint location, GLfloat v0, GLfloat v1, GLfloat v2);
        [DllImport(GLLibraryName)]
        public static extern void glUniform4f(GLint location, GLfloat v0, GLfloat v1, GLfloat v2, GLfloat v3);
        [DllImport(GLLibraryName)]
        public static extern void glUniform1i(GLint location, GLint v0);
        [DllImport(GLLibraryName)]
        public static extern void glUniform2i(GLint location, GLint v0, GLint v1);
        [DllImport(GLLibraryName)]
        public static extern void glUniform3i(GLint location, GLint v0, GLint v1, GLint v2);
        [DllImport(GLLibraryName)]
        public static extern void glUniform4i(GLint location, GLint v0, GLint v1, GLint v2, GLint v3);
        [DllImport(GLLibraryName)]
        public static extern void glUniform1fv(GLint location, GLsizei count, GLfloat[] value);
        [DllImport(GLLibraryName)]
        public static extern void glUniform2fv(GLint location, GLsizei count, GLfloat[] value);
        [DllImport(GLLibraryName)]
        public static extern void glUniform3fv(GLint location, GLsizei count, GLfloat[] value);
        [DllImport(GLLibraryName)]
        public static extern void glUniform4fv(GLint location, GLsizei count, GLfloat[] value);
        [DllImport(GLLibraryName)]
        public static extern void glUniform1iv(GLint location, GLsizei count, GLint[] value);
        [DllImport(GLLibraryName)]
        public static extern void glUniform2iv(GLint location, GLsizei count, GLint[] value);
        [DllImport(GLLibraryName)]
        public static extern void glUniform3iv(GLint location, GLsizei count, GLint[] value);
        [DllImport(GLLibraryName)]
        public static extern void glUniform4iv(GLint location, GLsizei count, GLint[] value);
        [DllImport(GLLibraryName)]
        public static extern void glUniformMatrix2fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
        [DllImport(GLLibraryName)]
        public static extern void glUniformMatrix3fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
        [DllImport(GLLibraryName)]
        public static extern void glUniformMatrix4fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
        [DllImport(GLLibraryName)]
        public static extern void glValidateProgram(GLuint program);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib1d(GLuint index, GLdouble x);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib1dv(GLuint index, GLdouble[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib1f(GLuint index, GLfloat x);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib1fv(GLuint index, GLfloat[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib1s(GLuint index, GLshort x);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib1sv(GLuint index, GLshort[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib2d(GLuint index, GLdouble x, GLdouble y);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib2dv(GLuint index, GLdouble[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib2f(GLuint index, GLfloat x, GLfloat y);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib2fv(GLuint index, GLfloat[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib2s(GLuint index, GLshort x, GLshort y);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib2sv(GLuint index, GLshort[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib3d(GLuint index, GLdouble x, GLdouble y, GLdouble z);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib3dv(GLuint index, GLdouble[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib3f(GLuint index, GLfloat x, GLfloat y, GLfloat z);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib3fv(GLuint index, GLfloat[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib3s(GLuint index, GLshort x, GLshort y, GLshort z);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib3sv(GLuint index, GLshort[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib4Nbv(GLuint index, GLbyte[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib4Niv(GLuint index, GLint[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib4Nsv(GLuint index, GLshort[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib4Nub(GLuint index, GLubyte x, GLubyte y, GLubyte z, GLubyte w);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib4Nubv(GLuint index, GLubyte[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib4Nuiv(GLuint index, GLuint[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib4Nusv(GLuint index, GLushort[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib4bv(GLuint index, GLbyte[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib4d(GLuint index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib4dv(GLuint index, GLdouble[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib4f(GLuint index, GLfloat x, GLfloat y, GLfloat z, GLfloat w);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib4fv(GLuint index, GLfloat[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib4iv(GLuint index, GLint[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib4s(GLuint index, GLshort x, GLshort y, GLshort z, GLshort w);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib4sv(GLuint index, GLshort[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib4ubv(GLuint index, GLubyte[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib4uiv(GLuint index, GLuint[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttrib4usv(GLuint index, GLushort[] v);
        [DllImport(GLLibraryName)]
        public static extern void glVertexAttribPointer(GLuint index, GLint size, GLenum type, GLboolean normalized, GLsizei stride, IntPtr pointer);

        [DllImport(GLLibraryName)]
        public static extern void glGenQueries(GLsizei n, GLuint[] ids);
        [DllImport(GLLibraryName)]
        public static extern void glDeleteQueries(GLsizei n, GLuint[] ids);
        [DllImport(GLLibraryName)]
        public static extern GLboolean glIsQuery(GLuint id);
        [DllImport(GLLibraryName)]
        public static extern void glBeginQuery(GLenum target, GLuint id);
        [DllImport(GLLibraryName)]
        public static extern void glEndQuery(GLenum target);
        [DllImport(GLLibraryName)]
        public static extern void glGetQueryiv(GLenum target, GLenum pname, GLint[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glGetQueryObjectiv(GLuint id, GLenum pname, GLint[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glGetQueryObjectuiv(GLuint id, GLenum pname, GLuint[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glBindBuffer(GLenum target, GLuint buffer);
        [DllImport(GLLibraryName)]
        public static extern void glDeleteBuffers(GLsizei n, GLuint[] buffers);
        [DllImport(GLLibraryName)]
        public static extern void glGenBuffers(GLsizei n, GLuint[] buffers);
        [DllImport(GLLibraryName)]
        public static extern GLboolean glIsBuffer(GLuint buffer);
        [DllImport(GLLibraryName)]
        public static extern void glBufferData(GLenum target, GLsizeiptr size, IntPtr data, GLenum usage);
        [DllImport(GLLibraryName)]
        public static extern void glBufferSubData(GLenum target, GLintptr offset, GLsizeiptr size, IntPtr data);
        [DllImport(GLLibraryName)]
        public static extern void glGetBufferSubData(GLenum target, GLintptr offset, GLsizeiptr size, IntPtr data);
        [DllImport(GLLibraryName)]
        public static extern IntPtr glMapBuffer(GLenum target, GLenum access);
        [DllImport(GLLibraryName)]
        public static extern GLboolean glUnmapBuffer(GLenum target);
        [DllImport(GLLibraryName)]
        public static extern void glGetBufferParameteriv(GLenum target, GLenum pname, GLint[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glGetBufferPointerv(GLenum target, GLenum pname, IntPtr[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glBlendFuncSeparate(GLenum sfactorRGB, GLenum dfactorRGB, GLenum sfactorAlpha, GLenum dfactorAlpha);
        [DllImport(GLLibraryName)]
        public static extern void glMultiDrawArrays(GLenum mode, GLint[] first, GLsizei[] count, GLsizei drawcount);
        [DllImport(GLLibraryName)]
        public static extern void glMultiDrawElements(GLenum mode, GLsizei[] count, GLenum type, IntPtr[] indices, GLsizei drawcount);
        [DllImport(GLLibraryName)]
        public static extern void glPointParameterf(GLenum pname, GLfloat param);
        [DllImport(GLLibraryName)]
        public static extern void glPointParameterfv(GLenum pname, GLfloat[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glPointParameteri(GLenum pname, GLint param);
        [DllImport(GLLibraryName)]
        public static extern void glPointParameteriv(GLenum pname, GLint[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glActiveTexture(GLenum texture);
        [DllImport(GLLibraryName)]
        public static extern void glSampleCoverage(GLfloat value, GLboolean invert);
        [DllImport(GLLibraryName)]
        public static extern void glCompressedTexImage3D(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLsizei imageSize, IntPtr data);
        [DllImport(GLLibraryName)]
        public static extern void glCompressedTexImage2D(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLint border, GLsizei imageSize, IntPtr data);
        [DllImport(GLLibraryName)]
        public static extern void glCompressedTexImage1D(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLint border, GLsizei imageSize, IntPtr data);
        [DllImport(GLLibraryName)]
        public static extern void glCompressedTexSubImage3D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLsizei imageSize, IntPtr data);
        [DllImport(GLLibraryName)]
        public static extern void glCompressedTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLsizei imageSize, IntPtr data);
        [DllImport(GLLibraryName)]
        public static extern void glCompressedTexSubImage1D(GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLsizei imageSize, IntPtr data);
        [DllImport(GLLibraryName)]
        public static extern void glGetCompressedTexImage(GLenum target, GLint level, IntPtr img);
        [DllImport(GLLibraryName)]
        public static extern void glBlendColor(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);
        [DllImport(GLLibraryName)]
        public static extern void glBlendEquation(GLenum mode);
        [DllImport(GLLibraryName)]
        public static extern void glDrawRangeElements(GLenum mode, GLuint start, GLuint end, GLsizei count, GLenum type, IntPtr indices);
        [DllImport(GLLibraryName)]
        public static extern void glTexImage3D(GLenum target, GLint level, GLint internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLenum format, GLenum type, IntPtr pixels);
        [DllImport(GLLibraryName)]
        public static extern void glTexSubImage3D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLenum type, IntPtr pixels);
        [DllImport(GLLibraryName)]
        public static extern void glCopyTexSubImage3D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLint x, GLint y, GLsizei width, GLsizei height);
 * */

        [System.Diagnostics.DebuggerHidden]
        [System.Diagnostics.DebuggerStepThrough]
        public static void HandleOpenGLError()
        {
            GLenum error = glGetError();
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
            glGetError();
        }

        public static readonly ErrorHandling OpenGLErrorFunctions = new ErrorHandling
            {
                FlushError = typeof(GL).GetMethod("FlushOpenGLError", BindingFlags.Public | BindingFlags.Static),
                CheckErrorState = typeof(GL).GetMethod("HandleOpenGLError", BindingFlags.Public | BindingFlags.Static)
            };

        [DllImport(GLLibraryName)]
        public static extern void glDrawArrays(DrawMode mode, GLint first, GLsizei count);
        [DllImport(GLLibraryName)]
        public static extern void glDrawElements(DrawMode mode, GLsizei count, ElementBufferItemType type, IntPtr indices);
        [DllImport(GLLibraryName)]
        public static extern void glGetPointerv(GLenum pname, IntPtr[] @params);
        [DllImport(GLLibraryName)]
        public static extern void glPolygonOffset(GLfloat factor, GLfloat units);
        [DllImport(GLLibraryName)]
        public static extern void glCopyTexImage1D(GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLint border);
        [DllImport(GLLibraryName)]
        public static extern void glCopyTexImage2D(GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLsizei height, GLint border);
        [DllImport(GLLibraryName)]
        public static extern void glCopyTexSubImage1D(GLenum target, GLint level, GLint xoffset, GLint x, GLint y, GLsizei width);
        [DllImport(GLLibraryName)]
        public static extern void glCopyTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint x, GLint y, GLsizei width, GLsizei height);
        [DllImport(GLLibraryName)]
        public static extern void glTexSubImage1D(GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLenum type, IntPtr pixels);
        [DllImport(GLLibraryName)]
        public static extern void glTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLenum type, IntPtr pixels);
        [DllImport(GLLibraryName)]
        public static extern void glBindTexture(TextureTarget target, GLuint texture);
        [DllImport(GLLibraryName)]
        public static extern void glDeleteTextures(GLsizei n, GLuint[] textures);
        [DllImport(GLLibraryName)]
        public static extern void glGenTextures(GLsizei n, GLuint[] textures);
        [DllImport(GLLibraryName)]
        public static extern GLboolean glIsTexture(GLuint texture);


        public const string GLLibraryName = "opengl32";

        private static TDelegate GetFunction<TDelegate>(string functionName)
        {
            throw new NotImplementedException();
        }
    }
}
