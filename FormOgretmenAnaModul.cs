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

        FormNotGiris frm5;

        //Şuan sadece kullanıcı TC aldık
        public string kullaniciTC;

        private void btnNotGiris_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (frm5 == null || frm5.IsDisposed)
            {
                frm5 = new FormNotGiris();
                frm5.TC = kullaniciTC; //ekledik
                frm5.MdiParent = this;
                frm5.Show();
            }

        }
    }
}
