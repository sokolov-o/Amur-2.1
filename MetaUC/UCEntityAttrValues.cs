using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Amur.Meta
{
    public partial class UCEntityAttrValues : UserControl
    {
        public UCEntityAttrValues(string entityName)
        {
            InitializeComponent();

            EntityName = entityName;
        }
        public UCEntityAttrValues()
        {
            InitializeComponent();
        }

        public string EntityName { get; set; }
        public int EntityTypeId { get; set; }
        public int EntityId { get; set; }
        private List<EntityAttrValue> Values { get; set; }
        private List<EntityAttrType> Types { get; set; }

        public void Fill(string entityName, int entityId, int entityTypeId)
        {
            EntityId = entityId;
            EntityTypeId = entityTypeId;

            //if (editedRows.Count > 0)
            //    MessageBox.Show("Изменения атрибутов пункта не сохранены. В следующий раз будьте аккуратнее.");
            //editedRows.Clear();

            Values = Meta.DataManager.GetInstance().EntityAttrRepository.SelectAttrValues(entityName, new List<int>(new int[] { entityId }));
            Fill();
        }

        internal void Clear()
        {
            dgv.Rows.Clear();
        }

        private void UCEntityAttrValues_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                if (EntityName != null)
                    Types = Meta.DataManager.GetInstance().EntityAttrRepository.SelectAttrTypes(EntityName);
                attrSetToolStripComboBox.SelectedIndex = 1;
            }
        }
        //List<DataGridViewRow> editedRows = new List<DataGridViewRow>();
        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgv.Rows[e.RowIndex];
            if (dgv.Columns[e.ColumnIndex].Name == "value" && row.Cells["dateS"].Value == null)
                row.Cells["dateS"].Value = new DateTime(1900, 01, 01);

            SaveRow(row);

            //saveToolStripButton.Enabled = true;
            //if (!editedRows.Exists(x => x == row))
            //    editedRows.Add(row);
        }
        void SaveRow(DataGridViewRow row)
        {
            try
            {
                EntityAttrValue value = (EntityAttrValue)row.Tag;

                DateTime date;
                if (!DateTime.TryParse(row.Cells["dateS"].Value.ToString(), out date))
                {
                    MessageBox.Show("Атрибут \"" + row.Cells["attrTypeName"].Value + "\" - ошибка в дате."
                        + "\nСохранение отменено.");
                    return;
                }
                object svalue = row.Cells["value"].Value;
                if (svalue == null || string.IsNullOrEmpty(svalue.ToString()))
                {
                    MessageBox.Show("Атрибут \"" + row.Cells["attrTypeName"].Value + "\" - пустое значение."
                        + "\nСохранение отменено.");
                    return;
                }

                Meta.DataManager.GetInstance().EntityAttrRepository.InsertUpdateValue(EntityName,
                    value.EntityId, value.AttrTypeId,
                    date, svalue.ToString());
            }
            finally
            {
                Values = Meta.DataManager.GetInstance().EntityAttrRepository.SelectAttrValues(EntityName, EntityId);
                Fill();
            }
        }

        private void attrSetToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fill();
        }
        private void Fill(EntityAttrValue eav = null)
        {
            Clear();
            if (attrSetToolStripComboBox.SelectedIndex < 0 || Values == null) return;

            foreach (var item in Values)
            {
                switch ((EntityAttrValue.AttrSet)attrSetToolStripComboBox.SelectedIndex)
                {
                    case EntityAttrValue.AttrSet.All:
                        break;
                    case EntityAttrValue.AttrSet.Exists:
                        if (string.IsNullOrEmpty(item.Value))
                            continue;
                        break;
                    case EntityAttrValue.AttrSet.MandatoryNotExists:
                        if (
                            !(
                            string.IsNullOrEmpty(item.Value)
                            && Types.Exists(x => x.Id == item.AttrTypeId && x.Mandatories.Exists(y => y == EntityTypeId)))
                            )
                            continue;
                        break;
                    case EntityAttrValue.AttrSet.MandatoryOnly:
                        if (!Types.Exists(x => x.Id == item.AttrTypeId && x.Mandatories.Exists(y => y == EntityTypeId)))
                            continue;
                        break;
                    case EntityAttrValue.AttrSet.NotMandatory:
                        if (Types.Exists(x => x.Id == item.AttrTypeId && x.Mandatories.Exists(y => y == EntityTypeId)))
                            continue;
                        break;
                    case EntityAttrValue.AttrSet.Empty:
                        if (!string.IsNullOrEmpty(item.Value))
                            continue;
                        break;
                    default:
                        throw new Exception("Неизвестный тип набора значений атрибутов.");
                }

                DataGridViewRow row = dgv.Rows[dgv.Rows.Add()];
                row.Tag = item;

                row.Cells["attrTypeName"].Value = Types.Find(x => x.Id == item.AttrTypeId).Name;
                row.Cells["dateS"].Value = item.DateS;// (item.DateS == DateTime.MaxValue ? null : (DateTime?)item.DateS);
                row.Cells["value"].Value = item.Value;

                if (eav != null && item.AttrTypeId == eav.AttrTypeId && item.DateS == eav.DateS && item.EntityId == eav.EntityId)
                    row.Cells[0].Selected = true;
            }
            dgv.Sort(dgv.Columns["attrTypeName"], ListSortDirection.Ascending);
            infoToolStripLabel.Text = dgv.Rows.Count + " из " + Values.Count;
            if (dgv.Rows.Count > 0 && dgv.SelectedCells.Count == 0)
            {
                dgv.Rows[0].Cells[0].Selected = true;
            }
        }

        private void deleteToolStripButton_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedCells != null && dgv.SelectedCells.Count > 0)
            {
                EntityAttrValue value = (EntityAttrValue)dgv.SelectedCells[0].OwningRow.Tag;
                if (!string.IsNullOrEmpty(value.Value))
                {
                    Meta.DataManager.GetInstance().EntityAttrRepository.DeleteValue
                        (EntityName, value.EntityId, value.AttrTypeId, (DateTime)value.DateS);
                    Values = Meta.DataManager.GetInstance().EntityAttrRepository.SelectAttrValues(EntityName, EntityId);
                    Fill();
                }
            }
        }

        FormEntityAttrValue frm = null;
        private void addToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (frm == null)
                    frm = new FormEntityAttrValue(EntityName, Types);
                if (frm.ShowNewAttr(EntityId) == DialogResult.OK)
                {
                    EntityAttrValue eav = frm.EntityAttrValue;
                    Meta.DataManager.GetInstance().EntityAttrRepository.InsertUpdateValue(EntityName,
                        eav.EntityId, eav.AttrTypeId, (DateTime)eav.DateS, eav.Value);
                }
            }
            finally
            {
                Values = Meta.DataManager.GetInstance().EntityAttrRepository.SelectAttrValues(EntityName, EntityId);
                Fill();
            }
        }
    }
}
