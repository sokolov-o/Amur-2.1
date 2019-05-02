using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Amur.Meta;
using SOV.Common;
using SOV.Amur.Sys;
using System.Windows.Forms.DataVisualization.Charting;

namespace SOV.Amur.Data
{
    /// <summary>
    /// Табличная форма представления данных: НЕСКОЛЬКО станций, НЕСКОЛЬКО переменных за ПЕРИОД времени
    /// 
    /// Варианты отображение данных:
    /// 
    ///     0 - В столбцах ВСЕ ПЕРЕМЕННЫЕ за один срок (с возможностью смены срока).
    ///     1 - В столбцах ВСЕ СРОКИ одной переменной  (с возможностью смены переменной).
    ///     2 - В строках даты, в столбцах переменные одной станции (с возможностью смены станции).
    /// </summary>
    public partial class UCDataTable : UserControl
    {
        /// <summary>
        /// Тип формы отображения данных в таблице.
        /// </summary>
        public enum ViewType
        {
            Unknown = -1,
            /// <summary>
            /// В строках станции, в столбцах ВСЕ ПЕРЕМЕННЫЕ за один срок (с возможностью смены срока).
            /// </summary>
            Date_RStations_CVariables = 0,
            /// <summary>
            /// В строках станции, в столбцах ВСЕ СРОКИ одной переменной  (с возможностью смены переменной).
            /// </summary>
            Variable_RStations_CDates = 1,
            /// <summary>
            /// В строках даты, в столбцах переменные одной станции (с возможностью смены станции).
            /// </summary>
            Station_RDates_CVariables = 2

        }

        public int UserOrganisationId { get; set; }
        public Sys.EntityInstanceValue UserDirExportSAV { get; set; }

        FormDataFilter _FormDataFilter = null;
        public FormDataFilter FormDataFilter
        {
            get
            {
                if (_FormDataFilter == null)
                {
                    _FormDataFilter = new FormDataFilter(null);
                }
                return _FormDataFilter;
            }
            set
            {
                _FormDataFilter = value;
            }
        }

        bool isInitializingNow = true;

        public UCDataTable()
        {
            InitializeComponent();

            UserOrganisationId = -1;
            UserDirExportSAV = null;

            isInitializingNow = false;
        }
        public UCDataTable(int userOrganisationId,
            EntityInstanceValue dataFilterSAV = null,
            Sys.EntityInstanceValue userDirExportSAV = null,
            FormDataFilter formDataFilter = null
            )
        {
            InitializeComponent();

            UserOrganisationId = userOrganisationId;
            UserDirExportSAV = userDirExportSAV;

            FormDataFilter = (formDataFilter == null) ? new FormDataFilter(dataFilterSAV) : formDataFilter;
            isInitializingNow = false;
        }
        public bool DataFilterEnabled
        {
            get
            {
                return dataFilterToolStripButton.Enabled;
            }
            set
            {
                dataFilterToolStripButton.Enabled = value;
            }
        }

        public DataFilter DataFilter { get; set; }

        static string DATE_FORMAT = "dd.MM.yyyy HH:mm";
        static string DATE_FORMAT_COMPACT = "yyyyMMdd.HHmm";

        public List<Catalog> CatalogList { get; private set; }

        /// <summary>
        /// Выбранные в соответствие с фильтром данные.
        /// </summary>
        List<DataValue> _dvs = null;
        /// <summary>
        /// Объекты указанные в фильтре
        /// </summary>
        List<Site> _sites;
        /// <summary>
        /// Объекты указанные в фильтре
        /// </summary>
        List<SiteType> _siteTypes;
        /// <summary>
        /// Объекты указанные в фильтре
        /// </summary>
        List<Variable> _vars;
        /// <summary>
        /// Даты, присутствующие в выбранных данных.
        /// </summary>
        List<DateTime> _datesLOC;
        /// <summary>
        /// Даты, присутствующие в выбранных данных.
        /// </summary>
        List<DateTime> _datesUTC;

