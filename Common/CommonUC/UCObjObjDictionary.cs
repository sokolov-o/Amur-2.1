using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Common
{
    public partial class UCObjObjDictionary : UserControl
    {
        public UCObjObjDictionary()
        {
            InitializeComponent();
        }
        public void SetUCOptions(bool showAddButton, bool nameColumnReadOnly, bool valueColumnReadOnly,
            bool tableEnableAdding, bool tableEnableDeleteing)
        {
            addButton.Visible = showAddButton;
            dgv.Columns[0].ReadOnly = nameColumnReadOnly;
            dgv.Columns[1].ReadOnly = valueColumnReadOnly;
            dgv.AllowUserToAddRows = tableEnableAdding;
            dgv.AllowUserToDeleteRows = tableEnableDeleteing;
        }
        public void Add(object name, object value)
        {
            dgv.Rows.Add(new object[] { name, value });
        }
        public string UCNameColumnHeader
        {
            set
            {
                dgv.Columns[0].HeaderText = value;
            }
        }
        public string UCValueColumnHeader
        {
            set
            {
                dgv.Columns[1].HeaderText = value;
            }
        }
        public void Fill(Dictionary<object, object> dic, string nameColumnHeader, string valueColumnHeader)
        {
            dgv.Rows.Clear();
            foreach (KeyValuePair<object, object> kvp in dic)
            {
                dgv.Rows.Add(new object[] { kvp.Key, kvp.Value });
            }
            UCNameColumnHeader = nameColumnHeader;
            UCValueColumnHeader = valueColumnHeader;
        }

        public Dictionary<object, object> GetDictionary()
        {
            Dictionary<object, object> ret = new Dictionary<object, object>();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[0].Value != null)
                    ret.Add(row.Cells[0].Value, row.Cells[1].Value);
            }
            return ret;
        }
        public void Clear()
        {
            dgv.Rows.Clear();
        }
        public object[/*Key, Value*/] Current
        {
            get
            {
                if (dgv.Rows.Count > 0 && dgv.CurrentRow != null)
                {
                    object[] ret = new object[2];
                    ret[0] = dgv.CurrentRow.Cells[0].Value;
                    ret[1] = dgv.CurrentRow.Cells[1].Value;
                }
                return null;
            }
        }

        public delegate void UCAddNewEventHandler();
        public event UCAddNewEventHandler UCAddNew;
        private void addButton_Click(object sender, EventArgs e)
        {
            //UCAddNew?.Invoke();
            if (UCAddNew != null) UCAddNew();
        }

        public delegate void UCCurrentChangedEventHandler();
        public event UCCurrentChangedEventHandler UCCurrentChanged;
        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //UCCurrentChanged?.Invoke();
            if (UCCurrentChanged != null) UCCurrentChanged();
        }
    }
}
