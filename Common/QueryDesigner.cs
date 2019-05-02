using System.Collections.Generic;
using System.Linq;

namespace FERHRI.Common
{
    public static class QueryDesigner
    {
        public static bool IsList(object field)
        {
            if (field == null) return false;
            return field.GetType().IsGenericType &&
                   field.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        /// <summary>
        /// Сформировать строку параметров запроса, соединив через предикат "И"
        /// </summary>
        /// <param name="fields">Словарь параметров, где ключ - название параметра в запросе</param>
        public static string JoinByAnd(Dictionary<string, object> fields)
        {
            return string.Join(
                " and ",
                fields.Keys.Select(x => string.Format("(:{0} is null or {0} = {1})",
                    x,
                    IsList(fields[x]) ? ("ANY(:" + x + ")") : (":" + x)
                ))
            );
        }
    }
}