        public void ClearTable()
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
        }
        public void Fill(DataFilter dataFilter)
        {
            if (isInitializingNow) return;

            if (dataFilter == null)
            {
                MessageBox.Show("Не установлен фильтр данных. Установите фильтр и повторно нажмите конпку \"Обновить\".");
                return;
            }

            System.Windows.Forms.Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            _isFilled = true;

            try
            {
                // CLEAR TABLE

                ClearTable();

                DataFilter = dataFilter;

                // SELECT DATA
                bool isSelectDeleted = DataFilter.IsSelectDeleted;
                DataFilter.IsSelectDeleted = false;
                _dvs = DataManager.GetInstance().DataValueRepository.SelectA(DataFilter);
                DataFilter.IsSelectDeleted = isSelectDeleted;
                CatalogList = Meta.DataManager.GetInstance().CatalogRepository.Select(_dvs.Select(x => x.CatalogId).Distinct().ToList());

                _sites = Meta.DataManager.GetInstance().SiteRepository.Select(DataFilter.CatalogFilter.Sites);
                _siteTypes = Meta.DataManager.GetInstance().SiteTypeRepository.Select();
                _vars = Meta.DataManager.GetInstance().VariableRepository.Select(DataFilter.CatalogFilter.Variables);

                _datesLOC = _dvs.Select(x => x.DateLOC).Distinct().OrderBy(x => x).ToList();
                _datesUTC = _dvs.Select(x => x.DateUTC).Distinct().OrderBy(x => x).ToList();

                // FILL TABLE
                FillVarsComboBox();
                FillStationsComboBox();
                FillDateComboBox();

                _isFilled = false;
                Fill();
            }
            finally
            {
                this.Cursor = cs;
                _isFilled = false;
            }
        }
        public void SetViewType(ViewType viewType)
        {
            viewTypeToolStripComboBox.SelectedIndex = (int)viewType;
        }
        void Fill()
        {
            System.Windows.Forms.Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // CLEAR TABLE

                dgv.Rows.Clear();
                dgv.Columns.Clear();

                if (_dvs == null)
                {
                    //MessageBox.Show("Не выбраны пункты, переменные и др. Выберите данные, примените фильтр.");
                    return;
                }

                // FILL TABLE

                switch (CurViewType)
                {
                    case ViewType.Variable_RStations_CDates: FillVSD(); break;
                    case ViewType.Date_RStations_CVariables: FillDSV(); break;
                    case ViewType.Station_RDates_CVariables: FillSDV(); break;
                    default:
                        MessageBox.Show("Неизвестный тип отображения " + CurViewType);
                        break;
                }
            }
            finally
            {
                this.Cursor = cs;
            }
        }
        private void FillVSD()
        {
            // CREATE DGV COLUMNS (NO TAG)

            dgv.Columns[dgv.Columns.Add("siteCode", "Индекс")].Frozen = true;
            dgv.Columns[0].ReadOnly = true;
            dgv.Columns[0].DefaultCellStyle.BackColor = Color.LightYellow;
            dgv.Columns[dgv.Columns.Add("siteName", "Пункт")].Frozen = true;
            dgv.Columns[1].ReadOnly = true;
            dgv.Columns[1].DefaultCellStyle.BackColor = Color.LightYellow;
            dgv.Columns[dgv.Columns.Add("siteType", "Тип")].Frozen = true;
            dgv.Columns[2].ReadOnly = true;
            dgv.Columns[2].DefaultCellStyle.BackColor = Color.LightYellow;
            _iColDataS = 3;

            // CREATE DGV COLUMNS (TAG IS DATE)

            foreach (var date in GetDates())
            {
                DataGridViewColumn col = dgv.Columns[dgv.Columns.Add(date.ToString(DATE_FORMAT), date.ToString(DATE_FORMAT))];
                col.Tag = date;
            }

            // CREATE DGV ROWS & FILL CELLS BY DATA (HEADER TAG = SITE)

            foreach (Site site in _sites)
            {
                //////Station station = _stations.Find(x => x.Id == site.StationId);

                DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                row.Tag = site;
                row.Cells[0].Value = site.Code;
                row.Cells[1].Value = site.Name;
                row.Cells[2].Value = _siteTypes.Find(x => x.Id == site.TypeId).NameShort;
            }
            FillDataCells();

            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            SetInfoLabelText(dgv.Rows.Count, dgv.Columns.Count, GetDates().Count());
        }

        private IEnumerable<DateTime> GetDates()
        {
            return (this.TimeType == EnumDateType.UTC) ? _datesUTC : _datesLOC;
        }
        int _iColDataS = -1;

        private void FillDSV()
        {
            // CREATE DGV COLUMNS (HEADER TAG = VARIABLE except first site name column)

            dgv.Columns[dgv.Columns.Add("siteCode", "Индекс")].Frozen = true;
            dgv.Columns[0].ReadOnly = true;
            dgv.Columns[0].DefaultCellStyle.BackColor = Color.LightYellow;
            dgv.Columns[dgv.Columns.Add("siteName", "Пункт")].Frozen = true;
            dgv.Columns[1].ReadOnly = true;
            dgv.Columns[1].DefaultCellStyle.BackColor = Color.LightYellow;
            dgv.Columns[dgv.Columns.Add("siteType", "Тип")].Frozen = true;
            dgv.Columns[2].ReadOnly = true;
            dgv.Columns[2].DefaultCellStyle.BackColor = Color.LightYellow;
            _iColDataS = 3;

            foreach (var variable in Meta.DataManager.GetInstance().VariableRepository.Select(DataFilter.CatalogFilter.Variables).OrderBy(x => x.NameRus))
            {
                DataGridViewColumn col = dgv.Columns[dgv.Columns.Add(variable.Id.ToString(), variable.NameRus)];
                col.Tag = variable;
            }

            // CREATE DGV ROWS & FILL CELLS BY DATA (HEADER TAG = SITE)

            foreach (Site site in _sites)
            {
                DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                row.Tag = site;
                row.Cells[0].Value = site.Code;
                row.Cells[1].Value = site.Name;
                row.Cells[2].Value = _siteTypes.Find(x => x.Id == site.TypeId).NameShort;
            }
            FillDataCells();

            infoToolStripLabel.Text = dgv.Rows.Count.ToString() + "x" + dgv.Columns.Count.ToString() + "x" + datesComboBox.Items.Count;

            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            SetInfoLabelText(dgv.Rows.Count, dgv.Columns.Count, datesComboBox.Items.Count);
        }

        private void FillSDV()
        {
            dgv.Columns[dgv.Columns.Add("dateLOC", "Дата, ЛОК")].Frozen = true;
            dgv.Columns[0].ReadOnly = true;
            dgv.Columns[0].DefaultCellStyle.BackColor = Color.LightYellow;
            dgv.Columns[0].DefaultCellStyle.Format = DATE_FORMAT;

            dgv.Columns[dgv.Columns.Add("dateUTC", "Дата, ВСВ")].Frozen = true;
            dgv.Columns[1].ReadOnly = true;
            dgv.Columns[1].DefaultCellStyle.BackColor = Color.LightYellow;
            dgv.Columns[1].DefaultCellStyle.Format = DATE_FORMAT;

            _iColDataS = 2;

            // CREATE DGV COLUMNS (HEADER TAG = VARIABLE except first site name column)

            foreach (var variable in Meta.DataManager.GetInstance().VariableRepository.Select(DataFilter.CatalogFilter.Variables).OrderBy(x => x.NameRus))
            {
                DataGridViewColumn col = dgv.Columns[dgv.Columns.Add(variable.Id.ToString(), variable.NameRus)];
                col.Tag = variable;
            }


            FillDataCells();

            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            SetInfoLabelText(dgv.Rows.Count, dgv.Columns.Count, datesComboBox.Items.Count);
        }
        private void FillDateComboBox()
        {
            datesComboBox.Items.Clear();

            if (_dvs == null) return;
            //{
            //    MessageBox.Show("Не выбраны пункты, переменные и др. Нажмите кнопку \"Фильтр\".");
            //    return;
            //}

            IEnumerable<DateTime> dates = GetDates();

            if (dates.Count() > 0)
            {
                foreach (DateTime date in dates)
                {
                    datesComboBox.Items.Add(date);
                }
                datesComboBox.SelectedIndex = 0;
            }
            //FillDataCells();
        }
        private void FillVarsComboBox()
        {
            varsComboBox.Items.Clear();
            if (_vars != null && _vars.Count > 0)
            {
                foreach (var item in _vars)
                {
                    varsComboBox.Items.Add(item);
                }
                varsComboBox.SelectedIndex = 0;
            }
        }
        private void FillStationsComboBox()
        {
            sitesComboBox.Items.Clear();
            if (_sites != null && _sites.Count > 0)
            {
                foreach (var item in _sites)
                {
                    sitesComboBox.Items.Add(new Common.DicItem(item.Id, item.GetName(1, SiteTypeRepository.GetCash()), item));
                }
                sitesComboBox.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// Заполнить таблицу для текущей даты
        /// </summary>
        private void FillDataCells()
        {
            DateTime curDateTime;

            switch (CurViewType)
            {
                case ViewType.Date_RStations_CVariables:
                    if (_dvs.Count == 0) return;
                    // TODO: Here im 201710
                    if (CurDateTime.HasValue)
                    {
                        curDateTime = (DateTime)CurDateTime;
                        List<DataValue> dvsD = (TimeType == EnumDateType.UTC)
                            ? _dvs.FindAll(x => x.DateUTC == curDateTime)
                            : _dvs.FindAll(x => x.DateLOC == curDateTime);

                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            Site site = GetRowSite(row.Index);
                            List<Catalog> ctls = CatalogList.FindAll(x => x.SiteId == site.Id);

                            for (int i = _iColDataS; i < row.Cells.Count; i++)
                            {
                                CellDataValue.Clear(row.Cells[i]);

                                if (ctls.Count > 0)
                                {
                                    List<Catalog> ctl = ctls.FindAll(x => x.VariableId == GetColumnVariable(row.Cells[i].ColumnIndex).Id);
                                    if (ctl.Count == 0) continue;

                                    List<DataValue> dvsDSV = dvsD.FindAll(x => ctl.Exists(y => y.Id == x.CatalogId));
                                    //if (dvsDSV.Count > 1) throw new Exception("Ошибка алгоритма: количество значений для пункта более одного.");
                                    if (dvsDSV.Count == 1)
                                        CellDataValue.SetDataValue(row.Cells[i], dvsDSV);
                                }
                            }
                        }
                    }
                    break;
                case ViewType.Variable_RStations_CDates:
                    if (_dvs.Count == 0) return;

                    int? varId = CurVariableId;
                    List<DataValue> dvsV = _dvs.FindAll(x => CatalogList.Exists(y => y.VariableId == varId && y.Id == x.CatalogId));

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        Site site = GetRowSite(row.Index);

                        List<DataValue> dvsVS = new List<DataValue>();
                        List<Catalog> ctls = CatalogList.FindAll(x => x.SiteId == site.Id && x.VariableId == varId);
                        if (ctls.Count > 0 && dvsV.Count > 0)
                        {
                            dvsVS = dvsV.FindAll(x => ctls.Exists(y => y.Id == x.CatalogId));
                        }
                        for (int i = _iColDataS; i < row.Cells.Count; i++)
                        {
                            CellDataValue.Clear(row.Cells[i]);

                            if (dvsVS.Count > 0)
                            {
                                curDateTime = (DateTime)dgv.Columns[i].Tag;
                                List<DataValue> dvsVSD = dvsVS.FindAll(x => (TimeType == EnumDateType.UTC) ? x.DateUTC == curDateTime : x.DateLOC == curDateTime);
                                if (dvsVSD.Count > 1) throw new Exception("Ошибка алгоритма: количество значений для пункта более одного.");
                                if (dvsVSD.Count == 1)
                                    CellDataValue.SetDataValue(row.Cells[i], dvsVSD);
                            }
                        }
                    }
                    break;
                case ViewType.Station_RDates_CVariables:

                    dgv.Rows.Clear();
                    if (_dvs.Count == 0) return;

                    List<Catalog> ctlsS = CatalogList.FindAll(y => y.SiteId == CurSiteId);
                    List<DataValue> dvsS = _dvs.FindAll(x => ctlsS.Exists(y => y.Id == x.CatalogId));

                    foreach (DateTime dateUTC in dvsS.Select(x => x.DateUTC).Distinct().OrderBy(x => x))
                    {
                        List<DataValue> dvsSD = dvsS.FindAll(x => x.DateUTC == dateUTC);

                        // CREATE ROWS FOR DATE LOC
                        List<DateTime> datesLOC = dvsSD.Select(x => x.DateLOC).Distinct().OrderBy(x => x).ToList();
                        List<DataGridViewRow> rows = new List<DataGridViewRow>();
                        foreach (var dateLOC in datesLOC)
                        {
                            DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                            row.Cells[0].Value = dateLOC;
                            row.Cells[1].Value = dateUTC;
                            if (datesLOC.Count > 1)
                                row.Cells[0].Style.BackColor = Color.Pink;
                            rows.Add(row);
                        }

                        // FILL ROWS CELLS
                        for (int i = _iColDataS; i < dgv.Columns.Count; i++)
                        {
                            List<DataValue> dvsSDV = dvsSD.FindAll(x => CatalogList.Exists(y =>
                                y.VariableId == GetColumnVariable(i).Id && y.Id == x.CatalogId));

                            //if (dvsSDV.Count > 1) throw new Exception("(dvsSDV.Count > 1) " + dvsSDV.Count);
                            // TODO: например, не работает при разных offset_value
                            if (dvsSDV.Count == 1)
                            {
                                DataGridViewRow row = rows.Count == 1 ? rows[0] : rows.Where(x => (DateTime)x.Cells[0].Value == dvsSDV[0].DateLOC).ElementAt(0);
                                CellDataValue.SetDataValue(row.Cells[i], dvsSDV);
                            }
                        }

                    }
                    break;
                default:
                    MessageBox.Show("Неизвестный тип отображения таблицы данных " + CurViewType);
                    break;
            }
        }

        private Variable GetColumnVariable(int iCol)
        {
            return (Variable)dgv.Columns[iCol].Tag;
        }
        Site GetRowSite(int iRow)
        {
            return (Site)dgv.Rows[iRow].Tag;
        }
        private void dataFilterToolStripButton_Click(object sender, EventArgs e)
        {
            if (DataFilter != null) FormDataFilter.DataFilter = DataFilter;
            if (FormDataFilter.ShowDialog() == DialogResult.OK)
            {
                Fill(FormDataFilter.DataFilter);
            }
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            ClearTable();

            DateTime? curDate = CurDateTime == null ? null : (DateTime?)DateTime.FromBinary(((DateTime)CurDateTime).ToBinary());
            Fill(DataFilter);
            CurDateTime = curDate;
        }
        List<DataGridViewCell> cellsEdited = new List<DataGridViewCell>();
        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Tag == null)
            {
                MessageBox.Show("Вставка новых значений не реализована...\nsov@201511", "Операция не реализована");
                return;
            }

            cellsEdited.Add(cell);
            saveEditedToolStripButton.Enabled = true;
        }

        private void saveEditedToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                int inserted = 0;
                int deleted = 0;
                int skiped = 0;
                int actualized = 0;
                DataValueRepository repdv = Data.DataManager.GetInstance().DataValueRepository;
                foreach (DataGridViewCell cell in cellsEdited)
                {
                    DataValue dv = CellDataValue.GetDataValue(cell);
                    double? value = ((cell.Value == null || cell.Value.ToString() == "") ? null : (double?)double.Parse(cell.Value.ToString()));

                    if (dv != null)
                    {
                        if (value.HasValue && dv.Value != value)
                        {
                            dv.Value = (double)value;
                            dv.FlagAQC = (byte)EnumFlagAQC.Success;

                            Catalog ctl0 = Meta.DataManager.GetInstance().CatalogRepository.Select(dv.CatalogId);
                            ctl0.MethodId = (int)EnumMethod.Operator;
                            ctl0.SourceId = UserOrganisationId;
                            List<Catalog> ctl = Meta.DataManager.GetInstance().CatalogRepository.Select(
                                new List<int> { ctl0.SiteId }, new List<int> { ctl0.VariableId },
                                new List<int> { ctl0.MethodId }, new List<int> { ctl0.SourceId },
                                new List<int> { ctl0.OffsetTypeId }, ctl0.OffsetValue);
                            if (ctl.Count == 0)
                            {
                                ctl.Add(Meta.DataManager.GetInstance().CatalogRepository.Insert(ctl0));
                            }
                            dv.CatalogId = ctl[0].Id;

                            DataValue dvExists = repdv.SelectDataValue(dv.CatalogId, dv.DateUTC, dv.Value);
                            if (dvExists != null)
                            {
                                repdv.Actualize(dvExists.Id);
                                skiped++;
                                actualized++;
                            }
                            else
                            {
                                repdv.Insert(dv);
                                inserted++;
                            }
                        }
                        else
                        {
                            repdv.DeleteDataValue(dv.Id);
                            deleted++;
                        }
                    }
                    else
                    {
                        throw new NotImplementedException("Вставка новых значений не реализована...");
                    }
                }
                if (dgv.SelectedCells.Count == 1)
                    FillDataDetail(dgv.SelectedCells[0]);

                MessageBox.Show("Сохранено " + inserted + " значений."
                    + "\nУдалено " + deleted + " значений."
                    + "\nСброшено (не сохранено) " + skiped + " значений."
                    + "\nАктуализировано " + actualized + " значений.",
                    "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cellsEdited = new List<DataGridViewCell>();
                saveEditedToolStripButton.Enabled = false;

                Fill(DataFilter);
            }
        }

        private void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            FillDataDetail(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex]);
            //ucDataDetail.Clear();

            //if (e.ColumnIndex >= _iColDataS && ShowDataDetails)
            //{
            //    DataGridViewCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //    if (cell.Tag != null && cell.Tag.GetType() == typeof(List<DataValue>))
            //    {
            //        DataValue dv = CellDataValue.GetDataValue(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex]);
            //        ucDataDetail.Fill(dv);
            //    }
            //}
        }
        FormOptionsUCDataEdit _formOptionsUCDataEdit = new FormOptionsUCDataEdit();
        private void optionsToolStripButton_Click(object sender, EventArgs e)
        {
            if (_formOptionsUCDataEdit.ShowDialog() == DialogResult.OK)
            {
                AcceptOptions();
            }
        }
        private void AcceptOptions()
        {
            OptionsDataEdit opt = _formOptionsUCDataEdit.Options;

            ShowDataDetails = opt.ShowDataDetails;
            ShowChart = opt.ShowChart;
            ucDataDetail.ShowAQC = opt.ShowAQC;
            ucDataDetail.ShowDerived = opt.ShowDerived;
            if (opt.ShowVarCodeText)
                ShowVarCodeTextInToolTips();
        }

        private void ShowVarCodeTextInToolTips()
        {
            List<Variable> vars = _vars.FindAll(x => x.UnitId == (int)EnumUnit.Categorical);
            if (vars != null && vars.Count > 0)
            {
                List<VariableCode> varCodes = Meta.DataManager.GetInstance().VariableCodeRepository.Select(vars.Select(x => x.Id).ToList());
                //TODO: не доделал... OSokolov@fehri.ru
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.ToolTipText = string.Empty;
                        DataValue dv = CellDataValue.GetDataValue(cell);
                        if (dv != null)
                        {
                            int varId = CatalogList.FirstOrDefault(x => x.Id == dv.CatalogId).VariableId;
                            int[] codes;
                            if (dv.Value <= 99)
                            {
                                codes = new int[] { (int)dv.Value };
                            }
                            else
                            {
                                codes = new int[] { (int)dv.Value / 100, 0 };
                                codes[1] = (int)(dv.Value - codes[0] * 100);
                                if (codes[0] == codes[1])
                                    codes = new int[] { codes[0] };
                            }
                            foreach (var code in codes)
                            {
                                VariableCode vc = varCodes.FirstOrDefault(x => x.VariableId == varId && x.Code == code);
                                cell.ToolTipText += code + " - " + (vc == null ? "нет описания кода..." : vc.Name) + "\n";
                            }
                        }
                    }
                }
            }
        }
        public bool ShowFilterButton
        {
            get
            {
                return dataFilterToolStripButton.Visible;
            }
            set
            {
                dataFilterToolStripButton.Visible = value;
            }
        }

        public bool ShowDataDetails
        {
            get
            {
                return !splitContainer1.Panel2Collapsed;
            }
            set
            {
                splitContainer1.Panel2Collapsed = !value;
            }
        }
        public bool ShowChart
        {
            get
            {
                return !splitContainer2.Panel2Collapsed;
            }
            set
            {
                splitContainer2.Panel2Collapsed = !value;
            }
        }

        #region EVENTS
        public delegate void UCCurrentDataValueChangedEventHandler(DataValue dv);
        public event UCCurrentDataValueChangedEventHandler UCCurrentDataValueChangedEvent;
        protected virtual void RaiseCurrentDataValueChangedEvent(DataValue dv)
        {
            if (UCCurrentDataValueChangedEvent != null)
            {
                UCCurrentDataValueChangedEvent(dv);
            }
        }

        public delegate void UCChangeOptionsEventHandler();
        public event UCChangeOptionsEventHandler UCChangeOptionsEvent;
        protected virtual void RaiseChangeOptionsEvent()
        {
            if (UCChangeOptionsEvent != null)
            {
                UCChangeOptionsEvent();
            }
        }
        #endregion

        private void dgv_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            RaiseCurrentDataValueChangedEvent(null);
        }

        internal void AsseptOptions(OptionsDataEdit options)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                bool visible = true;
                if (options.OnlyRedValues)
                {
                    bool isRed = false;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Style.ForeColor == Color.Red)
                        {
                            isRed = true;
                            break;
                        }
                    }
                    visible = isRed;
                }
                row.Visible = visible;
            }
        }
        /// <summary>
        /// Экспорт в файл.
        /// </summary>
        private void exportToolStripButton_Click(object sender, EventArgs e)
        {
            System.IO.StreamWriter sw = null;
            try
            {
                if (UserDirExportSAV == null)
                {
                    MessageBox.Show("Для пользователя отсутствует атрибут \"Директорий экспорта\"."
                        + "\nУстановите атрибут в настройках комплекса и повторите экспорт."
                        + "\nЭкспорт данных отменён.");
                    return;
                }

                string fileName = UserDirExportSAV.Value + "\\" + Text
                    + "." + DateTime.Now.ToString(DATE_FORMAT_COMPACT) + ".csv";
                if (System.IO.File.Exists(fileName))
                    System.IO.File.Delete(fileName);
                System.IO.FileStream file = System.IO.File.Create(fileName);

                sw = new System.IO.StreamWriter(file, Encoding.GetEncoding(1251));

                sw.Write(dgv.Columns[0].HeaderText.ToString());
                for (int i = 1; i < dgv.Columns.Count; i++)
                {
                    sw.Write(";" + dgv.Columns[i].HeaderText);
                }
                sw.Write("\n");
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    sw.Write(row.Cells[0].Value.ToString());
                    for (int i = 1; i < row.Cells.Count; i++)
                    {
                        sw.Write(";" + (row.Cells[i].Value == null ? "" : row.Cells[i].Value.ToString()));
                    }
                    sw.Write("\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sw != null) sw.Close();
            }
        }

        private void dgv_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            MessageBox.Show("dgv_ColumnHeaderMouseDoubleClick");
        }

        Meta.EnumDateType _TimeType = EnumDateType.UTC;
        public Meta.EnumDateType TimeType
        {
            get
            {
                return _TimeType;
            }
            set
            {
                switch (value)
                {
                    case EnumDateType.UTC: dateTypeToolStripButton.Text = "ВСВ"; break;
                    case EnumDateType.LOC: dateTypeToolStripButton.Text = "ЛОК"; break;
                }
                _TimeType = value;
                FillDateComboBox();
            }
        }
        private void dateTypeToolStripButton_Click(object sender, EventArgs e)
        {
            switch (TimeType)
            {
                case EnumDateType.UTC: TimeType = EnumDateType.LOC; break;
                case EnumDateType.LOC: TimeType = EnumDateType.UTC; break;
            }
        }
        public DateTime? CurDateTime
        {
            get
            {
                switch (CurViewType)
                {
                    case ViewType.Date_RStations_CVariables:
                        return datesComboBox.SelectedItem == null ? null : (DateTime?)(DateTime)datesComboBox.SelectedItem;
                    case ViewType.Station_RDates_CVariables:
                        return dgv.SelectedCells.Count != 1 ? null : (DateTime?)dgv.SelectedCells[0].OwningRow.Tag;
                    case ViewType.Variable_RStations_CDates:
                        return dgv.SelectedCells.Count != 1 ? null : (DateTime?)dgv.SelectedCells[0].OwningColumn.Tag;
                    default:
                        return null;
                }
            }
            set
            {
                if (CurViewType == ViewType.Date_RStations_CVariables)
                    datesComboBox.SelectedItem = value;
            }
        }
        public int? CurVariableId
        {
            get
            {
                switch (CurViewType)
                {
                    case ViewType.Date_RStations_CVariables:
                        return dgv.SelectedCells.Count != 1 ? null : (int?)((Variable)dgv.SelectedCells[0].OwningColumn.Tag).Id;
                    case ViewType.Station_RDates_CVariables:
                        return dgv.SelectedCells.Count != 1 ? null : (int?)((Variable)dgv.SelectedCells[0].OwningColumn.Tag).Id;
                    case ViewType.Variable_RStations_CDates:
                        return varsComboBox.SelectedItem == null ? null : (int?)((Variable)varsComboBox.SelectedItem).Id;
                    default:
                        return null;
                }
            }
            set
            {
                varsComboBox.SelectedItem = value;
            }
        }
        public int? CurSiteId
        {
            get
            {
                switch (CurViewType)
                {
                    case ViewType.Date_RStations_CVariables:
                        return dgv.SelectedCells.Count != 1 ? null : (int?)dgv.SelectedCells[0].OwningRow.Tag;
                    case ViewType.Station_RDates_CVariables:
                        return sitesComboBox.SelectedItem == null ? null : (int?)((Common.DicItem)sitesComboBox.SelectedItem).Id;
                    case ViewType.Variable_RStations_CDates:
                        return dgv.SelectedCells.Count != 1 ? null : (int?)dgv.SelectedCells[0].OwningRow.Tag;
                    default:
                        return null;
                }
            }
            set
            {
                sitesComboBox.SelectedItem = value;
            }
        }

        private void dateListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isFilled)
                FillDataCells();
        }

        private void timeBackwardToolStripButton_Click(object sender, EventArgs e)
        {
            if (datesComboBox.SelectedIndex == 0)
                datesComboBox.SelectedIndex = datesComboBox.Items.Count - 1;
            else
                datesComboBox.SelectedIndex = datesComboBox.SelectedIndex - 1;
        }

        private void timeForwardToolStripButton_Click(object sender, EventArgs e)
        {
            if (datesComboBox.SelectedIndex == datesComboBox.Items.Count - 1)
                datesComboBox.SelectedIndex = 0;
            else
                datesComboBox.SelectedIndex = datesComboBox.SelectedIndex + 1;
        }
        bool _isFilled = false;
        private void varsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isFilled)
                FillDataCells();
        }

        private void UCDataTableF2_Load(object sender, EventArgs e)
        {
            if (CurViewType == ViewType.Unknown)
                viewTypeToolStripComboBox.SelectedIndex = 0;

            _FormMethodSelect = new FormMethodFcsSelect();
            _FormMethodSelect.Fill(
                Meta.DataManager.GetInstance().MethodForecastRepository.Select(),
                Social.DataManager.GetInstance().LegalEntityRepository.SelectAll()
                );
            _FormMethodSelect.StartPosition = FormStartPosition.CenterScreen;

            AcceptOptions();
        }

        public ViewType CurViewType
        {
            get
            {
                return (ViewType)viewTypeToolStripComboBox.SelectedIndex;
            }
            set
            {
                viewTypeToolStripComboBox.SelectedIndex = (int)value;
            }
        }
        private void viewTypeToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            timeBackwardToolStripButton.Visible =
            timeForwardToolStripButton.Visible =
            datesComboBox.Visible =
            dateTypeToolStripButton.Visible =
            varsComboBox.Visible =
            sitesComboBox.Visible = false;

            add2ChartToolStripButton.Enabled = false;
            add2ChartToolStripButton.ToolTipText = null;

            switch ((ViewType)viewTypeToolStripComboBox.SelectedIndex)
            {
                case ViewType.Date_RStations_CVariables:
                    datesComboBox.Visible =
                    timeBackwardToolStripButton.Visible =
                    timeForwardToolStripButton.Visible =
                    dateTypeToolStripButton.Visible = true;
                    break;
                case ViewType.Variable_RStations_CDates:
                    varsComboBox.Visible = true;
                    break;
                case ViewType.Station_RDates_CVariables:
                    sitesComboBox.Visible = true;
                    add2ChartToolStripButton.Enabled = true;
                    add2ChartToolStripButton.ToolTipText = "Добавить данные текущего столбца на график";
                    break;
            }
            Fill();
        }

        private void sitesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isFilled)
                FillDataCells();
        }

        private void add2ChartToolStripButton_Click(object sender, EventArgs e)
        {
            switch (CurViewType)
            {
                case ViewType.Station_RDates_CVariables:

                    int iCol = dgv.SelectedCells[0].ColumnIndex;

                    List<DataValue> dvs = new List<DataValue>();
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        dvs.Add(GetDataValue(row.Cells[iCol], (DateTime)row.Cells[0].Value, (DateTime)row.Cells[1].Value));
                    }
                    ucChart.AddSeria(dgv.Columns[iCol].HeaderText, dvs, TimeType, iCol);

                    break;
                default:
                    MessageBox.Show("Для данного типа отображения таблицы добавление данных на график не реализовано. OSokolov@2015.12");
                    break;
            }
        }
        /// <summary>
        /// Для ячеек DataValue & DataForecast
        /// </summary>
        /// <returns>DataValue</returns>
        DataValue GetDataValue(DataGridViewCell cell, DateTime dateLOC, DateTime dateUTC)
        {
            DataValue ret = new DataValue(-1, -1, double.NaN, dateLOC, dateUTC, (byte)0, (float)(dateLOC - dateUTC).TotalHours);

            if (cell.Tag != null)
            {
                if (cell.Tag.GetType() == typeof(List<DataValue>))
                {
                    ret = CellDataValue.GetDataValue(cell);
                }
                else if (cell.Tag.GetType() == typeof(List<DataForecast>))
                {
                    List<DataForecast> dfs = CellDataForecast.GetDataValues(cell);
                    if (dfs != null)
                    {
                        ret.CatalogId = dfs[0].CatalogId;
                        ret.Value = dfs[0].Value;
                    }
                }
            }
            return ret;
        }

        FormMethodFcsSelect _FormMethodSelect;

        private void addColumnForecastButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.SelectedCells.Count == 1 && TableViewType == ViewType.Station_RDates_CVariables)
                {
                    DataGridViewCell cell = dgv.SelectedCells[0];

                    // GET OBS CATALOG
                    DataValue dv = CellDataValue.GetDataValue(cell);
                    Catalog obsCatalog = CatalogList.Find(x => x.Id == dv.CatalogId);

                    // GET FCS METHOD PARAMS
                    if (_FormMethodSelect.ShowDialog() == DialogResult.Cancel)
                        return;

                    int fcsMethodId = _FormMethodSelect.MethodForecast.Method.Id;
                    int fcsSourceId = _FormMethodSelect.Source.Id;
                    List<double> fcsLags = _FormMethodSelect.FcsLags;

                    int[] fcsOffset = ObsVsFcs.ObsVsFcs.GetFcsOffset(((Variable)cell.OwningColumn.Tag).Id);
                    if (fcsOffset == null)
                    {
                        MessageBox.Show("Для выбранного наблюдённого параметра отсутствует соответствующий ему offset в классе Obs2Fcs.");
                        return;
                    }

                    int fcsOffsetTypeId = fcsOffset[0];
                    double fcsOffsetValue = fcsOffset[1];

                    // GET FCS CATALOG
                    Catalog fcsCatalog = Meta.DataManager.GetInstance().CatalogRepository.SelectForecastCatalog(
                        obsCatalog.SiteId, obsCatalog.VariableId, fcsMethodId, fcsSourceId, fcsOffsetTypeId, fcsOffsetValue);

                    // GET DATA FORECASTS & FILL DGV FCS COLUMN
                    if ((object)fcsCatalog != null)
                    {
                        // GET FCS DATA
                        List<DataForecast> fcsData = Data.DataManager.GetInstance().DataForecastRepository.Select(fcsCatalog.Id,
                            _datesUTC.Min(x => x), _datesUTC.Max(x => x), null, true);
                        if (fcsLags != null)
                            fcsData.RemoveAll(x => fcsLags.Exists(y => y != x.LagFcs));

                        // INSERT FCS COLUMN
                        Variable fcsVar = VariableRepository.GetCash().Find(x => x.Id == fcsCatalog.VariableId);
                        DataGridViewColumn column = dgv.Columns[dgv.Columns.Add(
                            "fcs_catalog_id_" + fcsCatalog.Id,
                            fcsVar.NameRus + " - " + fcsMethodId)]
                        ;
                        column.Tag = fcsCatalog;

                        int iCol = cell.OwningColumn.Index + 1;
                        dgv.Columns.Remove(column);
                        dgv.Columns.Insert(iCol, column);

                        // FILL FCS COLUMN
                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            DateTime dateUTC = (DateTime)row.Cells[1].Value;
                            CellDataForecast.SetValues(row.Cells[iCol], fcsData.FindAll(x => x.DateFcs == dateUTC).OrderBy(x => x.LagFcs).ToList());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Для выбранного параметра и атрибутов метода прогноза отсутствует запись каталога для прогноза.");
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        ViewType TableViewType
        {
            get
            {
                return (ViewType)viewTypeToolStripComboBox.SelectedIndex;
            }
        }

        private void ucDataDetail_UCCurrentDataValueActualizedEvent()
        {
            DataValue dv = CellDataValue.GetDataValue(dgv.CurrentCell);

            Catalog ctl = Meta.DataManager.GetInstance().CatalogRepository.Select(dv.CatalogId);
            List<DataValue> dvs = Data.DataManager.GetInstance().DataValueRepository.SelectA(dv.DateUTC, dv.DateUTC, false,
               new List<int> { (int)CurSiteId }, new List<int> { (int)CurVariableId }, new List<int> { (int)ctl.OffsetTypeId }, ctl.OffsetValue,
               true, false, null, null, null);

            if (dvs.Count != 1)
                throw new Exception("(dvs.Count != 1) OSokolov@SOV.ru 2017.01");

            CellDataValue.SetDataValue(dgv.CurrentCell, dvs);

            FillDataDetail(dgv.CurrentCell);
        }
        private void FillDataDetail(DataGridViewCell cell)
        {
            ucDataDetail.Clear();

            if (cell != null && cell.ColumnIndex >= _iColDataS && ShowDataDetails)
            {
                if (cell.Tag != null && cell.Tag.GetType() == typeof(List<DataValue>))
                {
                    DataValue dv = CellDataValue.GetDataValue(cell);
                    ucDataDetail.Fill(dv);
                }
            }
        }
        bool _isAllRows = true;
        private void showHideEmptyRowsToolStripButton_Click(object sender, EventArgs e)
        {
            dgv.SuspendLayout();

            int emptyRowsCount = 0;
            if (_isAllRows)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    bool hasValue = false;
                    for (int i = _iColDataS; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Value != null)
                        {
                            hasValue = true;
                            break;
                        }
                    }
                    if (!hasValue)
                    {
                        emptyRowsCount++;
                        row.Visible = false;
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    row.Visible = true;
                }
            }
            _isAllRows = !_isAllRows;
            infoToolStripLabel.Text = (dgv.Rows.Count - emptyRowsCount) + " of " + dgv.Rows.Count;

            dgv.ResumeLayout();
        }
        void SetInfoLabelText(int count1, int count2, int count3)
        {
            infoToolStripLabel.Text = count1 + "x" + count2 + "x" + count3;
            _isAllRows = true;
        }
    }
}
