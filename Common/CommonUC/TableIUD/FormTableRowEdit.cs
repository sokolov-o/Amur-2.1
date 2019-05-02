using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SOV.Common.TableIUD
{
    ///<summary>
    /// Форма для изменения/создания поля таблицы БД 
    ///</summary> 
    public partial class FormTableRowEdit<T> : Form where T : class
    {
        public enum TableType
        {
            Update,
            Insert
        }
        ///<summary>
        /// Событие нажатия кнопки "Сохранить"
        ///</summary> 
        public delegate void OnSaveClickDelegator(FormTableRowEdit<T> sender);
        public event OnSaveClickDelegator OnSaveClick;

        ///<summary>
        /// Функция, выполняющая создание/обработку изменений записи 
        ///</summary> 
        public delegate void SubmitFormDelegator(FormTableRowEdit<T> formTableRowEdit);
        public SubmitFormDelegator SubmitForm;

        public TableType Type { get; private set; }
        ///<summary>
        /// Массив полей, обрабатываемой записи
        ///</summary> 
        public List<TableField> Fields { get; private set; }
        ///<summary>
        /// Объект-структура, представляющая редактируемую запись БД. = Null, если форма создает запись.
        ///</summary> 
        protected T obj;
        ///<summary>
        /// Отображаемое в заголовке формы, имя таблицы.
        ///</summary> 
        protected string tableTitle;

        public FormTableRowEdit()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        public FormTableRowEdit(List<TableField> fields, TableType type, string tableName, T obj = null) : this()
        {
            this.Type = type;
            this.Fields = fields;
            this.obj = obj;
            this.tableTitle = tableName;
            Load += FormTableRowEdit_Load;
        }

        public virtual void InitUCFields()
        {
            foreach (var field in Fields)
                AddFieldUCToContainer(field, topPanel, obj);
        }

        /// <summary>
        /// Создать UCTableField, соответствующий переданному типу поля и добавить его в контейнер.
        /// </summary>
        /// <param name="field">Представление поля</param>
        /// <param name="container">Контейнер, в который будет добавлен созданный UCTableField</param>
        /// <param name="cuurObj">
        /// Объект-структура, представляющая редактируемую запись БД. = Null, если форма создает запись. Содержит данное поле.
        /// </param>
        public void AddFieldUCToContainer<U>(TableField field, Control container = null, U cuurObj = null) where U : class
        {
            if (Type == TableType.Update && !field.inUpdate || Type == TableType.Insert && !field.inInsert)
                return;
            container = container ?? topPanel;
            var uc = Type == TableType.Update ?
                UCTableField.CreateUC(field, true, ObjectHandler.FieldVal<U>(cuurObj, field.value)) :
                UCTableField.CreateUC(field, false);
            uc.Name = field.db;
            container.Controls.Add(uc);
            container.Controls.SetChildIndex(uc, 0);
        }

        /// <summary>
        /// Получить словарь "имя" => "значение". Данные извлекаются из всех UC, лежащих на панели topPanel
        /// </summary>
        /// <returns>Словарь "имя" => "значение"</returns>
        public virtual Dictionary<string, object> FieldVals()
        {
            return UCTableField.ControlFieldsVal(topPanel);
        }

        protected void save_Click(object sender, EventArgs e)
        {
            if (OnSaveClick != null) OnSaveClick(this);
            Close();
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormTableRowEdit_Load(object sender, EventArgs e)
        {
            Text = (Type == TableType.Update ? "Редактирование" : "Создание") + " строки таблицы '" + tableTitle + "'";
            InitUCFields();
        }

        /// <summary>
        /// Найти UC поля по его названию в БД
        /// </summary>
        /// <param name="dbFieldName">Название поля в БД</param>
        /// <returns></returns>
        public UCTableField FindUC(string dbFieldName)
        {
            foreach (var uc in topPanel.Controls)
                if (((UCTableField)uc).Field.db == dbFieldName)
                    return (UCTableField)uc;
            return null;
        }
    }
}
