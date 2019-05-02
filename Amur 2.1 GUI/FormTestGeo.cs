using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapWinGIS;
using AxMapWinGIS;

namespace FERHRI.Amur
{
    public partial class FormTestGeo : Form
    {
        public FormTestGeo()
        {
            InitializeComponent();

            this.map.PreviewKeyDown += delegate(object sender, PreviewKeyDownEventArgs e)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                    case Keys.Right:
                    case Keys.Up:
                    case Keys.Down:
                        e.IsInputKey = true;
                        return;
                }
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            map.Latitude = 48.5F;
            map.Longitude = 135;
            map.CurrentZoom = 12;

            map.DrawPoint(map.Longitude, map.Latitude, 20, (uint)Color.Pink.ToKnownColor());
        }
    }
}
