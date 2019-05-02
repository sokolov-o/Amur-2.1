using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Social
{
    public class Enums
    {
        public enum AddrType
        {
            Organization = 40
        }
        public enum LegalEntityType
        {
            Organization = 1,
            Person = 2
        }
        static public char? ToStringLegalEntityType(LegalEntityType let)
        {
            return
                  let == LegalEntityType.Organization ? (char?)'o'
                : let == LegalEntityType.Person ? (char?)'p'
                : null;
        }
    }
}
