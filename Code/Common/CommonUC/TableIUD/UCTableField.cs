using System.Collections.Generic;
using System.Windows.Forms;

namespace SOV.Common.TableIUD
{
    /// <summary>
    /// UC для редактирования поля таблицы
    /// </summary>
    public partial class UCTableField : UserControl
    {
        public TableField Field { get; protected set; }
        public Control ucInput { get; protected set; }

        public delegate void onChangeDelegator(UCTableField sender);
        protected onChangeDelegator onChange = null; 

        public UCTableField() : this(null) { }

        public UCTableField(TableField field)
        {
            InitializeComponent();
            this.Field = field;
            ucInput = null;
        }

        public virtual KeyValuePair<string, object> NameAndVal()
        {
            return new KeyValuePair<string, object>();
        }

        public static Dictionary<string, object> ControlFieldsVal(Control parent)
        {
            var fields = new Dictionary<string, object>();
            foreach (var ucField in parent.Controls)
            {
                var pair = ((UCTableField)ucField).NameAndVal();
                fields[pair.Key] = pair.Value;
            }
            return fields;
        }

        public static UCTableField CreateUC(TableField field, bool isUpdate, object val = null)
        {
            UCTableField ucField = null;
            switch (field.type)
            {
                case TableFieldUCType.Text: ucField = new UCTextTableField((TextTableField)field, val); break;
                case TableFieldUCType.ComboBox: ucField = new UCComboBoxTableField((ComboBoxTableField)field, val); break;
                case TableFieldUCType.ImageInput: ucField = new UCImageInputTableField((ImageInputTableField)field); break;
                case TableFieldUCType.ImageGallery: ucField = new UCImageGalleryTableField((ImageGalleryTableField)field, val); break;
                case TableFieldUCType.Tree: ucField = new UCTreeTableField((TreeTableField)field, val); break;
            }
            ucField.Dock = DockStyle.Top;
            ucField.Enabled = !(isUpdate && field.LockedUpdate || !isUpdate && field.LockedInsert);
            ucField.Visible = !(isUpdate && field.HiddenUpdate || !isUpdate && field.HiddenInsert);
            return ucField;
        }

        public void SetOnChangeEvent(onChangeDelegator onChange)
        {
            this.onChange = onChange;
        }
    }
}
