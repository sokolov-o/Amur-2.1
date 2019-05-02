using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Social
{
    public partial class UCLegalEntity : UserControl
    {
        public UCLegalEntity()
        {
            InitializeComponent();
        }
        Addr _Addr;
        Addr Addr
        {
            get
            {
                return _Addr;
            }
            set
            {
                addrAddTextBox.Text = (value == null) ? null : value.Name;
                _Addr = value;
            }
        }
        LegalEntity _ParentLE;
        LegalEntity ParentLE
        {
            get
            {
                return _ParentLE;
            }
            set
            {
                parentNameRusShortTextBox.Text = (value == null) ? null : value.NameRusShort;
                _ParentLE = value;
            }
        }
        public LegalEntity LegalEntity
        {
            set
            {
                Clear();

                if (value != null)
                {
                    idTextBox.Text = value.Id.ToString();
                    typeTextBox.Text = value.Type == ' ' ? null : value.Type.ToString();
                    nameRusTextBox.Text = value.NameRus;
                    nameRusShortTextBox.Text = value.NameRusShort;
                    nameEngTextBox.Text = value.NameEng;
                    nameEngShortTextBox.Text = value.NameEngShort;
                    addrAddTextBox.Text = value.AddrAdd;
                    phonesTextBox.Text = value.Phones;
                    emailTextBox.Text = value.Email;
                    weblinkTextBox.Text = value.WebSite;

                    Addr = (!value.AddrId.HasValue) ? null : DataManager.GetInstance().AddrRepository.Select((int)value.AddrId);
                    ParentLE = (!value.ParentId.HasValue) ? null : DataManager.GetInstance().LegalEntityRepository.Select((int)value.ParentId);
                }
            }
            get
            {
                if (!string.IsNullOrEmpty(idTextBox.Text) || !string.IsNullOrEmpty(typeTextBox.Text))
                {
                    int id;
                    if (!int.TryParse(idTextBox.Text, out id)) id = -1;

                    try
                    {
                        return new LegalEntity()
                        {
                            Id = id,
                            NameRus = nameRusTextBox.Text,
                            NameRusShort = nameRusShortTextBox.Text,
                            NameEng = nameEngTextBox.Text,
                            NameEngShort = nameEngShortTextBox.Text,
                            Email = emailTextBox.Text,
                            Phones = phonesTextBox.Text,
                            WebSite = weblinkTextBox.Text,
                            AddrAdd = addrAddTextBox.Text,
                            Type = typeTextBox.Text.ToCharArray()[0],
                            AddrId = Addr == null ? null : (int?)Addr.Id,
                            ParentId = ParentLE == null ? null : (int?)ParentLE.Id
                        };
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Возможно, заполнены не все обязательные поля формы ввода и редактирования субъекта. Провеоьте ещё раз." +
                            "\n\n" + ex.ToString());
                        return null;
                    }
                }
                return null;
            }
        }
        public void Clear()
        {
            idTextBox.Text =
            typeTextBox.Text =
            nameRusTextBox.Text =
            nameRusShortTextBox.Text =
            nameEngTextBox.Text =
            nameEngShortTextBox.Text =
            addrAddTextBox.Text =
            addrAddTextBox.Text =
            phonesTextBox.Text =
            emailTextBox.Text =
            weblinkTextBox.Text =
            parentNameRusShortTextBox.Text =
            null;

            Addr = null;
            ParentLE = null;
        }

        private void typeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(typeTextBox.Text) && typeTextBox.Text != "o" && typeTextBox.Text != "p")
            {
                MessageBox.Show("Тип субъекта может быть либо o либо p в английском регистре.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                typeTextBox.Text = "o";
            }
        }
        FormSelectLegalEntities _FormSelectLegalEntitiesOrgs;
        FormSelectLegalEntities FormSelectLegalEntitiesOrgs {
            get
            {
                if (_FormSelectLegalEntitiesOrgs == null)
                {
                    _FormSelectLegalEntitiesOrgs = new FormSelectLegalEntities(Enums.LegalEntityType.Organization);
                    _FormSelectLegalEntitiesOrgs.StartPosition = FormStartPosition.CenterScreen;
                }
                return _FormSelectLegalEntitiesOrgs;
            }
            set
            {
                _FormSelectLegalEntitiesOrgs = value;
            }
        }
        private void editParentButton_Click(object sender, EventArgs e)
        {
            if (FormSelectLegalEntitiesOrgs.ShowDialog() == DialogResult.OK)
            {
                ParentLE = FormSelectLegalEntitiesOrgs.LegalEntitySelected;
            }
        }
    }
}
