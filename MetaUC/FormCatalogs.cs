using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Amur.Sys;
using SOV.Common;
using SOV.Amur.Meta;

namespace SOV.Amur.Meta
{
    /// <summary>
    /// При нажатии кнопок формы Accept || Cansel производится Hide. Не Close.
    /// </summary>
    public partial class FormCatalogs : Form
    {
        public FormCatalogs(Sys.EntityInstanceValue catalogFilterSAV, System.EventHandler externalShowDataEventHandler)
        {

            InitializeComponent();

            ucCatalogs.ShowDataValueEventHandler = externalShowDataEventHandler;
            if (catalogFilterSAV != null)
                ucCatalogs.CatalogFilter = Meta.CatalogFilter.Parse(catalogFilterSAV.Value);
        }

        public bool AcceptButtonVisible
        {
            get
            {
                return okButton.Visible;
            }
            set
            {
                okButton.Visible = value;
            }
        }
        public string AcceptButtonText
        {
            set
            {
                okButton.Text = value;
            }
        }
        public bool CancelButtonVisible
        {
            get
            {
                return cancelButton.Visible;
            }
            set
            {
                cancelButton.Visible = value;
            }
        }
        public bool DeleteCatalogButtonVisible
        {
            get
            {
                return ucCatalogs.UCDeleteCatalogButtonVisible;
            }
            set
            {
                ucCatalogs.UCDeleteCatalogButtonVisible = value;
            }
        }
        public bool AddCatalogButtonVisible
        {
            get
            {
                return ucCatalogs.UCAddCatalogButtonVisible;
            }
            set
            {
                ucCatalogs.UCAddCatalogButtonVisible = value;
            }
        }
        public string CancelButtonText
        {
            set
            {
                cancelButton.Text = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Hide();
        }
        public List<Catalog> GetCatalogs()
        {
            return ucCatalogs.GetCatalogs();
        }
        public List<Catalog> GetSelectedCatalogs()
        {
            return ucCatalogs.GetSelectedCatalogs();
        }

        private void ucCatalogs_UCDeleteButtonClick(List<int> catalogsId)
        {
            if ((object)ucCatalogs.CurCatalog != null)
                Meta.DataManager.GetInstance().CatalogRepository.Delete(catalogsId);
        }
        public void SetModeSelect()
        {
            CancelButtonVisible = true;
            AcceptButtonVisible = true;
            CancelButtonText = "Отменить";
            AcceptButtonText = "Выбрать";

            ucCatalogs.SetModeSelect();
        }
        public void SetModeEdit()
        {
            CancelButtonVisible = false;
            AcceptButtonVisible = true;
            AcceptButtonText = "Закрыть";

            ucCatalogs.SetModeEdit();
        }

        public delegate void UCSaveFilterUserSettingsEventHandler(CatalogFilter cf);
        public event UCSaveFilterUserSettingsEventHandler UCSaveFilterUserSettingsEvent;
        protected virtual void RaiseSaveFilterUserSettingsEvent(CatalogFilter cf)
        {
            if (UCSaveFilterUserSettingsEvent != null)
            {
                UCSaveFilterUserSettingsEvent(cf);
            }
        }

        //private void ucCatalogs_UCShowData4Catalog(Catalog catalog)
        //{
        //    ExternalShowDataEventHandler?.Invoke(this, new CatalogEventArgs() { Catalog = catalog });
        //}
    }
}
