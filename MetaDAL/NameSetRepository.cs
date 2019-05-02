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
    public class NameSetRepository : BaseRepository<NameSet>
    {
        internal NameSetRepository(Common.ADbNpgsql db) : base(db, "meta.name_set") { }

        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return (NameSet)DataManager.ParseDataIdNameParent(rdr);
        }
        public override List<NameSet> Select(List<int> idList)
        {
            List<NameSet> ret = base.Select(idList);
            List<NameItem> nameItems = DataManager.GetInstance().NameItemRepository.SelectByNameSetId(idList);

            ret.ForEach(y => y.NameItems = nameItems.Where(x => x.NameSetId == y.Id).ToList());

            return ret;
        }
        /// <summary>
        /// Вставка набора имен. Элементы набора, если они есть, не записываются - нужно записывать отдельно.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Insert(NameSet item)
        {
            var fields = new Dictionary<string, object>()
            {
                {"name", item.Name},
                {"parent_id", item.ParentId}
            };
            item.Id = InsertWithReturn(fields);
            return item.Id;
        }
    }
}
