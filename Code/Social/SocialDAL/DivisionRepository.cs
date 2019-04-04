using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using SOV.Social;
using Npgsql;

namespace SOV.Social
{
    public class DivisionRepository : BaseRepository<Division>
    {
        internal DivisionRepository(ADbNpgsql db) : base(db, "social.division") { }

        static public Dictionary<string, object> GetFieldDictionary(Division item, bool withId)
        {
            Dictionary<string, object> ret = IdNameRus.GetFieldDictionary(item, withId);

            ret.Add("leorg_id_employer", item.Employer.Id);
            ret.Add("date_s", item.DateS);
            ret.Add("date_f", item.DateF);
            ret.Add("parent_id", item.ParentDivision == null ? DBNull.Value : (object)item.ParentDivision.Id);

            return ret;
        }
        /// <summary>
        /// Получить список тем.
        /// </summary>
        /// <param name="yearS"></param>
        /// <param name="yearF"></param>
        /// <param name="withChilds">Включить в общий список темы-наследники.</param>
        /// <returns></returns>
        public List<Division> SelectRoots(DateTime dateActual, bool withChilds)
        {
            //List<Division> parents = ExecQuery<Division>("select * from " + TableName + " where parent_id is null", new Dictionary<string, object>(), ParseData);
            //parents.Where(x => x.DateS.Year >= yearS && x.DateS.Year <= yearF && x.DateF.Year >= yearS && x.DateF.Year <= yearF);

            //if (withChilds)
            //    return Select(yearS, yearF, parents.Select(x => x.Id).ToList(), withChilds);
            //return parents;
            throw new NotImplementedException();
        }

        public List<Division> Select(int employerId, DateTime? dateActual)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>()
            {
                { "employer_id", employerId},
                {"date_actual",dateActual }
            };
            List<Division> ret = ExecQuery<Division>(DateSF.GetSelectSql(TableName, fields), fields, ParseData);
            return UpdateFK(ret);
        }

        public List<Division> UpdateFK(List<Division> ret)
        {
            List<LegalEntity> emps = SOV.Social.DataManager.GetInstance().LegalEntityRepository.Select(ret.Select(x => x.Employer.Id).ToList());
            ret.ForEach(x => x.Employer = emps.Find(y => y.Id == x.Employer.Id));

            ret.ForEach(x => x.ParentDivision = x.ParentDivision == null ? null : ret.Find(y => y.Id == x.ParentDivision.Id));
            List<Division> parents = ret.Where(x => x.ParentDivision != null && string.IsNullOrEmpty(x.ParentDivision.NameRus)).ToList();
            if (parents.Count > 0)
            {
                parents = Select(parents.Select(x => x.Id).ToList());
                ret.ForEach(x => x.ParentDivision = x.ParentDivision == null ? null : parents.Find(y => y.Id == x.ParentDivision.Id));
            }

            return ret;
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            IdNameRus nr = (IdNameRus)IdNameRus.ParseData(rdr);
            DateSF dateSF = DateSF.ParseData(rdr);
            int? parentId = ADbNpgsql.GetValueInt(rdr, "parent_id");

            return new Division()
            {
                Id = nr.Id,
                NameRus = nr.NameRus,
                NameRusShort = nr.NameRusShort,
                ParentDivision = parentId.HasValue ? new Division() { Id = (int)parentId } : null,
                Employer = new LegalEntity() { Id = (int)rdr["employer_id"] },
                DateS = dateSF.DateS,
                DateF = dateSF.DateF
            };
        }
        public int Insert(Division item)
        {
            return InsertWithReturn(GetFieldDictionary(item, false));
        }
        public void Update(Division item)
        {
            Update(GetFieldDictionary(item, true));
        }
    }
}
