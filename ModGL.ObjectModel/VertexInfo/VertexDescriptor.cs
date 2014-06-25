using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using ModGL.NativeGL;
using InvalidOperationException = System.InvalidOperationException;

namespace ModGL.ObjectModel.VertexInfo
{
    public sealed class VertexDescriptor<TElementType> : VertexDescriptor, IVertexDescriptor<TElementType>
        where TElementType : struct
    {
        public VertexDescriptor(IEnumerable<VertexElement> elements)
            : base(typeof(TElementType), elements)
        {
        }
    }

    public class VertexDescriptor : IVertexDescriptor
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
        public static IVertexDescriptor<TElementType> Create<TElementType>()
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

            return new VertexDescriptor<TElementType>
            (                
                results
            );
        }
    }
}
