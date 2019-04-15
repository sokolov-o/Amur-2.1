using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using Npgsql;

namespace SOV.Social
{
    public class OrgRepository : BaseRepository<Org>
    {
        internal OrgRepository(Common.ADbNpgsql db) : base(db, "social.entity_org") {}

        protected override object ParseData(NpgsqlDataReader reader)
        {
            return new Org()
            {
                Id = (int)reader["legal_entity_id"],
                StaffIdFirstFace = ADbNpgsql.GetValueInt(reader, "staff_id_first_face")
            };
        }

        public override List<Org> Select(List<int> idList)
        {
            return Select(new Dictionary<string, object>() { { "legal_entity_id", idList } });
        }

        public List<Org> SelectAll()
        {
            return Select();
        }
        /// <summary>
        /// Получить список по списку кодов сущностей.
        /// </summary>
        /// <param name="ids">Список кодов ресурсов.</param>
        /// <returns>Список ресурсов.</returns>
        public List<Org> SelectById(List<int> ids)
        {
            return Select(ids);
        }

        public override void Delete(Org org)
        {
            Delete(new Dictionary<string, object> { { "legal_entity_id", org.Id } });
        }

        public override void Delete(Dictionary<string, object> fields)
        {
            fields = new Dictionary<string, object> { { "legal_entity_id", fields["legal_entity_id"] } };
            Delete(new List<Dictionary<string, object>>() { fields } );
        }

        public override void Update(Dictionary<string, object> fields)
        {
            Update(new List<Dictionary<string, object>>() { fields }, new List<string>() { "legal_entity_id" });
        }
    }
}
