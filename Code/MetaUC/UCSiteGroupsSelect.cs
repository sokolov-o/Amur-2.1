using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FERHRI.Amur.Meta
{
    public partial class UCSiteGroupsSelect : UserControl
    {
        public UCSiteGroupsSelect()
        {
            InitializeComponent();
        }
        public delegate void UCSiteGroupCurrentIndexChangedEventHandler(SiteGroup siteGroup);
        public event UCSiteGroupCurrentIndexChangedEventHandler UCSiteGroupCurrentIndexChangedEvent;
        protected virtual void RaiseUCSiteGroupCurrentIndexChangedEvent()
        {
            if (UCSiteGroupCurrentIndexChangedEvent != null)
            {
                UCSiteGroupCurrentIndexChangedEvent(SiteGroup);
            }
        }

        public SiteGroup SiteGroup
        {
            get { return cb.SelectedItem == null ? null : (SiteGroup)cb.SelectedItem; }
            set
            {
                cb.SelectedIndex = (value == null) ? -1 : cb.Items.IndexOf(value);
            }
        }

        List<SiteGroup> _specGroups = new List<SiteGroup>
        {
            new SiteGroup(-1,0,"(_Все пункты)"),
            new SiteGroup(-2,0,"(_Все гидрологические посты)"),
            new SiteGroup(-3,0,"(_Все метеорологические станции)"),
            new SiteGroup(-4,0,"(_Все гео-объекты)")
        };
        private void refreshButton_Click(object sender, EventArgs e)
        {
            cb.Items.Clear();
            List<SiteGroup> groups = Meta.DataManager.GetInstance().SiteGroupRepository.SelectGroups();
            groups.AddRange(_specGroups);
            cb.Items.AddRange(groups.OrderBy(x => x.Name).ToArray());
        }

        private void UCSiteGroups_Load(object sender, EventArgs e)
        {
            refreshButton_Click(null, null);
            SiteGroup = null;
        }
        public bool ShowRefreshButton
        {
            set
            {
                refreshButton.Visible = value;
            }
        }
        public bool ShowLabel
        {
            set
            {
                toolStripLabel1.Visible = value;
            }
        }
    }
}
