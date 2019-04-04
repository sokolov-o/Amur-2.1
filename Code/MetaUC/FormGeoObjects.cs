using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Common;

namespace SOV.Amur.Meta
{
    public partial class FormGeoObjects : Form
    {
        public FormGeoObjects()
        {
            InitializeComponent();
        }

        private void FormWObjects_Load(object sender, EventArgs e)
        {
            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ucGeoobTree.ShowToolStrip =
                ucGeoobTree.ShowAddButton =
                ucGeoobTree.ShowCloneButton =
                ucGeoobTree.ShowDeleteButton =
                ucGeoobTree.ShowRefreshButton = true;

                if (DesignMode) return;

                Fill();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = cs;
            }
        }
        void Fill(int selectedGOId = -1)
        {
            List<GeoObject> gos = Meta.DataManager.GetInstance().GeoObjectRepository.Select();
            List<DicItem> godics = GeoObject.ToDicItemTree(gos);
            ucGeoobTree.Clear();
            ucGeoobTree.AddRange(godics);
            GeoObject go = gos.FirstOrDefault(x => x.Id == selectedGOId);
            ucGeoobTree.SelectedDicItem = go == null ? null : go.ToDicItem();

            FillStations(ucGeoobTree.SelectedDicItem);
        }
        bool _stationOrderChanged = false;
        void CheckStationsOrderChange()
        {
            if (_stationOrderChanged)
            {
                if (MessageBox.Show(
                    "Изменён порядок пунктов в пределах объекта [" + _currentWObject.Name + "]."
                    + "\nCОХРАНИТЬ изменения порядка?",
                    "Порядок изменён",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveStationsOrder();
                }
            }
        }
        void SaveStationsOrder()
        {
            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                Meta.DataManager.GetInstance().StationGeoObjectRepository.UpdateStationOrder(_currentWObject.Id,
                    ucStations.GetDataSource().Select(x => ((Station)x).Id).ToList());
                _stationOrderChanged = false;
            }
            finally
            {
                this.Cursor = cs;
            }
        }
        void CheckWObjectsOrderChange()
        {
            if (_geoobOrderChanged)
            {
                if (MessageBox.Show(
                    "Изменён порядок водных объектов."
                    + "\nCОХРАНИТЬ изменения порядка?",
                    "Порядок изменён",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveWObjectsOrder();
                }
            }
        }
        void SaveWObjectsOrder()
        {
            throw new NotImplementedException();
            //Cursor cs = this.Cursor;
            //this.Cursor = Cursors.WaitCursor;
            //try
            //{
            //    Meta.DataManager.GetInstance().GeoObjectRepository.UpdateGeoObjectOrder(ucGeoobList.DicItemList.Select(x => x.Id).ToList());
            //    _geooOrderChanged = false;
            //}
            //finally
            //{
            //    this.Cursor = cs;
            //}
        }
        Common.DicItem _currentWObject;
        void FillStations(Common.DicItem go)
        {
            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                CheckStationsOrderChange();
                _currentWObject = go;

                if (go != null)
                {
                    Dictionary<GeoObject, List<Station>> gos = Meta.DataManager.GetInstance().StationGeoObjectRepository.SelectByGeoObjectsFK(new List<int>(new int[] { go.Id }));
                    List<Station> stations = gos.Count == 0 ? new List<Station>() : gos.ElementAt(0).Value;

                    ucStations.Clear();
                    ucStations.SetDataSource(stations.ToArray<object>().ToList(), "Name");
                }
            }
            finally
            {
                this.Cursor = cs;
            }
        }

        private void ucWObjects_SelectedDicItemChangedEvent(Common.DicItem dic)
        {
            FillStations(dic);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckStationsOrderChange();
            CheckWObjectsOrderChange();

            Close();
        }

