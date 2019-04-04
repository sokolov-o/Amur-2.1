using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Parser
{
    public class SysParsersXSitesRepository
    {
        Common.ADbNpgsql _db;
        internal SysParsersXSitesRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }

        public List<SysParsersXSites> Select(int sysObjId)
        {
            return Select(new List<int>(new int[] { sysObjId }));
        }
        public List<SysParsersXSites> Select(List<int> sysObjIds)
        {
            List<SysParsersXSites> ret = new List<SysParsersXSites>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from parser.SysParsersXSitesView  where sysobjid = any(:ids)", cnn))
                {
                    cmd.Parameters.AddWithValue("ids", sysObjIds);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new SysParsersXSites()
                            {
                                Id = (int)rdr["id"],
                                SysObjId = (int)rdr["sysobjid"],
                                ExtSiteId = (rdr.IsDBNull(rdr.GetOrdinal("extsiteid"))) ? null : rdr["extsiteid"].ToString(),
                                isActual = (bool)rdr["isactual"],
                                SysParsersParamsSetId = (int)rdr["sysparsersparamssetid"],
                                SiteId = (int)rdr["siteid"],
                                SiteTypeId = (int)rdr["sitetypeid"],
                                SiteCode = rdr["sitecode"].ToString(),
                                SiteName = rdr["sitename"].ToString()
                            });
                        }
                    }
                }
            }
            return ret;
        }

    }
}
