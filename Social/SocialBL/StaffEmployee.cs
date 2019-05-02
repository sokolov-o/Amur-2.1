using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.Social
{
    public class StaffEmployee : DateSF
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public int EmployeeId { get; set; }
        public double Percent { get; set; }
        public bool ReadyForInsert
        {
            get
            {
                return StaffId > 0 && EmployeeId > 0 && Percent > 0;
            }
        }
    }
}
