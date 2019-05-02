using System.Collections.Generic;
using System.Windows.Forms;

namespace SOV.Common.TableIUD
{
    public partial class UCTextTableField : UCTableField
    {
        public UCTextTableField(TableField field, object currValue = null) : base(field)
        {
            InitializeComponent();
            this.ucInput = textBox;
            label.Text = field.title;
            textBox.Text = currValue != null ? currValue.ToString() : null;
        }

        private void textBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (onChange != null) onChange(this);
        }

        public override KeyValuePair<string, object> NameAndVal()
        {
            return new KeyValuePair<string, object>(Field.db, textBox.Text == "" ? null : textBox.Text);
        }
    }
}
