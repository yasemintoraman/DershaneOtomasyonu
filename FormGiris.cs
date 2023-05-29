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
            MySqlCommand komut = new MySqlCommand("Select OGRTTC,OGRT_SIFRE from tbl_ayarlar inner join tbl_ogretmenler on tbl_ayarlar.AYARLAROGRID= tbl_ogretmenler.ogr_id where ogrt_tc=@p1 and OGRT_SIFRE=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTC.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            MySqlDataReader dr = komut.ExecuteReader();
            if(dr.Read())
            {
                FormAnaModul frm1 = new FormAnaModul();
                frm1.kullaniciTC = mskTC.Text;
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

        private void btnOgretmen_Click(object sender, EventArgs e)
        {
            MySqlCommand komut = new MySqlCommand("Select OGRTTC,OGRT_SIFRE from tbl_ayarlar inner join tbl_ogretmenler on tbl_ayarlar.AYARLAROGRID= tbl_ogretmenler.ogr_id where ogrt_tc=@p1 and OGRT_SIFRE=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTC.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            MySqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FormOgretmenAnaModul frm2 = new FormOgretmenAnaModul();
                frm2.kullaniciTC = mskTC.Text;
                frm2.Show();
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

        private void btnOgrenci_Click(object sender, EventArgs e)
        {
            MySqlCommand komut = new MySqlCommand("Select tc,ogrn_sifre from tbl_ogrnayarlar inner join öğrenci on tbl_ogrnayarlar.ayarlar_ogrnid= öğrenci.id where tc=@p1 and ogrn_sifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTC.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            MySqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FormOgrenciAnaModul frm3 = new FormOgrenciAnaModul();
                frm3.OgrTC = mskTC.Text;
                frm3.Show();
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
