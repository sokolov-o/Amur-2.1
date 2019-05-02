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
    public partial class UCVariable : UserControl
    {
        public UCVariable()
        {
            InitializeComponent();
        }
        public Variable Variable
        {
            set
            {
                ClearFields();

                if (!DesignMode && value != null)
                {
                    idTextBox.Text = value.Id.ToString();

                    ucVariableType.SelectedId = value.VariableTypeId;
                    ucUnit.SelectedId = value.UnitId;
                    ucTime.SelectedId = value.TimeId;
                    ucDataType.SelectedId = value.DataTypeId;
                    ucGeneralCategory.SelectedId = value.GeneralCategoryId;
                    ucSampleMedium.SelectedId = value.SampleMediumId;
                    ucValueType.SelectedId = value.ValueTypeId;

                    timeSupportTextBox.Text = value.TimeSupport.ToString();
                    nameTextBox.Text = value.NameRus;
                    nameEngTextBox.Text = value.NameEng;

                    codeNoDataTextBox.Text = value.CodeNoData.ToString();
                    codeErrDataTextBox.Text = value.CodeErrData.ToString();
                }
            }
            get
            {
                if (DesignMode)
                    return null;

                int nullId = int.MinValue;

                return new Variable(
                    int.Parse(idTextBox.Text),
                    Miscel.Coalesce(ucVariableType.SelectedId, nullId),
                    Miscel.Coalesce(ucTime.SelectedId, nullId),
                    Miscel.Coalesce(ucUnit.SelectedId, nullId),
                    Miscel.Coalesce(ucDataType.SelectedId, nullId),
                    Miscel.Coalesce(ucValueType.SelectedId, nullId),
                    Miscel.Coalesce(ucGeneralCategory.SelectedId, nullId),
                    Miscel.Coalesce(ucSampleMedium.SelectedId, nullId),
                    int.Parse(timeSupportTextBox.Text),
                    nameTextBox.Text, nameEngTextBox.Text,
                    int.Parse(codeNoDataTextBox.Text),
                    int.Parse(codeErrDataTextBox.Text)
                );
            }
        }
        public Variable Filter
        {
            get
            {
                if (DesignMode)
                    return null;

                return new Variable(
                    string.IsNullOrEmpty(idTextBox.Text) ? -1 : int.Parse(idTextBox.Text),
                    Miscel.Coalesce(ucVariableType.SelectedId, -1),
                    Miscel.Coalesce(ucTime.SelectedId, -1),
                    Miscel.Coalesce(ucUnit.SelectedId, -1),
                    Miscel.Coalesce(ucDataType.SelectedId, -1),
                    Miscel.Coalesce(ucValueType.SelectedId, -1),
                    Miscel.Coalesce(ucGeneralCategory.SelectedId, -1),
                    Miscel.Coalesce(ucSampleMedium.SelectedId, -1),
                    Miscel.Coalesce(timeSupportTextBox.Text, -1),
                    string.IsNullOrEmpty(nameTextBox.Text) ? string.Empty : nameTextBox.Text,
                    string.IsNullOrEmpty(nameEngTextBox.Text) ? string.Empty : nameEngTextBox.Text,
                    string.IsNullOrEmpty(codeNoDataTextBox.Text) ? -1 : int.Parse(codeNoDataTextBox.Text),
                    string.IsNullOrEmpty(codeErrDataTextBox.Text) ? -1 : int.Parse(codeNoDataTextBox.Text)
                );
            }
            set
            {
                if (value != null)
                {
                    idTextBox.Text = value.Id.ToString();
                    nameTextBox.Text = value.NameRus;
                    ucVariableType.SelectedIndex = value.VariableTypeId;
                    ucUnit.SelectedIndex = value.UnitId;
                    ucTime.SelectedIndex = value.TimeId;
                    timeSupportTextBox.Text = value.TimeSupport == -1 ? null : value.TimeSupport.ToString();
                    ucValueType.SelectedIndex = value.ValueTypeId;
                    ucDataType.SelectedIndex = value.DataTypeId;
                    ucGeneralCategory.SelectedIndex = value.GeneralCategoryId;
                    ucSampleMedium.SelectedIndex = value.SampleMediumId;
                }
            }
        }
        private void UCVariable_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                // FILL DIC COMBOBOXES

                List<VariableType> vnc = Meta.DataManager.GetInstance().VariableTypeRepository.Select().OrderBy(x => x.Name).ToList();
                ucVariableType.Clear();
                ucVariableType.AddRange(vnc.Select(x => (DicItem)x).OrderBy(x => x.Name).ToList());

                List<Unit> uc = Meta.DataManager.GetInstance().UnitRepository.Select().OrderBy(x => x.Name).ToList();
                ucUnit.Clear();
                ucUnit.AddRange(Unit.ToList<DicItem>(uc));

                uc = Meta.DataManager.GetInstance().UnitRepository.Select(null, "Time").OrderBy(x => x.Name).ToList();
                ucTime.Clear();
                ucTime.AddRange(Unit.ToList<DicItem>(uc));

                List<DataType> dtc = Meta.DataManager.GetInstance().DataTypeRepository.Select().OrderBy(pet => pet.Name).ToList();
                ucDataType.Clear();
                ucDataType.AddRange(dtc.Select(x => (DicItem)x).ToList());

                List<GeneralCategory> gcc = Meta.DataManager.GetInstance().GeneralCategoryRepository.Select().OrderBy(pet => pet.Name).ToList();
                ucGeneralCategory.Clear();
                ucGeneralCategory.AddRange(gcc.Select(x => (DicItem)x).OrderBy(x => x.Name).ToList());

                List<SampleMedium> smc = Meta.DataManager.GetInstance().SampleMediumRepository.Select().OrderBy(pet => pet.Name).ToList();
                ucSampleMedium.Clear();
                ucSampleMedium.AddRange(SampleMedium.ToList<DicItem>(smc));

                List<ValueType> vtc = Meta.DataManager.GetInstance().ValueTypeRepository.Select().OrderBy(pet => pet.Name).ToList();
                ucValueType.Clear();
                ucValueType.AddRange(vtc.Select(x => (DicItem)x).OrderBy(x => x.Name).ToList());
            }
        }
        /// <summary>
        /// Новая переменная.
        /// </summary>
        public void New()
        {
            ClearFields();
            idTextBox.Text = "-1";
        }
        /// <summary>
        /// Новая переменная на основе заданной.
        /// </summary>
        public void New(Variable var)
        {
            Variable = var;
            idTextBox.Text = "-1";
        }
        internal void ClearFields()
        {
            idTextBox.Text = null;

            ucVariableType.SelectedIndex = -1;
            ucUnit.SelectedIndex = -1;
            ucTime.SelectedIndex = -1;
            ucDataType.SelectedIndex = -1;
            ucGeneralCategory.SelectedIndex = -1;
            ucSampleMedium.SelectedIndex = -1;
            ucValueType.SelectedIndex = -1;

            timeSupportTextBox.Text = null;
            nameTextBox.Text = null;
        }
    }
}
