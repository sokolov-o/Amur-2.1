using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    /// <summary>
    /// Коды меньшие 1 и пустые строки преобразуются в null.
    /// </summary>
    public class SiteNewFilter
    {
        string _CodeLike = null;
        public string CodeLike { get { return _CodeLike; } set { _CodeLike = string.IsNullOrEmpty(value) ? null : value; } }
        string _NameLike = null;
        public string NameLike { get { return _NameLike; } set { _NameLike = string.IsNullOrEmpty(value) ? null : value; } }
        int? _SiteTypeId = null;
        public int? SiteTypeId { get { return _SiteTypeId; } set { _SiteTypeId = value < 1 ? null : value; } }
        int? _AddrId = null;
        public int? AddrId { get { return _AddrId; } set { _AddrId = value < 1 ? null : value; } }
        int? _OrgId = null;
        /// <summary>
        /// Код организации.
        /// </summary>
        public int? OrgId { get { return _OrgId; } set { _OrgId = value < 1 ? null : value; } }
    }
}
