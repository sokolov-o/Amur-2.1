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
    public partial class FormEntityAttrValue : Form
    {
        List<EntityAttrType> EntityAttrs { get; set; }
        public FormEntityAttrValue(string entityName, List<EntityAttrType> ea = null)
        {
            InitializeComponent();

            EntityAttrs = ea;
            if (ea == null)
                EntityAttrs = Meta.DataManager.GetInstance().EntityAttrRepository.SelectAttrTypes(entityName);

            eaComboBox.Items.AddRange(EntityAttrs.ToArray());

            this.StartPosition = FormStartPosition.CenterParent;
        }
        public DialogResult ShowNewAttr(int entityId)
        {
            Text = "Новый атрибут";
            EntityId = entityId;
            valueTextBox.Text = string.Empty;
            dateTextBox.Text = "01.01.1900";

            return ShowDialog();
        }
        public EntityAttrValue EntityAttrValue
        {
            get
            {
                DateTime date;
                if (!DateTime.TryParse(dateTextBox.Text, out date))
                {
                    MessageBox.Show("Ошибка в дате.");
                    return null;
                }

                return new EntityAttrValue(EntityId, ((EntityAttrType)eaComboBox.SelectedItem).Id, date, valueTextBox.Text);
            }
        }

        private int EntityId { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
