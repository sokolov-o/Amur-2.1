using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using Npgsql;

namespace SOV.Social
{
    public class PersonRepository : BaseRepository<Person>
    {
        internal PersonRepository(Common.ADbNpgsql db) : base(db, "social.entity_person") { }

        protected override object ParseData(NpgsqlDataReader reader)
        {
            return new Person() {LegalEntityId = (int)reader["legal_entity_id"], Sex = ADbNpgsql.GetValueChar(reader, "sex") };
        }

        public override List<Person> Select(List<int> idList)
        {
            return Select(new Dictionary<string, object>() { { "legal_entity_id", idList } });
        }

        public List<Person> SelectAll()
        {
            return SelectById(null);
        }
        /// <summary>
        /// Получить список по списку кодов сущностей.
        /// </summary>
        /// <param name="ids">Список кодов ресурсов.</param>
        /// <returns>Список ресурсов.</returns>
        public List<Person> SelectById(List<int> ids)
        {
            return Select(ids);
        }

        public override void Delete(Person person)
        {
            Delete(new Dictionary<string, object> { { "legal_entity_id", person.LegalEntityId } });
        }

        public override void Delete(Dictionary<string, object> fields)
        {
            fields = new Dictionary<string, object> { { "legal_entity_id", fields["legal_entity_id"] } };
            Delete(new List<Dictionary<string, object>>() { fields });
        }

        public override void Update(Dictionary<string, object> fields)
        {
            Update(new List<Dictionary<string, object>>() { fields }, new List<string>() { "legal_entity_id" });
        }
    }
}
