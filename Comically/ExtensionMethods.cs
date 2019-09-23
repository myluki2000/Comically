using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Comically
{
    static class ExtensionMethods
    {
        public static void SortNaturally(this List<string> a)
        {
            a.Sort(StrCmpLogicalW);
        }

        public static void SortNaturally(this string[] a)
        {
            Array.Sort(a, StrCmpLogicalW);
        }

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string psz1, string psz2);
    }
}
