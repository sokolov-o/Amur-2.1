using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Common;
using SOV.Amur.Meta;

namespace SOV.Amur.Data
{
    public partial class UCDataValueList : UserControl
    {
        public UCDataValueList()
        {
            InitializeComponent();

            #region void CreateDGV()
            {
                AddDGVColumn("value", "Значение", "");
                AddDGVColumn("flagAQC", "АКК", "Признак автоматического контроля данных.");
                AddDGVColumn("dateLOC", "Дата ЛОК", "Время измерения (местное, локальное).");
                AddDGVColumn("siteName", "Пункт", "Пункт/пост измерения.");
                AddDGVColumn("variableName", "Переменная", "Измеренная переменная.");
                AddDGVColumn("methodName", "Метод", "Метод измерения.");
                AddDGVColumn("sourceName", "Источник", "Источник данных.");
                AddDGVColumn("offsetTypeName", "Смещ. тип", "Тип смещения от пункта наблюдений.");
                AddDGVColumn("offsetValue", "Смещ. знач.", "Величина смещения от пункта наблюдений.");
                AddDGVColumn("dataSource", "Источник данных", "Истоник данных (телегр., файл, др.).");
                AddDGVColumn("dateUTC", "Дата ВСВ", "Время измерения (ВСВ).");
                AddDGVColumn("utcOffset", "Пояс", "Разница с ВСВ (час).");
                AddDGVColumn("id", "ID", "Уникальный идентификатор значения.");

                List<IdName> idNames = new List<IdName>();
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    idNames.Add(new IdName() { Id = i, Name = dgv.Columns[i].HeaderCell.ToolTipText });
                }
                ucDicColumnsVisible.SetDataSource(idNames);
            }
            #endregion

