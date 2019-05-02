using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class VariableFilter
    {
        [DataMember]
        public List<int> VariableTypeIds { get; set; }
        [DataMember]
        public List<int> ValueTypeIds { get; set; }
        [DataMember]
        public List<int> TimeIds { get; set; }
        [DataMember]
        public List<int> TimeSupports { get; set; }
        [DataMember]
        public List<int> UnitIds { get; set; }
        [DataMember]
        public List<int> DataTypeIds { get; set; }
        [DataMember]
        public List<int> GeneralCategoryIds { get; set; }
        [DataMember]
        public List<int> SampleMediumIds { get; set; }
    }
}
