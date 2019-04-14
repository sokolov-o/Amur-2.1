using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Common;

namespace SOV.Amur.Meta
{
    public partial class UCGeoObject : UserControl
    {
        public UCGeoObject()
        {
            InitializeComponent();

            refreshGeoobList_Click(null, null);
            refreshTypeList_Click(null, null);
        }
        int _id = -1;
        public GeoObject GeoObject
        {
            get
            {
                try
                {
                    return new GeoObject(
                        _id,
                        (int)geoTypeDicComboBox.SelectedId,
                        nameTextBox.Text,
                        fallIntoDicComboBox.SelectedId,
                        string.IsNullOrEmpty(orderTextBox.Text) ? int.MinValue : int.Parse(orderTextBox.Text)
                    );
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                if (value == null)
                {
                    value = new GeoObject(-1, (int)EnumGeoObject.River, null);
                }

                // Объект
                _id = value.Id;
                nameTextBox.Text = value.Name;
                geoTypeDicComboBox.SelectedId = value.GeoTypeId;

                fallIntoDicComboBox.SelectedId = value.FallIntoId;
                orderTextBox.Text = value.OrderBy.ToString();

                // Впадают/принадлежат геообъекту
                childsDicListBox.Clear();
                childsDicListBox.Enabled = false;
                groupBox1.Text = "";

                if (_id > 0)
                {
                    List<GeoObject> gos = Meta.DataManager.GetInstance().GeoObjectRepository.SelectInfluentTo(value.Id);
                    childsDicListBox.SetDataSource(gos.ToList<object>(), "Name");
                    childsDicListBox.Enabled = true;
                }
            }
        }

        private void refreshTypeList_Click(object sender, EventArgs e)
        {
            int? id = geoTypeDicComboBox.SelectedId;
            geoTypeDicComboBox.Clear();

            geoTypeDicComboBox.AddRange(GeoType.ToList<DicItem>(GeoTypeRepository.GetCash()));

            geoTypeDicComboBox.SelectedId = id;
        }

        private void refreshGeoobList_Click(object sender, EventArgs e)
        {
            int? id = fallIntoDicComboBox.SelectedId;
            fallIntoDicComboBox.Clear();

            List<GeoObject> gos = Meta.DataManager.GetInstance().GeoObjectRepository.Select().OrderBy(x => x.Name).ToList();
            fallIntoDicComboBox.AddRange(GeoObject.ToDicItemList(gos));

            fallIntoDicComboBox.SelectedId = id;
        }
    }
}
