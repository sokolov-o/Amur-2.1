using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Common
{
    /// <summary>
    /// Дерево объектов с интерфейсом IParent
    /// </summary>
    public partial class FormTree : Form
    {
        public FormTree(string caption)
        {
            InitializeComponent();

            Text = caption;
        }
        public UCTreeIParent UCTree { get { return ucTreeIdParent; } }

        private void button1_Click(object sender, EventArgs e) { DialogResult = DialogResult.OK; Close(); }

        private void button2_Click(object sender, EventArgs e) { Close(); }

        private void ucTreeIdParent_UCDoubleClickEvent()
        {
            button1_Click(null, null);
        }

        private void FormTree_Load(object sender, EventArgs e)
        {
            UCTree.ShowToolStrip = UCTree.ShowFindTextBox = true;
            UCTree.ShowAddButton = UCTree.ShowCloneButton = UCTree.ShowDeleteButton = UCTree.ShowEditButton = UCTree.ShowRefreshButton = false;
        }
    }
}
