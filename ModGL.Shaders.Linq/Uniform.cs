using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ModGL.NativeGL;
using ModGL.Numerics;
using InvalidOperationException = System.InvalidOperationException;

// The names of the following fields are used to reflect GLSL naming convention.
// ReSharper disable once InconsistentNaming

namespace ModGL.Shaders.Linq
{
    public static class Uniform
    {
        public static Matrix4f mat4 { get { throw new InvalidOperationException();} }
        public static Vector2f vec2 { get {  throw new InvalidOperationException();} }
        public static Vector3f vec3 { get { throw new InvalidOperationException(); } }
        public static Vector4f vec4 { get { throw new InvalidOperationException(); } }
        public static float @float { get { throw new InvalidOperationException();} }
        public static int @int { get { throw new InvalidOperationException(); } }
        public static bool @bool { get {  throw new InvalidOperationException();} }
    }
}