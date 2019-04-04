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
    public partial class UCMethods : UserControl
    {
        public enum MethodSet { AllMethods = 0, ForecastMethodsOnly = 1, ClimateMethodsOnly = 2 }
        string[] _methodSets = new string[]
        {
            "(Все методы)",
            "Прогноз",
            "Климатические расчёты"
        };
        bool isInitializingNow = true;

        public UCMethods()
        {
            InitializeComponent();

            viewTypeComboBox.Items.AddRange(_methodSets.ToArray());
            viewTypeComboBox.SelectedIndex = -1;
            isInitializingNow = false;
        }
        public void SetMethodSet(MethodSet methodSet)
        {
            viewTypeComboBox.SelectedIndex = (int)methodSet;
        }

        private void viewTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializingNow) return;

            dgvMethods.DataSource = null;
            List<Meta.Method> methods = null;

            switch (viewTypeComboBox.SelectedIndex)
            {
                // Все методы
                case 0:
                    methods = Meta.DataManager.GetInstance().MethodRepository.Select();
                    break;
                // Прогноз
                case 1:
                    List<MethodForecast> mfs = Meta.DataManager.GetInstance().MethodForecastRepository.Select();
                    methods = Meta.DataManager.GetInstance().MethodRepository.Select(mfs.Select(x => x.Method.Id).ToList());
                    break;
                // Климат
                case 2:
                    List<MethodClimate> mcs = Meta.DataManager.GetInstance().MethodClimateRepository.Select();
                    methods = Meta.DataManager.GetInstance().MethodRepository.Select(mcs.Select(x => x.MethodId).ToList());
                    break;
                default:
                    //MessageBox.Show("Неизвестный набор методов с индексом " + viewTypeComboBox.SelectedIndex);
                    return;
            }
            dgvMethods.DataSource = methods.OrderBy(x => x.Name).ToList();
        }
        public Method CurrentMethod
        {
            get
            {
                if (dgvMethods.CurrentRow != null && dgvMethods.CurrentRow.DataBoundItem != null)
                {
                    return (Method)dgvMethods.CurrentRow.DataBoundItem;
                }
                return null;
            }
        }
        int PrevMethodId = int.MinValue;
        int CurrentMethodId
        {
            get
            {
                return (CurrentMethod == null) ? int.MinValue : CurrentMethod.Id;
            }
        }
        private void dgvMethods_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (isInitializingNow) return;

            Cursor cs = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (CurrentMethodId != PrevMethodId)
                {
                    ucMethod.Clear();
                    splitContainer2.Panel2.Controls.Clear();

                    if (CurrentMethodId != -1)
                    {
                        Method method = CurrentMethod;
                        if (!splitContainer1.Panel2Collapsed)
                        {
                            ucMethod.Method = method;

                            List<MethodForecast> mf = DataManager.GetInstance().MethodForecastRepository.Select(new List<int>() { method.Id });
                            if (mf != null && mf.Count == 1)
                            {
                                return;
                            }
                            MethodClimate mc = Meta.DataManager.GetInstance().MethodClimateRepository.Select(method.Id);
                            if (mc != null)
                            {
                                UCMethodClimate uc = new UCMethodClimate();
                                splitContainer2.Panel2.Controls.Add(uc);
                                uc.Dock = DockStyle.Fill;
                                return;
                            }
                        }
                    }
                    frmFilter = null;
                    PrevMethodId = CurrentMethodId;
                    RaiseCurrentMethodChangedEvent();
                }
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
        public bool UCToolbarVisible
        {
            set
            {
                toolStrip.Visible = value;
            }
            get
            {
                return toolStrip.Visible;
            }
        }
        public bool UCMethodDetailVisible
        {
            set
            {
                splitContainer1.Panel2Collapsed = !value;
            }
        }

        private void showHideMethodDetailsToolStripButton_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
        }

        public delegate void UCCurrentMethodChangedEventHandler(Method method);
        public event UCCurrentMethodChangedEventHandler UCCurrentMethodChangedEvent;
        protected virtual void RaiseCurrentMethodChangedEvent()
        {
            if (UCCurrentMethodChangedEvent != null)
            {
                UCCurrentMethodChangedEvent(CurrentMethod);
            }
        }
        public delegate void UCDataFilterChangedEventHandler(CatalogFilter catalogFilter);
        public event UCDataFilterChangedEventHandler UCDataFilterChangedEvent;
        protected virtual void RaiseDataFilterChangedEvent()
        {
            if (UCDataFilterChangedEvent != null)
            {
                UCDataFilterChangedEvent(frmFilter == null ? null : frmFilter.CatalogFilter);
            }
        }


        FormCatalogFilter frmFilter = null;
        Method frmFilterMethod = null;
        private void dataFilterToolStripButton_Click(object sender, EventArgs e)
        {
            if (CurrentMethod == null)
            {
                if (frmFilter != null && !frmFilter.IsDisposed)
                    frmFilter.Dispose();
                frmFilter = null;

                MessageBox.Show("Не выбран метод.");
            }
            else if (frmFilter == null || frmFilter.IsDisposed || frmFilterMethod.Id != CurrentMethod.Id)
            {
                if (frmFilter != null && !frmFilter.IsDisposed)
                    frmFilter.Dispose();

                frmFilter = new FormCatalogFilter(null, "Фильтр для климатических данных");
                frmFilter.StartPosition = FormStartPosition.CenterParent;
                frmFilter.SelectAllButtonsVisible = true;
                frmFilter.ShowItemsTypeOfSetGroup = false;
                frmFilter.SetItemsTypeOfSet( UCCatalogFilter0.EnumItemTypeOfSet.Handset);
                frmFilterMethod = CurrentMethod;

                DataManager dmm = DataManager.GetInstance();
                List<Catalog> catalogs = dmm.CatalogRepository.Select(null, null, new List<int>() { CurrentMethod.Id }, null, null, (double?)null);
                frmFilter.SitesOnly = dmm.SiteRepository.Select(catalogs.Select(x => x.SiteId).Distinct().ToList());
                frmFilter.VariablesOnly = dmm.VariableRepository.Select(catalogs.Select(x => x.VariableId).Distinct().ToList());
                frmFilter.MethodsOnly = dmm.MethodRepository.Select(catalogs.Select(x => x.MethodId).Distinct().ToList());
                frmFilter.SourcesOnly = Social.DataManager.GetInstance().LegalEntityRepository.Select(catalogs.Select(x => x.SourceId).Distinct().ToList());
                frmFilter.OffsetTypesOnly = dmm.OffsetTypeRepository.Select(catalogs.Select(x => x.OffsetTypeId).Distinct().ToList());
            }
            frmFilter.ShowDialog();
            RaiseDataFilterChangedEvent();
        }
        public CatalogFilter CatalogFilter
        {
            get
            {
                return frmFilter == null ? null : frmFilter.CatalogFilter;
            }
        }
    }
}
