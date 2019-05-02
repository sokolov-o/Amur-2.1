using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Import
{
    internal class Common
    {
        internal static string GetDirImported(string dir)
        {
            string ret = dir + @"\Imported";
            if (!Directory.Exists(ret))
                Directory.CreateDirectory(ret);
            return ret;
        }
    }
}
