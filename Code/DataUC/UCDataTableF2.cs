using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FERHRI.Amur.Meta;
using FERHRI.Common;
using FERHRI.Amur.Sys;

namespace FERHRI.Amur.Data
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
    public partial class UCDataTableF2 : UserControl
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
        AttrValueCollection _UserSettings;
        public AttrValueCollection UserSettings
        {
            get { return _UserSettings; }
            set
            {
                _UserSettings = value;
                if (_UserSettings != null)
                {
                    AttrValue eiv = _UserSettings.FirstOrDefault(x => x.AttrId == (int)AttrEnum.UserDataFilter2S);
                    if (eiv != null)
                        FormDataFilter.DataFilter = DataFilter.Parse(eiv.Value);
                }
            }
        }
        Sys.AttrValue UserOrganisation
        {
            get
            {
                return UserSettings.Find(x => x.AttrId == (int)AttrEnum.UserOrganizationId);
            }
        }

        public FormDataFilter FormDataFilter;

        public UCDataTableF2(FormDataFilter formDataFilter = null, AttrValueCollection userSettings = null)
        {
            InitializeComponent();

            FormDataFilter = formDataFilter == null ? new FormDataFilter(true) : formDataFilter;
            UserSettings = userSettings;
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

        DateTime CurrentDateTime
        {
            get
            {
                return (DateTime)timeListComboBox.SelectedItem;
            }
        }
        List<DataValue> _dvs = null;
        List<Site> _sites;
        List<Station> _stations;
        List<SiteType> _siteTypes;
        List<Variable> _vars;
        List<DateTime> _datesLOC;
        List<DateTime> _datesUTC;

        public void Fill(DataFilter dataFilter)
        {
            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            _isFilled = true;

            try
            {
                // CLEAR TABLE

                dgv.Rows.Clear();
                dgv.Columns.Clear();

                DataFilter = dataFilter;

                // SELECT DATA

                _dvs = Data.DataManager.GetInstance().DataValueRepository.SelectA(DataFilter);
                CatalogList = Data.DataManager.GetInstance().CatalogRepository.Select(_dvs.Select(x => x.CatalogId).Distinct().ToList());

                _sites = Meta.DataManager.GetInstance().SiteRepository.Select(DataFilter.SiteIdList);
                _stations = Meta.DataManager.GetInstance().StationRepository.Select(_sites.Select(x => x.StationId).Distinct().ToList());
                _siteTypes = Meta.DataManager.GetInstance().SiteTypeRepository.Select();
                _vars = Meta.DataManager.GetInstance().VariableRepository.Select(DataFilter.VariableIdList);

                _datesLOC = _dvs.Select(x => x.DateLOC).Distinct().OrderBy(x => x).ToList();
                _datesUTC = _dvs.Select(x => x.DateUTC).Distinct().OrderBy(x => x).ToList();

                //var dates = _dvs.Select(x => new { DateLOC = x.DateLOC, DateUTC = x.DateUTC }).Distinct();//.ToList();
                //_datesLOC = new List<DateTime>();
                //_datesUTC = new List<DateTime>();
                //foreach (var item in dates.OrderBy(x => x.DateLOC))
                //{
                //    _datesLOC.Add(item.DateLOC);
                //    _datesUTC.Add(item.DateUTC);
                //}

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
        void Fill()
        {
            Cursor cs = this.Cursor;
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
                Station station = _stations.Find(x => x.Id == site.StationId);

                DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                row.Tag = new object[] { site, station };
                row.Cells[0].Value = station.Code;
                row.Cells[1].Value = station.Name;
                row.Cells[2].Value = _siteTypes.Find(x => x.Id == site.SiteTypeId).NameShort;
            }
            FillDataCells();

            infoToolStripLabel.Text = dgv.Rows.Count.ToString() + "x" + dgv.Columns.Count.ToString() + "x" + GetDates().Count();

            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private IEnumerable<DateTime> GetDates()
        {
            //return (this.TimeType == EnumTimeType.GMT) ? _datesUTC.Distinct().OrderBy(x => x).ToList() : _datesLOC.Distinct().OrderBy(x => x).ToList();
            return (this.TimeType == EnumTimeType.GMT) ? _datesUTC : _datesLOC;
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

            foreach (var variable in Meta.DataManager.GetInstance().VariableRepository.Select(DataFilter.VariableIdList).OrderBy(x => x.Name))
            {
                DataGridViewColumn col = dgv.Columns[dgv.Columns.Add(variable.Id.ToString(), variable.Name)];
                col.Tag = variable;
            }

            // CREATE DGV ROWS & FILL CELLS BY DATA (HEADER TAG = SITE)

            foreach (Site site in _sites)
            {
                Station station = _stations.Find(x => x.Id == site.StationId);

                DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                row.Tag = new object[] { site, station };
                row.Cells[0].Value = station.Code;
                row.Cells[1].Value = station.Name;
                row.Cells[2].Value = _siteTypes.Find(x => x.Id == site.SiteTypeId).NameShort;
            }
            FillDataCells();

            infoToolStripLabel.Text = dgv.Rows.Count.ToString() + "x" + dgv.Columns.Count.ToString() + "x" + timeListComboBox.Items.Count;

            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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

            foreach (var variable in Meta.DataManager.GetInstance().VariableRepository.Select(DataFilter.VariableIdList).OrderBy(x => x.Name))
            {
                DataGridViewColumn col = dgv.Columns[dgv.Columns.Add(variable.Id.ToString(), variable.Name)];
                col.Tag = variable;
            }

            //// CREATE DGV ROWS & FILL CELLS BY DATA

            //for (int i = 0; i < _datesLOC.Count; i++)
            //{
            //    DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
            //    row.Tag = new DateTime[] { _datesLOC[i], _datesUTC[i] };
            //    row.Cells[0].Value = _datesLOC[i];
            //    row.Cells[1].Value = _datesUTC[i];
            //}

            FillDataCells();

            infoToolStripLabel.Text = dgv.Rows.Count.ToString() + "x" + dgv.Columns.Count.ToString() + "x" + timeListComboBox.Items.Count;

            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private void FillDateComboBox()
        {
            timeListComboBox.Items.Clear();

            if (_dvs == null)
            {
                MessageBox.Show("Не выбраны пункты, переменные и др. Нажмите кнопку \"Фильтр\".");
                return;
            }

            IEnumerable<DateTime> dates = GetDates();

            if (dates.Count() > 0)
            {
                foreach (DateTime date in dates)
                {
                    timeListComboBox.Items.Add(date);
                }
                timeListComboBox.SelectedIndex = 0;
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
                    sitesComboBox.Items.Add(new Common.DicItem(item.Id, item.Name));
                }
                sitesComboBox.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// Заполнить таблицу для текущей даты
        /// </summary>
        private void FillDataCells()
        {
            switch (CurViewType)
            {
                case ViewType.Date_RStations_CVariables:
                    if (_dvs.Count == 0) return;

                    DateTime curDateTime = CurDateTime;
                    List<DataValue> dvsD = (TimeType == EnumTimeType.GMT)
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
                                if (dvsDSV.Count > 1) throw new Exception("Ошибка алгоритма: количество значений для пункта более одного.");
                                if (dvsDSV.Count == 1)
                                    CellDataValue.SetDataValue(row.Cells[i], dvsDSV);
                            }
                        }
                    }
                    break;
                case ViewType.Variable_RStations_CDates:
                    if (_dvs.Count == 0) return;

                    Variable var = CurVariable;
                    List<DataValue> dvsV = _dvs.FindAll(x => CatalogList.Exists(y => y.VariableId == var.Id && y.Id == x.CatalogId));

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        Site site = GetRowSite(row.Index);

                        List<DataValue> dvsVS = new List<DataValue>();
                        List<Catalog> ctls = CatalogList.FindAll(x => x.SiteId == site.Id && x.VariableId == var.Id);
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
                                List<DataValue> dvsVSD = dvsVS.FindAll(x => (TimeType == EnumTimeType.GMT) ? x.DateUTC == curDateTime : x.DateLOC == curDateTime);
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

                    List<Catalog> ctlsS = CatalogList.FindAll(y => y.SiteId == CurSite.Id);
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

                            if (dvsSDV.Count > 1) throw new Exception("(dvsSDV.Count > 1) " + dvsSDV.Count);
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
            return (Site)((object[])dgv.Rows[iRow].Tag)[0];
        }
        private void dataFilterToolStripButton_Click(object sender, EventArgs e)
        {
            if (DataFilter != null) FormDataFilter.DataFilter = DataFilter;
            if (FormDataFilter.ShowDialog() == DialogResult.OK)
            {
                Fill(FormDataFilter.DataFilter);
                UserSettings.Update((int)Sys.AttrEnum.UserDataFilter2S, UserSettings[0].EntityInstance, DataFilter.ToString());
            }
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            if (CurDateTime != null)
            {
                DateTime curDate = DateTime.FromBinary(CurDateTime.ToBinary());
                Fill(DataFilter);
                CurDateTime = curDate;
            }
        }
        List<DataGridViewCell> cellsEdited = new List<DataGridViewCell>();
        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (CellDataValue.GetDataValue(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex]) == null)
            {
                MessageBox.Show("Вставка новых значений не реализована...\nsov@201511", "Операция не реализована");
                return;
            }

            cellsEdited.Add(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex]);
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

                            Catalog ctl0 = Data.DataManager.GetInstance().CatalogRepository.Select(dv.CatalogId);
                            ctl0.MethodId = (int)EnumMethod.Operator;
                            ctl0.SourceId = (UserOrganisation == null) ? 0 : int.Parse(UserOrganisation.Value);
                            List<Catalog> ctl = Data.DataManager.GetInstance().CatalogRepository.Select(
                                new List<int>(new int[] { ctl0.SiteId }),
                                new List<int>(new int[] { ctl0.VariableId }),
                                ctl0.OffsetTypeId, ctl0.MethodId, ctl0.SourceId, ctl0.OffsetValue);
                            if (ctl.Count == 0)
                            {
                                ctl.Add(Data.DataManager.GetInstance().CatalogRepository.Insert(ctl0));
                            }
                            dv.CatalogId = ctl[0].Id;

                            long? id = repdv.SelectDataId(dv.CatalogId, dv.DateLOC, dv.Value);
                            if (id.HasValue)
                            {
                                id = repdv.Actualize((long)id);
                                if (!id.HasValue)
                                    throw new Exception("Не удалось актуализировать значение: " + dv);
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
            ucDataDetail.Clear();

            if (e.ColumnIndex >= _iColDataS && ShowDataDetails)
            {
                DataValue dv = CellDataValue.GetDataValue(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex]);
                //RaiseCurrentDataValueChangedEvent(dv);
                ucDataDetail.Fill(dv);
            }
        }
        FormOptionsUCDataEdit _formOptionsUCDataEdit = new FormOptionsUCDataEdit();
        private void optionsToolStripButton_Click(object sender, EventArgs e)
        {
            if (_formOptionsUCDataEdit.ShowDialog() == DialogResult.OK)
            {
                OptionsDataEdit opt = _formOptionsUCDataEdit.Options;

                ShowDataDetails = opt.ShowDataDetails;
                ShowChart = opt.ShowChart;
                ucDataDetail.ShowAQC = opt.ShowAQC;
                ucDataDetail.ShowDerived = opt.ShowDerived;
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
                Sys.AttrValue attr = UserSettings.Find(x => x.AttrId == (int)AttrEnum.UserDirExport);
                if (attr == null)
                {
                    MessageBox.Show("Для текущего пользователя " + UserSettings[0].EntityInstance + " отсутствует атрибут \"Директорий экспорта\"."
                        + "\nУстановите атрибут в настройках комплекса и повторите экспорт."
                        + "\nЭкспорт данных отменён.");
                    return;
                }

                string fileName = attr.Value + "\\" + Text + "." + DateTime.Now.ToString(DATE_FORMAT_COMPACT) + ".csv";
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

        Meta.EnumTimeType _TimeType = EnumTimeType.GMT;
        public Meta.EnumTimeType TimeType
        {
            get
            {
                return _TimeType;
            }
            set
            {
                switch (value)
                {
                    case EnumTimeType.GMT: dateTypeToolStripButton.Text = "ВСВ"; break;
                    case EnumTimeType.LOC: dateTypeToolStripButton.Text = "ЛОС"; break;
                }
                _TimeType = value;
                FillDateComboBox();
            }
        }
        private void dateTypeToolStripButton_Click(object sender, EventArgs e)
        {
            switch (TimeType)
            {
                case EnumTimeType.GMT: TimeType = EnumTimeType.LOC; break;
                case EnumTimeType.LOC: TimeType = EnumTimeType.GMT; break;
            }
        }
        public DateTime CurDateTime
        {
            get
            {
                return (DateTime)timeListComboBox.SelectedItem;
            }
            set
            {
                timeListComboBox.SelectedItem = value;
            }
        }
        public Variable CurVariable
        {
            get
            {
                return (Variable)varsComboBox.SelectedItem;
            }
            set
            {
                varsComboBox.SelectedItem = value;
            }
        }
        public Common.DicItem CurSite
        {
            get
            {
                return (Common.DicItem)sitesComboBox.SelectedItem;
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
            if (timeListComboBox.SelectedIndex == 0)
                timeListComboBox.SelectedIndex = timeListComboBox.Items.Count - 1;
            else
                timeListComboBox.SelectedIndex = timeListComboBox.SelectedIndex - 1;
        }

        private void timeForwardToolStripButton_Click(object sender, EventArgs e)
        {
            if (timeListComboBox.SelectedIndex == timeListComboBox.Items.Count - 1)
                timeListComboBox.SelectedIndex = 0;
            else
                timeListComboBox.SelectedIndex = timeListComboBox.SelectedIndex + 1;
        }
        bool _isFilled = false;
        private void varsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isFilled)
                FillDataCells();
        }

        private void UCDataTableF2_Load(object sender, EventArgs e)
        {
            viewTypeToolStripComboBox.SelectedIndex = 0;
        }

        public ViewType CurViewType
        {
            get
            {
                return (ViewType)viewTypeToolStripComboBox.SelectedIndex;
            }
            set
            {
                //timeBackwardToolStripButton.Visible = false;
                //timeForwardToolStripButton.Visible = false;

                //timeListComboBox.Visible = false;
                //varsComboBox.Visible = false;
                //sitesComboBox.Visible = false;

                //switch (value)
                //{
                //    case ViewType.Date_RStations_CVariables:
                //        timeListComboBox.Visible = true;
                //        timeBackwardToolStripButton.Visible = true;
                //        timeForwardToolStripButton.Visible = true;
                //        break;
                //    case ViewType.Variable_RStations_CDates:
                //        varsComboBox.Visible = true;
                //        break;
                //    case ViewType.Station_RDates_CVariables:
                //        sitesComboBox.Visible = true;
                //        break;
                //}
                viewTypeToolStripComboBox.SelectedIndex = (int)value;
                //Fill();
            }
        }
        private void viewTypeToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CurViewType = (ViewType)viewTypeToolStripComboBox.SelectedIndex;
            timeBackwardToolStripButton.Visible = false;
            timeForwardToolStripButton.Visible = false;

            timeListComboBox.Visible = false;
            varsComboBox.Visible = false;
            sitesComboBox.Visible = false;

            switch ((ViewType)viewTypeToolStripComboBox.SelectedIndex)
            {
                case ViewType.Date_RStations_CVariables:
                    timeListComboBox.Visible = true;
                    timeBackwardToolStripButton.Visible = true;
                    timeForwardToolStripButton.Visible = true;
                    break;
                case ViewType.Variable_RStations_CDates:
                    varsComboBox.Visible = true;
                    break;
                case ViewType.Station_RDates_CVariables:
                    sitesComboBox.Visible = true;
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
                    if (iCol < _iColDataS) return;

                    List<DataValue> dvs = new List<DataValue>();
                    List<DateTime> x = new List<DateTime>();
                    List<double> y = new List<double>();

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        x.Add((DateTime)((TimeType == EnumTimeType.GMT) ? row.Cells[1].Value : row.Cells[0].Value));
                        DataValue dv = CellDataValue.GetDataValue(row.Cells[iCol]);
                        y.Add((dv == null) ? double.NaN : dv.Value);
                        dvs.Add(dv);
                    }
                    ucChart.AddSeria(GetColumnVariable(iCol).Name, x, y, dvs);
                    break;
            }
        }
    }
}
