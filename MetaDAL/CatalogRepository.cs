using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using SOV.Common;
using Npgsql;
using SOV.Amur.Meta;

namespace SOV.Amur.Meta
{
    public class CatalogRepository : BaseRepository<Catalog>
    {
        internal CatalogRepository(Common.ADbNpgsql db) : base(db, "data.catalog") { }

        /// <summary>
        /// Функция парсинга строки результата запроса. Используется по умолчанию в ExecQuery.
        /// </summary>
        protected override object ParseData(NpgsqlDataReader reader)
        {
            return new Catalog(
                (int)reader["id"],
                (int)reader["site_id"],
                (int)reader["variable_id"],
                (int)reader["method_id"],
                (int)reader["source_id"],
                (int)reader["offset_type_id"],
                (double)reader["offset_value"],
                (int)reader["parent_id"]
            );
        }

        //static internal Catalog Parse(Npgsql.NpgsqlDataReader rdr)
        //{
        //    return new Catalog(
        //        (int)rdr["id"],
        //        (int)rdr["site_id"],
        //        (int)rdr["variable_id"],
        //        (int)rdr["method_id"],
        //        (int)rdr["source_id"],
        //        (int)rdr["offset_type_id"],
        //        (double)rdr["offset_value"],
        //        (int)rdr["parent_id"]
        //        );
        //}

        public List<Catalog> Select(List<int> siteId, List<int> varId, List<int> methodId, List<int> sourceId, List<int> offsetTypeId, double? offsetValue)
        {
            return Select(siteId, varId, methodId, sourceId, offsetTypeId,
                offsetValue.HasValue ? new List<double>() { (double)offsetValue } : null);
        }
        /// <summary>
        /// Каждый параметр может быть null. Это означает, что будут выбираться все значения для данного параметра.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="varId"></param>
        /// <param name="offsetTypeId"></param>
        /// <param name="methodId"></param>
        /// <param name="sourceId"></param>
        /// <param name="offsetValue"></param>
        /// <returns></returns>
        public List<Catalog> Select(List<int> siteId, List<int> varId, List<int> methodId, List<int> sourceId, List<int> offsetTypeId, List<double> offsetValue)
        {
            var fields = new Dictionary<string, object>()
            {
                {"site_id", siteId},
                {"variable_id", varId},
                {"method_id", methodId},
                {"source_id", sourceId},
                {"offset_type_id", offsetTypeId},
                {"offset_value", offsetValue}
            };
            return Select(fields);

            //List<Catalog> ret = new List<Catalog>();
            //using (var cnn = _db.Connection)
            //{
            //    using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(
            //        "select * from data.catalog where "
            //        + "     (:site_id is null or site_id = any(:site_id))"
            //        + " and (:variable_id is null or variable_id = any(:variable_id))"
            //        + " and (:method_id is null or method_id = any(:method_id))"
            //        + " and (:source_id is null or source_id = any(:source_id))"
            //        + " and (:offset_type_id is null or offset_type_id = any(:offset_type_id))"
            //        + " and (:offset_value is null or offset_value = any(:offset_value))"
            //        , cnn))
            //    {
            //        cmd.Parameters.AddWithValue("site_id", siteId);
            //        cmd.Parameters.AddWithValue("variable_id", varId);
            //        cmd.Parameters.AddWithValue("method_id", methodId);
            //        cmd.Parameters.AddWithValue("source_id", sourceId);
            //        cmd.Parameters.AddWithValue("offset_type_id", offsetTypeId);
            //        cmd.Parameters.AddWithValue("offset_value", offsetValue);

            //        using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                ret.Add((Catalog)ParseData(rdr));
            //            }
            //            return ret;
            //        }
            //    }
            //}
        }
        public List<Catalog> Select(CatalogFilter cf)
        {
            //return Select(
            //    cf.Sites,
            //    cf.Variables,
            //    cf.Methods == null || cf.Methods.Count == 0 ? null : (int?)cf.Methods[0],
            //    cf.Sources == null || cf.Sources.Count == 0 ? null : (int?)cf.Sources[0],
            //    cf.OffsetTypes == null || cf.OffsetTypes.Count == 0 ? null : (int?)cf.OffsetTypes[0],
            //    cf.OffsetValue
            //);
            return Select(cf.Sites, cf.Variables, cf.Methods, cf.Sources, cf.OffsetTypes,
                cf.OffsetValue.HasValue ? new List<double>() { (double)cf.OffsetValue } : null);
        }
        //public List<Catalog> Select(List<int> siteId = null, List<int> varId = null, int? offsetTypeId = null, int? methodId = null, int? sourceId = null, double? offsetValue = null)
        //{
        //    List<Catalog> ret = Select(
        //        siteId,
        //        varId,
        //        methodId.HasValue ? new List<int>(new int[] { (int)methodId }) : null,
        //        sourceId.HasValue ? new List<int>(new int[] { (int)sourceId }) : null,
        //        offsetTypeId.HasValue ? new List<int>(new int[] { (int)offsetTypeId }) : null,
        //        offsetValue);
        //    //throw new Exception(ret.Count.ToString());
        //    return ret;
        //}
        ////public Catalog Select(int ctlId)
        ////{
        ////    List<Catalog> ret = Select(new List<int>(new int[] { ctlId }));
        ////    return ret.Count == 0 ? null : ret[0];
        ////}
        ////public List<Catalog> Select(List<int> ctlIds)
        ////{
        ////    if (ctlIds == null) throw new Exception("(ctlId == null)");

