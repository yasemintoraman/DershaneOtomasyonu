using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DershaneOtomasyonu
{
    public partial class FormGiris : Form
    {
        public FormGiris()
        {
            InitializeComponent();
        }

        mysqlbaglantisi bgl = new mysqlbaglantisi();

        private void btnYonetici_Click(object sender, EventArgs e)
        {
            MySqlCommand komut = new MySqlCommand("Select ogrt_tc,OGRT_SIFRE from TBL_AYARLAR inner join tbl_ogretmenler on tbl_ayarlar.ayarlarogrid= tbl_ogretmenler.ogr_id where ogrt_tc=@p1 and OGRT_SIFRE=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTC.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            MySqlDataReader dr = komut.ExecuteReader();
            if(dr.Read())
            {
                FormAnaModul frm1 = new FormAnaModul();
                frm1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı veya Şifre");
                mskTC.Text = "";
                txtSifre.Text = "";
            }
            bgl.baglanti().Close();
        }
    }

}