        private void ucGeoobTree_UCRefresh()
        {
            Fill(ucGeoobTree.SelectedDicItem == null ? -1 : ucGeoobTree.SelectedDicItem.Id);
        }
        FormGeoObject formGeoObject = new FormGeoObject();
        /// <summary>
        /// Создать новый экземпляр с родителем, равным выбранному элементу.
        /// </summary>
        /// <param name="diciCurrent"></param>
        private void ucGeoobTree_UCAddNewItem(DicItem diciCurrent)
        {
            GeoObject go = (GeoObject)diciCurrent.Entity;
            formGeoObject.GeoObject = new GeoObject(-1, go.GeoTypeId, "##" + go.Name, go.Id);

            formGeoObject.StartPosition = FormStartPosition.CenterParent;
            if (formGeoObject.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Fill(formGeoObject.GeoObject.Id);
            }
        }
        /// <summary>
        /// Клонировать экземпляр.
        /// </summary>
        /// <param name="diciCurrent"></param>
        private void ucGeoobTree_UCCloneItem(DicItem diciCurrent)
        {
            GeoObject go = (GeoObject)diciCurrent.Entity;
            formGeoObject.GeoObject = new GeoObject(-1, go.GeoTypeId, "##" + go.Name, go.FallIntoId);

            formGeoObject.StartPosition = FormStartPosition.CenterParent;
            if (formGeoObject.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Fill(formGeoObject.GeoObject.Id);
            }
        }

        private void ucGeoobTree_UCDeleteItem(DicItem dici)
        {
            try
            {
                Meta.DataManager.GetInstance().GeoObjectRepository.Delete(dici.Id);

                ucGeoobTree.Remove(dici);

                if (dici.ParentDicItem != null)
                    ucGeoobTree.SelectedDicItem = dici.ParentDicItem;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        bool _geoobOrderChanged = false;
        private void ucGeoobTree_UCNodesOrderChangedEvent()
        {
            _geoobOrderChanged = true;
        }

        private void ucGeoobTree_UCSelectedItemChanged(DicItem dici)
        {
            FillStations(ucGeoobTree.SelectedDicItem);
        }

        private void cmsEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit(((GeoObject)ucGeoobTree.SelectedDicItem.Entity));
        }

        private void ucGeoobTree_UCEditNewItem(DicItem dici)
        {
            Edit((GeoObject)dici.Entity);
        }
        private void Edit(GeoObject go)
        {
            formGeoObject.GeoObject = go;
            if (formGeoObject.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Fill(ucGeoobTree.SelectedDicItem.Id);
            }
        }

        private void ucStations_UCAddNewEvent()
        {
            List<StationType> sts = SiteTypeRepository.GetCash();
            FormSelectListItems frm = new FormSelectListItems("Добавить пункт к гео-объекту",
                StationRepository.GetCash()
                .Select(x => new IdName() { Id = x.Id, Name = x.Name + " " + x.Code + " " + sts.Find(y => y.Id == x.TypeId).NameShort })
                .ToArray(),
                "Name"
            );
            frm.StartPosition = FormStartPosition.CenterParent;

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (frm.SelectedItemsId.Count > 0)
                {
                    foreach (var id in frm.SelectedItemsId)
                    {
                        Meta.DataManager.GetInstance().StationGeoObjectRepository.Insert(_currentWObject.Id, id);
                    }
                    FillStations(_currentWObject);
                    ucStations.SetSelectedItemById(frm.SelectedItemsId[0]);
                }
            }
        }

        private void ucStations_UCDeleteEvent(int id)
        {
            if (ucStations.CurrentId != null)
            {
                DataManager.GetInstance().StationGeoObjectRepository.Delete(_currentWObject.Id, (int)ucStations.CurrentId);
                FillStations(_currentWObject);
            }
        }

        private void ucStations_UCSaveEvent()
        {
            SaveStationsOrder();
        }

        private void ucStations_UCItemOrderChangedEvent()
        {
            _stationOrderChanged = true;
        }
    }
}
