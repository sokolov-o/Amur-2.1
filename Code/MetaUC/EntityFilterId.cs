using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Amur.Meta
{
    /// <summary>
    /// 
    /// Класс фильтров для разных сущностей. 
    /// Каждая сущность должна иметь форму фильтра с методом типа GetFilteredId или GetFilteredXXXX, 
    /// который возвращает  выбранные пользователем коды элементов сущности 
    /// или сами элементы с кодами (экземпляры класса сущности).
    /// 
    /// Пример:
    /// <code>
    ///     // Сущность Site
    ///     case "site_view":
    ///     if (fsf == null) fsf = new FormSiteFilter();
    ///     return fsf.GetFilteredSites().Select(x => x.Id).ToList();
    /// </code>
    /// 
    /// Не все сущности имеют формы фильтров. Поэтому: 
    ///     1) формы фильтров нужно создавать для сущностей и 
    ///     2) добавлять обработку формы в этот класс в метод GetFilteredId.
    /// 
    /// </summary>
    public class EntityFilterId
    {
        FormSiteFilter fsf = null;
        /// <summary>
        /// Получить коды элементов сущности, соответствующих установленному фильтру.
        /// </summary>
        /// <returns>Коды сущностей, соответствующих установленному фильтру.</returns>
        public List<int> GetFilteredId(string entityName)
        {
            switch (entityName)
            {
                case "site":
                    if (fsf == null) fsf = new FormSiteFilter();
                    return fsf.GetFilteredSites().Select(x => x.Id).ToList();
                default:
                    throw new Exception("Неизвестная сущность " + entityName + ".");
            }
        }

    }
}
