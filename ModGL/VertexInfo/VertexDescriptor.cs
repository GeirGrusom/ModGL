using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using ModGL.NativeGL;
using ModGL.Shaders;

namespace ModGL.VertexInfo
{

    public class VertexElement
    {
        public int Length { get; internal set; }
        public string Name { get; internal set; }
        public DataType Type { get; internal set; }
        public int Offset { get; set; }
    }

    internal static class ElementTypeHelper
    {
        internal static readonly Dictionary<Type, DataType> TypeConversionTable = new Dictionary<Type, DataType>
        {
            { typeof(byte), DataType.UnsignedByte },
            { typeof(sbyte), DataType.Byte },
            { typeof(short), DataType.Short },
            { typeof(ushort), DataType.UnsignedShort },
            { typeof(int), DataType.Int },
            { typeof(uint), DataType.UnsignedInt },
            { typeof(float), DataType.Float },
            { typeof(double), DataType.Double }
        };
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Struct)]
    public class VertexElementAttribute : Attribute
    {
        public DataType DataType { get; set; }
    }

    public class VertexDescriptor<TElementType>
        where TElementType : struct
    {
        public IEnumerable<VertexElement> Elements { get; private set; }

        private static DataType GetElementType(Type type)
        {
            if(!type.IsValueType)
                throw new InvalidOperationException("Cannot use reference types as a vertex element.");
            DataType result;
            if (ElementTypeHelper.TypeConversionTable.TryGetValue(type, out result))
                return result;
            var attribute = type.GetCustomAttribute<VertexElementAttribute>(false);
            if (attribute != null)
                return attribute.DataType;
            throw new InvalidOperationException("Could not determine the vertex element datatype.");
        }

        private static VertexElement ConvertField(FieldInfo field)
        {
            var attrib = field.GetCustomAttribute<VertexElementAttribute>();
            DataType type;
            if (attrib != null)
                type = attrib.DataType;
            else
                type = GetElementType(field.FieldType);
            return new VertexElement
            {
                Name = field.Name,
                Length = System.Runtime.InteropServices.Marshal.SizeOf(field.FieldType),
                Type = type
            };
        }

        /// <summary>
        /// Creates a vertex descriptor for <see cref="TElementType"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Thrown if a field is a value type, or if the class was unable to determine a correct datatype for a field.</exception>
        public static VertexDescriptor<TElementType> Create()
        {
            var type = typeof(TElementType);

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            return new VertexDescriptor<TElementType>
            {
                Elements = fields.Select(ConvertField).ToArray()
            };
        }

        public void Apply(Program program, IOpenGL30 gl)
        {
            
        }

        public void Apply(VertexArray<TElementType> array, IOpenGL30 gl)
        {
            using (array.Bind())
            {
                foreach (var e in Elements.Select((e, i) => new { Index = i, Item = e}))
                {
                    if (e.Item.Type == DataType.Half || e.Item.Type == DataType.Float)
                    {
                        gl.glVertexAttribPointer(
                            (uint)e.Index,
                            e.Item.Length,
                            e.Item.Type,
                            GLboolean.False,
                            System.Runtime.InteropServices.Marshal.SizeOf(typeof(TElementType)),
                            new IntPtr(e.Item.Offset));
                    }
                    else
                    {
                        gl.glVertexAttribIPointer(
                            (uint)e.Index,
                            e.Item.Length,
                            e.Item.Type,
                            System.Runtime.InteropServices.Marshal.SizeOf(typeof(TElementType)),
                            new IntPtr(e.Item.Offset));
                    }
                    gl.glEnableVertexAttribArray((uint)e.Index);
                }
            }
        }
    }
}
