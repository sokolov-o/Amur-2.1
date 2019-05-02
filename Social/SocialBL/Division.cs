using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.Social
{
    public class Division : IdNameRus, IParent
    {
        public Division ParentDivision { get; set; }
        public LegalEntity Employer { get; set; }
        public DateTime DateS { get; set; }
        public DateTime? DateF { get; set; }

        public int GetId()
        {
            return Id;
        }

        public string GetName()
        {
            return ToString();
        }

        public int? GetParentId()
        {
            return ParentDivision == null ? null : (int?)ParentDivision.Id;
        }

        public string ToStringBranch()
        {
            List<string> branchNodeNames = new List<string>() { this.NameRusShort };
            return Employer.NameRusShort + " -> " + ToStringBranch(this.ParentDivision, branchNodeNames);
        }
        string ToStringBranch(Division div, List<string> branchNodeNames)
        {
            if (div == null)
            {
                string ret = "";
                for (int i = branchNodeNames.Count - 1; i >= 0; i--)
                {
                    ret += "" + branchNodeNames[i];
                }
                return ret;
            }
            branchNodeNames.Add(div.NameRusShort);
            return ToStringBranch(div.ParentDivision, branchNodeNames);
        }
    }
}
