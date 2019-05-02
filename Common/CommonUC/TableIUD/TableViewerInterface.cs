using System.Collections.Generic;
using System.Windows.Forms;

namespace SOV.Common.TableIUD
{
    public delegate void TableViewerMouseEvent(object sender, MouseEventArgs e);
    public delegate void TableViewerDragEvent(object sender, DragEventArgs e);

    public interface TableViewer<T> where T : class
    {
        /// <summary>
        /// Массив полей, обрабатываемой записи
        /// </summary>
        List<TableField> Fields { get; set; }
        /// <summary>
        /// Получить выбранный объект
        /// </summary>
        T SelectedItem();
        /// <summary>
        /// Обновить данные отображаемых записей
        /// </summary>
        void RefreshData<ElmT>(List<ElmT> data) where ElmT : class;

        event TableViewerMouseEvent OnViewerMouseUp;
        event TableViewerDragEvent OnViewerDragDrop;
    }
}
