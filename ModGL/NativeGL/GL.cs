using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

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
using GLhalf = System.UInt16;
using GLfloat = System.Single;
using GLclampf = System.Single;
using GLdouble = System.Double;
using GLclampd = System.Double;
using GLvoid = System.Void;
using GLintptr = System.IntPtr;
using GLsizeiptr = System.IntPtr;
using GLhandle = System.UInt32;


namespace ModGL.NativeGL
{
    // ReSharper disable InconsistentNaming
    /*
    
    public delegate void GLDEBUGPROC(GLenum source, GLenum type, GLuint id, GLenum severity, GLsizei length, string message, IntPtr userParam);
    public delegate void PFNGLCULLFACEPROC(GLenum mode);
    public delegate void PFNGLFRONTFACEPROC(GLenum mode);
    public delegate void PFNGLHINTPROC(GLenum target, GLenum mode);
    public delegate void PFNGLLINEWIDTHPROC(GLfloat width);
    public delegate void PFNGLPOINTSIZEPROC(GLfloat size);
    public delegate void PFNGLPOLYGONMODEPROC(GLenum face, GLenum mode);
    public delegate void PFNGLSCISSORPROC(GLint x, GLint y, GLsizei width, GLsizei height);
    public delegate void PFNGLTEXPARAMETERFPROC(GLenum target, GLenum pname, GLfloat param);
    public delegate void PFNGLTEXPARAMETERFVPROC(GLenum target, GLenum pname, GLfloat[] @params);
    public delegate void PFNGLTEXPARAMETERIPROC(GLenum target, GLenum pname, GLint param);
    public delegate void PFNGLTEXPARAMETERIVPROC(GLenum target, GLenum pname, GLint[] @params);
    public delegate void PFNGLTEXIMAGE1DPROC(GLenum target, GLint level, GLint internalformat, GLsizei width, GLint border, GLenum format, GLenum type, IntPtr pixels);
    public delegate void PFNGLTEXIMAGE2DPROC(GLenum target, GLint level, GLint internalformat, GLsizei width, GLsizei height, GLint border, GLenum format, GLenum type, IntPtr pixels);
    public delegate void PFNGLDRAWBUFFERPROC(GLenum mode);
    public delegate void PFNGLCLEARPROC(GLbitfield mask);
    public delegate void PFNGLCLEARCOLORPROC(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);
    public delegate void PFNGLCLEARSTENCILPROC(GLint s);
    public delegate void PFNGLCLEARDEPTHPROC(GLdouble depth);
    public delegate void PFNGLSTENCILMASKPROC(GLuint mask);
    public delegate void PFNGLCOLORMASKPROC(GLboolean red, GLboolean green, GLboolean blue, GLboolean alpha);
    public delegate void PFNGLDEPTHMASKPROC(GLboolean flag);
    public delegate void PFNGLDISABLEPROC(GLenum cap);
    public delegate void PFNGLENABLEPROC(GLenum cap);
    public delegate void PFNGLFINISHPROC();
    public delegate void PFNGLFLUSHPROC();
    public delegate void PFNGLBLENDFUNCPROC(GLenum sfactor, GLenum dfactor);
    public delegate void PFNGLLOGICOPPROC(GLenum opcode);
    public delegate void PFNGLSTENCILFUNCPROC(GLenum func, GLint @ref, GLuint mask);
    public delegate void PFNGLSTENCILOPPROC(GLenum fail, GLenum zfail, GLenum zpass);
    public delegate void PFNGLDEPTHFUNCPROC(GLenum func);
    public delegate void PFNGLPIXELSTOREFPROC(GLenum pname, GLfloat param);
    public delegate void PFNGLPIXELSTOREIPROC(GLenum pname, GLint param);
    public delegate void PFNGLREADBUFFERPROC(GLenum mode);
    public delegate void PFNGLREADPIXELSPROC(GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, IntPtr pixels);
    public delegate void PFNGLGETBOOLEANVPROC(GLenum pname, GLboolean[] @params);
    public delegate void PFNGLGETDOUBLEVPROC(GLenum pname, GLdouble[] @params);
    public delegate GLenum PFNGLGETERRORPROC();
    public delegate void PFNGLGETFLOATVPROC(GLenum pname, GLfloat[] @params);
    public delegate void PFNGLGETINTEGERVPROC(GLenum pname, GLint[] @params);
    public delegate string PFNGLGETSTRINGPROC(GLenum name);
    public delegate void PFNGLGETTEXIMAGEPROC(GLenum target, GLint level, GLenum format, GLenum type, IntPtr pixels);
    public delegate void PFNGLGETTEXPARAMETERFVPROC(GLenum target, GLenum pname, GLfloat[] @params);
    public delegate void PFNGLGETTEXPARAMETERIVPROC(GLenum target, GLenum pname, GLint[] @params);
    public delegate void PFNGLGETTEXLEVELPARAMETERFVPROC(GLenum target, GLint level, GLenum pname, GLfloat[] @params);
    public delegate void PFNGLGETTEXLEVELPARAMETERIVPROC(GLenum target, GLint level, GLenum pname, GLint[] @params);
    public delegate GLboolean PFNGLISENABLEDPROC(GLenum cap);
    public delegate void PFNGLDEPTHRANGEPROC(GLdouble near, GLdouble far);
    public delegate void PFNGLVIEWPORTPROC(GLint x, GLint y, GLsizei width, GLsizei height);



    public delegate void PFNGLDRAWARRAYSPROC(GLenum mode, GLint first, GLsizei count);
    public delegate void PFNGLDRAWELEMENTSPROC(GLenum mode, GLsizei count, GLenum type, IntPtr indices);
    public delegate void PFNGLGETPOINTERVPROC(GLenum pname, IntPtr[] @params);
    public delegate void PFNGLPOLYGONOFFSETPROC(GLfloat factor, GLfloat units);
    public delegate void PFNGLCOPYTEXIMAGE1DPROC(GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLint border);
    public delegate void PFNGLCOPYTEXIMAGE2DPROC(GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLsizei height, GLint border);
    public delegate void PFNGLCOPYTEXSUBIMAGE1DPROC(GLenum target, GLint level, GLint xoffset, GLint x, GLint y, GLsizei width);
    public delegate void PFNGLCOPYTEXSUBIMAGE2DPROC(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint x, GLint y, GLsizei width, GLsizei height);
    public delegate void PFNGLTEXSUBIMAGE1DPROC(GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLenum type, IntPtr pixels);
    public delegate void PFNGLTEXSUBIMAGE2DPROC(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLenum type, IntPtr pixels);
    public delegate void PFNGLBINDTEXTUREPROC(GLenum target, GLuint texture);
    public delegate void PFNGLDELETETEXTURESPROC(GLsizei n, GLuint[] textures);
    public delegate void PFNGLGENTEXTURESPROC(GLsizei n, GLuint[] textures);
    public delegate GLboolean PFNGLISTEXTUREPROC(GLuint texture);



    public delegate void PFNGLBLENDCOLORPROC(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);
    public delegate void PFNGLBLENDEQUATIONPROC(GLenum mode);
    public delegate void PFNGLDRAWRANGEELEMENTSPROC(GLenum mode, GLuint start, GLuint end, GLsizei count, GLenum type, IntPtr indices);
    public delegate void PFNGLTEXIMAGE3DPROC(GLenum target, GLint level, GLint internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLenum format, GLenum type, IntPtr pixels);
    public delegate void PFNGLTEXSUBIMAGE3DPROC(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLenum type, IntPtr pixels);
    public delegate void PFNGLCOPYTEXSUBIMAGE3DPROC(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLint x, GLint y, GLsizei width, GLsizei height);





    public delegate void PFNGLACTIVETEXTUREPROC(GLenum texture);
    public delegate void PFNGLSAMPLECOVERAGEPROC(GLfloat value, GLboolean invert);
    public delegate void PFNGLCOMPRESSEDTEXIMAGE3DPROC(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLsizei imageSize, IntPtr data);
    public delegate void PFNGLCOMPRESSEDTEXIMAGE2DPROC(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLint border, GLsizei imageSize, IntPtr data);
    public delegate void PFNGLCOMPRESSEDTEXIMAGE1DPROC(GLenum target, GLint level, GLenum internalformat, GLsizei width, GLint border, GLsizei imageSize, IntPtr data);
    public delegate void PFNGLCOMPRESSEDTEXSUBIMAGE3DPROC(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLsizei imageSize, IntPtr data);
    public delegate void PFNGLCOMPRESSEDTEXSUBIMAGE2DPROC(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLsizei imageSize, IntPtr data);
    public delegate void PFNGLCOMPRESSEDTEXSUBIMAGE1DPROC(GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLsizei imageSize, IntPtr data);
    public delegate void PFNGLGETCOMPRESSEDTEXIMAGEPROC(GLenum target, GLint level, IntPtr img);



    public delegate void PFNGLBLENDFUNCSEPARATEPROC(GLenum sfactorRGB, GLenum dfactorRGB, GLenum sfactorAlpha, GLenum dfactorAlpha);
    public delegate void PFNGLMULTIDRAWARRAYSPROC(GLenum mode, GLint[] first, GLsizei[] count, GLsizei drawcount);
    public delegate void PFNGLMULTIDRAWELEMENTSPROC(GLenum mode, GLsizei[] count, GLenum type, IntPtr[] indices, GLsizei drawcount);
    public delegate void PFNGLPOINTPARAMETERFPROC(GLenum pname, GLfloat param);
    public delegate void PFNGLPOINTPARAMETERFVPROC(GLenum pname, GLfloat[] @params);
    public delegate void PFNGLPOINTPARAMETERIPROC(GLenum pname, GLint param);
    public delegate void PFNGLPOINTPARAMETERIVPROC(GLenum pname, GLint[] @params);



    public delegate void PFNGLGENQUERIESPROC(GLsizei n, GLuint[] ids);
    public delegate void PFNGLDELETEQUERIESPROC(GLsizei n, GLuint[] ids);
    public delegate GLboolean PFNGLISQUERYPROC(GLuint id);
    public delegate void PFNGLBEGINQUERYPROC(GLenum target, GLuint id);
    public delegate void PFNGLENDQUERYPROC(GLenum target);
    public delegate void PFNGLGETQUERYIVPROC(GLenum target, GLenum pname, GLint[] @params);
    public delegate void PFNGLGETQUERYOBJECTIVPROC(GLuint id, GLenum pname, GLint[] @params);
    public delegate void PFNGLGETQUERYOBJECTUIVPROC(GLuint id, GLenum pname, GLuint[] @params);
    public delegate void PFNGLBINDBUFFERPROC(GLenum target, GLuint buffer);
    public delegate void PFNGLDELETEBUFFERSPROC(GLsizei n, GLuint[] buffers);
    public delegate void PFNGLGENBUFFERSPROC(GLsizei n, GLuint[] buffers);
    public delegate GLboolean PFNGLISBUFFERPROC(GLuint buffer);
    public delegate void PFNGLBUFFERDATAPROC(GLenum target, GLsizeiptr size, IntPtr data, GLenum usage);
    public delegate void PFNGLBUFFERSUBDATAPROC(GLenum target, GLintptr offset, GLsizeiptr size, IntPtr data);
    public delegate void PFNGLGETBUFFERSUBDATAPROC(GLenum target, GLintptr offset, GLsizeiptr size, IntPtr data);
    public delegate IntPtr PFNGLMAPBUFFERPROC(GLenum target, GLenum access);
    public delegate GLboolean PFNGLUNMAPBUFFERPROC(GLenum target);
    public delegate void PFNGLGETBUFFERPARAMETERIVPROC(GLenum target, GLenum pname, GLint[] @params);
    public delegate void PFNGLGETBUFFERPOINTERVPROC(GLenum target, GLenum pname, IntPtr[] @params);



    public delegate void PFNGLBLENDEQUATIONSEPARATEPROC(GLenum modeRGB, GLenum modeAlpha);
    public delegate void PFNGLDRAWBUFFERSPROC(GLsizei n, GLenum[] bufs);
    public delegate void PFNGLSTENCILOPSEPARATEPROC(GLenum face, GLenum sfail, GLenum dpfail, GLenum dppass);
    public delegate void PFNGLSTENCILFUNCSEPARATEPROC(GLenum face, GLenum func, GLint @ref, GLuint mask);
    public delegate void PFNGLSTENCILMASKSEPARATEPROC(GLenum face, GLuint mask);
    public delegate void PFNGLATTACHSHADERPROC(GLuint program, GLuint shader);
    public delegate void PFNGLBINDATTRIBLOCATIONPROC(GLuint program, GLuint index, string name);
    public delegate void PFNGLCOMPILESHADERPROC(GLuint shader);
    public delegate GLuint PFNGLCREATEPROGRAMPROC();
    public delegate GLuint PFNGLCREATESHADERPROC(GLenum type);
    public delegate void PFNGLDELETEPROGRAMPROC(GLuint program);
    public delegate void PFNGLDELETESHADERPROC(GLuint shader);
    public delegate void PFNGLDETACHSHADERPROC(GLuint program, GLuint shader);
    public delegate void PFNGLDISABLEVERTEXATTRIBARRAYPROC(GLuint index);
    public delegate void PFNGLENABLEVERTEXATTRIBARRAYPROC(GLuint index);
    public delegate void PFNGLGETACTIVEATTRIBPROC(GLuint program, GLuint index, GLsizei bufSize, GLsizei[] length, GLint[] size, GLenum[] type, GLchar[] name);
    public delegate void PFNGLGETACTIVEUNIFORMPROC(GLuint program, GLuint index, GLsizei bufSize, GLsizei[] length, GLint[] size, GLenum[] type, GLchar[] name);
    public delegate void PFNGLGETATTACHEDSHADERSPROC(GLuint program, GLsizei maxCount, GLsizei[] count, GLuint[] obj);
    public delegate GLint PFNGLGETATTRIBLOCATIONPROC(GLuint program, string name);
    public delegate void PFNGLGETPROGRAMIVPROC(GLuint program, GLenum pname, GLint[] @params);
    public delegate void PFNGLGETPROGRAMINFOLOGPROC(GLuint program, GLsizei bufSize, GLsizei[] length, GLchar[] infoLog);
    public delegate void PFNGLGETSHADERIVPROC(GLuint shader, GLenum pname, GLint[] @params);
    public delegate void PFNGLGETSHADERINFOLOGPROC(GLuint shader, GLsizei bufSize, GLsizei[] length, GLchar[] infoLog);
    public delegate void PFNGLGETSHADERSOURCEPROC(GLuint shader, GLsizei bufSize, GLsizei[] length, GLchar[] source);
    public delegate GLint PFNGLGETUNIFORMLOCATIONPROC(GLuint program, string name);
    public delegate void PFNGLGETUNIFORMFVPROC(GLuint program, GLint location, GLfloat[] @params);
    public delegate void PFNGLGETUNIFORMIVPROC(GLuint program, GLint location, GLint[] @params);
    public delegate void PFNGLGETVERTEXATTRIBDVPROC(GLuint index, GLenum pname, GLdouble[] @params);
    public delegate void PFNGLGETVERTEXATTRIBFVPROC(GLuint index, GLenum pname, GLfloat[] @params);
    public delegate void PFNGLGETVERTEXATTRIBIVPROC(GLuint index, GLenum pname, GLint[] @params);
    public delegate void PFNGLGETVERTEXATTRIBPOINTERVPROC(GLuint index, GLenum pname, IntPtr[] pointer);
    public delegate GLboolean PFNGLISPROGRAMPROC(GLuint program);
    public delegate GLboolean PFNGLISSHADERPROC(GLuint shader);
    public delegate void PFNGLLINKPROGRAMPROC(GLuint program);
    public delegate void PFNGLSHADERSOURCEPROC(GLuint shader, GLsizei count, string[] strings, GLint[] length);
    public delegate void PFNGLUSEPROGRAMPROC(GLuint program);
    public delegate void PFNGLUNIFORM1FPROC(GLint location, GLfloat v0);
    public delegate void PFNGLUNIFORM2FPROC(GLint location, GLfloat v0, GLfloat v1);
    public delegate void PFNGLUNIFORM3FPROC(GLint location, GLfloat v0, GLfloat v1, GLfloat v2);
    public delegate void PFNGLUNIFORM4FPROC(GLint location, GLfloat v0, GLfloat v1, GLfloat v2, GLfloat v3);
    public delegate void PFNGLUNIFORM1IPROC(GLint location, GLint v0);
    public delegate void PFNGLUNIFORM2IPROC(GLint location, GLint v0, GLint v1);
    public delegate void PFNGLUNIFORM3IPROC(GLint location, GLint v0, GLint v1, GLint v2);
    public delegate void PFNGLUNIFORM4IPROC(GLint location, GLint v0, GLint v1, GLint v2, GLint v3);
    public delegate void PFNGLUNIFORM1FVPROC(GLint location, GLsizei count, GLfloat[] value);
    public delegate void PFNGLUNIFORM2FVPROC(GLint location, GLsizei count, GLfloat[] value);
    public delegate void PFNGLUNIFORM3FVPROC(GLint location, GLsizei count, GLfloat[] value);
    public delegate void PFNGLUNIFORM4FVPROC(GLint location, GLsizei count, GLfloat[] value);
    public delegate void PFNGLUNIFORM1IVPROC(GLint location, GLsizei count, GLint[] value);
    public delegate void PFNGLUNIFORM2IVPROC(GLint location, GLsizei count, GLint[] value);
    public delegate void PFNGLUNIFORM3IVPROC(GLint location, GLsizei count, GLint[] value);
    public delegate void PFNGLUNIFORM4IVPROC(GLint location, GLsizei count, GLint[] value);
    public delegate void PFNGLUNIFORMMATRIX2FVPROC(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
    public delegate void PFNGLUNIFORMMATRIX3FVPROC(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
    public delegate void PFNGLUNIFORMMATRIX4FVPROC(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
    public delegate void PFNGLVALIDATEPROGRAMPROC(GLuint program);
    public delegate void PFNGLVERTEXATTRIB1DPROC(GLuint index, GLdouble x);
    public delegate void PFNGLVERTEXATTRIB1DVPROC(GLuint index, GLdouble[] v);
    public delegate void PFNGLVERTEXATTRIB1FPROC(GLuint index, GLfloat x);
    public delegate void PFNGLVERTEXATTRIB1FVPROC(GLuint index, GLfloat[] v);
    public delegate void PFNGLVERTEXATTRIB1SPROC(GLuint index, GLshort x);
    public delegate void PFNGLVERTEXATTRIB1SVPROC(GLuint index, GLshort[] v);
    public delegate void PFNGLVERTEXATTRIB2DPROC(GLuint index, GLdouble x, GLdouble y);
    public delegate void PFNGLVERTEXATTRIB2DVPROC(GLuint index, GLdouble[] v);
    public delegate void PFNGLVERTEXATTRIB2FPROC(GLuint index, GLfloat x, GLfloat y);
    public delegate void PFNGLVERTEXATTRIB2FVPROC(GLuint index, GLfloat[] v);
    public delegate void PFNGLVERTEXATTRIB2SPROC(GLuint index, GLshort x, GLshort y);
    public delegate void PFNGLVERTEXATTRIB2SVPROC(GLuint index, GLshort[] v);
    public delegate void PFNGLVERTEXATTRIB3DPROC(GLuint index, GLdouble x, GLdouble y, GLdouble z);
    public delegate void PFNGLVERTEXATTRIB3DVPROC(GLuint index, GLdouble[] v);
    public delegate void PFNGLVERTEXATTRIB3FPROC(GLuint index, GLfloat x, GLfloat y, GLfloat z);
    public delegate void PFNGLVERTEXATTRIB3FVPROC(GLuint index, GLfloat[] v);
    public delegate void PFNGLVERTEXATTRIB3SPROC(GLuint index, GLshort x, GLshort y, GLshort z);
    public delegate void PFNGLVERTEXATTRIB3SVPROC(GLuint index, GLshort[] v);
    public delegate void PFNGLVERTEXATTRIB4NBVPROC(GLuint index, GLbyte[] v);
    public delegate void PFNGLVERTEXATTRIB4NIVPROC(GLuint index, GLint[] v);
    public delegate void PFNGLVERTEXATTRIB4NSVPROC(GLuint index, GLshort[] v);
    public delegate void PFNGLVERTEXATTRIB4NUBPROC(GLuint index, GLubyte x, GLubyte y, GLubyte z, GLubyte w);
    public delegate void PFNGLVERTEXATTRIB4NUBVPROC(GLuint index, GLubyte[] v);
    public delegate void PFNGLVERTEXATTRIB4NUIVPROC(GLuint index, GLuint[] v);
    public delegate void PFNGLVERTEXATTRIB4NUSVPROC(GLuint index, GLushort[] v);
    public delegate void PFNGLVERTEXATTRIB4BVPROC(GLuint index, GLbyte[] v);
    public delegate void PFNGLVERTEXATTRIB4DPROC(GLuint index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);
    public delegate void PFNGLVERTEXATTRIB4DVPROC(GLuint index, GLdouble[] v);
    public delegate void PFNGLVERTEXATTRIB4FPROC(GLuint index, GLfloat x, GLfloat y, GLfloat z, GLfloat w);
    public delegate void PFNGLVERTEXATTRIB4FVPROC(GLuint index, GLfloat[] v);
    public delegate void PFNGLVERTEXATTRIB4IVPROC(GLuint index, GLint[] v);
    public delegate void PFNGLVERTEXATTRIB4SPROC(GLuint index, GLshort x, GLshort y, GLshort z, GLshort w);
    public delegate void PFNGLVERTEXATTRIB4SVPROC(GLuint index, GLshort[] v);
    public delegate void PFNGLVERTEXATTRIB4UBVPROC(GLuint index, GLubyte[] v);
    public delegate void PFNGLVERTEXATTRIB4UIVPROC(GLuint index, GLuint[] v);
    public delegate void PFNGLVERTEXATTRIB4USVPROC(GLuint index, GLushort[] v);
    public delegate void PFNGLVERTEXATTRIBPOINTERPROC(GLuint index, GLint size, GLenum type, GLboolean normalized, GLsizei stride, IntPtr pointer);



    public delegate void PFNGLUNIFORMMATRIX2X3FVPROC(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
    public delegate void PFNGLUNIFORMMATRIX3X2FVPROC(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
    public delegate void PFNGLUNIFORMMATRIX2X4FVPROC(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
    public delegate void PFNGLUNIFORMMATRIX4X2FVPROC(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
    public delegate void PFNGLUNIFORMMATRIX3X4FVPROC(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
    public delegate void PFNGLUNIFORMMATRIX4X3FVPROC(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);



    public delegate void PFNGLCOLORMASKIPROC(GLuint index, GLboolean r, GLboolean g, GLboolean b, GLboolean a);
    public delegate void PFNGLGETBOOLEANI_VPROC(GLenum target, GLuint index, GLboolean[] data);
    public delegate void PFNGLGETINTEGERI_VPROC(GLenum target, GLuint index, GLint[] data);
    public delegate void PFNGLENABLEIPROC(GLenum target, GLuint index);
    public delegate void PFNGLDISABLEIPROC(GLenum target, GLuint index);
    public delegate GLboolean PFNGLISENABLEDIPROC(GLenum target, GLuint index);
    public delegate void PFNGLBEGINTRANSFORMFEEDBACKPROC(GLenum primitiveMode);
    public delegate void PFNGLENDTRANSFORMFEEDBACKPROC();
    public delegate void PFNGLBINDBUFFERRANGEPROC(GLenum target, GLuint index, GLuint buffer, GLintptr offset, GLsizeiptr size);
    public delegate void PFNGLBINDBUFFERBASEPROC(GLenum target, GLuint index, GLuint buffer);
    public delegate void PFNGLTRANSFORMFEEDBACKVARYINGSPROC(GLuint program, GLsizei count, string[] varyings, GLenum bufferMode);
    public delegate void PFNGLGETTRANSFORMFEEDBACKVARYINGPROC(GLuint program, GLuint index, GLsizei bufSize, GLsizei[] length, GLsizei[] size, GLenum[] type, GLchar[] name);
    public delegate void PFNGLCLAMPCOLORPROC(GLenum target, GLenum clamp);
    public delegate void PFNGLBEGINCONDITIONALRENDERPROC(GLuint id, GLenum mode);
    public delegate void PFNGLENDCONDITIONALRENDERPROC();
    public delegate void PFNGLVERTEXATTRIBIPOINTERPROC(GLuint index, GLint size, GLenum type, GLsizei stride, IntPtr pointer);
    public delegate void PFNGLGETVERTEXATTRIBIIVPROC(GLuint index, GLenum pname, GLint[] @params);
    public delegate void PFNGLGETVERTEXATTRIBIUIVPROC(GLuint index, GLenum pname, GLuint[] @params);
    public delegate void PFNGLVERTEXATTRIBI1IPROC(GLuint index, GLint x);
    public delegate void PFNGLVERTEXATTRIBI2IPROC(GLuint index, GLint x, GLint y);
    public delegate void PFNGLVERTEXATTRIBI3IPROC(GLuint index, GLint x, GLint y, GLint z);
    public delegate void PFNGLVERTEXATTRIBI4IPROC(GLuint index, GLint x, GLint y, GLint z, GLint w);
    public delegate void PFNGLVERTEXATTRIBI1UIPROC(GLuint index, GLuint x);
    public delegate void PFNGLVERTEXATTRIBI2UIPROC(GLuint index, GLuint x, GLuint y);
    public delegate void PFNGLVERTEXATTRIBI3UIPROC(GLuint index, GLuint x, GLuint y, GLuint z);
    public delegate void PFNGLVERTEXATTRIBI4UIPROC(GLuint index, GLuint x, GLuint y, GLuint z, GLuint w);
    public delegate void PFNGLVERTEXATTRIBI1IVPROC(GLuint index, GLint[] v);
    public delegate void PFNGLVERTEXATTRIBI2IVPROC(GLuint index, GLint[] v);
    public delegate void PFNGLVERTEXATTRIBI3IVPROC(GLuint index, GLint[] v);
    public delegate void PFNGLVERTEXATTRIBI4IVPROC(GLuint index, GLint[] v);
    public delegate void PFNGLVERTEXATTRIBI1UIVPROC(GLuint index, GLuint[] v);
    public delegate void PFNGLVERTEXATTRIBI2UIVPROC(GLuint index, GLuint[] v);
    public delegate void PFNGLVERTEXATTRIBI3UIVPROC(GLuint index, GLuint[] v);
    public delegate void PFNGLVERTEXATTRIBI4UIVPROC(GLuint index, GLuint[] v);
    public delegate void PFNGLVERTEXATTRIBI4BVPROC(GLuint index, GLbyte[] v);
    public delegate void PFNGLVERTEXATTRIBI4SVPROC(GLuint index, GLshort[] v);
    public delegate void PFNGLVERTEXATTRIBI4UBVPROC(GLuint index, GLubyte[] v);
    public delegate void PFNGLVERTEXATTRIBI4USVPROC(GLuint index, GLushort[] v);
    public delegate void PFNGLGETUNIFORMUIVPROC(GLuint program, GLint location, GLuint[] @params);
    public delegate void PFNGLBINDFRAGDATALOCATIONPROC(GLuint program, GLuint color, GLchar[] name);
    public delegate GLint PFNGLGETFRAGDATALOCATIONPROC(GLuint program, GLchar[] name);
    public delegate void PFNGLUNIFORM1UIPROC(GLint location, GLuint v0);
    public delegate void PFNGLUNIFORM2UIPROC(GLint location, GLuint v0, GLuint v1);
    public delegate void PFNGLUNIFORM3UIPROC(GLint location, GLuint v0, GLuint v1, GLuint v2);
    public delegate void PFNGLUNIFORM4UIPROC(GLint location, GLuint v0, GLuint v1, GLuint v2, GLuint v3);
    public delegate void PFNGLUNIFORM1UIVPROC(GLint location, GLsizei count, GLuint[] value);
    public delegate void PFNGLUNIFORM2UIVPROC(GLint location, GLsizei count, GLuint[] value);
    public delegate void PFNGLUNIFORM3UIVPROC(GLint location, GLsizei count, GLuint[] value);
    public delegate void PFNGLUNIFORM4UIVPROC(GLint location, GLsizei count, GLuint[] value);
    public delegate void PFNGLTEXPARAMETERIIVPROC(GLenum target, GLenum pname, GLint[] @params);
    public delegate void PFNGLTEXPARAMETERIUIVPROC(GLenum target, GLenum pname, GLuint[] @params);
    public delegate void PFNGLGETTEXPARAMETERIIVPROC(GLenum target, GLenum pname, GLint[] @params);
    public delegate void PFNGLGETTEXPARAMETERIUIVPROC(GLenum target, GLenum pname, GLuint[] @params);
    public delegate void PFNGLCLEARBUFFERIVPROC(GLenum buffer, GLint drawbuffer, GLint[] value);
    public delegate void PFNGLCLEARBUFFERUIVPROC(GLenum buffer, GLint drawbuffer, GLuint[] value);
    public delegate void PFNGLCLEARBUFFERFVPROC(GLenum buffer, GLint drawbuffer, GLfloat[] value);
    public delegate void PFNGLCLEARBUFFERFIPROC(GLenum buffer, GLint drawbuffer, GLfloat depth, GLint stencil);

    public delegate GLubyte[] PFNGLGETSTRINGIPROC(GLenum name, GLuint index);*/

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
        void glTexImage1D(GLenum target, GLint level, GLint internalformat, GLsizei width, GLint border, GLenum format, GLenum type, IntPtr pixels);
        void glTexImage2D(GLenum target, GLint level, GLint internalformat, GLsizei width, GLsizei height, GLint border, GLenum format, GLenum type, IntPtr pixels);
        void glDrawBuffer(GLenum mode);
        void glClear(GLbitfield mask);
        void glClearColor(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);
        void glClearStencil(GLint s);
        void glClearDepth(GLdouble depth);
        void glStencilMask(GLuint mask);
        void glColorMask(GLboolean red, GLboolean green, GLboolean blue, GLboolean alpha);
        void glDepthMask(GLboolean flag);
        void glDisable(GLenum cap);
        void glEnable(GLenum cap);
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
        void glDrawArrays(GLenum mode, GLint first, GLsizei count);
        void glDrawElements(GLenum mode, GLsizei count, GLenum type, IntPtr indices);
        void glGetPointerv(GLenum pname, IntPtr[] @params);
        void glPolygonOffset(GLfloat factor, GLfloat units);
        void glCopyTexImage1D(GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLint border);
        void glCopyTexImage2D(GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLsizei height, GLint border);
        void glCopyTexSubImage1D(GLenum target, GLint level, GLint xoffset, GLint x, GLint y, GLsizei width);
        void glCopyTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint x, GLint y, GLsizei width, GLsizei height);
        void glTexSubImage1D(GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLenum type, IntPtr pixels);
        void glTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLenum type, IntPtr pixels);
        void glBindTexture(GLenum target, GLuint texture);
        void glDeleteTextures(GLsizei n, GLuint[] textures);
        void glGenTextures(GLsizei n, GLuint[] textures);
        GLboolean glIsTexture(GLuint texture);

    }

    public interface IOpenGL30
    {
        void glColorMaski(GLuint index, GLboolean r, GLboolean g, GLboolean b, GLboolean a);
        void glGetBooleani_v(GLenum target, GLuint index, GLboolean[] data);
        void glGetIntegeri_v(GLenum target, GLuint index, GLint[] data);
        void glEnablei(GLenum target, GLuint index);
        void glDisablei(GLenum target, GLuint index);
        GLboolean glIsEnabledi(GLenum target, GLuint index);
        void glBeginTransformFeedback(GLenum primitiveMode);
        void glEndTransformFeedback();
        void glBindBufferRange(GLenum target, GLuint index, GLuint buffer, GLintptr offset, GLsizeiptr size);
        void glBindBufferBase(GLenum target, GLuint index, GLuint buffer);
        void glTransformFeedbackVaryings(GLuint program, GLsizei count, string[] varyings, GLenum bufferMode);
        void glGetTransformFeedbackVarying(GLuint program, GLuint index, GLsizei bufSize, GLsizei[] length, GLsizei[] size, GLenum[] type, GLchar[] name);
        void glClampColor(GLenum target, GLenum clamp);
        void glBeginConditionalRender(GLuint id, GLenum mode);
        void glEndConditionalRender();
        void glVertexAttribIPointer(GLuint index, GLint size, GLenum type, GLsizei stride, IntPtr pointer);
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
        void glGetProgramiv(GLuint program, GLenum pname, GLint[] @params);
        void glGetProgramInfoLog(GLuint program, GLsizei bufSize, GLsizei[] length, GLchar[] infoLog);
        void glGetShaderiv(GLuint shader, GLenum pname, GLint[] @params);
        void glGetShaderInfoLog(GLuint shader, GLsizei bufSize, GLsizei[] length, string infoLog);
        void glGetShaderSource(GLuint shader, GLsizei bufSize, GLsizei[] length, string source);
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
        void glVertexAttribPointer(GLuint index, GLint size, GLenum type, GLboolean normalized, GLsizei stride, IntPtr pointer);
        // 1.5
        void glGenQueries(GLsizei n, GLuint[] ids);
        void glDeleteQueries(GLsizei n, GLuint[] ids);
        GLboolean glIsQuery(GLuint id);
        void glBeginQuery(GLenum target, GLuint id);
        void glEndQuery(GLenum target);
        void glGetQueryiv(GLenum target, GLenum pname, GLint[] @params);
        void glGetQueryObjectiv(GLuint id, GLenum pname, GLint[] @params);
        void glGetQueryObjectuiv(GLuint id, GLenum pname, GLuint[] @params);
        void glBindBuffer(GLenum target, GLuint buffer);
        void glDeleteBuffers(GLsizei n, GLuint[] buffers);
        void glGenBuffers(GLsizei n, GLuint[] buffers);
        GLboolean glIsBuffer(GLuint buffer);
        void glBufferData(GLenum target, GLsizeiptr size, IntPtr data, GLenum usage);
        void glBufferSubData(GLenum target, GLintptr offset, GLsizeiptr size, IntPtr data);
        void glGetBufferSubData(GLenum target, GLintptr offset, GLsizeiptr size, IntPtr data);
        IntPtr glMapBuffer(GLenum target, GLenum access);
        GLboolean glUnmapBuffer(GLenum target);
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
        void glTexImage3D(GLenum target, GLint level, GLint internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLenum format, GLenum type, IntPtr pixels);
        void glTexSubImage3D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLenum type, IntPtr pixels);
        void glCopyTexSubImage3D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLint x, GLint y, GLsizei width, GLsizei height);
    }
    // ReSharper restore InconsistentNaming

    public interface IOpenGL31 : IOpenGL30
    {

    }

    public interface IOpenGL32 : IOpenGL31
    {

    }

    public interface IOpenGL33 : IOpenGL32
    {

    }

    public interface IOpenGL40 : IOpenGL33
    {

    }
    public interface IOpenGL41 : IOpenGL40
    {

    }
    public interface IOpenGL42 : IOpenGL41
    {

    }

    public interface IOpenGL43 : IOpenGL42
    {

    }

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

    public enum TextureTarget
    {
        ProxyTexture_1D = 0x8063,
        ProxyTexture_2D = 0x8064
    }

    public enum TextureWrapMode
    {
        Repeat = 0x2901
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
        public static extern void glTexImage1D(GLenum target, GLint level, GLint internalformat, GLsizei width, GLint border, GLenum format, GLenum type, IntPtr pixels);
        [DllImport(GLLibraryName)]
        public static extern void glTexImage2D(GLenum target, GLint level, GLint internalformat, GLsizei width, GLsizei height, GLint border, GLenum format, GLenum type, IntPtr pixels);
        [DllImport(GLLibraryName)]
        public static extern void glDrawBuffer(GLenum mode);
        [DllImport(GLLibraryName)]
        public static extern void glClear(GLbitfield mask);
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
        public static extern void glDisable(GLenum cap);
        [DllImport(GLLibraryName)]
        public static extern void glEnable(GLenum cap);
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
        [DllImport(GLLibraryName)]
        public static extern void glDrawArrays(GLenum mode, GLint first, GLsizei count);
        [DllImport(GLLibraryName)]
        public static extern void glDrawElements(GLenum mode, GLsizei count, GLenum type, IntPtr indices);
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
        public static extern void glBindTexture(GLenum target, GLuint texture);
        [DllImport(GLLibraryName)]
        public static extern void glDeleteTextures(GLsizei n, GLuint[] textures);
        [DllImport(GLLibraryName)]
        public static extern void glGenTextures(GLsizei n, GLuint[] textures);
        [DllImport(GLLibraryName)]
        public static extern GLboolean glIsTexture(GLuint texture);


        public const string GLLibraryName = "opengl32";

        private static TDelegate GetFunction<TDelegate>(string functionName)
        {

        }
    }
}
