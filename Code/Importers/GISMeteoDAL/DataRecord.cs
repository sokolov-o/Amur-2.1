using System;
using System.Runtime.InteropServices;

namespace SOV.GISMeteo
{
    /// <summary>
    /// Запись данных по параметру
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class MDBDATA
    {
        public int values = 0;
        public int paramId = 0;
        public int check = 0;
        public int diff = 0;
        public int ltype = 0;
        public int lvalue = 0;
    }
}
