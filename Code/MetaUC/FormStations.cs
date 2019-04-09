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
    public partial class FormStations : Form
    {
        public FormStations()
        {
            InitializeComponent();
        }

        private void ucStations_UCNewStationEvent()
        {
            ucStationEdit.Station = new Station(-1, null, null, (int)EnumStationType.HydroPost, null, null, null);
        }

        private void ucStations_UCEditStationEvent(int stationId)
        {
            ucStationEdit.Clear();
            ucStationEdit.Fill(ucStations.Site.Id);
            ucStationEdit.Focus();
        }

        private void ucStations_UCSelectedStationChangedEvent(Station station)
        {
            ucStationEdit.Clear();
            if (ucStations.Site != null)
                ucStationEdit.Fill(ucStations.Site.Id);
        }
    }
}
