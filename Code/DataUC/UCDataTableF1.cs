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
    /// Табличная форма представления данных:
    /// 
    /// ОДНА станция, НЕСКОЛЬКО переменных за ПЕРИОД времени.
    /// 
    /// </summary>
    public partial class UCDataTableF1 : UserControl
    {
        public List<Sys.AttrValue> UserSettings { get; set; }
        Sys.AttrValue UserOrganisation
        {
            get
            {
                return UserSettings.Find(x => x.AttrId == (int)AttrEnum.UserOrganizationId);
            }
        }

        public FormDataFilter FormDataFilter { get; set; }

        public UCDataTableF1(FormDataFilter formDataFilter, List<Sys.AttrValue> userSettings)
        {
            InitializeComponent();

            FormDataFilter = formDataFilter;
            UserSettings = userSettings;

            DataFilterEnabled = false;
        }
        public UCDataTableF1()
        {
            InitializeComponent();
            DataFilterEnabled = false;
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

        int _iColDate = -1;

        static string DATE_FORMAT = "dd.MM.yyyy HH:mm";
        static string DATE_FORMAT_COMPACT = "yyyyMMdd.HHmm";

        public List<Catalog> CatalogDV { get; private set; }

        public void Fill(DataFilter dataFilter)
        {
            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                selectedDataValue = null;
                if (dgv.SelectedCells.Count > 0)
                    selectedDataValue = CellDataValue.GetDataValue(dgv.SelectedCells[0]);

                dgv.Rows.Clear();
                dgv.Columns.Clear();

                DataFilter = dataFilter;
                Text = GetText();

                // SELECT DATA

                List<DataValue> dvs0 = Data.DataManager.GetInstance().DataValueRepository.SelectA(dataFilter);
                List<DataValue> dvs1 = null; // Данные второго пункта. Например, реперного/основного.
                if (dataFilter.IsRefSiteData)
                {
                    Site site1 = Meta.DataManager.GetInstance().SiteRepository.SelectReferenceSite(Meta.DataManager.GetInstance().SiteRepository.Select(dataFilter.SiteIdList[0]));
                    if (site1 != null)
                    {
                        List<int> ids = dataFilter.SiteIdList;
                        dataFilter.SiteIdList = new List<int>(new int[] { site1.Id });

                        dvs1 = Data.DataManager.GetInstance().DataValueRepository.SelectA(dataFilter);

                        dataFilter.SiteIdList = ids;
                    }
                }
                CatalogDV = Data.DataManager.GetInstance().CatalogRepository.Select(dvs0.Select(x => x.CatalogId).Distinct().ToList());
                if (dvs1 != null)
                    CatalogDV.AddRange(Data.DataManager.GetInstance().CatalogRepository.Select(dvs1.Select(x => x.CatalogId).Distinct().ToList()));

                // CREATE DGV COLUMNS (HEADER TAG = VARIABLE)

                _iColDate = dgv.Columns.Add("date_loc", "Дата лок.");
                dgv.Columns[_iColDate].ReadOnly = true;
                dgv.Columns[_iColDate].DefaultCellStyle.BackColor = Color.LightYellow;
                dgv.Columns[_iColDate].DefaultCellStyle.Format = DATE_FORMAT;
                _iColDate = dgv.Columns.Add("date_utc", "Дата ВСВ");
                dgv.Columns[_iColDate].ReadOnly = true;
                dgv.Columns[_iColDate].DefaultCellStyle.BackColor = Color.LightYellow;
                dgv.Columns[_iColDate].DefaultCellStyle.Format = DATE_FORMAT;

                foreach (var variable in Meta.DataManager.GetInstance().VariableRepository.Select(dataFilter.VariableIdList).OrderBy(x => x.Name))
                {
                    DataGridViewColumn col = dgv.Columns[dgv.Columns.Add(variable.Id.ToString(), variable.Name)];
                    col.Tag = variable;
                    col.ToolTipText = variable.ToString();
                    if (dvs1 != null)
                    {
                        col = dgv.Columns[dgv.Columns.Add(variable.Id.ToString(), variable.Name + "/2")];
                        col.Tag = variable;
                        col.DefaultCellStyle.BackColor = Color.AliceBlue;
                    }
                }

                // CREATE DGV ROWS & FILL CELLS BY DATA (HEADER TAG = DATE)

                if (dvs0.Count > 0)
                {
                    var query = dvs0.GroupBy(
                        x => x.DateLOC,
                        (DateLOC, pets) => new
                        {
                            Key = DateLOC,
                            DateUTCList = pets.Select(y => y.DateUTC).Distinct().ToList()
                        }).OrderBy(x => x.Key);
                    foreach (var dateLOC1 in query)
                    {
                        DateTime dateLOC = dateLOC1.Key;

                        foreach (var dateUTC in dateLOC1.DateUTCList)
                        {
                            DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                            row.Tag = new object[] { dateLOC, dateUTC };
                            row.Cells[_iColDate - 1].Value = dateLOC;
                            row.Cells[_iColDate - 0].Value = dateUTC;

                            List<DataValue> dateDV0 = dvs0.FindAll(x => x.DateLOC == dateLOC && x.DateUTC == dateUTC);
                            List<DataValue> dateDV1 = (dvs1 == null) ? null : dvs1.FindAll(x => x.DateLOC == dateLOC && x.DateUTC == dateUTC);

                            for (int i = _iColDate + 1; i < row.Cells.Count; i += ((dvs1 != null) ? 2 : 1))
                            {
                                Variable var = (Variable)row.Cells[i].OwningColumn.Tag;
                                List<Catalog> ctl = CatalogDV.FindAll(y => y.VariableId == var.Id);

                                if (ctl.Count > 0)
                                {
                                    CellDataValue.SetDataValue(
                                        row.Cells[i],
                                        dateDV0.FindAll(x => ctl.Exists(y => y.Id == x.CatalogId))
                                    );
                                }
                                if (dvs1 != null)
                                {
                                    ctl = CatalogDV.FindAll(y => y.VariableId == var.Id);
                                    if ((object)ctl != null)
                                    {
                                        CellDataValue.SetDataValue(
                                            row.Cells[i + 1],
                                            dateDV1.Where(x => ctl.Exists(y => y.Id == x.CatalogId)).ToList()
                                        );
                                    }
                                }
                            }
                        }
                    }
                }
                infoToolStripLabel.Text = dgv.Rows.Count.ToString();
                dgv.Columns[_iColDate].Frozen = true;
            }
            finally
            {
                this.Cursor = cs;
                SetSelectedDataValueRow();
            }
        }

        private string GetText()
        {
            string ret = string.Empty;
            DicItem site;

            if (DataFilter.SiteIdList.Count == 1)
            {
                site = DicCash.Cash[typeof(Site)].Find(x => x.Id == DataFilter.SiteIdList[0]);
                ret += site.Name;
            }
            else
                throw new Exception("Невозможно составить текст для названия таблицы данных.");
            return ret;
        }

        private void dataFilterToolStripButton_Click(object sender, EventArgs e)
        {
            FormDataFilter.SiteNodeEnabled = true;
            if (DataFilter != null) FormDataFilter.DataFilter = DataFilter;
            if (FormDataFilter.ShowDialog() == DialogResult.OK)
            {
                DataFilter = FormDataFilter.DataFilter;
            }
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            Fill(DataFilter);
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
        DateTime GetRowDateTime(DataGridViewCell cell)
        {
            return (DateTime)cell.OwningRow.Tag;
        }
        int GetColumnVariableId(DataGridViewCell cell)
        {
            return ((Meta.Variable)cell.OwningColumn.Tag).Id;
        }

        DataValue selectedDataValue = null;
        void SetSelectedDataValueRow()
        {
            if (selectedDataValue != null)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells[_iColDate - 1].Value.ToString().CompareTo(selectedDataValue.DateLOC.ToString(DATE_FORMAT)) == 0
                        && row.Cells[_iColDate - 0].Value.ToString().CompareTo(selectedDataValue.DateUTC.ToString(DATE_FORMAT)) == 0)
                    {
                        row.Cells[_iColDate - 1].Selected = true;
                        dgv.FirstDisplayedCell = row.Cells[_iColDate - 1];
                        return;
                    }
                }
            }
        }


        //DateTime? selectedCellDate = null;
        //int? selectedCellVariableId = null;
        //DataGridViewCell SelectedCell
        //{
        //    get
        //    {
        //        if (selectedCellDate.HasValue)
        //        {
        //            foreach (DataGridViewRow row in dgv.Rows)
        //            {
        //                if (row.Cells[_iColDate].Value.ToString().CompareTo(((DateTime)selectedCellDate).ToString(DATE_FORMAT)) == 0)
        //                {
        //                    if (selectedCellVariableId < 0)
        //                    {
        //                        return row.Cells[_iColDate];
        //                    }
        //                    else
        //                        for (int i = _iColDate + 1; i < row.Cells.Count; i++)
        //                        {
        //                            if (GetColumnVariableId(row.Cells[i]) == selectedCellVariableId)
        //                                return row.Cells[i];
        //                        }
        //                }
        //            }
        //        }
        //        return null;
        //    }
        //    set
        //    {
        //        selectedCellDate = null; selectedCellVariableId = null;

        //        if (value != null)
        //        {
        //            selectedCellDate = GetRowDateTime(value);
        //            selectedCellVariableId = value.ColumnIndex > _iColDate ? GetColumnVariableId(dgv.SelectedCells[0]) : -1;
        //        }
        //    }
        //}


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
            if (e.ColumnIndex > _iColDate)
                RaiseCurrentDataValueChangedEvent(CellDataValue.GetDataValue(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex]));
        }


        private void dgv_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.RowIndex > -1)
            //{
            //    this.dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
            //}
        }

        private void optionsToolStripButton_Click(object sender, EventArgs e)
        {
            RaiseChangeOptionsEvent();
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
            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

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
                MessageBox.Show("Данные экспортированы в файл " + fileName, "Экспорт данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sw != null) sw.Close();
                this.Cursor = cs;
            }
        }

        private void dgv_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            MessageBox.Show("dgv_ColumnHeaderMouseDoubleClick");
        }
    }
}
