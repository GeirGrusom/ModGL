using System;
using System.Dynamic;
using System.IO;

namespace SpecBuilder.CodeGen
{
    public abstract class DataType : ICodeDomWriteable
    {
        public abstract void Write(StreamWriter writer, int tabs);

        public static readonly DataType GLubyte = Create<byte>();
        public static readonly DataType GLbyte = Create<sbyte>();
        public static readonly DataType GLshort = Create<short>();
        public static readonly DataType GLushort = Create<ushort>();
        public static readonly DataType GLint = Create<int>();
        public static readonly DataType GLuint = Create<uint>();
        public static readonly DataType GLsizei = Create<int>();
        public static readonly DataType GLenum = Create<uint>();
        public static readonly DataType GLboolean = Create("GLboolean");

        public static DataType Create<T>()
        {
            return new SystemDataType(typeof(T));
        }

        public static DataType Create(string name)
        {
            return new CustomDataType(name);
        }

        public static DataType Create(Type type)
        {
            return new SystemDataType(type);
        }
    }
}