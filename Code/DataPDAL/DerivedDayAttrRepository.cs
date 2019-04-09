using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace SOV.Amur.DataP
{
    public class DerivedDayAttrRepository
    {
        Common.ADbNpgsql _db;
        internal DerivedDayAttrRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }
        public DerivedDayAttr Select(int methodDerId,int dateTypeDstId, int siteTypeId, int meteoZoneId, int varId, int offsetTypeId, double offsetValue)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from datap.derived_day_attr  " +
                    " where method_id=:methodDerId and site_type_id=:siteTypeId " +
                    " and meteo_zone=:meteoZoneId and variable_id=:variableId and offset_type_id=:offsetTypeId and offset_value=:offsetValue"+
                    " and dst_date_type_id=:dateTypeDstId", cnn))
                {
                    cmd.Parameters.AddWithValue(":methodDerId", methodDerId);
                    cmd.Parameters.AddWithValue(":siteTypeId", siteTypeId);
                    cmd.Parameters.AddWithValue(":meteoZoneId", meteoZoneId);
                    cmd.Parameters.AddWithValue(":variableId", varId);
                    cmd.Parameters.AddWithValue(":offsetTypeId", offsetTypeId);
                    cmd.Parameters.AddWithValue(":offsetValue", offsetValue);
                    cmd.Parameters.AddWithValue(":dateTypeDstId", dateTypeDstId);

                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {

                        if (rdr.Read())
                        {
                            DerivedDayAttr ret = new DerivedDayAttr(
                                (int)rdr["id"],
                                rdr["name"].ToString(),
                                (int)rdr["method_id"],
                                (int)rdr["site_type_id"],
                                (int)rdr["meteo_zone"],
                                (int)rdr["variable_id"],
                                (int)rdr["offset_type_id"],
                                (double)rdr["offset_value"],
                                (int)rdr ["src_date_type_id"],
                                (int)rdr["hour_add_for_start"],
                                (bool)rdr["is_include_f"],
                                (int)rdr ["dst_date_type_id"]
                                );
                            if (rdr.Read())
                                throw new Exception("Нарушение ключа таблицы.");
                            return ret;
                        }
                        else
                            return null;
                    }

                }
            }
        }

        public bool IsExistDerivedDaySiteAttr(int methodDerId, int siteId, int varId, DateTime dateS, DateTime dateF,
           int offsetTypeId, double offsetValue)
        {
            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select datap.fn_exist_derived_day_attr(:methodDerId,:siteId,:variableId," +
                    ":dateS,:dateF,:offsetTypeId,:offsetValue)", cnn))
                {
                    cmd.Parameters.AddWithValue(":methodDerId", methodDerId);
                    cmd.Parameters.AddWithValue(":siteId", siteId);
                    cmd.Parameters.AddWithValue(":variableId", varId);
                    cmd.Parameters.AddWithValue(":dateS", dateS);
                    cmd.Parameters.AddWithValue(":dateF", dateF);
                    cmd.Parameters.AddWithValue(":offsetTypeId", offsetTypeId);
                    cmd.Parameters.AddWithValue(":offsetValue", offsetValue);

                    return (bool)cmd.ExecuteScalar();

                }
            }
        }
    }
}
