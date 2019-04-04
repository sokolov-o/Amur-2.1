using System.Collections.Generic;
using SOV.Common;
using Npgsql;

namespace SOV.Social
{
    public class LegalEntityXImageRepository : BaseRepository<LegalEntityXImage>
    {
        public LegalEntityXImageRepository(Common.ADbNpgsql db) : base(db, "social.legal_entity_x_image") { }

        protected override object ParseData(NpgsqlDataReader reader)
        {
            return new LegalEntityXImage() { OrgId = (int)reader["org_id"], ImageId = (int)reader["image_id"] };
        }

        public override void Delete(Dictionary<string, object> fields)
        {
            fields = new Dictionary<string, object> { { "org_id", fields["org_id"] }, { "image_id", fields["image_id"] } };
            Delete(new List<Dictionary<string, object>>() { fields });
        }

        public List<LegalEntityXImage> SelectByOrgs(List<int> orgs)
        {
            var fields = new Dictionary<string, object>() {{"org_id", orgs}};
            return Select(fields);
        }
    }
}
