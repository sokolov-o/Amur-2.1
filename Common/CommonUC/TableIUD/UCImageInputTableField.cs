using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SOV.Common.TableIUD
{
    public partial class UCImageInputTableField : UCTableField
    {
        public UCImageInputTableField(TableField field) : base(field)
        {
            InitializeComponent();
            this.ucInput = pictureBox;
        }

        private void view_Click(object sender, EventArgs e)
        {
            fileDialog.InitialDirectory = "c:\\";

            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            try
            {
                using (var stream = fileDialog.OpenFile())
                {
                    using (BinaryReader pgReader = new BinaryReader(new BufferedStream(stream)))
                    {
                        var bytes = pgReader.ReadBytes(Convert.ToInt32(stream.Length));
                        pictureBox.Init(bytes, null);
                    }
                    textBox.Text = fileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при чтении файла. Подробнее: " + ex.Message);
            }
        }

        public override KeyValuePair<string, object> NameAndVal()
        {
            return new KeyValuePair<string, object>(Field.db, pictureBox.ImgBytes);
        }
    }
}
