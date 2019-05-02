using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using FERHRI.Common;
using Npgsql;

namespace FERHRI.Amur.Meta
{
    public class SourceRepository
    {
        Common.ADbNpgsql _db;
        internal SourceRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        public int Insert(_DELETE_Source item)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("insert into meta.source (name,name_short,description,name_eng,description_eng)" +
                    " values (:name,:name_short,:description,:name_eng,:description_eng)", cnn))
                {
                    cmd.Parameters.AddWithValue("name", item.Name);
                    cmd.Parameters.AddWithValue("name_short", (!string.IsNullOrEmpty(item.NameShort)) ? (object)item.NameShort : DBNull.Value);
                    cmd.Parameters.AddWithValue("description", (!string.IsNullOrEmpty(item.Description)) ? (object)item.NameShort : DBNull.Value);
                    cmd.Parameters.AddWithValue("name_eng", (!string.IsNullOrEmpty(item.NameEng)) ? (object)item.NameEng : DBNull.Value);
                    cmd.Parameters.AddWithValue("description_eng", (!string.IsNullOrEmpty(item.DescriptionEng)) ? (object)item.DescriptionEng : DBNull.Value);

                    return int.Parse(cmd.ExecuteScalar().ToString());
                }
            }
        }

        public List<Source> Select(List<int> id = null)
        {
            //try
            //{
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.source"
                + ((id != null && id.Count > 0) ? " where id = any(:id)" : ""), cnn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<Source> ret = new List<Source>();
                        while (rdr.Read())
                        {
                            ret.Add(new Source(
                                (int)rdr["id"],
                                rdr["name"].ToString(),
                                (rdr.IsDBNull(rdr.GetOrdinal("name_short"))) ? null : rdr["name_short"].ToString(),
                                (rdr.IsDBNull(rdr.GetOrdinal("description"))) ? null : rdr["description"].ToString(),
                                rdr.HasOrdinal("name_eng") ? (rdr.IsDBNull(rdr.GetOrdinal("name_eng"))) ? null : rdr["name_eng"].ToString() : null,
                                rdr.HasOrdinal("description_eng") ? (rdr.IsDBNull(rdr.GetOrdinal("description_eng"))) ? null : rdr["description_eng"].ToString() : null
                            ));
                        }
                        return ret;
                    }
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //    return ret;
            //}
        }
    }
}