            ShowDeleted = true;
            tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.ColumnStyles[1].Width = 0;
        }
        void AddDGVColumn(string name, string headerText, string toolTipText)
        {
            DataGridViewColumn col = dgv.Columns[dgv.Columns.Add(name, headerText)];
            col.HeaderCell.ToolTipText = toolTipText;
        }
        private void mnuColumnsVisibleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            List<int> si = new List<int>();
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (dgv.Columns[i].Visible)
                    si.Add(i);
            }
            ucDicColumnsVisible.SetSelectedItemsById(si);
            tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.AutoSize;
        }
        private void buttonColumnsVisible_Click(object sender, EventArgs e)
        {
            List<int> si = ucDicColumnsVisible.GetSelectedItemsId();
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].Visible = si.Exists(pet => pet == i);
            }
            tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.ColumnStyles[1].Width = 0;
        }

        public List<string> ShowColumns
        {
            //get
            //{
            //    List<string> ret = new List<string>();
            //    foreach (DataGridViewColumn dgvcol in dgv.Columns)
            //    {
            //        if (dgvcol.Visible)
            //            ret.Add(dgvcol.Name);
            //    }
            //    return ret;
            //}
            set
            {
                foreach (DataGridViewColumn dgvcol in dgv.Columns)
                {
                    if (value != null)
                        dgvcol.Visible = value.Find(pet => pet.ToUpper() == dgvcol.Name.ToUpper()) != null;
                }
            }
        }
        public List<string> HideColumns
        {
            set
            {
                if (value != null)
                {
                    foreach (DataGridViewColumn dgvcol in dgv.Columns)
                    {
                        dgvcol.Visible = value.Find(pet => pet.ToUpper() == dgvcol.Name.ToUpper()) == null;
                    }
                }
            }
        }
        internal void Fill(DataValue dv)
        {
            Clear();
            if (dv != null)
            {
                Data.DataManager dmd = Data.DataManager.GetInstance();
                Meta.DataManager dmm = Meta.DataManager.GetInstance();

                Catalog ctl = dmm.CatalogRepository.Select(dv.CatalogId);
                List<DataValue> dvs = dmd.DataValueRepository.SelectA(dv.DateLOC, dv.DateLOC, true,
                    new List<int>(new int[] { ctl.SiteId }),
                    new List<int>(new int[] { ctl.VariableId }),
                    new List<int> { ctl.OffsetTypeId }, ctl.OffsetValue,
                    false, true, null);
                //dvs = dvs.OrderByDescending(x => x.Id).ToList();

                Dictionary<long, DataSource> dvsrcs = dmd.DataSourceRepository.Select(dvs.Select(x => x.Id).Distinct().ToList());

                Fill(dvs, dvsrcs);
            }
        }
        /// <summary>
        /// Заполнение таблицы данными.
        /// </summary>
        /// <param name="dvs">Коллекция отсортированных в нужном порядке значений.</param>
        /// <param name="dataSources">Источники данных. Допускается null.</param>
        internal void Fill(List<DataValue> dvs, Dictionary<long, DataSource> dataSources)
        {
            Clear();

            List<Catalog> ctls = Meta.DataManager.GetInstance().CatalogRepository.Select(dvs.Select(x => x.CatalogId).Distinct().ToList());

            foreach (DataValue dv in dvs)//.OrderByDescending(x => x.Id))
            {
                DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                row.Tag = dv;

                Catalog ctl = ctls.Find(x => x.Id == dv.CatalogId);

                row.Cells["id"].Value = dv.Id;
                row.Cells["value"].Value = dv.Value;
                row.Cells["dateLOC"].Value = dv.DateLOC;
                row.Cells["dateUTC"].Value = dv.DateUTC;
                row.Cells["utcOffset"].Value = dv.UTCOffset;

                row.Cells["siteName"].Value = SiteRepository.GetCash().FirstOrDefault(x => x.Id == ctl.SiteId).GetName(2, SiteTypeRepository.GetCash());
                row.Cells["variableName"].Value = VariableRepository.GetCash().FirstOrDefault(x => x.Id == ctl.VariableId).NameRus;
                row.Cells["offsetTypeName"].Value = OffsetTypeRepository.GetCash().FirstOrDefault(x => x.Id == ctl.OffsetTypeId).Name;
                row.Cells["methodName"].Value = MethodRepository.GetCash().FirstOrDefault(x => x.Id == ctl.MethodId).Name;
                row.Cells["sourceName"].Value = Social.LegalEntityRepository.GetCash().FirstOrDefault(x => x.Id == ctl.SourceId).NameRus;
                row.Cells["offsetValue"].Value = ctl.OffsetValue;

                row.Cells["flagAQC"].Value = dv.FlagAQC;

                row.Cells["dataSource"].Value = string.Empty;
                DataSource ds;
                if (dataSources != null && dataSources.TryGetValue(dv.Id, out ds))
                    row.Cells["dataSource"].Value = ds.Value;

                AcceptCustomStyle(row);
            }
            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.AutoSizeMode = (item.Name.ToUpper().IndexOf("DATE") >= 0) ? DataGridViewAutoSizeColumnMode.AllCells : DataGridViewAutoSizeColumnMode.ColumnHeader;
            }
            RaiseUCCurrentDataValueChangedEvent(DataValueSelected);
        }

        //void SetCellValue(DataGridViewCell cell, int dicId, Type dicType)
        //{
        //    List<DicItem> dicis = DicCash.ToListDicItems(dicType);
        //    DicItem dici = dicis.FirstOrDefault(x => x.Id == dicId);
        //    if (dici != null)
        //    {
        //        cell.Value = dici.Name;
        //        return;
        //    }
        //    cell.Value = dicId + " нет словаря в кэше...";
        //}
        internal void Clear()
        {
            dgv.Rows.Clear();
            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }

            RaiseUCCurrentDataValueChangedEvent(null);
        }
        DataValue GetDataValue(DataGridViewRow row)
        {
            return (DataValue)row.Tag;
        }
        DataValue DataValueSelected
        {
            get
            {
                return (this.dgv.SelectedRows.Count == 1) ? GetDataValue(dgv.SelectedRows[0]) : null;
            }
        }
        string CurDataSource
        {
            get
            {
                return (this.dgv.SelectedRows.Count == 1) ? dgv.SelectedRows[0].Cells["dataSource"].Value.ToString() : string.Empty;
            }
        }
        private void mnuDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 1)
            {
                DataValue dv = GetDataValue(dgv.SelectedRows[0]);
                Data.DataManager.GetInstance().DataValueRepository.DeleteDataValue(dv.Id);
                dv.FlagAQC = 255;
                AcceptCustomStyle(dgv.SelectedRows[0]);
            }
        }

        bool _showDeleted;
        bool ShowDeleted
        {
            get { return _showDeleted; }
            set
            {
                _showDeleted = value;

                mnuShowDeletedToolStripMenuItem.Image = value ? Properties.Resources.CheckBoxChecked : null;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    AcceptCustomStyle(row);
                }
            }
        }
        /// <summary>
        /// - Показать/скрыть
        /// - Отметить удалённые значения
        /// </summary>
        void AcceptCustomStyle(DataGridViewRow row)
        {
            if (GetDataValue(row).FlagAQC == 255)
            {
                row.DefaultCellStyle.Font = new Font(dgv.DefaultCellStyle.Font, FontStyle.Strikeout);
                row.Visible = ShowDeleted;
            }
        }
        private void mnuShowDeletedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDeleted = !ShowDeleted;
            mnuShowDeletedToolStripMenuItem.Image = ShowDeleted ? Properties.Resources.CheckBoxChecked : null;
        }

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.SelectedRows.Count == 1)
                RaiseUCCurrentDataValueChangedEvent(GetDataValue(dgv.SelectedRows[0]));
        }

        private void mnuActualizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataValue dv = DataValueSelected;
                if (dv != null)
                {
                    Data.DataManager.GetInstance().DataValueRepository.Actualize(dv.Id);
                    RaiseUCCurrentDataValueActualizedEvent();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region EVENTS
        public delegate void UCCurrentDataValueChangedEventHandler(DataValue dv);
        public event UCCurrentDataValueChangedEventHandler UCCurrentDataValueChangedEvent;
        protected virtual void RaiseUCCurrentDataValueChangedEvent(DataValue dv)
        {
            if (UCCurrentDataValueChangedEvent != null)
            {
                UCCurrentDataValueChangedEvent(dv);
            }
        }

        public delegate void UCCurrentDataValueActualizedEventHandler();
        public event UCCurrentDataValueActualizedEventHandler UCCurrentDataValueActualizedEvent;
        protected virtual void RaiseUCCurrentDataValueActualizedEvent()
        {
            if (UCCurrentDataValueActualizedEvent != null)
            {
                UCCurrentDataValueActualizedEvent();
            }
        }

        #endregion

        private void mnuShowTelegrammToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string ds = CurDataSource;
                if (ds != null)
                {
                    FormRichTextBox frm = new FormRichTextBox();
                    frm.Caption = "Источник данных для " + DataValueSelected.Id;
                    frm.Text = ds;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
