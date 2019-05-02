using System;
using System.Collections.Generic;

namespace SOV.Common.TableIUD
{
    public partial class UCComboBoxTableField : UCTableField
    {
        public UCComboBoxTableField(ComboBoxTableField field, object val = null) : base(field)
        {
            InitializeComponent();
            label.Text = field.title;
            this.ucInput = comboBox;
            foreach (var item in field.items)
            {
                comboBox.Items.Add(item);
                if (item.Entity.Equals(val)) comboBox.SelectedItem = item;
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (onChange != null) onChange(this);
        }

        public bool Selected()
        {
            return comboBox.SelectedIndex >= 0;
        }

        public int Id
        {
            get { return comboBox.SelectedItem == null ? 0 : ((DicItem)comboBox.SelectedItem).Id; }
        }

        public object Value
        {
            get { return comboBox.SelectedItem == null ? null : ((DicItem)comboBox.SelectedItem).Entity; }
            set
            {
                foreach (var item in comboBox.Items)
                    if (((DicItem)item).Entity.Equals(value)) comboBox.SelectedItem = item;
            }
        }

        public void SetIndex(int i)
        {
            comboBox.SelectedIndex = i;
        }

        public override KeyValuePair<string, object> NameAndVal()
        {
            return new KeyValuePair<string, object>(Field.db, Value);
        }
    }
}
