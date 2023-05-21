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
    public partial class FormAnaModul : Form
    {
        public FormAnaModul()
        {
            InitializeComponent();
        }

        FormOgretmenler frm1; //formogretmenler bir sinif old. icin erisip cagiracagiz
        FormOgrenciler frm2;
        FormAyarlar frm3;

        private void btnOgretmen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //eger aciksa formu tekrar acmamasi ve kapattiysak tekrar bastigimizda tekrar acmasi icin bu if blogunu yazdik
            if (frm1 == null || frm1.IsDisposed)
            { 
                frm1 = new FormOgretmenler(); //frm1 nesnesiyle FormOgretmenler sinifina ulastik
                frm1.MdiParent = this; //bu forma aktarilmasini sagladik
                frm1.Show();
            }
        }

        private void btnOgrenciler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(frm2 == null || frm2.IsDisposed)
            {
                frm2 = new FormOgrenciler();
                frm2.MdiParent = this;
                frm2.Show(); 
            }
        }
    }
}
