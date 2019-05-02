using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SOV.Common;
using SOV.Social;
using SOV.Common.TableIUD;

namespace SOV.Amur.Reports
{
    public partial class FormReportOrg : FormReportOrgTableEdit
    {
        public FormReportOrg() : base()
        {
            InitializeComponent();
        }

        public FormReportOrg(List<TableField> fields, TableType type, string tableName, Org obj = null)
            : base(fields, obj, type, tableName)
        {
            InitializeComponent();
            this.tableTitle = Text;
            SubmitForm = submit;
        }

        public override void InitUCFields()
        {
            foreach (var field in Fields)
                AddFieldUCToContainer(field, orgTab, obj);

            var imgField = Fields.Find(x => x.db == "img_id");
            var ucGallery = new UCImageGalleryTableField((ImageGalleryTableField)imgField);
            ucGallery.SetOnChangeEvent(onGallerySelection);
            ucGallery.Dock = DockStyle.Fill;
            imagesTab.Controls.Add(ucGallery);
            ((UCComboBoxTableField)orgTab.Controls.Find("org_id", false)[0]).SetOnChangeEvent(orgComboBox_Change);
            orgComboBox_Change(((UCComboBoxTableField)orgTab.Controls.Find("org_id", false)[0]));
        }

        private void orgComboBox_Change(UCTableField sender)
        {
            var images = new List<DicItem>();
            var selectedImgs = new List<int>();
            if (((UCComboBoxTableField) sender).Selected())
            {
                int orgId = ((UCComboBoxTableField) sender).Id;
                var socialDM = Social.DataManager.GetInstance();
                var orgXImages = socialDM.LegalEntityXImageRepository.SelectByOrgs(new List<int> {orgId})
                    .Select(x => x.ImageId);
                images = socialDM.ImageRepository.Select(orgXImages.ToList())
                    .Select(elm => new DicItem(elm.Id, "", elm.Img)).ToList();
                if (Type == TableType.Update && obj.ImgId.HasValue)
                    selectedImgs.Add(obj.ImgId.Value);
            }
            ((UCImageGalleryTableField)imagesTab.Controls[0]).ReloadGallery(images, selectedImgs);
        }

        private void onGallerySelection(UCTableField sender)
        {
            ((UCImageGalleryTableField)sender).RemoveAllSelection();
        }

        private void submit(FormTableRowEdit<Org> sender) 
        {
            var queryFields = new Dictionary<string, object>();
            foreach (var rowField in orgTab.Controls)
            {
                var nameAndVal = ((UCTableField)rowField).NameAndVal();
                queryFields.Add(nameAndVal.Key, nameAndVal.Value);
            }
            var ids = ((UCImageGalleryTableField)imagesTab.Controls[0]).SelectedIds();
            if (ids.Count == 0)
                queryFields["img_id"] = null;
            else
                queryFields["img_id"] = ids[0];
            if (Type == TableType.Update)
                DataManager.GetInstance().OrgRepository.Update(queryFields);
            else
                DataManager.GetInstance().OrgRepository.Insert(queryFields);
        }
    }

    public class FormReportOrgTableEdit : FormTableRowEdit<Org>
    {
        public FormReportOrgTableEdit() : base() {}
        public FormReportOrgTableEdit(List<TableField> fields, Org obj, TableType type, string tableName)
            : base(fields, type, tableName, obj)
        {
        }
    }
}
