using System;
using System.IO;

namespace SpecBuilder.CodeGen
{
    public sealed class SystemDataType : DataType
    {
        private readonly Type _type;
        public Type Type { get { return _type; } }
        
        public SystemDataType(Type type)
        {
            _type = type;
        }

        public override void Write(StreamWriter writer, int tabs)
        {
            var typeName = TypeNameHelper.GetFriendlyBaseTypeName(Type);
            writer.Write(typeName);
            if (Type.IsArray)

                writer.Write("[" + new string(',', Type.GetArrayRank() - 1) + "]");
        }
    }
}