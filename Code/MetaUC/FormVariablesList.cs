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
    public partial class FormVariablesList : Form
    {
        public FormVariablesList()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                ucVariablesList.Fill(Meta.DataManager.GetInstance().VariableRepository.Select());
            }
        }
       private void ucVariablesList_UCVariableChangedEvent(Variable var)
        {
            if (var == null)
            {
                ucVariable.ClearFields();
                ucVariableAttributes.Clear();
            }
            else
            {
                ucVariable.Variable = var;
                ucVariableAttributes.Fill(var.Id);
            }
            newButton.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                Variable var = ucVariable.Variable;
                VariableRepository rep = Meta.DataManager.GetInstance().VariableRepository;

                if (var.Id < 0)
                {
                    var.Id = rep.Insert(var);
                }
                else
                {
                    rep.Update(var);
                }
                ucVariablesList.Fill(rep.Select());
                ucVariablesList.SelectedId = var.Id;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Видимо, заполнены не все поля формы ввода данных. Возможно, запись не сохранена.\n\n"
                    + ex.ToString(), "Ошибка при сохранении изменений");
                return;
            }
            finally
            {
                this.Cursor = cs;
            }
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            ucVariable.New();
        }

        private void new1Button_Click(object sender, EventArgs e)
        {
            try
            {
                ucVariable.New(ucVariable.Variable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Видимо, заполнены не все поля формы ввода данных. Возможно, запись не сохранена.\n\n"
                    + ex.ToString(), "Ошибка при создании переменной на основе существующей.");
                return;
            }
        }
    }
}
