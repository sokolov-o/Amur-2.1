using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SOV.Common
{
    public partial class UCImageGalleryLoader : UserControl
    {
        public UCImageGalleryLoader()
        {
            InitializeComponent();
        }

        public List<byte[]> Imgs()
        {
            List<byte[]> res = new List<byte[]>();
            foreach (var img in imgPanel.Controls)
                res.Add(((UCImage)img).ImgBytes);
            return res;
        }

        private void viewFilesButton_Click(object sender, System.EventArgs e)
        {
            Stream stream;
            ImageConverter converter = new ImageConverter();
            fileDialog.InitialDirectory = "c:\\";
            imgPanel.Controls.Clear();
            fileNamesInput.Text = "";
            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            try
            {
                foreach (String file in fileDialog.FileNames)
                    if((stream = fileDialog.OpenFile()) != null)
                        using(stream)
                        {
                            using (Bitmap myBitmap = new Bitmap(file))
                            {
                                var bytes = (byte[])converter.ConvertTo(myBitmap, typeof(byte[]));
                                var uc = new UCImage(bytes, null, false, 50, 50);
                                imgPanel.Controls.Add(uc);
                            }
                            fileNamesInput.Text += file + "; ";
                        }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при чтении файлов. Подробнее: " + ex.Message);
            }
        }
    }
}
