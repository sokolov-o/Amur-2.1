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
    public class SysParsersParamsRepository
    {
        Common.ADbNpgsql _db;
        internal SysParsersParamsRepository(Common.ADbNpgsql db)
        {
            _db = db;
        }

        public List<SysParsersParams> Select(List<int> sysParsersParamsSetIds)
        {
            List<SysParsersParams> ret = new List<SysParsersParams>();

            using (NpgsqlConnection cnn = _db.Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from parser.sysparsersparamsview where sysparsersparamssetid = any(:ids)", cnn))
                {
                    cmd.Parameters.AddWithValue("ids", sysParsersParamsSetIds);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(new SysParsersParams()
                            {
                                CodeFormId = (int)rdr["codeformid"],
                                ExtLevelId = (int)rdr["extlevelid"],
                                ExtParam = (rdr.IsDBNull(rdr.GetOrdinal("extparam"))) ? null : rdr["extparam"].ToString(),
                                IntOffsetId = (int)rdr["intoffsetid"],
                                IntVariableId = (int)rdr["intvariableld"],
                                Multiplier = (double)rdr["multiplier"],
                                VarErrorDataValue = (double)rdr["varerrordatavalue"],
                                VarNoDataValue = (double)rdr["varnodatavalue"],
                                OffsetDescription = (rdr.IsDBNull(rdr.GetOrdinal("offsetdescription"))) ? null : rdr["offsetdescription"].ToString(),
                                OffsetUnitsId = (int)rdr["offsetunitsid"],
                                SysParsersParamsSetId = (int)rdr["sysparsersparamssetid"],
                                VariableName = (rdr.IsDBNull(rdr.GetOrdinal("variablename"))) ? null : rdr["variablename"].ToString()
                            });
                        }
                    }
                }
            }
            return ret;
        }

    }
}
