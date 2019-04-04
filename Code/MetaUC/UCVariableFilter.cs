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

namespace SOV.Amur.Meta
{
    public partial class UCVariableFilter : UserControl
    {
        public UCVariableFilter()
        {
            InitializeComponent();
        }
        void ShowDicToolbarButtons(Common.UCList dic)
        {
            dic.HideTollbarControls();

            dic.ShowToolbar =
            dic.ShowSelectAllToolbarButton =
            dic.ShowFindItemToolbarButton =
            dic.ShowSelectedOnlyToolbarButton =
            dic.ShowId =
            dic.ShowUnselectAllToolbarButton =
            true;
        }
        public VariableFilter VariableFilter
        {
            get
            {
                return new VariableFilter()
                {
                    VariableTypeIds = allVarTypesCheckBox.Checked ? null : varTypeListBox.GetSelectedItemsId(),
                    ValueTypeIds = allValueTypesCheckBox.Checked ? null : valueTypeListBox.GetSelectedItemsId(),
                    UnitIds = allUnitsCheckBox.Checked ? null : unitListBox.GetSelectedItemsId(),
                    TimeIds = allTimeCheckBox.Checked ? null : timeListBox.GetSelectedItemsId(),
                    DataTypeIds = allDataTypesCheckBox.Checked ? null : dataTypesListBox.GetSelectedItemsId(),
                    GeneralCategoryIds = allGeneralCategoriesCheckBox.Checked ? null : generalCategsListBox.GetSelectedItemsId(),
                    SampleMediumIds = allSamplesCheckBox.Checked ? null : samplesListBox.GetSelectedItemsId(),
                    TimeSupports = string.IsNullOrEmpty(timeSupportTextBox.Text) ? null : Common.StrVia.ToListInt(timeSupportTextBox.Text, ";")
                };
            }
            set
            {
                if (value != null)
                {
                    if (value.VariableTypeIds == null) allVarTypesCheckBox.Checked = true; else varTypeListBox.SetSelectedItemsById(value.VariableTypeIds);
                    if (value.ValueTypeIds == null) allValueTypesCheckBox.Checked = true; else valueTypeListBox.SetSelectedItemsById(value.ValueTypeIds);
                    if (value.UnitIds == null) allUnitsCheckBox.Checked = true; else unitListBox.SetSelectedItemsById(value.UnitIds);
                    if (value.TimeIds == null) allTimeCheckBox.Checked = true; else timeListBox.SetSelectedItemsById(value.TimeIds);
                    if (value.DataTypeIds == null) allDataTypesCheckBox.Checked = true; else dataTypesListBox.SetSelectedItemsById(value.DataTypeIds);
                    if (value.GeneralCategoryIds == null) allGeneralCategoriesCheckBox.Checked = true; else generalCategsListBox.SetSelectedItemsById(value.GeneralCategoryIds);
                    if (value.SampleMediumIds == null) allSamplesCheckBox.Checked = true; else samplesListBox.SetSelectedItemsById(value.SampleMediumIds);
                    timeSupportTextBox.Text = Common.StrVia.ToString(value.TimeSupports, ";");
                }
            }
        }
        private void allTypesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            varTypeListBox.Enabled = !allVarTypesCheckBox.Checked;
        }

        private void allValueTypesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            valueTypeListBox.Enabled = !allValueTypesCheckBox.Checked;
        }

        private void allTimeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            timeListBox.Enabled = !allTimeCheckBox.Checked;
        }

        private void allUnitsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            unitListBox.Enabled = !allUnitsCheckBox.Checked;
        }

        private void allDataTypesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            dataTypesListBox.Enabled = !allDataTypesCheckBox.Checked;
        }

        private void allGeneralCategoriesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            generalCategsListBox.Enabled = !allGeneralCategoriesCheckBox.Checked;
        }

        private void allSamplesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            samplesListBox.Enabled = !allSamplesCheckBox.Checked;
        }

        private void UCVariableFilter_Load(object sender, EventArgs e)
        {
            ShowDicToolbarButtons(varTypeListBox);
            ShowDicToolbarButtons(valueTypeListBox);
            ShowDicToolbarButtons(unitListBox);
            ShowDicToolbarButtons(timeListBox);
            ShowDicToolbarButtons(dataTypesListBox);
            ShowDicToolbarButtons(generalCategsListBox);
            ShowDicToolbarButtons(samplesListBox);

            varTypeListBox.SetDataSource(VariableTypeRepository.GetCash().Select(x => new IdName() { Id = x.Id, Name = x.Name }).ToList());
            valueTypeListBox.SetDataSource(ValueTypeRepository.GetCash().Select(x => new IdName() { Id = x.Id, Name = x.Name }).ToList());
            unitListBox.SetDataSource(UnitRepository.GetCash().Select(x => new IdName() { Id = x.Id, Name = x.Name }).ToList());
            timeListBox.SetDataSource(UnitRepository.GetCash().Where(x => x.Type == "Time").Select(x => new IdName() { Id = x.Id, Name = x.Name }).ToList());
            dataTypesListBox.SetDataSource(DataTypeRepository.GetCash().Select(x => new IdName() { Id = x.Id, Name = x.Name }).ToList());
            generalCategsListBox.SetDataSource(GeneralCategoryRepository.GetCash().Select(x => new IdName() { Id = x.Id, Name = x.Name }).ToList());
            samplesListBox.SetDataSource(SampleMediumRepository.GetCash().Select(x => new IdName() { Id = x.Id, Name = x.Name }).ToList());

            //valueTypeListBox.AddRange(ValueType.ToList<DicItem>(ValueTypeRepository.GetCash()));
            //varTypeListBox.AddRange(VariableType.ToList<DicItem>(VariableTypeRepository.GetCash()));
            //unitListBox.AddRange(Unit.ToList<DicItem>(UnitRepository.GetCash()));
            //timeListBox.AddRange(Unit.ToList<DicItem>(UnitRepository.GetCash().Where(x => x.Type == "Time").ToList()));
            //dataTypesListBox.AddRange(DataType.ToList<DicItem>(DataTypeRepository.GetCash()));
            //generalCategsListBox.AddRange(GeneralCategory.ToList<DicIteёm>(GeneralCategoryRepository.GetCash()));
            //samplesListBox.AddRange(SampleMedium.ToList<DicItem>(SampleMediumRepository.GetCash()));
        }
    }
}
