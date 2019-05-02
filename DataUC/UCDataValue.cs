using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Amur.Meta;

namespace SOV.Amur.Data
{
    public partial class UCDataValue : UserControl
    {
        private DataValue initDataValue;
        private int userOrganisationId;
        private Dictionary<byte, string> flagNames = new Dictionary<byte, string> {
            {(byte)EnumFlagAQC.NoAQC,  "Отсутствует критконтроль"},
            {(byte)EnumFlagAQC.Success,  "Успешный критконтроль"},
            {(byte)EnumFlagAQC.Error,  "Ошибка"},
            {(byte)EnumFlagAQC.Approved,  "Подтверждено"},
            {254, "Неизвестно"},
            {(byte)EnumFlagAQC.Deleted,  "Удалено"}
        };

        private class ComboBoxItem
        {
            public string name;
            public byte value;
            public ComboBoxItem(string _name, byte _value)
            {
                name = _name;
                value = _value;
            }
            public override string ToString()
            {
                return name;
            }
        }

        public UCDataValue(int _userOrganisationId, DataValue dataValue)
        {
            userOrganisationId = _userOrganisationId;
            initDataValue = dataValue;
            InitializeComponent();

            foreach (KeyValuePair<byte, string> entry in flagNames)
                flagComboBox.Items.Add(new ComboBoxItem(entry.Value, entry.Key));
            Fill(initDataValue);
        }

        private void Fill(DataValue dataValue)
        {
            Meta.DataManager dataManager = Meta.DataManager.GetInstance();
            Catalog catalog = dataManager.CatalogRepository.Select(dataValue.CatalogId);
            Variable var = dataManager.VariableRepository.Select(catalog.VariableId);

            dateLocText.Text = dataValue.DateLOC.ToString();
            dateUtcText.Text = dataValue.DateUTC.ToString();
            varText.Text = var.NameRus;
            valText.Text = dataValue.Value.ToString();
            flagComboBox.Text = !flagNames.ContainsKey(dataValue.FlagAQC) ? flagNames[254] : flagNames[dataValue.FlagAQC];
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                DataValueRepository repdv = Data.DataManager.GetInstance().DataValueRepository;
                double value = double.Parse(valText.Text);
                byte flag = ((ComboBoxItem)flagComboBox.SelectedItem).value;

                initDataValue.Value = (double)value;
                initDataValue.FlagAQC = (byte)EnumFlagAQC.Success;

                Catalog ctl0 = Meta.DataManager.GetInstance().CatalogRepository.Select(initDataValue.CatalogId);
                ctl0.MethodId = (int)EnumMethod.Operator;
                ctl0.SourceId = userOrganisationId;
                List<Catalog> ctl = Meta.DataManager.GetInstance().CatalogRepository.Select(
                    new List<int> { ctl0.SiteId }, new List<int> { ctl0.VariableId },
                    new List<int> { ctl0.MethodId }, new List<int> { ctl0.SourceId },
                    new List<int> { ctl0.OffsetTypeId }, ctl0.OffsetValue);
                if (ctl.Count == 0)
                    ctl.Add(Meta.DataManager.GetInstance().CatalogRepository.Insert(ctl0));
                initDataValue.CatalogId = ctl[0].Id;

                DataValue dvExists = repdv.SelectDataValue(initDataValue.CatalogId, initDataValue.DateUTC, initDataValue.Value);
                if (dvExists != null)
                    repdv.Actualize(dvExists.Id);
                else
                    repdv.Insert(initDataValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                onFieldChange(null, null);
            }
            ParentForm.Close();
        }

        private void onFieldChange(object sender, EventArgs e)
        {
            double val;
            double EPS = 1e-10;
            saveButton.Enabled = flagComboBox.SelectedItem != null && 
                                double.TryParse(valText.Text, out val) &&
                                (Math.Abs(initDataValue.Value - val) > EPS);
        }

        private void approve_Click(object sender, EventArgs e)
        {
            save_Click(null, null);
        }
    }
}
