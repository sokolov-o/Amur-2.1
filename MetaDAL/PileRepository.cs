using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Meta
{
    public class PileRepository
    {
        Common.ADbNpgsql _db;
        internal PileRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        public List<PileSet> SelectPiles(List<int> pileSetId)
        {
            List<PileSet> ret = new List<PileSet>();
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from meta.pile_view" +
                    " where :pile_set_id is null or pile_set_id = any(:pile_set_id)", cnn))
                {
                    cmd.Parameters.AddWithValue("pile_set_id", pileSetId);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int ps_id = (int)rdr["pile_set_id"];
                            PileSet ps = ret.FirstOrDefault(x => x.Id == ps_id);
                            if (ps == null)
                            {
                                ps = new PileSet()
                                {
                                    Id = ps_id,
                                    NameRus = ADbNpgsql.GetValueString(rdr, "pile_set_name_rus"),
                                    NameEng = ADbNpgsql.GetValueString(rdr, "pile_set_name_eng")
                                };
                                ret.Add(ps);
                            }
                            ps.Piles.Add(new PileSet.Pile()
                            {
                                Id = (int)rdr["id"],
                                PileSetId = ps_id,
                                NameRus = ADbNpgsql.GetValueString(rdr, "name_rus"),
                                NameEng = ADbNpgsql.GetValueString(rdr, "name_eng"),
                                Value1 = ADbNpgsql.GetValueDouble(rdr, "value1"),
                                Value2 = ADbNpgsql.GetValueDouble(rdr, "value2"),
                                OrderBy = (int)ADbNpgsql.GetValueInt(rdr, "order_by"),
                            });
                        }
                    }
                }
                return ret;
            }
        }
    }
}
