using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Amur.Meta
{
    public partial class FormGeoObject : Form
    {
        public FormGeoObject()
        {
            InitializeComponent();

            IsCloseAfterSave = true;
        }

        public GeoObject GeoObject
        {
            get
            {
                return ucGeoObject.GeoObject;
            }
            set
            {
                ucGeoObject.GeoObject = value;
                if (value.Id < 1)
                    Text = "Новый геообъект";
                else
                    Text = "Геообъект #" + value.Id;
            }
        }

        //////static public DialogResult ShowDialogNew()
        //////{
        //////    FormGeoObject frm = new FormGeoObject();
        //////    frm.ucGeoObject.GeoObject = new GeoObject(-1, -1, null);
        //////    frm.StartPosition = FormStartPosition.CenterParent;

        //////    return frm.ShowDialog();
        //////}
        //////static public DialogResult ShowDialogUpdate(int goId)
        //////{
        //////    return ShowDialogUpdate(Meta.DataManager.GetInstance().GeoObjectRepository.Select(goId));
        //////}
        //////static public DialogResult ShowDialogUpdate(GeoObject go)
        //////{
        //////    if (go != null)
        //////    {
        //////        FormGeoObject frm = new FormGeoObject();
        //////        frm.ucGeoObject.GeoObject = go;
        //////        frm.StartPosition = FormStartPosition.CenterParent;

        //////        return frm.ShowDialog();
        //////    }
        //////    return DialogResult.Cancel;
        //////}
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    if (ucGeoObject1.GeoObject != null)
        //    {
        //        if (ucGeoObject1.GeoObject.Id > 0)
        //        {
        //            Meta.DataManager.GetInstance().GeoObjectRepository.Update(ucGeoObject1.GeoObject);
        //        }
        //        else
        //        {
        //            Meta.DataManager.GetInstance().GeoObjectRepository.Insert(ucGeoObject1.GeoObject);
        //        }
        //    }
        //    Close();
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        bool _IsCloseAfterSave;
        public bool IsCloseAfterSave
        {
            get
            {
                return _IsCloseAfterSave;
            }
            set
            {
                _IsCloseAfterSave = value;
                if (_IsCloseAfterSave)
                {
                    button3.Visible = false;
                    button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    button2.Text = "Отменить";
                }
                else
                {
                    button3.Visible = true;
                    button2.DialogResult = System.Windows.Forms.DialogResult.None;
                    button2.Text = "Закрыть";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                GeoObject go = ucGeoObject.GeoObject;
                if (go != null)
                {
                    if (go.Id < 1)
                    {
                        go.Id = Meta.DataManager.GetInstance().GeoObjectRepository.Insert(go);
                    }
                    else
                    {
                        Meta.DataManager.GetInstance().GeoObjectRepository.Update(go);
                    }
                }
                ucGeoObject.GeoObject = go;

                if (_IsCloseAfterSave)
                    Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GeoObject go = ucGeoObject.GeoObject;
            go.Id = -1;
            ucGeoObject.GeoObject = go;
        }

        private void FormGeoObject_Load(object sender, EventArgs e)
        {
            ucGeoObject.Focus();
        }
    }
}
