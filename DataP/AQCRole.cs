using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.DataP
{
    /// <summary>
    /// Правила AQC.
    /// </summary>
    public class AQCRole
    {
        public int Id { get; set; }
        public int VariableId { get; set; }
        public Dictionary<string, string> Role { get; set; }
        public string RoleType { get; set; }
        public string RoleDescription { get; set; }
        public bool IsCritical { get; set; }
        public bool IsDeletableByAQC { get; set; }

        public AQCRole
        (
            int Id,
            int VariableId,
            string Role,
            string RoleType,
            string RoleDescription,
            bool IsCritical,
            bool IsDeletableByAQC
        )
        {
            this.Id = Id;
            this.VariableId = VariableId;
            this.Role = Common.StrVia.ToDictionary(Role);
            this.RoleType = RoleType;
            this.RoleDescription = RoleDescription;
            this.IsCritical = IsCritical;
            this.IsDeletableByAQC = IsDeletableByAQC;
        }
        public override string ToString()
        {
            return RoleDescription;
        }
    }
}
