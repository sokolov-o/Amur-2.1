using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SOV.Common.TableIUD
{
    ///<summary>
    /// Форма для отображения строк таблицы БД
    /// T: тип объекта-структы, представляющего строку БД
    /// F: тип формы, изменения/создания поля таблицы БД 
    ///</summary> 
    public partial class FormTableView<T, F> : Form 
        where T : class 
        where F : FormTableRowEdit<T>
    {
        public enum ViewerType
        {
            Grid,
            Tree
        }
        /// <summary>
        /// Тип обозревателя
        /// </summary>
        public ViewerType Type { get; private set; }
        ///<summary>
        /// Отображаемое в заголовке формы, имя таблицы.
        ///</summary> 
        private readonly string tableName;
        ///<summary>
        /// Объект-репозиторий, обрабатывающий данную таблицу
        ///</summary> 
        private readonly BaseRepository<T> repo;
        ///<summary>
        /// UC обозреватель. 
        ///</summary> 
        public TableViewer<T> TableViewer;
        ///<summary>
        /// Массив полей, обрабатываемой записи
        ///</summary>
        public List<TableField> Fields { get; set; }

        public delegate void FormTableViewEventHandler(FormTableView<T, F> formTableView);
        ///<summary>
        /// Событие открытия формы создания записи
        ///</summary>
        public event FormTableViewEventHandler OnInsert;
        ///<summary>
        /// Событие открытия формы редактирования записи
        ///</summary>
        public event FormTableViewEventHandler OnUpdate;
        ///<summary>
        /// Событие удаления записи
        ///</summary>
        public event FormTableViewEventHandler OnDelete;
        ///<summary>
        /// Событие обновления данных записей
        ///</summary>
        public event FormTableViewEventHandler OnRefreshViwer;

        ///<summary>
        /// Функция, выполняющая создание/обработку изменений записи в порожденной FormTableRowEdit
        ///</summary> 
        public FormTableRowEdit<T>.SubmitFormDelegator EditFormSubmit;

        public FormTableView(BaseRepository<T> repo, List<TableField> fields, string tableName, ViewerType type = ViewerType.Grid)
        {
            InitializeComponent();
            this.tableName = tableName;
            this.repo = repo;
            Text += "'" + tableName + "'";
            Fields = fields;

            InitTableViewer(type);
        }
        /// <summary>
        /// Инициализировать обозреватель и изменить его тип.
        /// <param name="type">Тип обозревателя</param>
        /// </summary>
        private void InitTableViewer(ViewerType type)
        {
            if (TableViewer != null)
                ((UserControl)TableViewer).Dispose();
            Type = type;
            TableViewer = type == ViewerType.Grid ? (TableViewer<T>)new UCGridTableViewer<T>(Fields) : new UCTreeTableViewer<T>(Fields);
            TableViewer.OnViewerMouseUp += viewerMouseUp;
            ((UserControl)TableViewer).Dock = DockStyle.Fill;
            Controls.Add((UserControl)TableViewer);
            Controls.SetChildIndex((UserControl)TableViewer, 0);
        }

        private void defaultOnRefreshViwer(object sender, EventArgs e)
        {
            if (OnRefreshViwer != null)
            {
                OnRefreshViwer(this);
                return;
            }
            TableViewer.RefreshData(repo.SelectAllFields());
        }

        private void viewerMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            contextMenu.Show(this, new System.Drawing.Point(e.X, e.Y));
        }

        private void defaultOnEditFormSubmit(FormTableRowEdit<T> sender)
        {
            if (sender.SubmitForm != null)
                sender.SubmitForm(sender);
            else if (sender.Type == FormTableRowEdit<T>.TableType.Insert)
                repo.Insert(sender.FieldVals());
            else
                repo.Update(sender.FieldVals());
            defaultOnRefreshViwer(null, null);
        }

        private void defaultOnInsertClick(object sender, EventArgs e)
        {
            if (OnInsert != null)
            {
                OnInsert(this);
                return;
            }
            var form = (F)Activator.CreateInstance(
                typeof(F),
                new object[] { Fields, FormTableRowEdit<T>.TableType.Insert, tableName, null }
            );
            form.OnSaveClick += defaultOnEditFormSubmit;
            form.SubmitForm = EditFormSubmit;
            form.Show();
        }

        private void defaultOnUpdateClick(object sender, EventArgs e)
        {
            if (OnUpdate != null)
            {
                OnUpdate(this);
                return;
            }
            var form = (F)Activator.CreateInstance(
                typeof(F), 
                new object[] {Fields, FormTableRowEdit<T>.TableType.Update, tableName, TableViewer.SelectedItem()}
            );
            form.OnSaveClick += defaultOnEditFormSubmit;
            form.Show();
        }

        private void deleteToolStripButton_Click(object sender, EventArgs e)
        {
            if (OnDelete == null)
                repo.Delete(TableViewer.SelectedItem());
            else
                OnDelete(this);
            defaultOnRefreshViwer(null, null);
        }
        /// <summary>
        /// Убрать функцию редактирования
        /// </summary>
        public void RemoveEditFunction()
        {
            updateToolStripMenuItem.Dispose();
            updateToolStripButton.Dispose();
        }

        private void gridTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitTableViewer(ViewerType.Grid);
            defaultOnRefreshViwer(null, null);
        }

        private void treeTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitTableViewer(ViewerType.Tree);
            defaultOnRefreshViwer(null, null);
        }
        /// <summary>
        /// Сделать доступным обозревать типа Tree
        /// </summary>
        public void EnableTreeType()
        {
            treeTypeToolStripMenuItem.Enabled = true;
        }
    }
}
