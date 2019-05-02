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
using SOV.Amur.Meta;
using SOV.Social;

namespace SOV.Amur.Meta
{
    /// <summary>
    /// Установка фильтра для каталога данных по его ключам.
    /// </summary>
    public partial class UCCatalogFilter : UserControl
    {
        TabPage _tpSite, _tpVariable, _tpOffset, _tpMethod, _tpSource;

        public UCCatalogFilter()
        {
            InitializeComponent();

            //Option = new Options()
            //{
            //    ShowAGSSites = new bool[] { true, true, true },
            //    ShowAGSVariables = new bool[] { true, true, true },
            //    ShowAGSMethods = new bool[] { true, true, true },
            //    ShowAGSSources = new bool[] { true, true, true },
            //    ShowAGSOffsetTypes = new bool[] { true, true, true }
            //};

            _tpSite = tc.TabPages["_tpSite"];
            _tpVariable = tc.TabPages["_tpVariable"];
            _tpOffset = tc.TabPages["_tpOffset"];
            _tpMethod = tc.TabPages["_tpMethod"];
            _tpSource = tc.TabPages["_tpSource"];

            sitesFilter.CaptionDictionary = "Список пунктов";
            varsFilter.CaptionDictionary = "Список переменных";
            methodsFilter.CaptionDictionary = "Список методов";
            sourcesFilter.CaptionDictionary = "Список источников";
            offsetsFilter.CaptionDictionary = "Список смещений";
        }


        public CatalogFilter CatalogFilter
        {
            get
            {
                return new CatalogFilter(
                    sitesFilter.GetFilterIds(),
                    varsFilter.GetFilterIds(),
                    methodsFilter.GetFilterIds(),
                    sourcesFilter.GetFilterIds(),
                    offsetsFilter.GetFilterIds(),
                    (string.IsNullOrEmpty(offsetValueTextBox.Text) ? null : (double?)double.Parse(offsetValueTextBox.Text))
                );
            }
            set
            {
                sitesFilter.SetFilterEmpty();
                varsFilter.SetFilterEmpty();
                methodsFilter.SetFilterEmpty();
                sourcesFilter.SetFilterEmpty();
                offsetsFilter.SetFilterEmpty();
                offsetValueTextBox.Text = string.Empty;

                if ((object)value != null)
                {
                    sitesFilter.SetFilter(value.Sites);
                    varsFilter.SetFilter(value.Variables);
                    methodsFilter.SetFilter(value.Methods);
                    sourcesFilter.SetFilter(value.Sources);
                    offsetsFilter.SetFilter(value.OffsetTypes);

                    offsetValueTextBox.Text = (value.OffsetValue == null) ? string.Empty : value.OffsetValue.ToString();
                }
                SetTabPageCaptions();
            }
        }
        void SetTabPageCaptions()
        {
            _tpSite.Text = "Пункты (" + sitesFilter.GetFilterCountText() + ")";
            _tpVariable.Text = "Переменные (" + varsFilter.GetFilterCountText() + ")";
            _tpMethod.Text = "Методы (" + methodsFilter.GetFilterCountText() + ")";
            _tpSource.Text = "Источники (" + sourcesFilter.GetFilterCountText() + ")";
            _tpOffset.Text = "Смещения (" + offsetsFilter.GetFilterCountText() + ")";
        }
        public static List<Common.DicItem> ToList(List<LegalEntity> items)
        {
            List<DicItem> ret = new List<DicItem>();
            if (items != null)
                foreach (var item in items)
                {
                    ret.Add(new DicItem(item.Id, item.NameRus + " / " + item.Id));
                }
            return ret;
        }
        internal void SetItemsTypeOfSet(UCCatalogFilter0.EnumItemTypeOfSet enumItemTypeOfSet)
        {
            SetItemsTypeOfSet(this, enumItemTypeOfSet);
        }
        void SetItemsTypeOfSet(Control control, UCCatalogFilter0.EnumItemTypeOfSet enumItemTypeOfSet)
        {
            if (control.GetType() == typeof(UCCatalogFilter0))
            {
                ((UCCatalogFilter0)control).SetItemsTypeOfSet(enumItemTypeOfSet);
                return;
            }
            foreach (Control item in control.Controls)
            {
                SetItemsTypeOfSet(item, enumItemTypeOfSet);
            }
        }

        /// <summary>
        /// Fill dictionaries from list.
        /// </summary>
        public void Fill(List<Site> sites, List<Variable> vars, List<Method> methods, List<LegalEntity> sources, List<OffsetType> offsetTypes)
        {
            if (DesignMode) return;

            Meta.DataManager dm = Meta.DataManager.GetInstance();
            if (dm == null) return;
            dm.TestConnection();

            // ENTITY ITEMS
            sitesFilter.SetDicItems(sites.Select(x => new IdName() { Id = x.Id, Name = x.GetName(2, SiteTypeRepository.GetCash()) }).ToList());
            varsFilter.SetDicItems(Variable.ToListIdName(vars, EnumLanguage.Rus));
            methodsFilter.SetDicItems(methods.ToList<IdName>());
            sourcesFilter.SetDicItems(sources.Select(x => new IdName() { Id = x.Id, Name = x.NameRus }).ToList());
            offsetsFilter.SetDicItems(offsetTypes.ToList<IdName>());

            // ENTITY GROUPS
            if (sitesFilter.GroupsCount == 0)
            {
                sitesFilter.SetGroups(Meta.DataManager.GetInstance().EntityGroupRepository.SelectGroupsFK("site"));
                varsFilter.SetGroups(Meta.DataManager.GetInstance().EntityGroupRepository.SelectGroupsFK("variable"));
                methodsFilter.SetGroups(Meta.DataManager.GetInstance().EntityGroupRepository.SelectGroupsFK("method"));
                sourcesFilter.SetGroups(Meta.DataManager.GetInstance().EntityGroupRepository.SelectGroupsFK("source"));
                offsetsFilter.SetGroups(Meta.DataManager.GetInstance().EntityGroupRepository.SelectGroupsFK("offset_type"));
            }
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            RaiseUCFilterButtonClickEvent();
        }

