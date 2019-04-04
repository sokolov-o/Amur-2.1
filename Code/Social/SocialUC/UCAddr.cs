using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Social
{
    public partial class UCAddr : UserControl
    {
        public UCAddr()
        {
            InitializeComponent();

            List<AddrType> addrTypes = Social.AddrTypeRepository.GetCash();
            List<Addr> addrs = Social.AddrRepository.GetCash();

            typeComboBox.DataSource = addrTypes.OrderBy(x => x.Name).ToList();
            parentComboBox.DataSource = addrs.OrderBy(x => x.Name).ToList();

            typeComboBox.SelectedIndex = parentComboBox.SelectedIndex = -1;
        }

        private void UCAddr_Load(object sender, EventArgs e)
        {
        }

        public Addr Addr
        {
            get
            {
                return new Addr(
                    string.IsNullOrEmpty(idTextBox.Text) ? -1 : int.Parse(idTextBox.Text),
                    typeComboBox.SelectedIndex == -1 ? -1 : ((AddrType)typeComboBox.SelectedItem).Id,
                    nameTxtBox.Text,
                    nameShortTextBox.Text,
                    parentComboBox.SelectedIndex == -1 ? null : (int?)((Addr)parentComboBox.SelectedItem).Id
                    );
            }
            set
            {
                idTextBox.Text = string.Empty;
                typeComboBox.SelectedIndex = -1;
                nameTxtBox.Text = string.Empty;
                nameShortTextBox.Text = string.Empty;
                parentComboBox.SelectedIndex = -1;

                if (value != null)
                {
                    idTextBox.Text = value.Id.ToString();
                    typeComboBox.SelectedItem = ((List<AddrType>)typeComboBox.DataSource).Find(x => x.Id == value.TypeId);
                    nameTxtBox.Text = value.Name;
                    nameShortTextBox.Text = value.NameShort;
                    parentComboBox.SelectedItem = ((List<Addr>)parentComboBox.DataSource).Find(x => x.Id == value.ParentId);
                }
            }
        }
    }
}
