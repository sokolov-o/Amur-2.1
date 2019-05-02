using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Amur.Meta;

namespace SOV.Amur.Data.Chart
{
    public partial class FormSiteAndVarSelection : Form
    {
        public int? SiteId {
            get {
                var sites = ucStations.SelectedSites();
                if (sites == null || sites.Count == 0)
                    return null;
                return sites[0].Id;
            }
            set {
                if (value.HasValue)
                    ucStations.SetSelectedSites(new List<int>() { value.Value });
            }
        }
        public int? VarId
        {
            get {
                return ucVariablesList.SelectedVariable.Id;
            }
            set
            {
                if (value.HasValue)
                    ucVariablesList.SelectedId = value.Value;
            }
        }

        public FormSiteAndVarSelection()
        {
            InitializeComponent();

            ucStations.SiteGroupId = 0;
            ucVariablesList.Fill(Meta.DataManager.GetInstance().VariableRepository.Select());
            VarId = (int)EnumVariable.GageHeightF;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!SiteId.HasValue || !VarId.HasValue)
            {
                string message = "Не выбраны станция и/или переменная";
                string caption = "Ошибка ввода";
                MessageBox.Show(message, caption, MessageBoxButtons.OK);
                return;
            }
            OnComplateSelectionEvent(this);
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public delegate void OnComplateSelectionEventHandler(FormSiteAndVarSelection sender);
        public event OnComplateSelectionEventHandler OnComplateSelectionEvent = null;

        private void ucStations_Load(object sender, EventArgs e)
        {
            Console.WriteLine(SiteId);
        }
    }
}
