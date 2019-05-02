using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Meta
{
    public class DataManager : Common.BaseDataManager
    {
        public PileRepository PileRepository;
        public VariableRepository VariableRepository;
        public VariableVirtualRepository VariableVirtualRepository;
        public EntityGroupRepository EntityGroupRepository;
        public EntityRepository EntityRepository;
        public EntityAttrRepository EntityAttrRepository;
        public GeoObjectRepository GeoObjectRepository;
        public CodeFormRepository CodeFormRepository;
        public DataTypeRepository DataTypeRepository;
        public FlaAQCRepository FlaAQCRepository;
        public GeneralCategoryRepository GeneralCategoryRepository;
        public GeoTypeRepository GeoTypeRepository;
        public MethodRepository MethodRepository;
        public MethodForecastRepository MethodForecastRepository;
        public MethodClimateRepository MethodClimateRepository;
        public OffsetTypeRepository OffsetTypeRepository;
        public SampleMediumRepository SampleMediumRepository;
        public SiteAttrTypeRepository SiteAttrTypeRepository;
        public SiteGeoObjectRepository SiteGeoObjectRepository;
        public SiteTypeRepository SiteTypeRepository;
        public StationAddrRegionRepository StationAddrRegionRepository;
        public UnitRepository UnitRepository;
        public ValueTypeRepository ValueTypeRepository;
        public VariableCodeRepository VariableCodeRepository;
        public VariableAttributesRepository VariableAttributesRepository;
        public VariableTypeRepository VariableTypeRepository;
        public MeteoZoneRepository MeteoZoneRepository;
        public CatalogRepository CatalogRepository;
        public SiteXSiteRepository SiteXSiteRepository;
        public InstrumentRepository InstrumentRepository;
        public SiteInstrumentRepository SiteInstrumentRepository;
        public MathVarRepository MathVarRepository;
        public NameItemRepository NameItemRepository;
        public CategoryItemRepository CategoryItemRepository;
        public CategorySetRepository CategorySetRepository;
        public SiteRepository SiteRepository;

        static public void ClearCashs()
        {
            VariableRepository.ClearCache();
            VariableVirtualRepository.ClearCache();
            SiteRepository.ClearCache();
            GeoObjectRepository.ClearCache();
            DataTypeRepository.ClearCache();
            GeneralCategoryRepository.ClearCache();
            GeoTypeRepository.ClearCache();
            MethodRepository.ClearCache();
            OffsetTypeRepository.ClearCache();
            SampleMediumRepository.ClearCache();
            SiteTypeRepository.ClearCache();
            UnitRepository.ClearCache();
            ValueTypeRepository.ClearCache();
            VariableTypeRepository.ClearCache();
            InstrumentRepository.ClearCache();
            SiteInstrumentRepository.ClearCache();
            MathVarRepository.ClearCache();
        }

        public DataManager(string connectionString)
            : base(connectionString)
        {
            PileRepository = new PileRepository(this);
            VariableRepository = new VariableRepository(this);
            VariableVirtualRepository = new VariableVirtualRepository(this);
            EntityGroupRepository = new EntityGroupRepository(this);
            EntityRepository = new EntityRepository(this);
            EntityAttrRepository = new EntityAttrRepository(this);
            GeoObjectRepository = new GeoObjectRepository(this);
            CodeFormRepository = new CodeFormRepository(this);
            DataTypeRepository = new DataTypeRepository(this);
            FlaAQCRepository = new FlaAQCRepository(this);
            GeneralCategoryRepository = new GeneralCategoryRepository(this);
            GeoTypeRepository = new GeoTypeRepository(this);
            MethodRepository = new MethodRepository(this);
            MethodForecastRepository = new MethodForecastRepository(this);
            MethodClimateRepository = new MethodClimateRepository(this);
            OffsetTypeRepository = new OffsetTypeRepository(this);
            SampleMediumRepository = new SampleMediumRepository(this);
            SiteAttrTypeRepository = new SiteAttrTypeRepository(this);
            SiteGeoObjectRepository = new SiteGeoObjectRepository(this);
            SiteTypeRepository = new SiteTypeRepository(this);
            StationAddrRegionRepository = new StationAddrRegionRepository(this);
            UnitRepository = new UnitRepository(this);
            ValueTypeRepository = new ValueTypeRepository(this);
            VariableCodeRepository = new VariableCodeRepository(this);
            VariableAttributesRepository = new VariableAttributesRepository(this);
            VariableTypeRepository = new VariableTypeRepository(this);
            MeteoZoneRepository = new MeteoZoneRepository(this);
            CatalogRepository = new CatalogRepository(this);
            SiteXSiteRepository = new SiteXSiteRepository(this);
            InstrumentRepository = new InstrumentRepository(this);
            SiteInstrumentRepository = new SiteInstrumentRepository(this);
            MathVarRepository = new MathVarRepository(this);
            NameItemRepository = new NameItemRepository(this);
            CategorySetRepository = new CategorySetRepository(this);
            CategoryItemRepository = new CategoryItemRepository(this);

            SiteRepository = new SiteRepository(this);
        }

        public static string GetDefaultConnectionString()
        {
            return Properties.Settings.Default.ConnectionString;
        }

        public static void SetDefaultConnectionString(string cnns)
        {
            Properties.Settings.Default["ConnectionString"] = cnns;
        }

        /// <summary>
        /// Экземпляр со строкой подключения по умолчанию.
        /// </summary>
        public static DataManager GetInstance()
        {
            return (DataManager)GetInstance(GetDefaultConnectionString(), Type.GetType("SOV.Amur.Meta.DataManager"));
        }
        /// <summary>
        /// Экземпляр с заданной строкой подключения.
        /// </summary>
        public static DataManager GetInstance(string connectionString)
        {
            return (DataManager)GetInstance(connectionString, Type.GetType("SOV.Amur.Meta.DataManager"));
        }

        public CatalogFilter GetCatalogFilter(string filterDumpString)
        {
            CatalogFilter ret = new CatalogFilter();
            foreach (var item in StrVia.ToDictionary(filterDumpString))
            {
                if (item.Key == "OFFSETVALUE")
                    ret.OffsetValue = string.IsNullOrEmpty(item.Value) ? null : (double?)double.Parse(item.Value);
                else
                {
                    List<int> ids = StrVia.ToListInt(item.Value);
                    switch (item.Key)
                    {
                        case "SITEIDS": ret.Sites = ids; break;
                        case "VARIABLEIDS": ret.Variables = ids; break;
                        case "METHODIDS": ret.Methods = ids; break;
                        case "SOURCEIDS": ret.Sources = ids; break;
                        case "OFFSETTYPEIDS": ret.OffsetTypes = ids; break;
                        default:
                            throw new Exception("Неизвестный атрибут фильтра в строке дампа: " + item.Key);
                    }
                }
            }
            return ret;
        }

        public static object ParseDataIdName(NpgsqlDataReader rdr)
        {
            return new IdName()
            {
                Id = (int)rdr["id"],
                Name = rdr["name"].ToString()
            };
        }

        public static object ParseDataIdNameParent(NpgsqlDataReader rdr)
        {
            IdNameParent ret = (IdNameParent)ParseDataIdName(rdr);
            int i = rdr.GetOrdinal("parent_id");
            ret.ParentId = rdr.IsDBNull(i) ? null : (int?)(int)rdr[i];
            return ret;
        }

    }
}
