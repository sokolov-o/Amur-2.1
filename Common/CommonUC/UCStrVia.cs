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
    public partial class UCStrVia : UserControl
    {
        public UCStrVia()
        {
            InitializeComponent();
        }
        public bool UCNameColumnReadOnly
        {
            set
            {
                dgv.Columns[0].ReadOnly = value;
            }
        }
        public bool UCValueColumnReadOnly
        {
            set
            {
                dgv.Columns[1].ReadOnly = value;
            }
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
        public void Fill(Dictionary<string, string> dic, string column0HeaderText = "Поле")
        {
            dgv.Rows.Clear();
            foreach (KeyValuePair<string, string> kvp in dic)
            {
                dgv.Rows.Add(new object[] { kvp.Key, kvp.Value });
            }
            dgv.Columns[0].HeaderText = column0HeaderText;
        }

        public Dictionary<string, string> GetDictionary()
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[0].Value != null)
                    ret.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
            }
            return ret;
        }
        public void Clear()
        {
            dgv.Rows.Clear();
        }
    }
}
