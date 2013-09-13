using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using ModGL.NativeGL;

namespace ModGL.VertexInfo
{

    public enum ElementType
    {
        @byte, @short, @int, @long,
        unsigned_byte, unsigned_short, unsigned_int, unsigned_long,
        half, @float, @double
    }

    public class VertexElement
    {
        public int Length { get; internal set; }
        public string Name { get; internal set; }
        public int Dimensions { get; internal set; }
        public ElementType Type { get; internal set; }
        public int Offset { get; set; }


    }

    internal static class ElementTypeHelper
    {
        internal static readonly Dictionary<Type, ElementType> TypeConversionTable = new Dictionary<Type, ElementType>
        {
            { typeof(byte), ElementType.@unsigned_byte },
            { typeof(sbyte), ElementType.@byte },
            { typeof(short), ElementType.@short },
            { typeof(ushort), ElementType.unsigned_short },
            { typeof(int), ElementType.@int },
            { typeof(uint), ElementType.unsigned_int },
            { typeof(long), ElementType.@long },
            { typeof(float), ElementType.@float },
            { typeof(double), ElementType.@double }
        };
    }

    public class VertexDescriptor<TElementType>
        where TElementType : struct
    {
        public IEnumerable<VertexElement> Elements { get; private set; }


        private static ElementType? GetElementType(Type type)
        {
            ElementType result;
            if (ElementTypeHelper.TypeConversionTable.TryGetValue(type, out result))
                return result;

            throw new NotImplementedException();
        }


        private static IEnumerable<VertexElement> ConvertField(FieldInfo field)
        {
            yield return new VertexElement
            {
                Name = field.Name,
                Length = System.Runtime.InteropServices.Marshal.SizeOf(field.FieldType),
                Type = GetElementType(field.FieldType).Value, Dimensions = 1
                
            };
        }

        public static VertexDescriptor<TElementType> Create()
        {
            var type = typeof(TElementType);

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            return new VertexDescriptor<TElementType>
            {
                Elements = fields.SelectMany(ConvertField).ToArray()
            };
        }

        private static uint ConvertElementTypeToGlType(ElementType type)
        {
            throw new NotImplementedException();
            switch (type)
            {
                case ElementType.unsigned_byte:
                    //return 
            }
        }

        public void Apply(VertexArray<TElementType> array, IOpenGL30 gl)
        {
            using (array.Bind())
            {
                foreach (var e in Elements.Select((e, i) => new { Index = i, Item = e}))
                {
                    gl.glVertexAttribPointer(
                        (uint)e.Index, 
                        e.Item.Length, 
                        ConvertElementTypeToGlType(e.Item.Type), 
                        GLboolean.False, 
                        System.Runtime.InteropServices.Marshal.SizeOf(typeof(TElementType)), 
                        new IntPtr(e.Item.Offset));
                }
            }
        }
    }
}
