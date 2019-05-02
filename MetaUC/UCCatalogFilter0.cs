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

namespace SOV.Amur.Meta
{
    /// <summary>
    /// Прототип для фильтра каталога данных.
    /// </summary>
    internal partial class UCCatalogFilter0 : UserControl
    {
        public enum EnumItemTypeOfSet { All = 0, Group = 1, Handset = 2 }

        public UCCatalogFilter0()
        {
            InitializeComponent();

        }
        public string GetFilterCountText()
        {
            List<int> ids = GetFilterIds();
            // ВСЕ
            if (ids == null)
            {
                return "все";
            }
            // ГРУППА
            else if (ids.Count > 0 && ids[0] < 0)
            {
                return (ids.Count - 1).ToString();
            }
            // Набор
            else
            {
                return ids.Count.ToString();
            }

        }
        public List<int> GetFilterIds()
        {
            if (allRadioButton.Checked)
            {
                return null;
            }
            else if (groupRadioButton.Checked)
            {
                if (comboGroups.SelectedItem == null)
                    return new List<int>();
                else
                {
                    List<int> ret = new List<int> { -((EntityGroup)comboGroups.SelectedItem).Id };
                    ret.AddRange(_groups[(EntityGroup)comboGroups.SelectedItem].Select(x => x[0]));
                    return ret;
                }
            }
            else
            {
                return ucList.GetSelectedItemsId();
            }
        }
        public string CaptionDictionary
        {
            set
            {
                groupBox1.Text = value;
            }
        }
        /// <summary>
        /// Показать/скрыть группe кнопок "Тип набора элементов"
        /// </summary>
        public bool ShowItemTypeOfSetGroup
        {
            set
            {
                itemsTypeOfSetGroupBox.Visible =
                comboGroups.Visible = value;
            }
        }
        public void SetItemsTypeOfSet(EnumItemTypeOfSet itemTypeOfSet)
        {
            switch (itemTypeOfSet)
            {
                case EnumItemTypeOfSet.All: allRadioButton.Checked = true; break;
                case EnumItemTypeOfSet.Group: groupRadioButton.Checked = true; break;
                case EnumItemTypeOfSet.Handset: default: handsetRadioButton.Checked = true; break;
            }
        }
        public void SetFilterEmpty()
        {
            handsetRadioButton.Checked = true;
            ucList.UnselectAll();
        }
        bool _isFilled = false;
        public void SetFilter(List<int> ids)
        {
            _isFilled = true;
            try
            {
                ucList.UnselectAll();
                comboGroups.SelectedIndex = -1;

                // ВСЕ
                if (ids == null)
                {
                    allRadioButton.Checked = true;
                }
                // ГРУППА
                else if (ids.Count > 0 && ids[0] < 0)
                {
                    groupRadioButton.Checked = true;
                    int groupId = -ids[0];
                    comboGroups.SelectedItem = null;
                    foreach (EntityGroup group in comboGroups.Items)
                    {
                        if (group.Id == groupId)
                        {
                            comboGroups.SelectedItem = group;
                            break;
                        }
                    }
                    if (comboGroups.SelectedItem == null)
                        throw new Exception("Неизвестная группа в фильтре " + groupId);
                }
                // Набор
                else
                {
                    handsetRadioButton.Checked = true;
                    ucList.SetSelectedItemsById(ids);
                }
                RaiseUCDicItemCheckedEvent(GetFilterIds());
            }
            finally
            {
                _isFilled = false;
            }
        }

        /// <summary>
        /// Fill dictionary
        /// </summary>
        public void SetDicItems(List<IdName> idNames)
        {
            _isFilled = true;
            try
            {
                ucList.Clear();
                if (idNames != null && idNames.Count > 0)
                    ucList.SetDataSource(idNames.OrderBy(x => x.Name).ToList());

            }
            finally
            {
                _isFilled = false;
            }
        }
        Dictionary<EntityGroup, List<int[]>> _groups;
        public int GroupsCount
        {
            get
            {
                return _groups == null ? 0 : _groups.Count;
            }
        }
        public bool SelectAllButtonsVisible
        {
            set
            {
                ucList.ShowSelectAllToolbarButton = value;
            }
        }
        /// <summary>
        /// Fill group combo
        /// </summary>
        public void SetGroups(Dictionary<EntityGroup, List<int[]>> groups)
        {
            _isFilled = true;
            try
            {
                _groups = groups;
                comboGroups.Items.Clear();
                if (groups != null)
                    comboGroups.Items.AddRange(groups.Keys.ToArray());
            }
            finally
            {
                _isFilled = false;
            }
        }

        private void allRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            EnableControls();
        }

        private void groupRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            EnableControls();
        }

        private void setofRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            EnableControls();
        }

        private void EnableControls()
        {
            if (allRadioButton.Checked)
            {
                comboGroups.Enabled = false;
                ucList.Enabled = false;
            }
            else if (groupRadioButton.Checked)
            {
                comboGroups.Enabled = true;
                ucList.Enabled = false;
            }
            else
            {
                comboGroups.Enabled = false;
                ucList.Enabled = true;
            }
        }

        private void UCCatalogFilter0_Load(object sender, EventArgs e)
        {
            ucList.ShowToolbar = ucList.ShowFindItemToolbarButton = ucList.ShowSelectedOnlyToolbarButton = ucList.ShowUnselectAllToolbarButton = true;
            //ShowAGS(new bool[] { true, true, true });
        }

        public delegate void UCDicItemCheckedEventHandler(List<int> dicIds);
        public event UCDicItemCheckedEventHandler UCDicItemCheckedEvent;
        protected virtual void RaiseUCDicItemCheckedEvent(List<int> dicIds)
        {
            if (!_isFilled)
                if (UCDicItemCheckedEvent != null)
                {
                    UCDicItemCheckedEvent(dicIds);
                }
        }
        public delegate void UCGroupChangedEventHandler(EntityGroup entityGroup, List<int[]> entityIds);
        public event UCGroupChangedEventHandler UCGroupChangedEvent;
        protected virtual void RaiseUCGroupChangedEvent()
        {
            if (!_isFilled)
                if (UCGroupChangedEvent != null)
                {
                    if (comboGroups.SelectedIndex < 0 || _groups == null)
                        UCGroupChangedEvent(null, null);
                    else
                        UCGroupChangedEvent((EntityGroup)comboGroups.SelectedItem, _groups[(EntityGroup)comboGroups.SelectedItem]);
                }
        }
        /// <summary>
        /// Показать/скрыть отдельные кнопки выбора группы "Тип набора элементов"
        /// </summary>
        /// <param name="isShow">Триплет - свойство для каждой радио-кнопки</param>
        public void _DELME_ShowAGS(bool[] isShow)
        {
            allRadioButton.Visible = isShow[0];
            groupRadioButton.Visible = isShow[1];
            handsetRadioButton.Visible = isShow[2];

            comboGroups.Visible = groupRadioButton.Visible;
        }
        private void dic_UCItemCheckedEvent(List<int> ids)
        {
            RaiseUCDicItemCheckedEvent(ids);
        }

        private void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RaiseUCGroupChangedEvent();
        }
    }
}
