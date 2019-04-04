using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using SOV.Common;

namespace SOV.Social
{
    public partial class UCStaffs : UserControl
    {
        public UCStaffs()
        {
            InitializeComponent();
        }
        LegalEntity EmployerFilter;
        List<Division> DivisionFilter;
        List<StaffPosition> StaffPositionsFilter;
        DateTime? DateActual;

        public void Fill(int employerId, List<int> divisionIds, List<int> staffPositionIds, DateTime? dateActual)
        {
            Clear();

            DateActual = dateActual;

            EmployerFilter = DataManager.GetInstance().LegalEntityRepository.Select(employerId);
            List<Staff> staffs = DataManager.GetInstance().StaffRepository.SelectByEmployer(employerId, dateActual)
                .OrderByDescending(x => x.DateS).ToList();

            if (divisionIds != null)
            {
                staffs = staffs.Where(x => divisionIds.Exists(y => y == x.Division.Id)).ToList();
                DivisionFilter = staffs.Select(x => x.Division).ToList();
                if (divisionIds.Count == 1)
                {
                    dgv.Columns["divisionDGVC"].Visible = false;
                    dgv.Columns["editDivisionDGVC"].Visible = false;
                }
            }
            else
                DivisionFilter = DataManager.GetInstance().DivisionRepository.Select(employerId, DateActual);

            if (staffPositionIds != null)
            {
                staffs = staffs.Where(x => staffPositionIds.Exists(y => y == x.StaffPosition.Id)).ToList();
                StaffPositionsFilter = staffs.Select(x => x.StaffPosition).ToList();
                if (staffPositionIds.Count == 1)
                {
                    dgv.Columns["staffPositionDGVC"].Visible = false;
                    dgv.Columns["editStaffPositionDGVC"].Visible = false;
                }
            }
            else
                StaffPositionsFilter = DataManager.GetInstance().StaffPositionRepository.Select();

            bindingSource.DataSource = staffs.OrderByDescending(x => x.DateS).ToList();
            SetInfo();
        }
        void SetInfo()
        {
            infoLabel.Text = bindingSource.Count.ToString();
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                DataGridViewColumn col = dgv.Columns[e.ColumnIndex];
                Staff item = CurrentItem;

                // ANY DGV BUTTON PRESSED?
                if (col.Name == "editDivisionDGVC")
                {
                    if (FormSelectDivision.ShowDialog() == DialogResult.OK)
                    {
                        item.Division = (Division)FormSelectDivision.SelectedItems[0];
                        Save(item);

                        dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = false;
                        dgv.Rows[e.RowIndex].Cells["divisionDGVC"].Selected = true;
                    }
                }
                if (col.Name == "editStaffPositionDGVC")
                {
                    if (FormSelectStaffPosition.ShowDialog() == DialogResult.OK)
                    {
                        item.StaffPosition = this.FormSelectStaffPosition.StaffPositionSelected;
                        Save(item);

                        dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = false;
                        dgv.Rows[e.RowIndex].Cells["staffPositionDGVC"].Selected = true;
                    }
                }
            }
        }

        private void Fill()
        {
            Fill(
                EmployerFilter.Id,
                DivisionFilter == null ? null : DivisionFilter.Select(x => x.Id).ToList(),
                StaffPositionsFilter == null ? null : StaffPositionsFilter.Select(x => x.Id).ToList(),
                DateActual
            );
        }

        FormSelectStaffPosition _FormSelectStaffPosition;
        FormSelectStaffPosition FormSelectStaffPosition
        {
            get
            {
                if (_FormSelectStaffPosition == null)
                {
                    _FormSelectStaffPosition = new FormSelectStaffPosition();
                    _FormSelectStaffPosition.StartPosition = FormStartPosition.CenterScreen;
                    _FormSelectStaffPosition.ShowNameRusOnly = true;
                }
                return _FormSelectStaffPosition;
            }
            set
            {
                _FormSelectStaffPosition = value;
            }
        }
        FormSelectListItems _FormSelectDivision;
        FormSelectListItems FormSelectDivision
        {
            get
            {
                if (_FormSelectDivision == null)
                {
                    //List<DicItem> dicis = new List<DicItem>();
                    //foreach (var item in DivisionFilter.OrderBy(x => x.NameRus))
                    //{
                    //    dicis.Add(new DicItem() { Id = item.Id, Name = item.NameRus, Entity = item });
                    //}
                    _FormSelectDivision = new FormSelectListItems("Выбрать подразделение", DivisionFilter.OrderBy(x => x.NameRus).ToArray(), "NameRus");
                    _FormSelectDivision.StartPosition = FormStartPosition.CenterScreen;
                }
                return _FormSelectDivision;
            }
        }

        internal void Clear()
        {
            EmployerFilter = null;
            StaffPositionsFilter = null;
            DivisionFilter = null;
            DateActual = null;
            bindingSource.Clear();
        }

        private void StaffBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (e.NewObject == null)
            {
                e.NewObject = new Staff()
                {
                    Id = -1,
                    StaffPosition = DefaultStaffPosition,
                    Division = DefaultDivision,
                    DateS = new DateTime(1963, 9, 7)
                };
            }
        }
        Division DefaultDivision
        {
            get
            {
                if (DivisionFilter == null)
                {
                    if (bindingSource != null && bindingSource.Count > 0)
                        return ((List<Staff>)bindingSource.DataSource)[0].Division;
                }
                else if (DivisionFilter.Count == 1)
                    return DivisionFilter[0];

                return null;
            }
        }
        StaffPosition DefaultStaffPosition
        {
            get
            {
                if (StaffPositionsFilter == null)
                {
                    if (bindingSource != null && bindingSource.Count > 0)
                        return ((List<Staff>)bindingSource.DataSource)[0].StaffPosition;
                }
                else if (StaffPositionsFilter.Count == 1)
                    return StaffPositionsFilter[0];

                return null;
            }
        }

        private void dgv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            Staff item = (Staff)e.Row.DataBoundItem;
            if (item.Id >= 0)
                DataManager.GetInstance().StaffRepository.Delete(item.Id);
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Save(CurrentItem);
        }
        Staff CurrentItem
        {
            get
            {
                return bindingSource != null && bindingSource.Current != null ? (Staff)bindingSource.Current : null;
            }
            set
            {
                if (bindingSource != null && value != null)
                {
                    int i = ((List<Staff>)bindingSource.DataSource).IndexOf(((List<Staff>)bindingSource.DataSource).Find(x => x.Id == value.Id));
                    bindingSource.Position = i;
                }
            }
        }
        private void Save(Staff item)
        {
            try
            {
                if (item.Division == null && DefaultDivision != null)
                    item.Division = DefaultDivision;

                if (item.StaffPosition == null && DefaultStaffPosition != null)
                    item.StaffPosition = DefaultStaffPosition;

                if (item.ReadyForInsert)
                {
                    //if (DataManager.GetInstance().StaffRepository.ExistsOpenedStaff(item))
                    //{
                    //    MessageBox.Show("Есть незакрытые позиции должностей для сотрудника и организации. Сначала их нужно закрыть - указать даты завершения работы в должности."
                    //        + "\nИ только затем создать новую должность для сотрудника.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}
                    if (item.Id < 0)
                    {
                        item.Id = DataManager.GetInstance().StaffRepository.Insert(item);
                        Fill();
                        CurrentItem = item;
                    }
                    else
                        DataManager.GetInstance().StaffRepository.Update(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
