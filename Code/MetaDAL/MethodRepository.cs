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
    public class MethodRepository : BaseRepository<Method>
    {
        internal MethodRepository(Common.ADbNpgsql db) : base(db, "meta.method") { }

        public static List<Method> GetCash() { return GetCash(DataManager.GetInstance().MethodRepository); }
        
        /// <summary>
        /// Создать метод.
        /// </summary>
        /// <param name="item">Method instance.</param>
        /// <returns></returns>
        public int Insert(Method item)
        {
            if (item.Id < 1)
            {
                int? idMax = SelectMaxId();
                if (!idMax.HasValue)
                    throw new Exception("(!idMax.HasValue)");
                item.Id = (int)idMax + 1;
            }
            var fields = new Dictionary<string, object>()
            {
                {"id", item.Id},
                {"name", item.Name},
                {"description", item.Description},
                {"parent_id", item.ParentId},
                {"order", item.Order},
                {"model_output_store_parameters",  item.MethodOutputStoreParameters == null ? null: Common.StrVia.ToString(item.MethodOutputStoreParameters) },
                {"legal_entity_id_source", item.SourceLegalEntityId}
            };
            Insert(fields);

            return item.Id;
        }

        public void Update(Method item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"id", item.Id},
                {"name", item.Name},
                {"description", item.Description},
                {"parent_id", item.ParentId},
                {"order", item.Order},
                {"model_output_store_parameters",  item.MethodOutputStoreParameters == null ? null: Common.StrVia.ToString(item.MethodOutputStoreParameters) },
                {"legal_entity_id_source", item.SourceLegalEntityId}
            };
            Update(fields);
        }

        /// <summary>
        /// Поиск первого родительского метода прогноза, у которого MethodOutputStoreParameters != null.
        /// </summary>
        /// <param name="methodId">Код метода, для которого ищется искомый.</param>
        /// <returns>Первый родительский метод прогноза, у которого MethodOutputStoreParameters != null && MethodForecast != null.</returns>
        public MethodForecast SelectParentFcsMethod(int methodId)
        {
            MethodForecast ret = null;
            Method method = Select(methodId);
            if (method.ParentId != null)
            {
                while (true)
                {
                    method = Select((int)method.ParentId);

                    if (method.MethodOutputStoreParameters != null)
                    {
                        if ((ret = DataManager.GetInstance().MethodForecastRepository.Select(method.Id)) != null)
                            break;
                    }
                    if (method.ParentId == null || method.ParentId == method.Id)
                        break;
                }
            }
            return ret;
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new Method(
                (int)rdr["id"],
                rdr["name"].ToString(),
                ADbNpgsql.GetValueString(rdr, "description")
            )
            {
                SourceLegalEntityId = ADbNpgsql.GetValueInt(rdr, "legal_entity_id_source"),
                ParentId = ADbNpgsql.GetValueInt(rdr, "parent_id"),
                Order = (short)rdr["order"],
                MethodOutputStoreParameters = rdr.IsDBNull(rdr.GetOrdinal("model_output_store_parameters")) ? null
                : Common.StrVia.ToDictionary(rdr["model_output_store_parameters"].ToString())
            };
        }
        public int Clone(int methodId)
        {
            Method meth = Select(methodId);

            meth.Id = -1;
            meth.Name = "#" + meth.Name;
            meth.Order = (short)((short)DataManager.GetInstance().MethodRepository.SelectMaxOrder() + 1);

            Insert(meth);

            MethodForecastRepository repFcs = DataManager.GetInstance().MethodForecastRepository;
            MethodForecast methFcs = DataManager.GetInstance().MethodForecastRepository.Select(methodId);
            if (methFcs != null)
            {
                methFcs.Method = meth;
                repFcs.InsertOrUpdate(methFcs);
            }
            else
            {
                MethodClimateRepository repClm = DataManager.GetInstance().MethodClimateRepository;
                MethodClimate methClm = repClm.Select(methodId);
                if (methClm != null)
                {
                    methClm.MethodId = meth.Id;
                    repClm.InsertOrUpdate(methClm);
                }
            }
            return meth.Id;
        }
    }
}
