﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ModGL.NativeGL
{
    public static class OpenGLHelpers
    {
        public static uint GenTexture(this IOpenGL30 gl)
        {
            var arr = new uint[1];
            gl.GenTextures(1, arr);
            return arr[0];
        }

        public static void DeleteTextures(this IOpenGL30 gl, params uint[] textures)
        {
            gl.DeleteTextures(textures.Length, textures);   
        }

        public static uint GenBuffer(this IOpenGL30 gl)
        {
            var arr = new uint[1];
            gl.GenBuffers(1, arr);
            return arr[0];
        }

        public static void DeleteBuffers(this IOpenGL30 gl, params uint[] buffers)
        {
            gl.DeleteBuffers(buffers.Length, buffers);
        }

        public static uint GenFramebuffer(this IOpenGL30 gl)
        {
            var arr = new uint[1];
            gl.GenFramebuffers(1, arr);
            return arr[0];
        }

        public static void DeleteFrameBuffers(this IOpenGL30 gl, params uint[] frameBuffers)
        {
            gl.DeleteFramebuffers(frameBuffers.Length, frameBuffers);
        }

        public static uint GenRenderBuffer(this IOpenGL30 gl)
        {
            var arr = new uint[1];
            gl.GenRenderbuffers(1, arr);
            return arr[0];
        }

        public static void DeleteRenderBuffers(this IOpenGL30 gl, params uint[] renderBuffers)
        {
            gl.DeleteRenderbuffers(renderBuffers.Length, renderBuffers);
        }

        public static uint GenVertexArray(this IOpenGL30 gl)
        {
            var arr = new uint[1];
            gl.GenVertexArrays(1, arr);
            return arr[0];
        }

        public static void DeleteVertexArrays(this IOpenGL30 gl, params uint[] vertexArrays)
        {
            gl.DeleteVertexArrays(vertexArrays.Length, vertexArrays);
        }

        public static uint GenQuery(this IOpenGL30 gl)
        {
            var arr = new uint[1];
            gl.GenQueries(1, arr);
            return arr[0];
        }

        public static void DeleteQueries(this IOpenGL30 gl, params uint[] queries)
        {
            gl.DeleteQueries(queries.Length, queries);
        }

        public static IEnumerable<string> GetSupportedExtensions(this IOpenGL30 gl)
        {
            var values = new int[1];
            gl.GetIntegerv((GetPName)0x821D /* GL_NUM_EXTENSIONS */, values);   
            var results = new string[values[0]];
            for (int i = 0; i < values[0]; i++)
                results[i] = gl.GetStringi(0x1F03, (uint)i);

            return results;
        }

        public static uint GenSampler(this IOpenGL33 gl)
        {
            var handles = new uint[1];
            gl.GenSamplers(1, handles);
            return handles[0];
        }

        public static void DeleteSampler(this IOpenGL33 gl, uint handle)
        {
            gl.DeleteSamplers(1, new [] { handle });
        }

        public static void DrawElements<T>(this IOpenGL30 gl, PrimitiveType mode, int count, ElementBufferItemType type, T[] data)
            where T : struct
        {
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                gl.DrawElements(mode, count, (uint)type, handle.AddrOfPinnedObject());
            }
            finally
            {
                handle.Free();
            }
        }

        public static void DrawElements(this IOpenGL30 gl, PrimitiveType mode, int count, ElementBufferItemType type)
        {
            gl.DrawElements(mode, count, (uint)type, System.IntPtr.Zero);
        }
    }
}