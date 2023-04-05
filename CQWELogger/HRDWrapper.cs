using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace HrdWrapper
{
    public class HrdInterface
    {
        [DllImport(
            "HRDInterface001.dll",
            SetLastError = true,
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "HRDInterfaceConnect")]
        public static extern bool Connect(
            [MarshalAs(UnmanagedType.LPTStr)] string address,
            UInt16 port);

        [DllImport(
            "HRDInterface001.dll",
            SetLastError = true,
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "HRDInterfaceDisconnect"),]
        public static extern void Disconnect();

        [DllImport(
            "HRDInterface001.dll",
            SetLastError = true,
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "HRDInterfaceFreeString")]
        unsafe
        private static extern void HRDInterfaceFreeString(
            char* _string);

        [DllImport(
            "HRDInterface001.dll",
            SetLastError = true,
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "HRDInterfaceGetLastCode")]
        public static extern UInt32 GetLastCode();

        // Returned char* can be converted directly to string because it's global
        // memory in the DLL and must not be freed.

        [DllImport(
            "HRDInterface001.dll",
            SetLastError = true,
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "HRDInterfaceGetLastError")]
        [return: MarshalAs(UnmanagedType.LPTStr)]
        public static extern string GetLastError();

        [DllImport(
            "HRDInterface001.dll",
            SetLastError = true,
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "HRDInterfaceIsConnected")]
        public static extern bool IsConnected();

        // Not relevant as it uses a C++ class CWnd
        //public static extern void       HRDInterfaceLogfileMessage(CWnd* pWnd, UINT  nMsg);

        // Although it's possible to use the undocumented __arglist keyword
        // to map C/C++ varargs, in practice it's shorter and more in keeping with the
        // .NET style to use .NET formatting instead.

        [DllImport(
            "HRDInterface001.dll",
            SetLastError = true,
            CharSet = CharSet.Unicode,
            ExactSpelling = true,
            CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "HRDInterfaceSendMessage")]
        unsafe
        private static extern char* HRDInterfaceSendMessage(
            [MarshalAs(UnmanagedType.LPTStr)] string format);

        [DllImport(
            "HRDInterface001.dll",
            SetLastError = true,
            CharSet = CharSet.Unicode,
            ExactSpelling = true,
            CallingConvention = CallingConvention.Cdecl,
           EntryPoint = "HRDInterfaceTracing")]
        public static extern void Tracing(
            bool state);

        // Wrapper for HRDInterfaceSendMessage to encapsulate use and release of memory pointers
        unsafe public static string SendMessage(string format, params object[] arglist)
        {
            char* lpszResult = HRDInterfaceSendMessage(String.Format(format, arglist));

            string reply = new string(lpszResult);

            HRDInterfaceFreeString(lpszResult);

            return reply;
        }
    }
}