        ////    List<Catalog> ret1 = new List<Catalog>();
        ////    using (var cnn = _db.Connection)
        ////    {
        ////        using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(
        ////            "select * from data.catalog where id = ANY(:ctl_id)"
        ////            , cnn))
        ////        {
        ////            cmd.Parameters.AddWithValue("ctl_id", ctlIds);
        ////            using (Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader())
        ////            {
        ////                while (rdr.Read())
        ////                {
        ////                    ret1.Add((Catalog)ParseData(rdr));
        ////                }
        ////                // Order ret as input
        ////                List<Catalog> ret = new List<Catalog>();
        ////                foreach (var i in ctlIds)
        ////                {
        ////                    ret.Add(ret1.Find(x => x.Id == i));
        ////                }
        ////                return ret;
        ////            }
        ////        }
        ////    }
        ////}

        public Catalog Insert(Catalog catalog)
        {
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(
                    "insert into data.catalog" +
                    " (site_id, variable_id, offset_type_id, method_id, source_id, offset_value)"
                    + "values (" + catalog.SiteId + ", " + catalog.VariableId
                    + ", " + catalog.OffsetTypeId + ", " + catalog.MethodId
                    + ", " + catalog.SourceId + ",  " + catalog.OffsetValue
                    + ");"
                    + "select max(id) from data.catalog;"
                    , cnn))
                {
                    catalog.Id = int.Parse(cmd.ExecuteScalar().ToString());

                    return catalog;
                }
            }
        }
        public void Update(Catalog catalog)
        {
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(
                    "update data.catalog" +
                    " set site_id=" + catalog.SiteId +
                    ",variable_id=" + catalog.VariableId +
                    ",offset_type_id=" + catalog.OffsetTypeId +
                    ",method_id=" + catalog.MethodId +
                    ",source_id=" + catalog.SourceId +
                    ",offset_value=" + catalog.OffsetValue +
                    " where id =" + catalog.Id
                    , cnn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Catalog SelectForecastCatalog(int siteId, int obsVariableId, int fcsMethodId, int fcsSourceId, int fcsOffsetTypeId, double fcsOffsetValue)
        {
            List<Catalog> ret = new List<Catalog>();
            using (var cnn = _db.Connection)
            {
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(
                    "data.get_catalog_forecast(:site_id,:variable_id_ini,:offset_type_id,:method_id,:source_id,:offset_value)", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("site_id", siteId);
                    cmd.Parameters.AddWithValue("variable_id_ini", obsVariableId);
                    cmd.Parameters.AddWithValue("method_id", fcsMethodId);
                    cmd.Parameters.AddWithValue("source_id", fcsSourceId);
                    cmd.Parameters.AddWithValue("offset_type_id", fcsOffsetTypeId);
                    cmd.Parameters.AddWithValue("offset_value", fcsOffsetValue);

                    string id = cmd.ExecuteScalar().ToString();
                    return string.IsNullOrEmpty(id) ? null : Select(int.Parse(id));
                }
            }
        }
        public Catalog Select(int siteId, int varId, int methodId, int sourceId, int offsetTypeId, double? offsetValue)
        {
            List<Catalog> ret = Select(

                new List<int>(new int[] { siteId }),
                new List<int>(new int[] { varId }),
                new List<int>(new int[] { methodId }),
                new List<int>(new int[] { sourceId }),
                new List<int>(new int[] { offsetTypeId }),
                offsetValue.HasValue ? new List<double>() { (double)offsetValue } : null
                );

            if (ret.Count > 1)
                throw new Exception("(ret.Count > 1)");
            return ret.Count == 1 ? ret[0] : null;
        }
    }
}
