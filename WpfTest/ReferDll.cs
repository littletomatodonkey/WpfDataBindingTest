using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WpfTest.DataBinding
{
    class ReferDll
    {
        private const string dllName = @"../../../libs/GenerateDll.dll";

        [DllImport(dllName, EntryPoint = "g_Add")]
        public extern static int Add(int a, int b);

        [DllImport(dllName, EntryPoint = "g_Minus")]
        public extern static int Minus(int a, int b);

        [DllImport(dllName, EntryPoint = "g_Multi")]
        public extern static int g_Multi(int a, int b);

        [DllImport(dllName, EntryPoint = "g_Div")]
        public extern static int g_Div(int a, int b);

        [DllImport(dllName, EntryPoint = "g_DotProd")]
        public extern static int DotProd(int[] a, int[] b, int num);

        // 字符串无法返回，否则会报错。。原因未知
        [DllImport(dllName, EntryPoint = "g_Char")]
        public extern static void CharArr(string a, int n);

    }

    class ImportFcnClass
    {
        [DllImport(@"../../../libs/GenerateDll.dll", EntryPoint = @"?printInfo@TestFcnDllClass@@SGXPBD@Z", CallingConvention = CallingConvention.StdCall)]
        public static extern void PrintInfo(string str);

        [DllImport(@"../../../libs/GenerateDll.dll", EntryPoint = @"?add@TestFcnDllClass@@SGHHH@Z", CallingConvention = CallingConvention.StdCall)]
        public static extern int Add(int a, int b);

        [DllImport(@"../../../libs/GenerateDll.dll", EntryPoint = @"?minus@TestFcnDllClass@@QAENNN@Z", CallingConvention = CallingConvention.StdCall)]
        public static extern double minus(double a, double b);

        [DllImport(@"../../../libs/GenerateDll.dll", EntryPoint = @"?addMnius@TestFcnDllClass@@QAENHHH@Z", CallingConvention = CallingConvention.StdCall)]
        public static extern double AddMnius(int a, int b, int c);

        [DllImport("../../../libs/GenerateDll.dll", EntryPoint = @"?HSCAN_SendCANMessage@TestFcnDllClass@@QAEHEEPAUHSCAN_MSG@@H@Z")]
        public static extern int HSCAN_SendCANMessage(byte nDevice, byte nPort, HSCAN_MSG[] pMsg, int nLength);

        [DllImport("../../../libs/GenerateDll.dll", EntryPoint = @"?GenerateMsg@TestFcnDllClass@@QAEPAUHSCAN_MSG@@XZ")]
        public static extern HSCAN_MSG GenerateMsg();

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HSCAN_MSG
    {
        // UnmanagedType.ByValArray， [MarshalAs(UnmanagedType.U1)]这个非常重要，就是申明对应类型和长度的
        //else all the data length is not sure
        [MarshalAs(UnmanagedType.U1)]
        public byte Port;
        [MarshalAs(UnmanagedType.U4)]
        public uint nId;
        [MarshalAs(UnmanagedType.U1)]
        public byte nCtrl;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] pData;
    };
}
