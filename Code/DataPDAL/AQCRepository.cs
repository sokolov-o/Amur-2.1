using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using SOV.Common;

namespace SOV.Amur.DataP
{
    public class AQCRepository
    {
        Common.ADbNpgsql _db;
        internal AQCRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        public List<AQCRole> SelectRoles(int? id = null)
        {
            List<AQCRole> ret = new List<AQCRole>();
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("select * from datap.aqc_role"
                    + ((id.HasValue) ? " where id = " + id : ""), cnn))
                {
                    using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new AQCRole(
                                (int)rdr["id"],
                                (int)rdr["variable_id"],
                                rdr["role"].ToString(),
                                rdr["role_type"].ToString(),
                                rdr["role_description"].ToString(),
                                (bool)rdr["is_critical"],
                                (bool)rdr["is_deletable_by_aqc"]
                                ));
                        }
                        return ret;
                    }
                }
            }
        }

        public void InsertDataValueAQC(List<AQCDataValue> aqcdv)
        {
            foreach (var item in aqcdv)
            {
                InsertDataValueAQC(item.DataValueId, item.AQCRoleId, item.IsAQCApplied);
            }
        }
        public void InsertDataValueAQC(long dvId, int roleId, bool isAQCApplied)
        {
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("insert into datap.aqc_datavalue"
                    + " values (" + dvId + "," + roleId + "," + isAQCApplied + ")", cnn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<AQCDataValue> SelectDataValueAQC(long dvId)
        {
            return SelectDataValueAQC(new List<long>(new long[] { dvId }));
        }
        public List<AQCDataValue> SelectDataValueAQC(List<long> dvId)
        {
            List<AQCDataValue> ret = new List<AQCDataValue>();
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("select * from datap.aqc_datavalue"
                    + " where data_value_id = ANY(:data_value_id)", cnn))
                {
                    cmd.Parameters.AddWithValue("data_value_id", dvId);
                    using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new AQCDataValue()
                            {
                                DataValueId = (long)rdr["data_value_id"],
                                AQCRoleId = (int)rdr["aqc_role_id"],
                                IsAQCApplied = (bool)rdr["is_aqc_applied"]
                            });
                        }
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// Удаление "только удаляемых" критериев качества для заданных значений.
        /// Например, критерий "признак следов осадков" для значения не удаляется.
        /// </summary>
        /// <param name="dvId"></param>
        public void DeleteDataValueAQC(List<long> dvId)
        {
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand("delete from datap.aqc_datavalue"
                    + " where data_value_id in (" + StrVia.ToString(dvId) + ")"
                    + " and aqc_role_id in (select id from datap.aqc_role where is_deletable_by_aqc is true)", cnn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
