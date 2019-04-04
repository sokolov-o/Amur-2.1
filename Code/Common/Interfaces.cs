using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Common
{
    public interface IParent
    {
        int? GetParentId();
        int GetId();
        string GetName();
    }
    public interface IPrecipRestTime
    {
        double? GetPrecipRestTime();
    }
}
