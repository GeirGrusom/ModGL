using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using ModGL.NativeGL;

namespace ModGL.VertexInfo
{
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public struct PositionNormalTexCoord
    {
        public static readonly VertexDescriptor Descriptor = VertexDescriptor.Create<PositionNormalTexCoord>();
        [FieldOffset(0)]
        public Vector3f Position;
        [FieldOffset(12)]
        public Vector3f Normal;
        [FieldOffset(24)]
        public Vector2f TexCoord;
    }
    public class VertexElement
    {
        public int Dimensions { get; internal set; }
        public string Name { get; internal set; }
        public DataType Type { get; internal set; }
        public int Offset { get; set; }

        public VertexElement(string name, DataType type, int dimensions, int offset)
        {
            Name = name;
            Type = type;
            Dimensions = dimensions;
            Offset = offset;
        }
    }

    internal static class ElementTypeHelper
    {
        internal class ElementDescription
        {
            internal DataType DataType { get; set; }
            internal int Dimensions { get; set; }

            internal ElementDescription(DataType dataType, int dimensions = 1)
            {
                DataType = dataType;
                Dimensions = dimensions;
            }

        }
        internal static readonly Dictionary<Type, ElementDescription> TypeConversionTable = new Dictionary<Type, ElementDescription>
        {
            { typeof(byte), new ElementDescription(DataType.UnsignedByte) },
            { typeof(sbyte), new ElementDescription(DataType.Byte) },
            { typeof(short), new ElementDescription(DataType.Short) },
            { typeof(ushort), new ElementDescription(DataType.UnsignedShort) },
            { typeof(int), new ElementDescription(DataType.Int) },
            { typeof(uint), new ElementDescription(DataType.UnsignedInt) },
            { typeof(float), new ElementDescription(DataType.Float) },
            { typeof(double), new ElementDescription(DataType.Double) },
            { typeof(Vector2f), new ElementDescription(DataType.Float, 2) },
            { typeof(Vector3f), new ElementDescription(DataType.Float, 3) },
            { typeof(Vector4f), new ElementDescription(DataType.Float, 4) }
        };
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class IgnoreVertexElementAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Struct)]
    public class VertexElementAttribute : Attribute
    {
        public VertexElementAttribute(DataType type, int dimensions)
        {
            DataType = type;
            Dimensions = dimensions;
        }

        public VertexElementAttribute(DataType type)
        {
            DataType = type;
            Dimensions = 1;
        }

        public DataType DataType;
        public int Dimensions;
    }

    public class VertexDescriptor
    {
        public Type ElementType { get; private set; }
        public IEnumerable<VertexElement> Elements { get; private set; }

        public VertexDescriptor(Type elementType, IEnumerable<VertexElement> elements)
        {
            ElementType = elementType;
            Elements = elements.ToArray();
        }

        [Pure]
        private static ElementTypeHelper.ElementDescription GetElementType(Type type)
        {
            if(!type.IsValueType)
                throw new InvalidOperationException("Cannot use reference types as a vertex element.");
            ElementTypeHelper.ElementDescription result;
            if (ElementTypeHelper.TypeConversionTable.TryGetValue(type, out result))
                return result;
            var attribute = type.GetCustomAttribute<VertexElementAttribute>(false);
            if (attribute != null)
                return new ElementTypeHelper.ElementDescription(attribute.DataType, attribute.Dimensions);
            throw new InvalidOperationException("Could not determine the vertex element datatype.");
        }

        [Pure]
        private static VertexElement ConvertField(FieldInfo field, int offset)
        {
            var attrib = field.GetCustomAttribute<VertexElementAttribute>();
            DataType type;
            int dimensions;
            if (attrib != null)
            {
                type = attrib.DataType;
                dimensions = attrib.Dimensions;
            }
            else
            {
                var typeAttrib = field.FieldType.GetCustomAttribute<VertexElementAttribute>();
                if (typeAttrib != null)
                {
                    type = typeAttrib.DataType;
                    dimensions = typeAttrib.Dimensions;
                }
                else
                {
                    var desc = GetElementType(field.FieldType);
                    type = desc.DataType;
                    dimensions = desc.Dimensions;
                }
            }

            return new VertexElement(field.Name, type, dimensions, offset);
        }

        /// <summary>
        /// Creates a vertex descriptor for <see cref="TElementType"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Thrown if a field is a value type, or if the class was unable to determine a correct datatype for a field.</exception>
        [Pure]
        public static VertexDescriptor Create<TElementType>()
            where TElementType : struct
        {
            var type = typeof(TElementType);

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            int offset = 0;
            var results = new List<VertexElement>(fields.Length);
            foreach (var field in fields)
            {
                var ret = ConvertField(field, offset);
                offset += System.Runtime.InteropServices.Marshal.SizeOf(field.FieldType);

                var ignoreVertexElementAttribute = field.GetCustomAttribute<IgnoreVertexElementAttribute>();
                if(ignoreVertexElementAttribute == null)
                    results.Add(ret);
            }

            return new VertexDescriptor
            (                
                typeof(TElementType),
                results
            );
        }

        internal void Apply(IOpenGL30 gl, int indexOffset)
        {
            var openGL41 = gl as IOpenGL41;
            foreach (var e in Elements.Select((e, i) => new { Index = i + indexOffset, Item = e}))
            {
                // Double is supported by glVertexAttribLPointer, which is not implemented in OpenGL 3.0.
                if (e.Item.Type == DataType.Half || e.Item.Type == DataType.Float) 
                {
                    gl.VertexAttribPointer(
                        (uint)e.Index,
                        e.Item.Dimensions,
                        (uint)e.Item.Type,
                        (byte)GLboolean.False,
                        System.Runtime.InteropServices.Marshal.SizeOf(ElementType),
                        new IntPtr(e.Item.Offset));
                }
                else if (openGL41 != null && e.Item.Type == DataType.Double)
                {
                    openGL41.VertexAttribLPointer(
                        (uint)e.Index,
                        e.Item.Dimensions,
                        (uint)e.Item.Type,
                        System.Runtime.InteropServices.Marshal.SizeOf(ElementType),
                        new IntPtr(e.Item.Offset));
                }
                else
                {
                    gl.VertexAttribIPointer(
                        (uint)e.Index,
                        e.Item.Dimensions,
                        (uint)e.Item.Type,
                        System.Runtime.InteropServices.Marshal.SizeOf(ElementType),
                        new IntPtr(e.Item.Offset));
                }
                gl.EnableVertexAttribArray((uint)e.Index);
            }
        }
    }
}
