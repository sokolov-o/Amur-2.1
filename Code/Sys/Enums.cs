using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.Amur.Sys
{
    public enum EntityEnum
    {
        User = 1,
        ProcedureAQC = 2
    }
    public enum AttrEnum
    {
        /// <summary>
        /// Фильтр данных пользователя для отдельной станции и параметров за период
        /// </summary>
        UserFilterData1Site = 1,
        /// <summary>
        /// Фильтр данных пользователя для нескольких станций и параметров за период
        /// </summary>
        UserFilterDataNSites = 6,
        UserSiteGroupId = 2,
        UserOrganizationId = 3,
        UserDirExport = 4,
        UserDirImport = 5,
        /// <summary>
        /// Фильтр каталога данных пользователя 
        /// </summary>
        UserFilterCatalog = 7,
        /// <summary>
        /// Фильтр прогностических данных
        /// </summary>
        UserFilterDataFcs = 8,
        /// <summary>
        /// Фильтр комплекса графиков
        /// </summary>
        UserFilterCharts = 13
    }
}
