using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Amur.Sys
{
    public partial class FormSys : Form
    {
        public FormSys(string parentText)
        {
            InitializeComponent();

            Text = parentText + " - системные настройки и свойства";
        }

        public List<Entity> Entityes { get; set; }
        public List<EntityAttr> CurAttrs { get; set; }

        string DATE_FORMAT = "dd.MM.yyyy HH:mm";

        private void FormSys_Load(object sender, EventArgs e)
        {
            Entityes = Sys.DataManager.GetInstance().SysEntityRepository.SelectEntity();
            Entityes = Entityes.OrderBy(x => x.Name).ToList();

            // ENTITYES
            foreach (var item in Entityes)
            {
                entityesListBox.Items.Add(item);

                entityLogToolStripComboBox.Items.Add(item);
            }

            // LOG

            DateF = DateTime.Today.AddDays(1).AddSeconds(-1);
            DateS = DateTime.Today.AddDays(-1);

            ShowParentsOnly = false;
        }

        private void entityLogToolStripComboBox_TextChanged(object sender, EventArgs e)
        {
            FillLog();
        }

        private void FillLog()
        {
            if (CurEntity4Log != null)
            {
                List<Log> log = Sys.DataManager.GetInstance().LogRepository.Select(DateS, DateF, CurEntity4Log.Id);
                dgvLog.DataSource = log;
                ShowParentsOnly = ShowParentsOnly;

                foreach (DataGridViewRow row in dgvLog.Rows)
                {
                    if (!((Log)row.DataBoundItem).ParentId.HasValue)
                        row.DefaultCellStyle.BackColor = Color.Gray;
                }
            }
            else
                Console.Beep();
        }

        public DateTime DateS
        {
            get
            {
                return DateTime.Parse(dateSToolStripTextBox.Text);
            }
            set
            {
                dateSToolStripTextBox.Text = value.ToString(DATE_FORMAT);
            }
        }
        public DateTime DateF
        {
            get
            {
                return DateTime.Parse(dateFToolStripTextBox.Text);
            }
            set
            {
                dateFToolStripTextBox.Text = value.ToString(DATE_FORMAT);
            }
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            FillLog();
        }

        private void deleteToolStripButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить все записи журнала событий указанного объекта за выбранный период?", "Удаление записей журнала",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Sys.DataManager.GetInstance().LogRepository.Delete(DateS, DateF, CurEntity4Log.Id);
            }
            FillLog();
        }
        bool _showParentsOnly;
        public bool ShowParentsOnly
        {
            get { return _showParentsOnly; }
            set
            {
                foreach (DataGridViewRow row in dgvLog.Rows)
                {
                    row.Visible = (!value) ? true : ((Log)row.DataBoundItem).ParentId.HasValue ? false : true;
                }
                _showParentsOnly = value;
            }
        }
        private void showParentsOnlyToolStripButton_Click(object sender, EventArgs e)
        {
            ShowParentsOnly = !ShowParentsOnly;
        }
        private void filterLikeToolStripButton_Click(object sender, EventArgs e)
        {
            bool showAllRows = string.IsNullOrEmpty(filterLikeToolStripTextBox.Text);

            foreach (DataGridViewCell item in dgvLog.SelectedCells)
            {
                item.Selected = false;
            }
            for (int i = 0; i < dgvLog.Rows.Count; i++)
            {
                DataGridViewRow row = dgvLog.Rows[i];
                row.Visible = true;
                if (!showAllRows && ((Log)row.DataBoundItem).Message.ToUpper().IndexOf(filterLikeToolStripTextBox.Text.ToUpper()) < 0)
                    row.Visible = false;
            }
        }
        Entity CurEntity4Attr
        {
            get
            {
                return entityesListBox.SelectedItem == null ? null : (Entity)entityesListBox.SelectedItem;
            }
        }
        Entity CurEntity4Log
        {
            get
            {
                return entityLogToolStripComboBox.SelectedItem == null ? null : (Entity)entityLogToolStripComboBox.SelectedItem;
            }
        }
        string CurInstance
        {
            get
            { return instancesListBox.SelectedItem == null ? null : (string)instancesListBox.SelectedItem; }
        }

        private void entityesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // FILL INSTANCES 
            instancesListBox.Items.Clear();
            dgvValues.DataSource = null;
            foreach (var item in Sys.DataManager.GetInstance().SysEntityRepository.SelectInstances(CurEntity4Attr.Id).OrderBy(x => x).ToList())
            {
                instancesListBox.Items.Add(item);
            }
        }

        private void instancesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // FILL VALUES
            DataGridViewComboBoxColumn col = (DataGridViewComboBoxColumn)dgvValues.Columns["EntityAttr"];
            var sysDM = Sys.DataManager.GetInstance();
            col.DataSource = sysDM.SysEntityRepository.SelectAttrs(CurEntity4Attr.Id).OrderBy(x => x.Name).ToList();

            List<Sys.EntityAttr> attrs = sysDM.SysEntityRepository.SelectAttrs(CurEntity4Attr.Id);
            dgvValues.DataSource = sysDM.SysEntityRepository.SelectValues(CurEntity4Attr.Id, CurInstance);//.OrderBy(x => x.Attr.Name).ToList();
        }
    }
}
