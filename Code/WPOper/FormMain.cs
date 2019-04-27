using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Amur.Data;
using SOV.Amur.Meta;
using System.Reflection;
using SOV.Amur.Data.Chart;
using SOV.Amur.Properties;
using SOV.Amur.Sys;
using SOV.Amur.Reports;
using SOV.Common;
using SOV.Common.TableIUD;
using SOV.Social;
using System.Diagnostics;

namespace SOV.Amur
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            Text = AssemblyTitle + " в. " + AssemblyFileVersionAttribute.Version;// + " (" + AssemblyCompany + ")";
        }

        int UserOrganisationId { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            AcceptUserSettings();

            // UserDataFilterFcsSAV
            UserFilterDataFcsSAV = Program.UserSettings.FirstOrDefault(x => x.AttrId == (int)Sys.AttrEnum.UserFilterDataFcs && x.EntityInstance == Program.User.Name);
            if (UserFilterDataFcsSAV == null)
            {
                UserFilterDataFcsSAV = new EntityInstanceValue((int)AttrEnum.UserFilterDataFcs, Program.User.Name, "");
                Program.UserSettings.Add(UserFilterDataFcsSAV);
            }

            // UserDataFilterNSitesSAV
            UserFilterDataNSitesSAV = Program.UserSettings.FirstOrDefault(x => x.AttrId == (int)Sys.AttrEnum.UserFilterDataNSites && x.EntityInstance == Program.User.Name);
            if (UserFilterDataNSitesSAV == null)
            {
                UserFilterDataNSitesSAV = new EntityInstanceValue((int)AttrEnum.UserFilterDataNSites, Program.User.Name, "");
                Program.UserSettings.Add(UserFilterDataNSitesSAV);
            }

            // CatalogFilterSAV
            UserFilterCatalogSAV = Program.UserSettings.FirstOrDefault(x => x.AttrId == (int)Sys.AttrEnum.UserFilterCatalog && x.EntityInstance == Program.User.Name);
            if (UserFilterCatalogSAV == null)
            {
                UserFilterCatalogSAV = new EntityInstanceValue((int)AttrEnum.UserFilterCatalog, Program.User.Name, "");
                Program.UserSettings.Add(UserFilterCatalogSAV);
            }

            // UserOrganisationId
            EntityInstanceValue userOrganisationSAV = Program.UserSettings.FirstOrDefault(x => x.AttrId == (int)Sys.AttrEnum.UserOrganizationId && x.EntityInstance == Program.User.Name);
            if (userOrganisationSAV == null)
            {
                UserOrganisationId = -1;
                MessageBox.Show("У пользователя отсутствует атрибут \"Организация\". Его надо предварительно создать через меню Файл." +
                    "\nВ противном случае возможны ошибки при работе с интерфейсом.");
            }
            else
            {
                UserOrganisationId = int.Parse(userOrganisationSAV.Value);
            }

            // UserDirExportSAV
            UserDirExportSAV = Program.UserSettings.FirstOrDefault(x => x.AttrId == (int)Sys.AttrEnum.UserDirExport && x.EntityInstance == Program.User.Name);
            if (UserDirExportSAV == null)
            {
                UserDirExportSAV = new EntityInstanceValue((int)AttrEnum.UserFilterDataNSites, Program.User.Name, "");
                Program.UserSettings.Add(UserDirExportSAV);
            }

            // UserFilterData1Site
            UserFilterData1SiteSAV = Program.UserSettings.FirstOrDefault(x => x.AttrId == (int)Sys.AttrEnum.UserFilterData1Site && x.EntityInstance == Program.User.Name);
            if (UserFilterData1SiteSAV == null)
            {
                UserFilterData1SiteSAV = new EntityInstanceValue((int)AttrEnum.UserFilterData1Site, Program.User.Name, "");
                Program.UserSettings.Add(UserFilterData1SiteSAV);
            }
            //formDataFilter = new FormDataFilter(UserFilterData1SiteSAV);

            // ucSiteGeoObjectList
            ucSites.AllowSpecialSiteGroups = true;
            ucSites.FillSiteGroups();
        }

        EntityInstanceValue UserFilterData1SiteSAV { get; set; }
        EntityInstanceValue UserFilterDataFcsSAV { get; set; }
        EntityInstanceValue UserFilterCatalogSAV { get; set; }

        private void mnuCloseCurrentTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tc.SelectedTab != null)
            {
                tc.TabPages.Remove(tc.SelectedTab);
            }
        }

        private void mnuCloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            while (tc.TabPages.Count > 0)
            {
                tc.TabPages[0].Dispose();
            }
        }
        #region Методы доступа к атрибутам сборки

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        static public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        public AssemblyFileVersionAttribute AssemblyFileVersionAttribute
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
                return (attributes.Length == 0) ? null : (AssemblyFileVersionAttribute)attributes[0];
            }
        }
        #endregion
        FormDataFilter _formDataFilter = null;
        FormDataFilter formDataFilter
        {
            get
            {
                if (_formDataFilter == null)
                {
                    _formDataFilter = new FormDataFilter(UserFilterData1SiteSAV);
                }
                return _formDataFilter;
            }
        }

        [DefaultValue(null)]
        DataFilter DataFilter;
        ChartsFilter chartsFilter = new ChartsFilter();

        private void mnuToSeparateWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tab = tc.SelectedTab;
            if (tab != null)
            {
                Form frm = new Form();
                frm.Controls.Add(tab.Controls[0]);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.Text = tab.Text;
                frm.Size = new System.Drawing.Size(800, 600);
                frm.Icon = Properties.Resources.EditData;
                frm.Show();

                tc.TabPages.Remove(tab);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserSettings();
        }
        private void SaveUserSettings()
        {
            try
            {
                Sys.DataManager.GetInstance().SysEntityRepository.InsertUpdate(Program.UserSettings.ToList());

                List<EntityInstanceValue> eiv = new List<EntityInstanceValue>();
                if (ucSites.SiteGroupId != null)
                    eiv.Add(new EntityInstanceValue((int)Sys.AttrEnum.UserSiteGroupId, Program.User.Name, ucSites.SiteGroupId.Value.ToString()));
                if (DataFilter != null)
                    eiv.Add(new EntityInstanceValue((int)Sys.AttrEnum.UserFilterData1Site, Program.User.Name, DataFilter.ToString()));
                if (chartsFilter != null)
                    eiv.Add(new EntityInstanceValue((int)Sys.AttrEnum.UserFilterCharts, Program.User.Name, chartsFilter.ToString()));

                Sys.DataManager.GetInstance().SysEntityRepository.InsertUpdate(eiv);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка сохранения настроек пользователя", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AcceptUserSettings()
        {
            try
            {
                List<EntityInstanceValue> eivs = Program.UserSettings;

                EntityInstanceValue chartEiv = eivs.Find(x => x.AttrId == (int)AttrEnum.UserFilterCharts);
                if (chartEiv != null)
                    chartsFilter = ChartsFilter.Parse(chartEiv.Value);

                EntityInstanceValue eiv = eivs.Find(x => x.AttrId == (int)AttrEnum.UserFilterData1Site);
                if (eiv != null)
                    DataFilter = DataFilter.Parse(eiv.Value);

                eiv = eivs.Find(x => x.AttrId == (int)AttrEnum.UserSiteGroupId);
                EntityInstanceValue eiv1 = eivs.Find(x => x.AttrId == (int)AttrEnum.UserSiteGroupId);
                if (eiv != null)
                    ucSites.SiteGroupId = int.Parse(eiv.Value);
                if (DataFilter != null && DataFilter.CatalogFilter.Sites != null && DataFilter.CatalogFilter.Sites.Count > 0)
                    ucSites.SetCurrentSite(DataFilter.CatalogFilter.Sites[0]);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка при сохранении настроек пользователя", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuFileExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuDataPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DataP.AQC().Run(
                (DateTime)DataFilter.DateTimePeriod.DateS, (DateTime)DataFilter.DateTimePeriod.DateF,
                //new DateTime(2014, 4, 1), new DateTime(2014, 5, 1), 
                //1900, 2000,
                new List<int>(new int[] { 3, 211 }));
        }

        private void mnuDicVariablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormVariablesList frm = new FormVariablesList();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void ucSiteGeoObjectList1_UCEditSiteEvent(int siteId)
        {
            EditSite(siteId);
        }

        private void EditSite(int siteId)
        {
            TabPage tabPage;
            Site site = Meta.DataManager.GetInstance().SiteRepository.Select(siteId);
            int i = tc.TabPages.IndexOfKey("station " + site.Id.ToString());
            if (i >= 0)
            {
                tabPage = tc.TabPages[i];
            }
            else
            {
                UCSiteEdit uc = new UCSiteEdit()
                {
                    ShowDataValueEventHandler = ShowDataValueEventHandler,
                    Dock = DockStyle.Fill
                };

                tabPage = new TabPage(site.Name);
                tabPage.Name = "site " + site.Id.ToString();
                tabPage.ImageIndex = 1;
                tabPage.Controls.Add(uc);
                this.tc.TabPages.Add(tabPage);

                uc.Fill(site.Id);
            }
            tc.SelectedTab = tabPage;
        }

        private void ucSiteGeoObjectList1_UCShowClimateEvent(Site site)
        {
            try
            {
                FormClimate4Site frm = new FormClimate4Site(site, new int[] { 1900, 2000 });
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void mnuServiceSysLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSys frm = new FormSys(this.Text);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void mnuGeoObjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGeoObjects frm = new FormGeoObjects();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void mnuDataClimateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClimate frm = new FormClimate();
            //frm.Fill(new List<int>(new int[] { 27 }), new int[] { 1900, 2000 });

            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void mnuAboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Amur.WPOper.FormAboutBox frm = new WPOper.FormAboutBox();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void mnuRefreshDicCashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Meta.DataManager.ClearCashs();
            Social.DataManager.ClearCashs();
            SiteGroupControls.GlobalReloadItems();
        }

        private void ucNet_UCSelectedNodeChangedEvent(TreeNode node)
        {
            if (node.Tag != null)
            {
                TabPage tabPage = null;

                if (node.Tag.GetType() == typeof(Site))
                {
                    Site site = (Site)((object[])node.Tag)[0];
                    int i = tc.TabPages.IndexOfKey("site");
                    if (i >= 0)
                    {
                        tabPage = tc.TabPages[i];
                    }
                    else
                    {
                        UCSiteEdit uc = new UCSiteEdit();
                        uc.Dock = DockStyle.Fill;

                        tabPage = new TabPage(site.Name);
                        tabPage.Name = "site";
                        tabPage.ImageIndex = 1;
                        tabPage.Controls.Add(uc);
                        this.tc.TabPages.Add(tabPage);
                    }
                    ((UCSiteEdit)tabPage.Controls[0]).Fill(site.Id);
                    tabPage.Text = site.Name;
                }
                if (node.Tag.GetType() == typeof(GeoObject))
                {
                    GeoObject go = (GeoObject)node.Tag;
                    UCGeoObject uc = new UCGeoObject();
                    uc.GeoObject = go;

                    int i = tc.TabPages.IndexOfKey("geo_object");
                    if (i >= 0)
                    {
                        tabPage = tc.TabPages[i];
                        tabPage.Controls.Clear();
                    }
                    else
                    {
                        //uc.Dock = DockStyle.Fill;
                        tabPage = new TabPage();
                        tabPage.Name = "geo_object";
                        tabPage.ImageIndex = 2;
                        this.tc.TabPages.Add(tabPage);
                    }
                    tabPage.Text = GeoObjectRepository.GetCash().Find(x => x.Id == go.Id).Name;
                    tabPage.Controls.Add(uc);
                }
                if (tabPage != null)
                    tc.SelectedTab = tabPage;
            }
        }
        EntityInstanceValue UserFilterDataNSitesSAV { get; set; }
        EntityInstanceValue UserDirExportSAV { get; set; }

        private void mnuDataF2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDataTable frm = new FormDataTable(UserOrganisationId, UserFilterDataNSitesSAV, UserDirExportSAV);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.WindowState = FormWindowState.Maximized;

            frm.Show();
        }

        private void ucNet_UCEditDataEvent(TreeNode node)
        {
            TabPage tabPage = null;
            // SITE
            if (node != null)
            {
                if (node.Tag.GetType() == typeof(Site))
                {
                    if (node.Tag == null) return;
                    Site site = (Site)node.Tag;

                    UCDataTable uc;
                    int i = tc.TabPages.IndexOfKey("data");
                    if (i >= 0)
                    {
                        tabPage = tc.TabPages[i];
                        uc = (UCDataTable)tabPage.Controls[0];
                    }
                    else
                    {
                        uc = new UCDataTable(UserOrganisationId, UserFilterData1SiteSAV)
                        {
                            CurViewType = UCDataTable.ViewType.Station_RDates_CVariables,
                            Dock = DockStyle.Fill
                        };

                        tabPage = new TabPage
                        {
                            Name = "data",
                            ImageIndex = 0
                        };
                        tabPage.Controls.Add(uc);
                        this.tc.TabPages.Add(tabPage);
                    }

                    List<int> siteOld = DataFilter.CatalogFilter.Sites;
                    DataFilter.CatalogFilter.Sites = new List<int>(new int[] { site.Id });
                    uc.Fill(DataFilter);
                    DataFilter.CatalogFilter.Sites = siteOld;

                    tabPage.Text = site.GetName(1, Meta.SiteTypeRepository.GetCash());
                    tc.SelectedTab = tabPage;
                }
            }
        }
        internal void ShowDataValueEventHandler(object sender, EventArgs e)
        {
            List<Catalog> catalogs = ((CatalogEventArgs)e).Catalogs;
            if (catalogs != null && catalogs.Count > 0)
            {
                FormDataTable formData = new FormDataTable((int)Meta.EnumLegalEntity.FERHRI);

                formData.UCDataTable.SetViewType(UCDataTable.ViewType.Station_RDates_CVariables);

                formData.UCDataTable.Fill(
                    new DataFilter()
                    {
                        DateTimePeriod = new DateTimePeriod(new DateTime(1900, 1, 1), DateTime.Now, DateTimePeriod.Type.Period, 0),
                        DateTypeId = (int)EnumDateType.UTC,
                        FlagAQC = null,
                        IsActualValueOnly = true,
                        IsDateLOC = false,
                        IsRefSiteData = false,
                        IsSelectDeleted = false,

                        CatalogFilter = new CatalogFilter() { Catalogs = catalogs }
                    });
                formData.ShowDialog();
            }
        }
        private void dataFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formDataFilter.SiteFilterEnabled = false;
            if (DataFilter != null) formDataFilter.DataFilter = DataFilter;

            if (formDataFilter.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataFilter = formDataFilter.DataFilter;
            }
        }

        private void mnuComplexGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ucChartsOper = new UCChartsOper(UserOrganisationId, chartsFilter);
            var form = new FormSingleUC(ucChartsOper, "Комплекс графиков", ucChartsOper.Width, ucChartsOper.Height);
            form.Icon = Icon.FromHandle(Resources.chart.GetHicon());
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Closed += formChartsOperOnClosed;
            form.Show();
            ucChartsOper.OnShow();
        }

        private void formChartsOperOnClosed(object sender, EventArgs eventArgs)
        {
            var UCChartsOper = (UCChartsOper)((FormSingleUC)sender).Controls[0];
            chartsFilter = UCChartsOper.chartsFilter;
            ((FormSingleUC)sender).Dispose();
        }

        private void showF50Report(object sender, EventArgs e)
        {
            try
            {
                FormF50ReportFilter frm = new FormF50ReportFilter();
                frm.StartPosition = FormStartPosition.CenterScreen;

                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Cursor cs = this.Cursor;
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        F50Collection f50 = int.Parse((string)((ToolStripMenuItem)sender).Tag) == 50 ?
                            F50Collection.Instance(frm.SiteGroup, frm.Year, frm.Month, frm.DecadeOfMonth, frm.FlagAQC) :
                            F50Collection.Instance(frm.SiteGroup, frm.Year, frm.Month, null, 0);

                        UCF50DGV uc = new UCF50DGV(f50);
                        Reports.Report rep = Reports.DataManager.GetInstance().ReportRepository.Select(50);
                        new FormSingleUC(uc, rep.NameFull);
                    }
                    finally
                    {
                        this.Cursor = cs;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void showDataFilterReport(object sender, EventArgs e)
        {
            int repId = int.Parse((string)((ToolStripMenuItem)sender).Tag);
            FormDataFilter fdf = new FormDataFilter(null);

            if (fdf.ShowDialog() == DialogResult.OK)
            {

                DataFilter dataFilter = fdf.DataFilter;
                if (dataFilter.DateTimePeriod.DateS == null || dataFilter.DateTimePeriod.DateF == null)
                    throw new Exception("Не заданы даты!");
                switch (repId)
                {
                    case 10://ReportEnums.AHC10:
                    case 11://ReportEnums.AHC11:
                        dataFilter.CatalogFilter.Variables = new List<int>() { 2 };
                        break;
                    case 12://ReportEnums.AHC12:
                        dataFilter.CatalogFilter.Variables = new List<int>() { 23 };
                        break;
                    case 25://ReportEnums.GP-25:
                        break;
                }
                UCReport ucR = new UCReport((int)repId, dataFilter, Program.User.Name);
                Reports.Report rep = Reports.DataManager.GetInstance().ReportRepository.Select(repId);
                new FormSingleUC(ucR, rep.NameFull).Show();
                //Report.Report rep = SOV.Amur.Report.DataManager.Reports.GetReport((int)repId);
                //frm.Text = "ОТЧЕТ: \"" + rep.Name + " (" + rep.NameShort + ") " + "\"";
            }
        }

        private void MnuDicEntityGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEntityGroup frm = new FormEntityGroup();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void ucSiteGeoObjectList1_UCSelectedSiteChangedEvent(Site site)
        {
            if (ucSites.IsOneDataTable)
                RefreshDataTabPage(site);
        }
        /// <summary>
        /// Обновить данные существующей вкладки пункта или создать новую вкладку с данными пункта.
        /// </summary>
        /// <param name="site">Пункт.</param>
        void RefreshDataTabPage(Site site)
        {
            if (site == null) return;

            if (DataFilter == null)
            {
                MessageBox.Show("Установите фильтр для выбора данных через панель управления. Отмена операции.", Text, MessageBoxButtons.OK);
                return;
            }

            TabPage tp = GetDataTabPage(site);
            // todo: Необходимо клонировать фильтр OSokolov@SOV.ru, 201805
            DataFilter.CatalogFilter.Sites = new List<int>(new int[] { site.Id });
            ((UCDataTable)tp.Controls[0]).Fill(DataFilter);
            tc.SelectedTab = tp;
        }
        /// <summary>
        /// Получить существующую или создать новую вкладку с таблицей данных для пункта.
        /// </summary>
        /// <param name="site">Станция.</param>
        /// <param name="station"></param>
        /// <param name="siteType"></param>
        /// <returns></returns>
        TabPage GetDataTabPage(Site site)
        {
            TabPage tabPage = null;

            if (ucSites.IsOneDataTable)
            {
                foreach (TabPage item in tc.TabPages)
                {
                    if (item.Tag != null && item.Tag.GetType() == typeof(UCDataTable))
                    {
                        tabPage = item;
                        //tabPage.Text = station.Code + " " + station.Name + " " + siteType.NameShort;
                        break;
                    }
                }
            }
            else
            {
                int i = tc.TabPages.IndexOfKey(site.Id.ToString());
                if (i >= 0)
                {
                    tabPage = tc.TabPages[i];
                }
            }

            if (tabPage == null)
            {
                UCDataTable uc = new UCDataTable(UserOrganisationId, null, UserDirExportSAV, this.formDataFilter);
                uc.CurViewType = UCDataTable.ViewType.Station_RDates_CVariables;
                uc.Dock = DockStyle.Fill;

                //tabPage = new TabPage(station.Code + " " + station.Name + " " + siteType.NameShort);
                tabPage = new TabPage();
                tabPage.Tag = uc;
                tabPage.Name = site.Id.ToString();
                tabPage.ImageIndex = 0;
                tabPage.Controls.Add(uc);
                this.tc.TabPages.Add(tabPage);
            }

            tabPage.Text = site.GetName(1, SiteTypeRepository.GetCash());
            return tabPage;
        }
        private void ucSiteGeoObjectList1_UCEditDataEvent(Site site)
        {
            RefreshDataTabPage(site);//, station, siteType);
        }

        private void mnuDataFcsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDataTableFcs frm = new FormDataTableFcs(UserFilterDataFcsSAV);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.WindowState = FormWindowState.Maximized;

            frm.Show();
        }

        private void ucStations1_UCEditStationEvent(int stationId)
        {
            EditSite(stationId);
        }
        FormCatalogs frmCatalogs = null;
        private void mnuDataCatalogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmCatalogs == null || frmCatalogs.IsDisposed)
            {
                frmCatalogs = new FormCatalogs(UserFilterCatalogSAV, ShowDataValueEventHandler);
                frmCatalogs.CancelButtonVisible = false;
                frmCatalogs.AcceptButtonText = "Закрыть";
                frmCatalogs.StartPosition = FormStartPosition.CenterParent;
            }
            frmCatalogs.Show();
        }

        private void mnuDicStationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormStations frm = new FormStations();
            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.Show(this);
        }

        private void mnuDicAddrRegionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Social.FormAddrs frm = new Social.FormAddrs();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void mnuDicVariableCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormVariableCodes frm = new FormVariableCodes();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void ucSites_UCShowDataGrpEvent(int siteGroupId)
        {
            try
            {
                EntityGroup siteGroup = Meta.DataManager.GetInstance().EntityGroupRepository.Select(siteGroupId);
                List<int[]> groupSiteIds = Meta.DataManager.GetInstance().EntityGroupRepository.SelectEntities(siteGroupId);
                List<Site> sites = Meta.DataManager.GetInstance().SiteRepository.Select(groupSiteIds.Select(x => x[0]).ToList());

                TabPage tabPage;
                string tabPageName = "data_grp_" + siteGroup.Id;
                int i = tc.TabPages.IndexOfKey(tabPageName);

                if (i >= 0)
                {
                    tabPage = tc.TabPages[i];
                }
                else
                {
                    UCDataGrpSVYM uc = new UCDataGrpSVYM
                    {
                        Dock = DockStyle.Fill
                    };

                    tabPage = new TabPage(siteGroup.Name);
                    tabPage.Name = tabPageName;
                    tabPage.ImageIndex = 4;
                    tabPage.Controls.Add(uc);
                    this.tc.TabPages.Add(tabPage);

                    DateTime date = DateTime.Today.AddMonths(-1);
                    uc.Fill(sites, null, date.Year, date.Year, null);
                }
                tc.SelectedTab = tabPage;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ucSites_UCShowSiteDataGrpEvent(int siteId)
        {
            Site site = Meta.DataManager.GetInstance().SiteRepository.Select(siteId);

            TabPage tabPage;
            string tabPageName = "data_grp_site_" + site.Id;
            int i = tc.TabPages.IndexOfKey(tabPageName);

            if (i >= 0)
            {
                tabPage = tc.TabPages[i];
            }
            else
            {
                UCDataGrpSVYM uc = new UCDataGrpSVYM();
                uc.Dock = DockStyle.Fill;

                tabPage = new TabPage(site.GetName(2, Meta.SiteTypeRepository.GetCash()));
                tabPage.Name = tabPageName;
                tabPage.ImageIndex = 4;
                tabPage.Controls.Add(uc);
                this.tc.TabPages.Add(tabPage);

                DateTime date = DateTime.Today.AddMonths(-1);
                uc.Fill(new List<Site>() { site }, null, date.Year, date.Year, null);//new List<int> { date.Month });
            }
            tc.SelectedTab = tabPage;
        }

        private void ucSites_UCAddSiteChartEvent(int siteId)
        {
            TabPage tabPage;
            UCChartsOper chart;
            string tabPageName = "chart";
            int i = tc.TabPages.IndexOfKey(tabPageName);

            if (i < 0)
            {
                tabPage = new TabPage("Комплексный график");
                tabPage.Name = tabPageName;
                this.tc.TabPages.Add(tabPage);
                chart = new UCChartsOper(UserOrganisationId, chartsFilter);
                chart.Dock = DockStyle.Fill;
                tabPage.Controls.Add(chart);
                chart.SitesGroupList.Clear();
            }
            else
                tabPage = tc.TabPages[i];
            chart = (UCChartsOper)tabPage.Controls[0];
            Site site = Meta.SiteRepository.GetCash().Find(x => x.Id == siteId);
            if (!chart.SitesGroupList.Contains(site))
                chart.SitesGroupList.Add(site);
            chart.OnShow();
            tc.SelectedTab = tabPage;
        }

        private void mnuReport30ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormWaterObjectReport().Show();
        }

        private void orgsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rep = Reports.DataManager.GetInstance().OrgRepository;
            var form = new FormTableView<Reports.Org, FormReportOrg>(rep, Reports.Org.ViewTableFields(), "Организации для отчета");
            form.OnRefreshViwer += onReportOrgRefreshEvent;
            form.Show();
        }

        private void onReportOrgRefreshEvent(FormTableView<Reports.Org, FormReportOrg> tableView)
        {
            var socialDM = Social.DataManager.GetInstance();
            var reportDM = Reports.DataManager.GetInstance();
            var fields = Reports.Org.ViewTableFields();
            var data = reportDM.OrgRepository.SelectAllFields();
            var orgsItems = socialDM.LegalEntityRepository.SelectByType('o').Select(x => new DicItem(x.Id, x.NameRus, x.Id));
            var repItems = reportDM.ReportRepository.Select().Select(x => new DicItem(x.Id, x.Name, x.Id));
            var orgXImages = socialDM.LegalEntityXImageRepository.SelectByOrgs(data.Select(x => x.OrgId).ToList());
            var allImages = socialDM.ImageRepository.Select(orgXImages.Select(x => x.ImageId).ToList());
            ((ComboBoxTableField)fields.Find(x => x.db == "report_id")).items = repItems.ToList();
            ((ComboBoxTableField)fields.Find(x => x.db == "org_id")).items = orgsItems.ToList();
            ((ImageGalleryTableField)fields.Find(x => x.db == "img_id")).imgs = allImages.Select(
                elm => new DicItem(elm.Id, "", elm.Img)
            ).ToList();
            tableView.Fields = fields;
            tableView.TableViewer.RefreshData(data);
        }

        private void socialStaffPosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rep = Social.DataManager.GetInstance().StaffPositionRepository;
            (new FormTableView<StaffPosition, FormTableRowEdit<StaffPosition>>(rep, TableFields.StaffPosition(), "Должность")).Show();
        }

        private void imageToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var rep = Social.DataManager.GetInstance().ImageRepository;
            var form = new FormTableView<Social.Image, FormTableRowEdit<Social.Image>>(rep, Social.TableFields.Image(), "Изображение");
            form.RemoveEditFunction();
            form.Show();
        }

        //private void legalEntityToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    var rep = Social.DataManager.GetInstance().LegalEntityRepository;
        //    var form = new FormTableView<LegalEntity, FormLegalEntity>(rep, TableFields.LegalEntity(), "Субъект права");
        //    form.Width = 800;
        //    form.EnableTreeType();
        //    form.OnRefreshViwer += onLegalEntityRefreshEvent;
        //    form.Show();
        //}

        //private void onLegalEntityRefreshEvent(FormTableView<LegalEntity, FormLegalEntity> tableView)
        //{
        //    var socialDM = Social.DataManager.GetInstance();
        //    var fields = TableFields.LegalEntity();
        //    var orgs = socialDM.LegalEntityRepository.Select().Select(x => new DicItem(x.Id, x.NameRus, x.Id));
        //    var addr = socialDM.AddrRepository.SelectTree();
        //    ((ComboBoxTableField)fields.Find(x => x.db == "parent_id")).items = orgs.ToList();
        //    ((TreeTableField)fields.Find(x => x.db == "address_id")).tree = addr;
        //    tableView.Fields = fields;
        //    if (tableView.Type == FormTableView<LegalEntity, FormLegalEntity>.ViewerType.Grid)
        //        tableView.TableViewer.RefreshData(socialDM.LegalEntityRepository.SelectAllFields());
        //    else if (tableView.Type == FormTableView<LegalEntity, FormLegalEntity>.ViewerType.Tree)
        //    {
        //        tableView.TableViewer.RefreshData<DicItem>(socialDM.LegalEntityRepository.SelectTree());
        //        tableView.TableViewer.OnViewerDragDrop += onLegalEntityTreeDragDropEvent;
        //    }
        //}

        private void onLegalEntityTreeDragDropEvent(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
                return;
            var pt = ((TreeView)sender).PointToClient(new System.Drawing.Point(e.X, e.Y));
            var parent = ((TreeView)sender).GetNodeAt(pt);
            if (parent != null && !parent.Bounds.Contains(pt)) parent = null;
            var child = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
            var childEntity = (LegalEntity)child.Tag;
            var parentEntity = (parent != null ? (LegalEntity)parent.Tag : null);
            if (child == parent || child.Parent == parent ||
                parent != null && (child.Nodes.Find(parent.Name, true).Length > 0 || childEntity.Type != parentEntity.Type))
                return;
            childEntity.ParentId = parentEntity == null ? null : (int?)parentEntity.Id;
            var fields = new Dictionary<string, object> { { "id", childEntity.Id }, { "parent_id", childEntity.ParentId } };
            Social.DataManager.GetInstance().LegalEntityRepository.Update(fields);
            (child.Parent != null ? child.Parent.Nodes : ((TreeView)sender).Nodes).Remove(child);
            (parent != null ? parent.Nodes : ((TreeView)sender).Nodes).Add(child);
            if (parent != null) parent.Expand();
        }

        private void jointChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var uc = new UCChartJoint();
            //var uc = new UCSiteInstruments();
            //uc.Fill(15);
            new FormSingleUC(uc, "Совместный график", uc.Width, uc.Height).Show();
            //new Test().Show();           
        }

        private void mnuDicSitesAttrsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSitesAttrs frm = new FormSitesAttrs();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void mnuDicLegacyTreeViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLegalEntitiesTree frm = new FormLegalEntitiesTree();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void mnuTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region TEST UCCategory
            UCCategorySet uc = new UCCategorySet();
            uc.Fill();
            #endregion TEST UCCategory

            #region TEST DataYT
            //try
            //{
            //    Site site = ucSites.Site;
            //    if (site != null && DataFilter != null)
            //    {
            //        DataFilter.CatalogFilter.Sites = new List<int>(new int[] { site.Id });

            //        List<DataValue> dvs = Data.DataManager.GetInstance().DataValueRepository.SelectA(DataFilter);
            //        List<Catalog> catalogs = Meta.DataManager.GetInstance().CatalogRepository.Select(dvs.Select(x => x.CatalogId).Distinct().ToList());

            //        Meta.EnumTime dstTime = Meta.EnumTime.Month;
            //        List<DataP.DataYT> dataYTs = DataP.DataYT.GroupBy(dstTime, DataValueC.GetDataValueCList(catalogs, dvs));
            //        Debug.Write(DataP.DataYT.ToString(dataYTs.OrderBy(x => x.DateYT).ToList()));

            //        string outFile = @"D:\Temp\FERHRI\Amur\Export\";
            //        outFile += "DataYT_" + site.SiteCode + "_" + dstTime + ".csv";
            //        System.IO.StreamWriter sw = new System.IO.StreamWriter(outFile);
            //        //dataYTs.OrderBy(x=>x.DateYT).ToList();
            //        sw.Write(DataP.DataYT.ToString(dataYTs.OrderBy(x => x.DateYT).ToList()));
            //        sw.Close();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            #endregion

            #region TEST DATA FORECAST
            //List<Station> stations = Meta.DataManager.GetInstance().StationRepository
            //    .SelectStationsByIndeces(new List<string>() { "5160" });
            //List<Site> sites = Meta.DataManager.GetInstance().SiteRepository
            //    .SelectByStations(stations.Select(x => x.Id).ToList())
            //    .Where(x => x.SiteTypeId == (int)EnumStationType.HydroPost)
            //    .ToList();
            //List<Variable> vars = Meta.DataManager.GetInstance().VariableRepository
            //    .Select(null, null, null, null, null, null, null, new List<int>() { (int)EnumValueType.Forecast });
            //List<Catalog> catalogs = Meta.DataManager.GetInstance().CatalogRepository
            //    .Select(new CatalogFilter() { Sites = sites.Select(x => x.Id).ToList(), Variables = vars.Select(x => x.Id).ToList() });
            //DateTime dateF = DateTime.Today.AddDays(1);
            //DateTime dateS = dateF.AddDays(-10);
            //Dictionary<int, List<DateTime>> cds = Data.DataManager.GetInstance().DataForecastRepository.SelectGroupByCD(dateS, dateF, false);

            //foreach (var catalog in catalogs)
            //{
            //    Console.WriteLine("catalog " + catalog + " (" + vars.Find(x => x.Id == catalog.VariableId).Name + ")");
            //    if (cds.ContainsKey(catalog.Id))
            //        foreach (var date in cds[catalog.Id].OrderBy(x => x))
            //        {
            //            Console.Write("\tdate_ini " + date);
            //            List<DataForecast> dfs = Data.DataManager.GetInstance().DataForecastRepository.Select(catalog.Id, date, date, null, false);
            //            foreach (var df in dfs)
            //            {
            //                Console.Write(" (" + df.LagFcs + ";" + df.Value + ")");
            //            }
            //            Console.WriteLine();
            //        }
            //}
            //Console.WriteLine();
            #endregion
        }

        private void mnuDataCurveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCurves frm = new FormCurves();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void mnuDicMethodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMethods formMethods = new FormMethods();
            formMethods.StartPosition = FormStartPosition.CenterScreen;
            formMethods.Show();
        }

        private void trendChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var uc = new UCChartTrend();
            new FormSingleUC(uc, "График тренда", uc.Width, uc.Height).Show();
        }

        private void mnuDicCategoryDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCategorySetList frm = new FormCategorySetList(EnumLanguage.Rus);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }
    }
}