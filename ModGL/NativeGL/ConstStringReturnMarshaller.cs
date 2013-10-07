using System;
using System.Runtime.InteropServices;

namespace ModGL.NativeGL
{
    /// <summary>
    /// Marshalls a const char * return value.
    /// </summary>
    public class ConstStringReturnMarshaller : ICustomMarshaler
    {
        static ConstStringReturnMarshaller()
        {
            instance = new ConstStringReturnMarshaller();
        }
        private static readonly ICustomMarshaler instance;
        public static ICustomMarshaler GetInstance(string what)
        {
            return instance;
        }
        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            return Marshal.PtrToStringAnsi(pNativeData);
        }


        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            throw new NotImplementedException();
        }


        public void CleanUpNativeData(IntPtr pNativeData)
        {

        }

        public void CleanUpManagedData(object ManagedObj)
        {

        }

        public int GetNativeDataSize()
        {
            return 0;
        }
    }
}