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
    public partial class UCSiteGroupsSelect : UserControl
    {
        public UCSiteGroupsSelect()
        {
            InitializeComponent();
        }
        public delegate void UCSiteGroupCurrentIndexChangedEventHandler(EntityGroup siteGroup);
        public event UCSiteGroupCurrentIndexChangedEventHandler UCSiteGroupCurrentIndexChangedEvent;
        protected virtual void RaiseUCSiteGroupCurrentIndexChangedEvent()
        {
            if (UCSiteGroupCurrentIndexChangedEvent != null)
            {
                UCSiteGroupCurrentIndexChangedEvent(SiteGroup);
            }
        }

        public EntityGroup SiteGroup
        {
            get { return cb.SelectedItem == null ? null : (EntityGroup)cb.SelectedItem; }
            set
            {
                cb.SelectedIndex = (value == null) ? -1 : cb.Items.IndexOf(value);
            }
        }

        List<EntityGroup> _specGroups = new List<EntityGroup>
        {
            new EntityGroup(-1,"(_Все пункты)","site"),
            new EntityGroup(-2,"(_Все гидрологические посты)","site"),
            new EntityGroup(-3,"(_Все метеорологические станции)","site"),
            new EntityGroup(-4,"(_Все гео-объекты)","site")
        };
        private void refreshButton_Click(object sender, EventArgs e)
        {
            cb.Items.Clear();
            List<EntityGroup> groups = Meta.DataManager.GetInstance().EntityGroupRepository.SelectByEntityTableName("site");
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
