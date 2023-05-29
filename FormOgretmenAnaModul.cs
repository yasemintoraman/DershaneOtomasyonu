using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DershaneOtomasyonu
{
    public partial class FormOgretmenAnaModul : Form
    {
        public FormOgretmenAnaModul()
        {
            InitializeComponent();
        }
        FormNotGiris frm4;

        public string kullaniciTC;

        private void BtnNotGiris_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frm4 == null || frm4.IsDisposed)
            {
                frm4 = new FormNotGiris();
                frm4.TC = kullaniciTC; //ekledik
                frm4.MdiParent = this;
                frm4.Show();
            }
        }
    }
}
