using System;
using System.Collections.Generic;

namespace SpecBuilder.CodeGen
{
    internal static class TypeNameHelper
    {
        public static readonly Dictionary<Type, string> TypeNameLookup = new Dictionary<Type, string>
        {
            {typeof(byte), "byte"},
            {typeof(sbyte), "sbyte"},
            {typeof(short), "short"},
            {typeof(ushort), "ushort"},
            {typeof(int), "int"},
            {typeof(uint), "uint"},
            {typeof(long), "long"},
            {typeof(ulong), "ulong"},
            {typeof(float), "float"},
            {typeof(double), "double"},
            {typeof(void), "void"}
        };

        private static string GetFriendlyTypeName(Type type)
        {
            string result;
            if (TypeNameLookup.TryGetValue(type, out result))
                return result;
            if (type == typeof (string))
                return "string";
            if (type == typeof (bool))
                return "bool";
            return type.Name;
        }

        public static string GetFriendlyBaseTypeName(Type type)
        {
            if (type.IsArray || type.IsPointer)
                return GetFriendlyTypeName(type.GetElementType());
            return GetFriendlyTypeName(type);
        }
    }
}