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
    }
}
