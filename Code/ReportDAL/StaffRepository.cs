using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FERHRI.Common;
using FERHRI.Amur.Meta;
using Npgsql;

namespace FERHRI.Amur.Report
{
    public class StaffRepository : BaseRepository<Staff>
    {
        internal StaffRepository(Common.ADbNpgsql db) : base(db, "report.staff") { }

        protected override object ParseData(NpgsqlDataReader reader)
        {
            return new Staff(
                (int)reader["id"],
                (int)reader["person_id"],
                (int)reader["person_pos_id"],
                (int)reader["org_id"]
            );
        }

        /// <summary>
        /// Получить словарь полей таблицы для отображения в Grid. 
        /// Ключ - имя поля класса; Значение - имя/title для отображения.
        /// </summary>
        public override List<TableRowField> ViewTableFields()
        {
            var prep = DataManager.GetInstance();
            var persons = prep.PersonRepository.Select().Select(x => new DicItem(x.id, x.name));
            var personsPos = prep.PersonPosRepository.Select().Select(x => new DicItem(x.id, x.name));
            var orgs = ((List<Social.LegalEntity>)Social.DicCash.GetList(typeof(Social.LegalEntity))).Select(x => new DicItem(x.Id, x.NameRus));
            return new List<TableRowField>()
            {
                new TableRowField("id", "Id", TableRowFieldType.Text, null, false, false),
                new TableRowField("personId", "Человек", TableRowFieldType.ComboBox, persons.ToList()),
                new TableRowField("personPosId", "Должность", TableRowFieldType.ComboBox, personsPos.ToList()),
                new TableRowField("orgId", "Организация", TableRowFieldType.ComboBox, orgs.ToList()),
            };
        }
    }
}
