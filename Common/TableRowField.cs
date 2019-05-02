using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using FERHRI.Common.Properties;

namespace FERHRI.Common
{
    public enum TableRowFieldUCType
    {
        Text,
        ComboBox,
        Image,
        ImageGallery
    }

    public class TableRowField
    {
        public string repName { get; private set; }
        public string dbName { get; private set; }
        public string viewName { get; private set; }
        public TableRowFieldUCType type { get; private set; }
        public List<DicItem> values { get; set; }
        public bool inEdit { get; private set; }
        public bool inInsert { get; private set; }
        public object value { get; set; }
        public object oldValue { get; set; }

        public TableRowField(string repName, string dbName, string viewName,
                            TableRowFieldUCType type = TableRowFieldUCType.Text, List<DicItem> values = null,
                                bool inEdit = true, bool inInsert = true)
        {
            this.repName = repName;
            this.dbName = dbName;
            this.viewName = viewName;
            this.type = type;
            this.values = values;
            this.inEdit = inEdit;
            this.inInsert = inInsert;
            this.value = null;
            this.oldValue = null;
        }

        public object GetObjField<U>(U obj) where U : class
        {
            if (obj == null)
                return null;
            Bitmap bitmap = null;
            object val = null;
            switch (type)
            {
                case TableRowFieldUCType.Text:
                    return ObjectHandler.FieldVal<U>(obj, repName);
                case TableRowFieldUCType.ComboBox:
                    val = ObjectHandler.FieldVal<U>(obj, repName);
                    if (val == null && values.FindIndex(x => x.Entity == null) >= 0)
                        val = values.Find(x => x.Entity.Equals(null)).Name;
                    if (val != null && !values.Contains(null))
                    {
                        val = values.Find(x => x.Entity != null && x.Entity.Equals(val));
                        if (val != null)
                            val = ((DicItem)val).Name;
                    }
                    return val;
                case TableRowFieldUCType.Image:
                    bitmap = new Bitmap(new MemoryStream(ObjectHandler.FieldVal<U>(obj, repName) as byte[]));
                    return bitmap.GetThumbnailImage(50, 50, null, IntPtr.Zero);
                case TableRowFieldUCType.ImageGallery:
                    val = ObjectHandler.MethodVal<U>(obj, repName);
                    bitmap = val == null ? Resources.empty : new Bitmap(new MemoryStream(val as byte[]));
                    return bitmap.GetThumbnailImage(50, 50, null, IntPtr.Zero);
                default: return null;
            }
        }
    }
}
