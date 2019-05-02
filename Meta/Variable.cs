using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using System.Runtime.Serialization;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class Variable : VariableVirtual
    {
        [DataMember]
        public string NameRus;
        [DataMember]
        public string NameEng;
        [DataMember]
        public int ValueTypeId { get; set; }
        [DataMember]
        public double CodeNoData { get; set; }
        [DataMember]
        public double CodeErrData { get; set; }

        // FK
        public List<VariableAttributes> VariableAttributes { get; set; }

        public static List<Common.DicItem> ToList<T1>(List<Variable> items)
        {
            List<DicItem> ret = new List<DicItem>();
            if (items != null)
                foreach (var item in items)
                {
                    ret.Add(new DicItem(item.Id, item.NameRus));
                }
            return ret;
        }

        public static List<IdName> ToListIdName(List<Variable> items, EnumLanguage enumLanguage)
        {
            return items.Select(x => new IdName() { Id = x.Id, Name = enumLanguage == EnumLanguage.Rus ? x.NameRus : x.NameEng }).ToList();
        }


        public Variable
        (
            int id,
            int VariableTypeId,
            int TimeId,
            int UnitId,
            int DataTypeId,
            int ValueTypeId,
            int GeneralCategoryId,
            int SampleMediumId,
            int TimeSupport,
            string NameRus,
            string NameEng,
            double CodeNoData,
            double CodeErrData
        )
        {
            Id = id;
            this.VariableTypeId = VariableTypeId;
            this.TimeId = TimeId;
            this.UnitId = UnitId;
            this.DataTypeId = DataTypeId;
            this.ValueTypeId = ValueTypeId;
            this.GeneralCategoryId = GeneralCategoryId;
            this.SampleMediumId = SampleMediumId;
            this.TimeSupport = TimeSupport;
            this.NameRus = NameRus;
            this.NameEng = NameEng;
            this.CodeNoData = CodeNoData;
            this.CodeErrData = CodeErrData;
        }

        public static List<Common.DicItem> ToDicList(List<Variable> items)
        {
            List<DicItem> ret = new List<DicItem>();
            foreach (var item in items)
            {
                ret.Add(new DicItem(item.Id, item.NameRus));
            }
            return ret;
        }

        [DataMember]
        public string NameC
        {
            get
            {
                return ToString();
            }
            set { }
        }
        override public string ToString()
        {
            return NameRus + "/" + Id;
        }
        public Variable Clone()
        {
            return new Variable(
                Id,
                VariableTypeId,
                TimeId,
                UnitId,
                DataTypeId,
                VariableTypeId,
                GeneralCategoryId,
                SampleMediumId,
                TimeSupport,
                NameRus.Clone() as string,
                NameEng.Clone() as string,
                CodeNoData,
                CodeErrData
            );
        }
    }

    /// <summary>
    /// Кодом виртуальной переменной, при выборке из БД, является МИНИМАЛЬНОЕ значение кода
    /// переменной из всех переменных, соответствующих данной (виртуальной) переменной.
    /// </summary>
    [DataContract]
    public class VariableVirtual : IdClass
    {
        [DataMember]
        public int VariableTypeId { get; set; }
        [DataMember]
        public int TimeId { get; set; }
        [DataMember]
        public int UnitId { get; set; }
        [DataMember]
        public int DataTypeId { get; set; }
        [DataMember]
        public int GeneralCategoryId { get; set; }
        [DataMember]
        public int SampleMediumId { get; set; }
        [DataMember]
        public int TimeSupport { get; set; }
    }

    //public class _DELME_Variable : IdName
    //{
    //    [DataMember]
    //    public int VariableTypeId { get; set; }
    //    [DataMember]
    //    public int TimeId { get; set; }
    //    [DataMember]
    //    public int UnitId { get; set; }
    //    [DataMember]
    //    public int DataTypeId { get; set; }
    //    [DataMember]
    //    public int ValueTypeId { get; set; }
    //    [DataMember]
    //    public int GeneralCategoryId { get; set; }
    //    [DataMember]
    //    public int SampleMediumId { get; set; }
    //    [DataMember]
    //    public int TimeSupport { get; set; }
    //    [DataMember]
    //    public string NameEng { get; set; }
    //    [DataMember]
    //    public double CodeNoData { get; set; }
    //    [DataMember]
    //    public double CodeErrData { get; set; }

    //    // FK
    //    public List<VariableAttributes> VariableAttributes { get; set; }

    //    public static List<Common.DicItem> ToList<T1>(List<Variable> items)
    //    {
    //        List<DicItem> ret = new List<DicItem>();
    //        if (items != null)
    //            foreach (var item in items)
    //            {
    //                ret.Add(new DicItem(item.Id, item.Name));
    //            }
    //        return ret;
    //    }


    //    public Variable
    //    (
    //        int id,
    //        int VariableTypeId,
    //        int TimeId,
    //        int UnitId,
    //        int DataTypeId,
    //        int ValueTypeId,
    //        int GeneralCategoryId,
    //        int SampleMediumId,
    //        int TimeSupport,
    //        string Name,
    //        string NameEng,
    //        double CodeNoData,
    //        double CodeErrData
    //    )
    //    {
    //        Id = id;
    //        this.VariableTypeId = VariableTypeId;
    //        this.TimeId = TimeId;
    //        this.UnitId = UnitId;
    //        this.DataTypeId = DataTypeId;
    //        this.ValueTypeId = ValueTypeId;
    //        this.GeneralCategoryId = GeneralCategoryId;
    //        this.SampleMediumId = SampleMediumId;
    //        this.TimeSupport = TimeSupport;
    //        this.Name = Name;
    //        this.NameEng = NameEng;
    //        this.CodeNoData = CodeNoData;
    //        this.CodeErrData = CodeErrData;
    //    }

    //    public static List<Common.DicItem> ToDicList(List<Variable> items)
    //    {
    //        List<DicItem> ret = new List<DicItem>();
    //        foreach (var item in items)
    //        {
    //            ret.Add(new DicItem(item.Id, item.Name));
    //        }
    //        return ret;
    //    }
    //    [DataMember]
    //    public string NameC
    //    {
    //        get
    //        {
    //            return ToString();
    //        }
    //        set { }
    //    }
    //    override public string ToString()
    //    {
    //        return Name + "/" + Id;
    //    }
    //    public Variable Clone()
    //    {
    //        return new Variable(
    //            Id,
    //            VariableTypeId,
    //            TimeId,
    //            UnitId,
    //            DataTypeId,
    //            VariableTypeId,
    //            GeneralCategoryId,
    //            SampleMediumId,
    //            TimeSupport,
    //            Name.Clone() as string,
    //            NameEng.Clone() as string,
    //            CodeNoData,
    //            CodeErrData
    //        );
    //    }
    //}
}
