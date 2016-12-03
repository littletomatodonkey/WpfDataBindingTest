using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WpfTest
{
    class ReferDll
    {
        [DllImport(@"../../../libs/GenerateDll.dll", EntryPoint = "g_Add")]
        public extern static int Add(int a, int b);

        [DllImport(@"../../../libs/GenerateDll.dll", EntryPoint = "g_Minus")]
        public extern static int Minus(int a, int b);

        [DllImport(@"../../../libs/GenerateDll.dll", EntryPoint = "g_Multi")]
        public extern static int g_Multi(int a, int b);

        [DllImport(@"../../../libs/GenerateDll.dll", EntryPoint = "g_Div")]
        public extern static int g_Div(int a, int b);

        [DllImport(@"../../../libs/GenerateDll.dll", EntryPoint = "g_DotProd")]
        public extern static int DotProd(int[] a, int[] b, int num);

        [DllImport(@"../../../libs/GenerateDll.dll", EntryPoint = "g_Char")]
        public extern static string CharArr(string a, int n);

    }
}
