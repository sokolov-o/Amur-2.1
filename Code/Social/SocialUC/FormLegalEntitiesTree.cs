using SOV.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Social
{
    public partial class FormLegalEntitiesTree : Form
    {
        public FormLegalEntitiesTree()
        {
            InitializeComponent();
        }

        TreeNode _NodeEdited = null;

        private void ucLegalEntitiesTree_UCSelectedNodeChangedEvent(TreeNode node)
        {
            _NodeEdited = node;
            ucStaffs.Clear();
            infoTextBox.Text = null;
            tab.Enabled = false;
            tab.TabPages.Clear();

            if (_NodeEdited != null)
            {
                if (_NodeEdited.Tag.GetType() == typeof(LegalEntity))
                {
                    tab.Enabled = true;
                    LegalEntity le = (LegalEntity)_NodeEdited.Tag;
                    if (le.Type == 'p')
                    {
                        TabPage tp = _tabPages.Find(x => x.Name == "staffEmployeeTabPage");
                        //tp.Text = "Должности сотрудника";
                        tab.TabPages.Add(tp);

                        ucStaffEmployees1.Fill(DefaultOrganization, null, null, new List<int>() { le.Id }, DateActual);
                    }
                    else if (le.Type == 'o')
                    {
                        TabPage tp = _tabPages.Find(x => x.Name == "staffTabPage");
                        //tp.Text = "Штатное расписание";
                        tab.TabPages.Add(tp);
                        tab.TabPages.Add(_tabPages.Find(x => x.Name == "orgInfoTabPage"));
                        ucStaffs.Fill(le.Id, null, null, DateActual);

                        var socialDM = DataManager.GetInstance();
                        tab.TabPages.Add(_tabPages.Find(x => x.Name == "loadImagesPage"));
                        tab.TabPages.Add(_tabPages.Find(x => x.Name == "imagesPage"));
                        var orgXImages = socialDM.LegalEntityXImageRepository.SelectByOrgs(new List<int>() { le.Id });
                        var allImages = socialDM.ImageRepository.Select();
                        ucImageGallery.ReloadGallery(
                            allImages.Select(elm => new DicItem(elm.Id, "", elm.Img)).ToList(),
                            null
                        );
                    }
                    else
                        throw new Exception("Неизвестный тип субъекта...");

                    ucLegalEntity.LegalEntity = le;
                    infoTextBox.Text = le.ToString();
                }
                else if (_NodeEdited.Tag.GetType() == typeof(Division))
                {
                    tab.Enabled = true;
                    Division div = (Division)_NodeEdited.Tag;
                    TabPage tp = _tabPages.Find(x => x.Name == "staffTabPage");
                    //tp.Text = "Штатное расписание";
                    tab.TabPages.Add(tp);
                    tab.TabPages.Add(_tabPages.Find(x => x.Name == "orgInfoTabPage"));

                    ucStaffs.Fill(div.Employer.Id, new List<int>() { div.Id }, null, DateActual);

                    ucLegalEntity.LegalEntity = div.Employer;
                    infoTextBox.Text = div.ToStringBranch();
                }
            }
        }
        DateTime? DateActual
        {
            get
            {
                return ucLegalEntitiesTree.DateActual;
            }
        }
        private void ucLegalEntitiesTree_UCAddNewLEEvent()
        {
            char type = ucLegalEntitiesTree.LegalEntitySelected == null ? 'p' : ucLegalEntitiesTree.LegalEntitySelected.Type;
            ucLegalEntitiesTree.LegalEntitySelected = null;
            _NodeEdited = null;

            ucLegalEntity.LegalEntity = new LegalEntity() { Id = -1, Type = type };
            ucLegalEntity.Focus();
        }

        public FormSelectLegalEntities FormSelectLegalEntities { get; set; }
        public FormSelectLegalEntities FormSelectLegalEntitiesOrgs { get; set; }
        public FormSelectLegalEntities FormSelectLegalEntitiesPersons { get; set; }
        public int DefaultOrganization { get; private set; }

        private void mnuFileExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                LegalEntity le = ucLegalEntity.LegalEntity;
                if (le != null)
                {
                    LegalEntity le0 = (_NodeEdited == null) ? null : (LegalEntity)_NodeEdited.Tag;

                    if (le0 != null && le0.Id != le.Id)
                        throw new Exception("(le0.Id != le.Id) OSokolov@201710");
                    ImageRepository imgRep = DataManager.GetInstance().ImageRepository;
                    LegalEntityRepository rep = DataManager.GetInstance().LegalEntityRepository;

                    if (rep.Select(le.Id) == null)
                        rep.Insert(le);
                    else
                        rep.Update(le);

                    rep.InsertImages(le.Id, imgRep.Insert(ucImageGalleryLoader.Imgs()));

                    le = rep.Select(le.Id);

                    // Редактирование субъекта
                    if (_NodeEdited != null)
                    {
                        UCLegalEntitiesTree.InitializeNode(_NodeEdited, le);

                        if (le0.ParentId != le.ParentId)
                            ucLegalEntitiesTree.FillByType(null);

                    }
                    // Ввод нового субъекта
                    //else
                    //    ucLegalEntitiesTree.Fill(null);

                    //ucLegalEntitiesTree.Focus();
                    //ucLegalEntitiesTree.LegalEntitySelected = le;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        List<TabPage> _tabPages = new List<TabPage>();

        private void FormLegalEntitiesTree_Load(object sender, EventArgs e)
        {
            foreach (TabPage item in tab.TabPages)
            {
                _tabPages.Add(item);
            }
            tab.TabPages.Clear();

            DefaultOrganization = 777;
        }

        private void ucLegalEntitiesTree_UCRefreshEvent()
        {
            LegalEntity le = ucLegalEntitiesTree.LegalEntitySelected;
            ucLegalEntitiesTree.FillByType(null);
            ucLegalEntitiesTree.LegalEntitySelected = le;
        }

        private void deleteImgButton_Click(object sender, EventArgs e)
        {
            var rep = DataManager.GetInstance().LegalEntityRepository;
            rep.DeleteImages(ucLegalEntity.LegalEntity.Id, ucImageGallery.SelectedIds());
            ucImageGallery.RemoveById(ucImageGallery.SelectedIds());
        }
    }
}