        public delegate void UCFilterButtonClickEventHandler();
        public event UCFilterButtonClickEventHandler UCFilterButtonClickEvent;
        protected virtual void RaiseUCFilterButtonClickEvent()
        {
            if (UCFilterButtonClickEvent != null)
            {
                UCFilterButtonClickEvent();
            }
        }

        private void saveFilterButton_Click(object sender, EventArgs e)
        {
            if (CatalogFilter != null)
                RaiseUCSaveFilterEvent();
        }
        public delegate void UCSaveFilterEventHandler(CatalogFilter catalogFilter);
        public event UCSaveFilterEventHandler UCSaveFilterEvent;
        protected virtual void RaiseUCSaveFilterEvent()
        {
            if (UCSaveFilterEvent != null)
            {
                UCSaveFilterEvent(CatalogFilter);
            }
        }

        public bool ShowToolStrip
        {
            get
            {
                return tableLayoutPanel1.RowStyles[0].SizeType == SizeType.AutoSize;
            }
            set
            {
                if (value)
                {
                    tableLayoutPanel1.RowStyles[0].SizeType = SizeType.AutoSize;
                }
                else
                {
                    tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Absolute;
                    tableLayoutPanel1.RowStyles[0].Height = 0;
                }
            }
        }

        public void ShowTabs(bool tabSite, bool tabVariable, bool tabOffset, bool tabMethod, bool tabSource)
        {
            tc.TabPages.Clear();

            if (_tpSite != null)
            {
                if (tabSite) tc.TabPages.Add(_tpSite);
                if (tabVariable) tc.TabPages.Add(_tpVariable);
                if (tabOffset) tc.TabPages.Add(_tpOffset);
                if (tabMethod) tc.TabPages.Add(_tpMethod);
                if (tabSource) tc.TabPages.Add(_tpSource);
            }
        }

        private void sitesFilter_UCDicItemCheckedEvent(List<int> dicIds)
        {
            SetTabPageCaptions();
        }

        private void sitesFilter_UCGroupChangedEvent(EntityGroup entityGroup, List<int[]> entityIds)
        {
            SetTabPageCaptions();
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {

        }
        public bool ShowItemTypeOfSetGroup
        {
            set
            {
                ShowItemTypeOfSetGroup_(this, value);
            }
        }
        void ShowItemTypeOfSetGroup_(Control control, bool value)
        {
            if (control.GetType() == typeof(UCCatalogFilter0))
            {
                ((UCCatalogFilter0)control).ShowItemTypeOfSetGroup = value;
                return;
            }
            foreach (Control item in control.Controls)
            {
                ShowItemTypeOfSetGroup_(item, value);
            }
        }

        //Options _option;
        //public Options Option
        //{
        //    get
        //    {
        //        return _option;
        //    }
        //    set
        //    {
        //        _option = value;
        //        sitesFilter.ShowAGS(_option.ShowAGSSites);
        //        varsFilter.ShowAGS(_option.ShowAGSVariables);
        //        methodsFilter.ShowAGS(_option.ShowAGSMethods);
        //        sourcesFilter.ShowAGS(_option.ShowAGSSources);
        //        offsetsFilter.ShowAGS(_option.ShowAGSOffsetTypes);
        //    }
        //}

        public bool SelectAllButtonsVisible
        {
            set
            {
                sitesFilter.SelectAllButtonsVisible =
                varsFilter.SelectAllButtonsVisible =
                methodsFilter.SelectAllButtonsVisible =
                sourcesFilter.SelectAllButtonsVisible =
                offsetsFilter.SelectAllButtonsVisible = value;
            }
        }

        //public class Options
        //{
        //    public bool[/*All;Group;Set*/] ShowAGSSites { get; set; }
        //    public bool[] ShowAGSVariables { get; set; }
        //    public bool[] ShowAGSMethods { get; set; }
        //    public bool[] ShowAGSSources { get; set; }
        //    public bool[] ShowAGSOffsetTypes { get; set; }
        //}

        private void sitesFilter_UCDicItemCheckedEvent()
        {

        }

        private void sitesFilter_UCGroupChangedEvent(EntityGroup entityGroup)
        {

        }

        private void refreshAllCashButton_Click(object sender, EventArgs e)
        {
            Meta.DataManager.ClearCashs();
            Fill(SiteRepository.GetCash(), VariableRepository.GetCash(), MethodRepository.GetCash(), LegalEntityRepository.GetCash(), OffsetTypeRepository.GetCash());
        }
    }
}
