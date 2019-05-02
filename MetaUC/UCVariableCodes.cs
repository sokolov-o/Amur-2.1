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
    public partial class UCVariableCodes : UserControl
    {
        public UCVariableCodes()
        {
            InitializeComponent();
        }
        bool _isFilled = false;
        public void Fill(int? variableIdSelected = null)
        {
            _isFilled = true;
            try
            {
                List<Variable> vars = DataManager.GetInstance().VariableRepository.Select(null, null, new List<int> { (int)EnumUnit.Categorical }, null, null, null, null, null);
                ucVariablesList.Fill(vars);
            }
            finally
            {
                _isFilled = false;
            }

        }
        Variable _var = null;
        private void ucVariablesList_UCVariableChangedEvent(Variable var)
        {
            _var = null;
            if (!_isFilled)
            {
                _var = var;
                variableCodeBindingSource.DataSource = DataManager.GetInstance().VariableCodeRepository.Select(var.Id).OrderBy(x => x.Code).ToList();
                infoToolStripLabel.Text = variableCodeBindingSource.Count.ToString() + " шт.  ";
            }
        }
        Dictionary<VariableCode, ListChangedType> _itemsChanged = new Dictionary<VariableCode, ListChangedType>();
        private void variableCodeBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (!_isFilled)
            {
                VariableCode vc = (VariableCode)variableCodeBindingSource.Current;
                if (vc != null)
                {
                    if (_itemsChanged.ContainsKey(vc))
                    {
                        if (e.ListChangedType == ListChangedType.ItemDeleted)
                            _itemsChanged[vc] = ListChangedType.ItemDeleted;
                    }
                    else if (e.ListChangedType == ListChangedType.ItemChanged || e.ListChangedType == ListChangedType.ItemDeleted)
                    {
                        _itemsChanged.Add(vc, e.ListChangedType);
                    }
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (_itemsChanged.Count() > 0)
            {
                List<string> err = new List<string>(); ;
                try
                {
                    foreach (var item in _itemsChanged)
                    {
                        VariableCode vc = item.Key;
                        if (vc.VariableId < 1) vc.VariableId = _var.Id;

                        switch (item.Value)
                        {
                            case ListChangedType.ItemChanged:
                                Meta.DataManager.GetInstance().VariableCodeRepository.Update(vc);
                                break;
                            case ListChangedType.ItemDeleted:
                                Meta.DataManager.GetInstance().VariableCodeRepository.Delete(vc);
                                break;
                            default:
                                err.Add("Неизвестное изменение элемента таблицы " + item.Value + "\n");
                                break;
                        }
                    }
                }
                finally
                {
                    if (err.Count() > 0)
                    {
                        string mess = "Обратитесь OSokolov@SOV.ru\n" + string.Concat(err.ToArray());
                        MessageBox.Show(mess, "Ошибки при сохранении изменений!", MessageBoxButtons.OK);
                    }
                    _itemsChanged = new Dictionary<VariableCode, ListChangedType>();
                }
            }
        }

        private void variableCodeBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            try
            {
                if (!_isFilled)
                    dgvCodes.CurrentRow.Cells[1].ReadOnly = false;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
