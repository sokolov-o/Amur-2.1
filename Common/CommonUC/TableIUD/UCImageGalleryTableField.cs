using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SOV.Common.TableIUD
{
    public partial class UCImageGalleryTableField : UCTableField
    {
        public List<int> oldSelected = new List<int>();

        public UCImageGalleryTableField(ImageGalleryTableField field, object selected = null) : base(field)
        {
            InitializeComponent();
            ReloadGallery(field.imgs, selected != null ? (List<int>) selected : null);
        }

        public void ReloadGallery(List<DicItem> images, List<int> selected = null)
        {
            ((ImageGalleryTableField)Field).imgs = images;
            oldSelected = selected ?? new List<int>();
            flowLayoutPanel.Controls.Clear();
            foreach (var dicElm in ((ImageGalleryTableField)Field).imgs)
            {
                var uc = new UCImage((byte[])dicElm.Entity, OnImageClick, false, 50, 50);
                uc.Tag = dicElm.Id;
                uc.Selected = oldSelected.Contains(dicElm.Id);
                flowLayoutPanel.Controls.Add(uc);
            }
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

        public override KeyValuePair<string, object> NameAndVal()
        {
            var selected = SelectedIds();
            object value = null;
            if (selected.Count == 1) value = selected[0];
            if (selected.Count > 1) value = selected;
            return new KeyValuePair<string, object>(Field.db, value);
        }
    }
}
