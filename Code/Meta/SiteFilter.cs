using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Amur.Meta
{
    /// <summary>
    /// Коды < 1 и пустые строки преобразуются в null.
    /// </summary>
    public class SiteFilter
    {
        string _StationCodeLike;
        public string StationCodeLike { get { return _StationCodeLike; } set { _StationCodeLike = string.IsNullOrEmpty(value) ? null : value; } }
        string _StationNameLike;
        public string StationNameLike { get { return _StationNameLike; } set { _StationNameLike = string.IsNullOrEmpty(value) ? null : value; } }
        int? _StationTypeId;
        public int? StationTypeId { get { return _StationTypeId; } set { _StationTypeId = value < 1 ? null : value; } }
        int? _SiteTypeId;
        public int? SiteTypeId { get { return _SiteTypeId; } set { _SiteTypeId = value < 1 ? null : value; } }
        int? _AddrId;
        public int? AddrId { get { return _AddrId; } set { _AddrId = value < 1 ? null : value; } }
        int? _OrgId;
        /// <summary>
        /// Код организации.
        /// </summary>
        public int? OrgId { get { return _OrgId; } set { _OrgId = value < 1 ? null : value; } }
    }
}
