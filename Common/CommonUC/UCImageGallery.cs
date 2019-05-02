using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SOV.Common
{
    public partial class UCImageGallery : UserControl
    {
        private List<int> oldSelected = new List<int>();
        private List<DicItem> imgs = new List<DicItem>();
        public delegate void onChangeDelegator(object sender);
        public onChangeDelegator onChange = null;

        public UCImageGallery()
        {
            InitializeComponent();
        }

        public UCImageGallery(List<DicItem> imgs, List<int> selected = null) : this()
        {
            ReloadGallery(imgs, selected);
        }

        public void ReloadGallery(List<DicItem> _imgs, List<int> selected = null)
        {
            imgs = _imgs;
            oldSelected = selected ?? new List<int>();
            flowLayoutPanel.Controls.Clear();
            foreach (var dicElm in imgs)
            {
                var uc = new UCImage((byte[])dicElm.Entity, OnImageClick, false, 50, 50);
                uc.Tag = dicElm.Id;
                uc.Selected = oldSelected.Contains(dicElm.Id);
                flowLayoutPanel.Controls.Add(uc);
            }
        }

        public void RemoveById(List<int> ids)
        {
            foreach (var img in flowLayoutPanel.Controls)
                if (ids.Contains((int)((UCImage)img).Tag))
                    flowLayoutPanel.Controls.Remove((UCImage)img);
        }

        public List<int> SelectedIds()
        {
            List<int> res = new List<int>();
            foreach (var img in flowLayoutPanel.Controls)
                if (((UCImage)img).Selected)
                    res.Add((int)((UCImage)img).Tag);
            return res;
        }

        public void RemoveAllSelection()
        {
            foreach (var img in flowLayoutPanel.Controls)
                ((UCImage)img).Selected = false;
        }

        private void OnImageClick(UCImage sender)
        {
            var val = sender.Selected;
            if (onChange != null)
                onChange(this);
            sender.Selected = !val;
        }

        public bool IsChanged()
        {
            var currSelected = SelectedIds();
            return currSelected.Count != oldSelected.Count || currSelected.Any(i => !oldSelected.Contains(i));
        }
    }
}
