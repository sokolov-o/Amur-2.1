using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Amur.Meta
{
    public partial class FormMethods : Form
    {
        public FormMethods()
        {
            InitializeComponent();

        }

        private void methodTree_UCRefresh()
        {
            ucMethodTree.Clear();
            List<Method> meths = DataManager.GetInstance().MethodRepository.Select();
            ucMethodTree.AddRange(meths.ToArray());
        }

        private void methodTree_UCSelectedItemChanged(object dici)
        {
            Method meth = (Method)ucMethodTree.SelectedItem;
            ucMethod.Method = meth;

        }

        private void saveMethodButton_Click(object sender, EventArgs e)
        {
            try
            {
                ucMethod.SaveMethod();
                ucMethodTree.SetSelectedNodeValue(ucMethod.Method);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ucMethodTree_UCAddNewItem(object item)
        {
            MessageBox.Show("Not implemented. OSokolov@201712");
        }

        private void ucMethodTree_UCCloneItem(object dici)
        {
            try
            {
                int id = DataManager.GetInstance().MethodRepository.Clone(((Method)ucMethodTree.SelectedItem).Id);

                //Method meth = (Method)ucMethodTree.SelectedItem;
                //meth.Id = -1;
                //meth.Name = "#" + meth.Name;
                //meth.Order = (short)((short)DataManager.GetInstance().MethodRepository.SelectMaxOrder() + 1);
                //int id = DataManager.GetInstance().MethodRepository.Insert(meth);

                methodTree_UCRefresh();
                ucMethodTree.SetSelectedNode(id, typeof(Method));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ucMethodTree_UCDeleteItem(object item)
        {
            Method meth = (Method)ucMethodTree.SelectedItem;
            DataManager.GetInstance().MethodRepository.Delete(meth.Id);
            methodTree_UCRefresh();
        }
    }
}
