using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Amur.Meta;
using SOV.Amur.Data;

namespace SOV.Amur
{
    public partial class FormTestChart : Form
    {
        public FormTestChart()
        {
            InitializeComponent();

            //ucChartHydro1.ToolbarVisible = false;
        }

       // private void ucChartHydro1_UCRefreshEvent(SOV.Amur.Data.UCChartHydro.ChartOptions co)
      //  {
            //Dictionary<object, object> data = new Dictionary<object, object>();
            //Meta.DataManager mrep = Meta.DataManager.GetInstance();
            //Data.DataManager drep = Data.DataManager.GetInstance();

            //if (co == null)
            //{
            //    int stationId = 23; // ГПКомсомольск
            //    DateTime dateF = new DateTime(2016, 10, 1);
            //    DateTime dateS = dateF.AddDays(-7);

            //    new UCChartHydro.ChartOptions()
            //    {
            //        Station = co.Station = mrep.StationRepository.Select(stationId),
            //        AxesMinGageHeight = 250,
            //        AxesMaxPrecipitation = 50,
            //        TimeType = EnumTimeType.LOC,
            //        DateTimePeriod = new Common.DateTimePeriod(dateS, dateF, Common.DateTimePeriod.Type.Period, 0)
            //    };
            //}

            //// SITES
            //List<Site> sites = mrep.SiteRepository.SelectByStations(new List<int>(new int[] { co.Station.Id }));
            //sites.RemoveAll(x => x.SiteTypeId != (int)EnumSiteType.StandartObsLocation && x.SiteTypeId != (int)EnumSiteType.AHK);

            //// RELATED STATIONS FOR PRECIPITATION
            //{
            //    List<SiteXSite> sitesRelated = mrep.SiteXSiteRepository.Select(co.Station.Id, 2, (int)EnumSiteXSiteType.Precipitation1For2);
            //    List<Site> sitesRelatedPrecipitation = mrep.SiteRepository.SelectByStations(sitesRelated.Select(x => x.SiteId1).ToList());

            //    sites.AddRange(sitesRelatedPrecipitation);
            //}

            //// VARIABLES
            //List<int> varIds = new List<int>(new int[]{
            //    (int)EnumVariable.GageHeightF, 
            //    (int)EnumVariable.PrecipDay01F, 
            //    (int)EnumVariable.PrecipDay12F,
            //    (int)EnumVariable.PrecipDay24F
            //});
            //List<Variable> vars = mrep.VariableRepository.Select(varIds);

            //// CATALOGS

            //DataFilter df = new DataFilter()
            //{
            //    CatalogFilter = new CatalogFilter()
            //    {
            //        Sites = sites.Select(x => x.Id).Distinct().ToList(),
            //        Variables = varIds,
            //        Methods = null,
            //        Sources = null,
            //        OffsetTypes = new List<int>(new int[] { (int)EnumOffsetType.NoOffset }),
            //        OffsetValue = 0
            //    },
            //    //DateTimePeriod = new Common.DateTimePeriod(co.DateTimePeriod.DateS, co.DateTimePeriod.DateF, Common.DateTimePeriod.Type.Period, 0),
            //    FlagAQC = null,
            //    IsActualValueOnly = true,
            //    IsRefSiteData = false,
            //    IsSelectDeleted = false
            //};
            //List<DataValue> datas = drep.DataValueRepository.SelectA(df);
            //List<Catalog> catalogs = mrep.CatalogRepository.Select(datas.Select(x => x.CatalogId).Distinct().ToList());

            //// CORRECT CATALOGS

            //data.Add(typeof(UCChartHydro.ChartOptions), co);
            //data.Add(typeof(List<Site>), sites);
            //data.Add(typeof(List<Variable>), vars);
            //data.Add(typeof(List<Catalog>), catalogs);
            //data.Add(typeof(List<DataValue>), datas);

            ////ucChartHydro1.EnableAxesTitle = false;

            //ucChartHydro1.Fill(data);
      //  }
    }
}
