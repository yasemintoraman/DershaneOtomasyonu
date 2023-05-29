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
    public partial class FormOgrenciler : Form
    {
        public FormOgrenciler()
        {
            InitializeComponent();
        }

        mysqlbaglantisi bgl = new mysqlbaglantisi();

        void listele()
        {
            //5.sinif
            DataTable dt1 = new DataTable();
            MySqlDataAdapter da1 = new MySqlDataAdapter("Select * from öğrenci where ogr_sinif = '5.SINIF'", bgl.baglanti());
            da1.Fill(dt1);
            gridControl1.DataSource = dt1;

            //6.sinif
            DataTable dt2 = new DataTable();
            MySqlDataAdapter da2 = new MySqlDataAdapter("Select * from öğrenci where ogr_sinif = '6.SINIF'", bgl.baglanti());
            da2.Fill(dt2);
            gridControl2.DataSource = dt2;

            //7.sinif
            DataTable dt3 = new DataTable();
            MySqlDataAdapter da3 = new MySqlDataAdapter("Select * from öğrenci where ogr_sinif = '7.SINIF'", bgl.baglanti());
            da3.Fill(dt3);
            gridControl3.DataSource = dt3;

            //8.sinif
            DataTable dt4 = new DataTable();
            MySqlDataAdapter da4 = new MySqlDataAdapter("Select * from öğrenci where ogr_sinif = '8.SINIF'", bgl.baglanti());
            da4.Fill(dt4);
            gridControl4.DataSource = dt4;
        }

        void temizle()
        {
            txtID.Text = "";
            mskOgrNo.Text = "";
            cmbSinif.Text = "";
            mskTC.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            mskTelefon.Text = "";
            dateEdit1.Text = "";
            mskYil.Text = "";
        }

        private void FormOgrenciler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            MySqlCommand komut = new MySqlCommand("insert into öğrenci (ogr_no, ogr_sinif, tc, ad, soyad, telefon, dogum_tarihi, kayit_yili) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskOgrNo.Text);
            komut.Parameters.AddWithValue("@p2", cmbSinif.Text);
            komut.Parameters.AddWithValue("@p3", mskTC.Text);
            komut.Parameters.AddWithValue("@p4", txtAd.Text);
            komut.Parameters.AddWithValue("@p5", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p6", mskTelefon.Text);
            komut.Parameters.AddWithValue("@p7", dateEdit1.Text);
            komut.Parameters.AddWithValue("@p8", mskYil.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Öğrenci Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["id"].ToString();
                mskOgrNo.Text = dr["ogr_no"].ToString();
                cmbSinif.Text = dr["ogr_sinif"].ToString();
                mskTC.Text = dr["tc"].ToString();
                txtAd.Text = dr["ad"].ToString();
                txtSoyad.Text = dr["soyad"].ToString();
                mskTelefon.Text = dr["telefon"].ToString();
                dateEdit1.Text = dr["dogum_tarihi"].ToString();
                mskYil.Text = dr["kayit_yili"].ToString();
            }
        }

        private void gridView2_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["id"].ToString();
                mskOgrNo.Text = dr["ogr_no"].ToString();
                cmbSinif.Text = dr["ogr_sinif"].ToString();
                mskTC.Text = dr["tc"].ToString();
                txtAd.Text = dr["ad"].ToString();
                txtSoyad.Text = dr["soyad"].ToString();
                mskTelefon.Text = dr["telefon"].ToString();
                dateEdit1.Text = dr["dogum_tarihi"].ToString();
                mskYil.Text = dr["kayit_yili"].ToString();

            }

        }

        private void gridView3_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView3.GetDataRow(gridView3.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["id"].ToString();
                mskOgrNo.Text = dr["ogr_no"].ToString();
                cmbSinif.Text = dr["ogr_sinif"].ToString();
                mskTC.Text = dr["tc"].ToString();
                txtAd.Text = dr["ad"].ToString();
                txtSoyad.Text = dr["soyad"].ToString();
                mskTelefon.Text = dr["telefon"].ToString();
                dateEdit1.Text = dr["dogum_tarihi"].ToString();
                mskYil.Text = dr["kayit_yili"].ToString();

            }

        }

        private void gridView4_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView4.GetDataRow(gridView4.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["id"].ToString();
                mskOgrNo.Text = dr["ogr_no"].ToString();
                cmbSinif.Text = dr["ogr_sinif"].ToString();
                mskTC.Text = dr["tc"].ToString();
                txtAd.Text = dr["ad"].ToString();
                txtSoyad.Text = dr["soyad"].ToString();
                mskTelefon.Text = dr["telefon"].ToString();
                dateEdit1.Text = dr["dogum_tarihi"].ToString();
                mskYil.Text = dr["kayit_yili"].ToString();

            }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            MySqlCommand komut = new MySqlCommand("Update öğrenci set ogr_no=@p1, ogr_sinif=@p2,tc=@p3,ad=@p4,soyad=@p5,telefon=@p6,dogum_tarihi=@p7,kayit_yili=@p8 where id=@p9", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskOgrNo.Text);
            komut.Parameters.AddWithValue("@p2", cmbSinif.Text);
            komut.Parameters.AddWithValue("@p3", mskTC.Text);
            komut.Parameters.AddWithValue("@p4", txtAd.Text);
            komut.Parameters.AddWithValue("@p5", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p6", mskTelefon.Text);
            komut.Parameters.AddWithValue("@p7", dateEdit1.Text);
            komut.Parameters.AddWithValue("@p8", mskYil.Text);
            komut.Parameters.AddWithValue("@p9", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Öğrenci Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            MySqlCommand komut = new MySqlCommand("Delete from öğrenci where id=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Öğrenci Bilgileri Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
