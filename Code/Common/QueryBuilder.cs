using System.Collections.Generic;
using System.Linq;

namespace SOV.Common
{
    public enum QueryPredicate
    {
        AND = 1,
        OR = 2,
        COMMA = 3,
    }

    public enum QueryOper
    {
        EQ = 1,
        LT = 2,
        GT = 3,
        LIKE = 4
    }

    public static class QueryBuilder
    {
        private static readonly Dictionary<QueryPredicate, string> PredicateNames = new Dictionary<QueryPredicate, string>()
        {
            {QueryPredicate.AND, "and"},
            {QueryPredicate.OR, "or"},
            {QueryPredicate.COMMA, ","},
        };

        private static readonly Dictionary<QueryOper, string> OperNames = new Dictionary<QueryOper, string>()
        {
            {QueryOper.EQ, "="},
            {QueryOper.LT, "<"},
            {QueryOper.GT, ">"},
            {QueryOper.LIKE, "like"},
        };

        public static bool IsList(object field)
        {
            if (field == null) return false;
            return field.GetType().IsGenericType &&
                   field.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        /// <summary>
        /// Сформировать строку параметров запроса, соединив через предикат
        /// </summary>
        /// <param name="fields">Словарь параметров, где ключ - название параметра в запросе</param>
        /// <param name="oper">Операция сравнения параметров. По-умолчанию '='</param>
        /// <param name="predicate">Логический предикат, соединяющий блоки параметров. По-умолчанию 'and'</param>
        private static string Join(
            Dictionary<string, object> fields, 
            QueryOper oper = QueryOper.EQ, 
            QueryPredicate predicate = QueryPredicate.AND)
        {
            var conditions = fields.Keys.Select(x => fields[x] == null
                ? ""
                : string.Format("({0} {1} {2})",
                    x,
                    OperNames[oper],
                    IsList(fields[x]) ? ("ANY(:" + x + ")") : (":" + x)
                )
            ).ToList();
            conditions.RemoveAll(x => x == "");
            return string.Join(" " + PredicateNames[predicate] + " ", conditions);
        }

        public static string Where(
            Dictionary<string, object> fields,
            QueryOper oper = QueryOper.EQ,
            QueryPredicate predicate = QueryPredicate.AND)
        {
            var joinString = Join(fields, oper, predicate);
            return joinString == "" ? "" : " Where " + joinString;
        }

        public static string Set(Dictionary<string, object> fields)
        {
            var conditions = fields.Keys.Select(x => fields[x] == null
                ? ""
                : string.Format("{0} = {1}", x, ":" + x)
            ).ToList();
            conditions.RemoveAll(x => x == "");
            string joinString = string.Join(", ", conditions);
            return joinString == "" ? "" : " Set " + joinString;
        }
    }
}