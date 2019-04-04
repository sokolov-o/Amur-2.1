using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using SOV.Common.Properties;

namespace SOV.Common
{
    public enum TableFieldUCType
    {
        Text,
        ComboBox,
        ImageInput,
        ImageGallery,
        Tree
    }

    public class TableField
    {
        /// <summary>
        /// Заголовок - название поля, отображаемое в UC
        /// </summary>
        public string title { get; private set; }
        /// <summary>
        /// Имя поля в базе данных
        /// </summary>
        public string db { get; private set; }
        /// <summary>
        /// Имя поля объекта, представляющего запись в БД. Является значением поля.
        /// </summary>
        public string value { get; private set; }
        /// <summary>
        /// Имя поля объекта, представляющего запись в БД. Является отображения его значения.
        /// </summary>
        public string view { get; private set; }
        /// <summary>
        /// Тип UC для отображения поля
        /// </summary>
        public TableFieldUCType type { get; private set; }
        /// <summary>
        /// Включать ли поле в UC для изменения значений строки БД
        /// </summary>
        public bool inUpdate { get; set; }
        /// <summary>
        /// Включать ли поле в UC для создания строки БД
        /// </summary>
        public bool inInsert { get; set; }
        /// <summary>
        /// Включать ли поле в UC для отображения строк БД
        /// </summary>
        public bool inView { get; set; }
        /// <summary>
        /// Запретить изменение поля в UC для изменения значений строки БД
        /// </summary>
        public bool LockedUpdate { get; set; }
        /// <summary>
        /// Запретить изменение поля в UC для создания строки БД
        /// </summary>
        public bool LockedInsert { get; set; }
        /// <summary>
        /// Скрыть поле в UC для изменения значений строки БД
        /// </summary>
        public bool HiddenUpdate { get; set; }
        /// <summary>
        /// Скрыть поле в UC для создания строки БД
        /// </summary>
        public bool HiddenInsert { get; set; }

        public TableField(TableFieldUCType type, 
                            string title = "", string db = "", string value = "", 
                            string view = null, bool inEdit = true, bool inInsert = true, bool inView = true,
                            bool lockedUpdate = false, bool lockedInsert = false,
                            bool hiddenUpdate = false, bool hiddenInsert = false)
        {
            this.title = title;
            this.db = db;
            this.value = value;
            this.view = view ?? value;
            this.type = type;
            this.inView = inView;
            this.inUpdate = inEdit;
            this.inInsert = inInsert;
            LockedUpdate = lockedUpdate;
            LockedInsert = lockedInsert;
            HiddenInsert = hiddenInsert;
            HiddenUpdate = hiddenUpdate;
        }
        /// <summary>
        /// Получить значение соответствующего поля объекта-структуры, представляющего запись в таблице БД
        /// </summary>
        /// <typeparam name="U">Тип объекта</typeparam>
        /// <param name="obj">Объект-структура</param>
        /// <returns>Значение поля</returns>
        public virtual object ObjView<U>(U obj) where U : class
        {
            return ObjectHandler.FieldVal<U>(obj, view);
        }
    }

    public class TextTableField : TableField
    {
        public TextTableField(string title, string db, string value, string view = null) :
                            base(TableFieldUCType.Text, title, db, value, view)
        {
        }
    }

    public class ComboBoxTableField : TableField
    {
        public List<DicItem> items { get; set; }

        public ComboBoxTableField(string title, string db, string value, string view = null) :
                                base(TableFieldUCType.ComboBox, title, db, value, view)
        {
            this.items = new List<DicItem>();
        }
    }

    public class ImageInputTableField : TableField
    {
        public ImageInputTableField(string title, string db, string value, string view = null) :
                                    base(TableFieldUCType.ImageInput, title, db, value, view)
        {
        }

        public override object ObjView<U>(U obj)
        {
            object val = ObjectHandler.FieldVal<U>(obj, view);
            using (Bitmap bitmap = val == null ? Resources.empty : new Bitmap(new MemoryStream(val as byte[])))
                return bitmap.GetThumbnailImage(50, 50, null, IntPtr.Zero);
        }
    }

    public class ImageGalleryTableField : TableField
    {
        public List<DicItem> imgs { get; set; }

        public ImageGalleryTableField(string title, string db, string value, string view = null) :
                                    base(TableFieldUCType.ImageGallery, title, db, value, view)
        {
            this.imgs = new List<DicItem>();
        }

        public override object ObjView<U>(U obj)
        {
            object val = ObjectHandler.FieldVal<U>(obj, view);
            using (Bitmap bitmap = val == null ? Resources.empty : new Bitmap(new MemoryStream(val as byte[])))
                return bitmap.GetThumbnailImage(50, 50, null, IntPtr.Zero);
        }
    }

    public class TreeTableField : TableField
    {
        public List<DicItem> tree { get; set; }

        public TreeTableField(string title, string db, string value, string view = null) :
                            base(TableFieldUCType.Tree, title, db, value, view)
        {
            this.tree = new List<DicItem>();
        }

        private string DeepFind(List<DicItem> root, int val)
        {
            string result;
            var index = root.FindIndex(x => x.Id.Equals(val));
            if (index >= 0)
                return (string)root[index].Entity;
            foreach (var item in root)
                if ((result = DeepFind(item.Childs, val)) != null)
                    return result;
            return null;
        }
    }
}
