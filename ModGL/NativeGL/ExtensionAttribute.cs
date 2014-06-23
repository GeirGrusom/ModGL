using System;
using System.Diagnostics;

namespace ModGL.NativeGL
{
    [AttributeUsage(AttributeTargets.Interface)]
    [Serializable]
    [DebuggerDisplay("ExtensionName = {_extensionName}")]
    public class ExtensionAttribute : Attribute
    {
        private readonly string _extensionName;

        public string ExtensionName { get { return _extensionName; } }

        public ExtensionAttribute(string extensionName)
        {
            _extensionName = extensionName;
        }
    }
}
