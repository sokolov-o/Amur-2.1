using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    /// <summary>
    /// Коды < 1 и пустые строки преобразуются в null.
    /// </summary>
    public class SiteFilter
    {
        string _CodeLike;
        public string CodeLike { get { return _CodeLike; } set { _CodeLike = string.IsNullOrEmpty(value) ? null : value; } }
        string _NameLike;
        public string NameLike { get { return _NameLike; } set { _NameLike = string.IsNullOrEmpty(value) ? null : value; } }
        int? _TypeId;
        public int? TypeId { get { return _TypeId; } set { _TypeId = value < 1 ? null : value; } }
        int? _AddrId;
        public int? AddrId { get { return _AddrId; } set { _AddrId = value < 1 ? null : value; } }
        int? _OrgId;
        /// <summary>
        /// Код организации.
        /// </summary>
        public int? OrgId { get { return _OrgId; } set { _OrgId = value < 1 ? null : value; } }
    }
}
