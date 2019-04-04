using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SOV.Common
{
    public partial class UCImage : UserControl
    {
        private const int IMAGE_WIDTH = 200;
        private const int IMAGE_HEIGHT = 150;

        private bool _selected = false;

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                SwitchBorder();
            }
        }

        public delegate void OnImageClick(UCImage sender);
        private OnImageClick onImageClick = null;

        public byte[] ImgBytes { get; private set; }

        public UCImage()
        {
            InitializeComponent();
        }

        public UCImage(byte[] imgBytes, OnImageClick onImageClick, bool fullSize = false,
                        int width = IMAGE_WIDTH, int height = IMAGE_HEIGHT)
            : this()
        {
            Init(imgBytes, onImageClick, fullSize, width, height);
        }

        public void Init(byte[] imgBytes, OnImageClick onImageClick, bool fullSize = false,
                        int width = IMAGE_WIDTH, int height = IMAGE_HEIGHT)
        {
            this.onImageClick = onImageClick;
            this.ImgBytes = imgBytes;
            var bitmap = new Bitmap(new MemoryStream(imgBytes));
            if (fullSize)
                image.Image = bitmap;
            else
            {
                image.Image = bitmap.GetThumbnailImage(width, height, null, IntPtr.Zero);
                bitmap.Dispose();
            }
            Width = image.Image.Width;
            Height = image.Image.Height;
        }

        public void SwitchBorder()
        {
            if (Selected)
            {
                image.BackColor = Color.Red;
                image.Padding = new Padding(2);
            }
            else
            {
                image.BackColor = Color.Transparent;
                image.Padding = new Padding(0);
            }
            //image.BorderStyle = Selected ? BorderStyle.FixedSingle : BorderStyle.None;        
        }

        private void image_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left: 
                    if (onImageClick != null)
                        onImageClick(this); 
                    break;
                case MouseButtons.Right:
                    var uc = new UCImage(ImgBytes, null, true);
                    new FormSingleUC(uc, "Полное изображение", uc.Width, uc.Height).Show();
                    break;
            }
        }
    }
}
