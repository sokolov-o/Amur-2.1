using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOV.Common
{
    public partial class UCIdNames : UserControl
    {
        public UCIdNames()
        {
            InitializeComponent();

            //_idRowHeight = tlp.RowStyles[0].Height;
        }
        int Id { get; set; }
        public IdNames IdNames
        {
            set
            {
                if (value == null) value = new Common.IdNames();

                Id = value.Id;
                nameRusTextBox.Text = value.NameRus;
                nameRusShortTextBox.Text = value.NameRusShort;
                nameEngTextBox.Text = value.NameEng;
                nameEngShortTextBox.Text = value.NameEngShort;
            }
            get
            {
                return new IdNames()
                {
                    Id = Id,
                    NameRus = nameRusTextBox.Text,
                    NameRusShort = nameRusShortTextBox.Text,
                    NameEng = nameEngTextBox.Text,
                    NameEngShort = nameEngShortTextBox.Text
                };
            }
        }
        //float _idRowHeight;
        //public bool ShowId
        //{
        //    get
        //    {
        //        return tlp.RowStyles[0].SizeType == SizeType.AutoSize ? true : false;
        //    }
        //    set
        //    {
        //        if (value)
        //        {
        //            tlp.RowStyles[0].SizeType = SizeType.AutoSize;
        //            tlp.RowStyles[0].Height = _idRowHeight;
        //        }
        //        else
        //        {
        //            tlp.RowStyles[0].SizeType = SizeType.Absolute;
        //            tlp.RowStyles[0].Height = 0;
        //        }
        //    }
        //}

        private void UCIdNames_Load(object sender, EventArgs e)
        {
            //ShowId = false;
        }
    }
}
