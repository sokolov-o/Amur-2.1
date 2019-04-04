using System.Windows.Forms;
using System.Linq;
using SOV.Social;
using System;

namespace SOV.Amur.Meta
{
    public partial class UCMethod : UserControl
    {
        public UCMethod()
        {
            InitializeComponent();

            methodDetailsTypeComboBox.Items.AddRange(new Common.IdName[]
            {
                new Common.IdName() { Id=1, Name="Нет" },
                new Common.IdName() { Id=2, Name="Прогноз" },
                new Common.IdName() { Id=3, Name="Климат" }
            });
        }
        Method _Method = null;

        public Method Method
        {
            set
            {
                Clear();
                if (value != null)
                {
                    methodNameTextBox.Text = value.Name;
                    descriptionTextBox.Text = value.Description;
                    outputStoreParametersTextBox.Text = value.MethodOutputStoreParameters != null ? Common.StrVia.ToString(value.MethodOutputStoreParameters) : null;
                    orderTextBox.Text = value.Order.ToString();
                    idTextBox.Text = value.Id.ToString();

                    ucLESource.Value = value.SourceLegalEntityId.HasValue
                        ? SOV.Social.DataManager.GetInstance().LegalEntityRepository.Select((int)value.SourceLegalEntityId)
                        : null;
                    ucParentMethod.Value = value.ParentId.HasValue
                        ? DataManager.GetInstance().MethodRepository.Select((int)value.ParentId)
                        : null;

                    FillMethodDetails(Method, null);
                }
            }
            get
            {
                return new Method()
                {
                    Id = string.IsNullOrEmpty(idTextBox.Text) ? -1 : int.Parse(idTextBox.Text),
                    Name = methodNameTextBox.Text,
                    MethodOutputStoreParameters = Common.StrVia.ToDictionary(outputStoreParametersTextBox.Text),
                    Order = string.IsNullOrEmpty(orderTextBox.Text) ? short.MaxValue : short.Parse(orderTextBox.Text),
                    ParentId = ucParentMethod.Value == null ? null : (int?)((Method)ucParentMethod.Value).Id,
                    Description = descriptionTextBox.Text,
                    SourceLegalEntityId = ucLESource.Value == null ? null : (int?)((LegalEntity)ucLESource.Value).Id
                    //MethodForecast = groupBox3.Controls[0].GetType() == typeof(UCMethodForecast) и т.д.
                };
            }
        }
        bool _isFilled = false;
        void FillMethodDetails(Method method, int? methodTypeId)
        {
            try
            {
                _isFilled = true;

                ClearMethodDetails();
                UserControl uc = null;
                methodDetailsTypeComboBox.SelectedIndex = 0;

                if (methodTypeId.HasValue)
                {
                    switch ((int)methodTypeId)
                    {
                        case 2: uc = GetMethodDetailsForecastUC(method, true); break;
                        case 3: uc = GetMethodDetailsClimateUC(method, true); break;
                        default:
                            MessageBox.Show("Неизвестны детали метода типа  " + ((Common.IdName)methodDetailsTypeComboBox.SelectedItem).Id);
                            break;
                    }
                    methodDetailsTypeComboBox.SelectedIndex = ((int)methodTypeId) - 1;
                }
                else
                {
                    uc = GetMethodDetailsForecastUC(method, false);
                    if (uc != null)
                    {
                        methodDetailsTypeComboBox.SelectedIndex = 1;
                    }
                    else
                    {
                        uc = GetMethodDetailsClimateUC(method, false);
                        if (uc != null)
                            methodDetailsTypeComboBox.SelectedIndex = 2;
                    }
                }
                if (uc != null)
                {
                    uc.Dock = DockStyle.Fill;
                    splitContainer1.Panel2.Controls.Add(uc);
                    splitContainer1.Panel2Collapsed = false;
                    return;
                }
            }
            finally
            {
                _isFilled = false;
            }
        }
        private UserControl GetMethodDetailsForecastUC(Method method, bool createUCIfNotExists)
        {
            UserControl uc = null;
            object methDetails = DataManager.GetInstance().MethodForecastRepository.Select(method.Id);
            if (methDetails != null)
            {
                uc = new UCMethodForecast();
                ((UCMethodForecast)uc).MethodForecast = (MethodForecast)methDetails;
                uc.Tag = methDetails;
                return uc;
            }
            if (createUCIfNotExists)
            {
                uc = new UCMethodForecast();
                ((UCMethodForecast)uc).MethodForecast = new MethodForecast() { Method = new Method() { Id = method.Id } };
            }
            return uc;
        }
        private UserControl GetMethodDetailsClimateUC(Method method, bool createUCIfNotExists)
        {
            UserControl uc = null;
            object methDetails = DataManager.GetInstance().MethodClimateRepository.Select(method.Id);
            if (methDetails != null)
            {
                uc = new UCMethodClimate();
                ((UCMethodClimate)uc).MethodClimate = (MethodClimate)methDetails;
                uc.Tag = methDetails;
                return uc;
            }
            if (createUCIfNotExists)
            {
                uc = new UCMethodClimate();
                ((UCMethodClimate)uc).MethodClimate = new MethodClimate() { MethodId = method.Id };
            }
            return uc;
        }

