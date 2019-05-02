using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FERHRI.Common;
using Npgsql;

namespace FERHRI.Amur.Report
{
    public class PersonPosRepository : BaseRepository<PersonPosition>
    {
        internal PersonPosRepository(Common.ADbNpgsql db) : base(db, "report.person_position") { }

        protected override object ParseData(NpgsqlDataReader reader)
        {
            return new PersonPosition(
                (int)reader["id"],
                reader["name"].ToString()
            );
        }

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
    }
}
