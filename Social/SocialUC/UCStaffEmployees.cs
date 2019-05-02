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
    public partial class UCStaffEmployees : UserControl
    {
        public UCStaffEmployees()
        {
            InitializeComponent();
        }
        List<LegalEntity> EmployeeFilter;
        LegalEntity EmployerFilter;
        List<Division> DivisionFilter;
        List<StaffPosition> StaffPositionsFilter;
        DateTime? DateActual;

        public void Fill(int employerId, List<int> divisionIds, List<int> staffPositionIds, List<int> employeeIds, DateTime? dateActual)
        {
            Clear();

            DateActual = dateActual;

            EmployerFilter = DataManager.GetInstance().LegalEntityRepository.Select(employerId);
            List<Staff> staffs = DataManager.GetInstance().StaffRepository.SelectByEmployer(employerId, dateActual);

            if (divisionIds != null)
            {
                staffs = staffs.Where(x => divisionIds.Exists(y => y == x.Division.Id)).ToList();
                DivisionFilter = DataManager.GetInstance().DivisionRepository.Select(divisionIds);
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
                StaffPositionsFilter = DataManager.GetInstance().StaffPositionRepository.Select(staffPositionIds);
                if (staffPositionIds.Count == 1)
                {
                    dgv.Columns["staffPositionDGVC"].Visible = false;
                    dgv.Columns["editStaffPositionDGVC"].Visible = false;
                }
            }
            else
                StaffPositionsFilter = DataManager.GetInstance().StaffPositionRepository.Select();

            staffBindingSource.DataSource = staffs.OrderByDescending(x => x.DateS).ToList();

            List<StaffEmployee> staffEmployees = DataManager.GetInstance().StaffEmployeeRepository.Select(staffs.Select(x => x.Id).ToList(), employeeIds, dateActual);
            if (employeeIds != null)
            {
                staffEmployees = staffEmployees.Where(x => employeeIds.Exists(y => y == x.EmployeeId)).ToList();
                EmployeeFilter = DataManager.GetInstance().LegalEntityRepository.Select(employeeIds);
                if (employeeIds.Count == 1)
                {
                    dgv.Columns["employeeDGVC"].Visible = false;
                    dgv.Columns["editEmployeeDGVC"].Visible = false;
                }
            }
            else
                EmployeeFilter = DataManager.GetInstance().LegalEntityRepository.Select('p', null);
            employeeBindingSource.DataSource = EmployeeFilter.OrderBy(x => x.NameRusShort).ToList();

            staffEmployeeBindingSource.DataSource = staffEmployees; // Как сортировать?

            SetInfo();
        }

        internal void Fill(object deafultOrganization, object p1, object p2, List<int> list, DateTime? dateActual)
        {
            throw new NotImplementedException();
        }

        void SetInfo()
        {
            infoLabel.Text = staffEmployeeBindingSource.Count.ToString();
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                //DataGridViewColumn col = dgv.Columns[e.ColumnIndex];
                //Staff item = CurrentItem;

                //// ANY DGV BUTTON PRESSED?
                //if (col.Name == "editDivisionDGVC")
                //{
                //    if (FormSelectDivision.ShowDialog() == DialogResult.OK)
                //    {
                //        item.Division = (Division)FormSelectDivision.CurrentDicItem.Entity;
                //        Save(item);

                //        dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = false;
                //        dgv.Rows[e.RowIndex].Cells["divisionDGVC"].Selected = true;
                //    }
                //}
                //if (col.Name == "editStaffPositionDGVC")
                //{
                //    if (FormSelectStaffPosition.ShowDialog() == DialogResult.OK)
                //    {
                //        item.StaffPosition = this.FormSelectStaffPosition.StaffPositionSelected;
                //        Save(item);

                //        dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = false;
                //        dgv.Rows[e.RowIndex].Cells["staffPositionDGVC"].Selected = true;
                //    }
                //}
            }
        }

        private void Fill()
        {
            Fill(
                EmployerFilter.Id,
                DivisionFilter == null ? null : DivisionFilter.Select(x => x.Id).ToList(),
                StaffPositionsFilter == null ? null : StaffPositionsFilter.Select(x => x.Id).ToList(),
                EmployeeFilter == null ? null : EmployeeFilter.Select(x => x.Id).ToList(),
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
                    List<DicItem> dicis = new List<DicItem>();
                    foreach (var item in DivisionFilter.OrderBy(x => x.NameRus))
                    {
                        dicis.Add(new DicItem() { Id = item.Id, Name = item.NameRus, Entity = item });
                    }
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
            staffBindingSource.Clear();
            staffEmployeeBindingSource.Clear();
            employeeBindingSource.Clear();
        }

        private void StaffBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (e.NewObject == null)
            {
                e.NewObject = new StaffEmployee()
                {
                    Id = -1,
                    StaffId = ((List<Staff>)staffBindingSource.DataSource)[0].Id,
                    EmployeeId = ((List<LegalEntity>)employeeBindingSource.DataSource)[0].Id,
                    Percent = 100,
                    DateS = new DateTime(1963, 9, 7)
                };
            }
        }
        //Division DefaultDivision
        //{
        //    get
        //    {
        //        if (DivisionFilter == null)
        //        {
        //            if (bindingSource != null && bindingSource.Count > 0)
        //                return ((List<Staff>)bindingSource.DataSource)[0].Division;
        //        }
        //        else if (DivisionFilter.Count == 1)
        //            return DivisionFilter[0];

        //        return null;
        //    }
        //}
        //StaffPosition DefaultStaffPosition
        //{
        //    get
        //    {
        //        if (StaffPositionsFilter == null)
        //        {
        //            if (bindingSource != null && bindingSource.Count > 0)
        //                return ((List<Staff>)bindingSource.DataSource)[0].StaffPosition;
        //        }
        //        else if (StaffPositionsFilter.Count == 1)
        //            return StaffPositionsFilter[0];

        //        return null;
        //    }
        //}

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
        StaffEmployee CurrentItem
        {
            get
            {
                return staffEmployeeBindingSource != null && staffEmployeeBindingSource.Current != null ? (StaffEmployee)staffEmployeeBindingSource.Current : null;
            }
            set
            {
                if (staffEmployeeBindingSource != null && value != null)
                {
                    int i = ((List<StaffEmployee>)staffEmployeeBindingSource.DataSource).IndexOf(((List<StaffEmployee>)staffEmployeeBindingSource.DataSource).Find(x => x.StaffId == value.StaffId && x.EmployeeId == value.EmployeeId));
                    staffEmployeeBindingSource.Position = i;
                }
            }
        }
        private void Save(StaffEmployee item)
        {
            try
            {
                //if (item.Division == null && DefaultDivision != null)
                //    item.Division = DefaultDivision;

                //if (item.StaffPosition == null && DefaultStaffPosition != null)
                //    item.StaffPosition = DefaultStaffPosition;

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
                        item.Id = DataManager.GetInstance().StaffEmployeeRepository.Insert(item);
                        Fill();
                        CurrentItem = item;
                    }
                    else
                        DataManager.GetInstance().StaffEmployeeRepository.Update(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
