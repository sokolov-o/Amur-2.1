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
    public partial class UCEntityAttrValue : UserControl
    {
        public UCEntityAttrValue()
        {
            InitializeComponent();

            DateTimeFormat = "dd.MM.yyyy HH:mm";
            EntityAttrTypes = new List<EntityAttrType>();
        }
        List<EntityAttrType> _EntityAttrTypes;
        public List<EntityAttrType> EntityAttrTypes
        {
            get
            {
                return _EntityAttrTypes;
            }
            set
            {
                eatComboBox.Items.Clear();
                if (value != null && value.Count > 0)
                {
                    eatComboBox.Items.AddRange(value.ToArray());
                }
                _EntityAttrTypes = value;
            }
        }

        public string DateTimeFormat { get { return labelDateFormat.Text; } set { labelDateFormat.Text = value; } }
        public EntityAttrValue EntityAttrValue
        {
            get
            {
                return new EntityAttrValue(
                     int.Parse(eIdTextBox.Text),
                     ((EntityAttrType)eatComboBox.SelectedItem).Id,
                     dateS.Value,
                     valueTextBox.Text
                    );
            }
            set
            {
                eatComboBox.SelectedIndex = -1;
                dateS.Value = new DateTime(1900, 1, 1);
                eIdTextBox.Text =
                valueTextBox.Text = null;

                if (value != null)
                {
                    EntityAttrType eat = EntityAttrTypes.Find(x => x.Id == value.AttrTypeId);
                    if (eat == null)
                        throw new Exception("Не найден тип атрибута с кодом " + value.AttrTypeId);

                    eIdTextBox.Text = value.EntityId.ToString();
                    eatComboBox.SelectedIndex = eatComboBox.Items.IndexOf(eat);
                    dateS.Value = value.DateS;
                    valueTextBox.Text = value.Value;
                }
            }
        }

        private void valueTextBox_TextChanged(object sender, EventArgs e)
        {
            groupBox1.ForeColor = string.IsNullOrEmpty(valueTextBox.Text) ? Color.Red : Color.Black;

        }
        public bool UCEnableEntityAttrTypeComboBox
        {
            set
            {
                eatComboBox.Enabled = value;
            }
        }
    }
}
