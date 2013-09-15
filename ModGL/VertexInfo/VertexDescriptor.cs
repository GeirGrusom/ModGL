﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using ModGL.NativeGL;
using ModGL.Shaders;

namespace ModGL.VertexInfo
{

    public class VertexElement
    {
        public int Dimensions { get; internal set; }
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
            int dimensions;
            if (attrib != null)
            {
                type = attrib.DataType;
                dimensions = attrib.Dimensions;
            }
            else
            {
                type = GetElementType(field.FieldType);
                dimensions = 1;
            }

            return new VertexElement
            {
                Name = field.Name,
                Dimensions = dimensions,
                Type = type
            };
        }

        /// <summary>
        /// Creates a vertex descriptor for <see cref="TElementType"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Thrown if a field is a value type, or if the class was unable to determine a correct datatype for a field.</exception>
        public static VertexDescriptor Create<TElementType>()
            where TElementType : struct
        {
            var type = typeof(TElementType);

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            return new VertexDescriptor
            {
                Elements = fields.Select(ConvertField).ToArray(),
                ElementType = typeof(TElementType)
            };
        }

        public void Apply(Program program, IOpenGL30 gl)
        {
            
        }

        internal void Apply(IOpenGL30 gl, int indexOffset)
        {
            foreach (var e in Elements.Select((e, i) => new { Index = i, Item = e}))
            {
                // Double is supported by glVertexAttribLPointer, which is not implemented in OpenGL 3.0.
                if (e.Item.Type == DataType.Half || e.Item.Type == DataType.Float) 
                {
                    gl.glVertexAttribPointer(
                        (uint)e.Index,
                        e.Item.Dimensions,
                        e.Item.Type,
                        GLboolean.False,
                        System.Runtime.InteropServices.Marshal.SizeOf(ElementType),
                        new IntPtr(e.Item.Offset));
                }
                else
                {
                    gl.glVertexAttribIPointer(
                        (uint)e.Index,
                        e.Item.Dimensions,
                        e.Item.Type,
                        System.Runtime.InteropServices.Marshal.SizeOf(ElementType),
                        new IntPtr(e.Item.Offset));
                }
                gl.glEnableVertexAttribArray((uint)e.Index);
            }
        }
    }
}