        public void Clear()
        {
            ClearMethodDetails();
            foreach (var control in this.Controls)
            {
                if (control.GetType() == typeof(System.Windows.Forms.TextBox)) ((System.Windows.Forms.TextBox)control).Text = null;
                if (control.GetType() == typeof(System.Windows.Forms.ComboBox)) ((System.Windows.Forms.ComboBox)control).DataSource = null;
            }
        }
        public void ClearMethodDetails()
        {
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2Collapsed = true;
            methodDetailsTypeComboBox.SelectedIndex = 0;
        }

        public int SaveMethod()
        {
            Method meth = Method;
            if (meth.Id < 1)
            {
                DataManager.GetInstance().MethodRepository.Insert(meth);
                idTextBox.Text = meth.Id.ToString();
            }
            else
            {
                DataManager.GetInstance().MethodRepository.Update(meth);

                if (splitContainer1.Panel2.Controls.Count > 0)
                {
                    Control ucMethodDetails = splitContainer1.Panel2.Controls[0];
                    if (ucMethodDetails.GetType() == typeof(UCMethodClimate))
                    {
                        ((UCMethodClimate)ucMethodDetails).Save();
                    }
                    if (ucMethodDetails.GetType() == typeof(UCMethodForecast))
                    {
                        ((UCMethodForecast)ucMethodDetails).Save();
                    }
                    else
                    {
                        MessageBox.Show(this + ": детали метода для неизвестного типа " + splitContainer1.Panel2.GetType() + " сохранить не удалось.");
                    }
                }
            }

            return meth.Id;
        }

        private void ucLESource_UCEditButtonPressedEvent()
        {
            Social.FormSelectLegalEntities formSelectLegalEntities = new Social.FormSelectLegalEntities(Social.Enums.LegalEntityType.Organization);
            formSelectLegalEntities.StartPosition = FormStartPosition.CenterScreen;
            if (formSelectLegalEntities.ShowDialog() == DialogResult.OK)
            {
                ucLESource.Value = formSelectLegalEntities.LegalEntitySelected;
            }
        }

        private void parentMethodTextBox_UCEditButtonPressedEvent()
        {
            Common.FormTree formSelectParentMethod = new Common.FormTree("Выбор метода-родителя");
            formSelectParentMethod.StartPosition = FormStartPosition.CenterScreen;

            formSelectParentMethod.UCTree.AddRange(DataManager.GetInstance().MethodRepository.Select()
                .OrderBy(x => x.Name)
                .ToArray()
                );

            if (formSelectParentMethod.ShowDialog() == DialogResult.OK)
            {
                ucParentMethod.Value = (Method)formSelectParentMethod.UCTree.SelectedItem;
            }
        }

        private void methodDetailsTypeComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (_isFilled) return;

            // ПЕРЕКЛЮЧЕНИЕ ДЕТАЛЕЙ МЕТОДА
            int methodTypeId = ((Common.IdName)methodDetailsTypeComboBox.SelectedItem).Id;
            switch (((Common.IdName)methodDetailsTypeComboBox.SelectedItem).Id)
            {
                case 1: // Нет
                    ClearMethodDetails();
                    break;
                case 2:
                case 3:
                    FillMethodDetails(Method, methodTypeId);
                    break;
                default:
                    MessageBox.Show("Неизвестны детали метода типа  " + methodTypeId);
                    break;
            }
        }

        private void deleteMethodDetailsButton_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2.Controls.Count > 0)
            {
                Control ucMethodDetails = splitContainer1.Panel2.Controls[0];
                if (ucMethodDetails.GetType() == typeof(UCMethodClimate))
                {
                    DataManager.GetInstance().MethodClimateRepository.Delete(((UCMethodClimate)ucMethodDetails).MethodClimate.MethodId);
                }
                if (ucMethodDetails.GetType() == typeof(UCMethodForecast))
                {
                    DataManager.GetInstance().MethodForecastRepository.Delete(((UCMethodForecast)ucMethodDetails).MethodForecast.Method.Id);
                }
                else
                {
                    MessageBox.Show(this + ": детали метода для неизвестного типа " + splitContainer1.Panel2.GetType() + " elfkbnm не удалось.");
                }
            }
        }
    }
}
