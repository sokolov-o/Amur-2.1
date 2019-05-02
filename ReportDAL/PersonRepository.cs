using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FERHRI.Common;
using Npgsql;

namespace FERHRI.Amur.Report
{
    public class PersonRepository : BaseRepository<Person>
    {
        internal PersonRepository(Common.ADbNpgsql db) : base(db, "report.person") { }

        /// <summary>
        /// Получить словарь полей таблицы для отображения в Grid. 
        /// Ключ - имя поля класса; Значение - имя/title для отображения.
        /// </summary>
        public override List<TableRowField> ViewTableFields()
        {
            return new List<TableRowField>()
            {
                new TableRowField("id", "Id", TableRowFieldType.Text, null, false, false),
                new TableRowField("name", "Имя")
            };
        }

        protected override object ParseData(NpgsqlDataReader reader)
        {
            return new Person(
                (int)reader["id"],
                reader["name"].ToString()
            );
        }
    }
}
