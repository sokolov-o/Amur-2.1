using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOV.Amur.Meta;

namespace SOV.Amur.Data
{
    public partial class FormClimate : Form
    {
        public FormClimate()
        {
            InitializeComponent();

            ucMethods.UCToolbarVisible = false;
            ucMethods.UCMethodDetailVisible = false;
            ucMethods.SetMethodSet(Meta.UCMethods.MethodSet.ClimateMethodsOnly);
        }

        private void ucMethods_UCCurrentMethodChangedEvent(Meta.Method method)
        {
            ucDataTable.ClearTable();
            ucDataTable.DataFilter = null;
        }

        private void ucMethods_UCDataFilterChangedEvent(CatalogFilter catalogFilter)
        {
            ucDataTable.ClearTable();
            ucDataTable.DataFilter = null;

            MethodClimate mc = Meta.DataManager.GetInstance().MethodClimateRepository.Select(ucMethods.CurrentMethod.Id);
            if (mc == null)
            {
                MessageBox.Show("Метод не является методом обработки климатических значений. " + ucMethods.CurrentMethod);
                return;
            }
            if (catalogFilter == null)
            {
                MessageBox.Show("Не установлен фильтр климатических данных. Установите фильтр." + ucMethods.CurrentMethod);
                return;
            }
            ucDataTable.DataFilter = new DataFilter()
            {
                DateTimePeriod = new Common.DateTimePeriod(
                    new DateTime(mc.YearS, 1, 1),
                    (new DateTime(mc.YearS + 1, 1, 1)).AddMilliseconds(-1),
                    SOV.Common.DateTimePeriod.Type.Period, 0
                ),
                DateTypeId = (int)SOV.Amur.Meta.EnumDateType.UTC,
                FlagAQC = null,
                IsActualValueOnly = true,
                IsDateLOC = false,
                IsRefSiteData = false,
                IsSelectDeleted = false,

                CatalogFilter = catalogFilter
            };
        }
    }
}
