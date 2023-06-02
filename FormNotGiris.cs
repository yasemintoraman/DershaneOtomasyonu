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
    public partial class FormNotGiris : Form
    {
        public FormNotGiris()
        {
            InitializeComponent();
        }

        mysqlbaglantisi bgl = new mysqlbaglantisi();

     
        public string TC;

        void temizle()
        {
            TxtID.Text = "";
            CmbSinif.Text = "";
            lookUpEdit1.Text = "";
            LblTC.Text = "";
            TxtSinav1.Text = "";
            TxtSinav2.Text = "";
            TxtOrtalama.Text = "";
            durum = false;
        }

        void ogrtgetir()
        {
            MySqlCommand komut = new MySqlCommand("Select ad, soyad, brans from tbl_ogretmenler where ogrt_tc = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TC); 

            MySqlDataReader dr7 = komut.ExecuteReader();
            if (dr7.Read())
            {
                string ad = dr7.GetString(0);
                string soyad = dr7.GetString(1);
                string brans = dr7.GetString(2);

                LblOgrtAdSoyad.Text = ad + " " + soyad;
                LblOgrtBrans.Text = brans;
            }

            dr7.Close();
        }

        void ogrgetir() {
            MySqlCommand komut = new MySqlCommand("Select id, CONCAT(öğrenci.ad, ' ', öğrenci.soyad) AS adsoyad,tc from öğrenci where ogr_sinif = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbSinif.Text);

            MySqlDataReader dr = komut.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(dr);

            lookUpEdit1.Properties.ValueMember = "id";
            lookUpEdit1.Properties.DisplayMember = "adsoyad";
            lookUpEdit1.Properties.NullText = "Öğrenci Seçiniz";
            lookUpEdit1.Properties.DataSource = dataTable;
        }


        void ogrnotlistele()
        {
            MySqlCommand komut = new MySqlCommand("SELECT * FROM tbl_notlar WHERE NOTBRANS = @p1 AND NOTSINIF = '5.SINIF'", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblOgrtBrans.Text);

            DataTable dt1 = new DataTable();
            MySqlDataAdapter da1 = new MySqlDataAdapter(komut);
            da1.Fill(dt1);
            gridControl1.DataSource = dt1;

            MySqlCommand komut2 = new MySqlCommand("SELECT * FROM tbl_notlar WHERE NOTBRANS = @p1 AND NOTSINIF = '6.SINIF'", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", LblOgrtBrans.Text);

            DataTable dt2 = new DataTable();
            MySqlDataAdapter da2 = new MySqlDataAdapter(komut2);
            da2.Fill(dt2);
            gridControl2.DataSource = dt2;


            MySqlCommand komut3 = new MySqlCommand("SELECT * FROM tbl_notlar WHERE NOTBRANS = @p1 AND NOTSINIF = '7.SINIF'", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", LblOgrtBrans.Text);

            DataTable dt3 = new DataTable();
            MySqlDataAdapter da3 = new MySqlDataAdapter(komut3);
            da3.Fill(dt3);
            gridControl3.DataSource = dt3;


            MySqlCommand komut4 = new MySqlCommand("SELECT * FROM tbl_notlar WHERE NOTBRANS = @p1 AND NOTSINIF = '8.SINIF'", bgl.baglanti());
            komut4.Parameters.AddWithValue("@p1", LblOgrtBrans.Text);

            DataTable dt4 = new DataTable();
            MySqlDataAdapter da4 = new MySqlDataAdapter(komut4);
            da4.Fill(dt4);
            gridControl4.DataSource = dt4;
        }

        void mudurnotlistele()
        {
            MySqlCommand komut5 = new MySqlCommand("SELECT * FROM tbl_notlar WHERE NOTSINIF = '5.SINIF'", bgl.baglanti());
            DataTable dt5 = new DataTable();
            MySqlDataAdapter da5 = new MySqlDataAdapter(komut5);
            da5.Fill(dt5);
            gridControl1.DataSource = dt5;

            MySqlCommand komut6 = new MySqlCommand("SELECT * FROM tbl_notlar WHERE NOTSINIF = '6.SINIF'", bgl.baglanti());
            DataTable dt6 = new DataTable();
            MySqlDataAdapter da6 = new MySqlDataAdapter(komut6);
            da6.Fill(dt6);
            gridControl2.DataSource = dt6;


            MySqlCommand komut7 = new MySqlCommand("SELECT * FROM tbl_notlar WHERE NOTSINIF = '7.SINIF'", bgl.baglanti());
            DataTable dt7 = new DataTable();
            MySqlDataAdapter da7 = new MySqlDataAdapter(komut7);
            da7.Fill(dt7);
            gridControl3.DataSource = dt7;


            MySqlCommand komut8 = new MySqlCommand("SELECT * FROM tbl_notlar WHERE NOTSINIF = '8.SINIF'", bgl.baglanti());
            DataTable dt8 = new DataTable();
            MySqlDataAdapter da8 = new MySqlDataAdapter(komut8);
            da8.Fill(dt8);
            gridControl4.DataSource = dt8;
        }

        private void FormNotGiris_Load(object sender, EventArgs e)
        {
            LblOgrtTC.Text = TC;
            ogrtgetir();

            if (LblOgrtBrans.Text == "Müdür" || LblOgrtBrans.Text == "Müdür Yardımcısı")
            {
                mudurnotlistele();
                MySqlCommand komut9 = new MySqlCommand("SELECT BRANSID,BRANSAD from tbl_branslar", bgl.baglanti());
                MySqlDataAdapter da9 = new MySqlDataAdapter(komut9);
                DataTable dt9 = new DataTable();
                da9.Fill(dt9);

                CmbBrans.DataSource = dt9;
                CmbBrans.DisplayMember = "BRANSAD";
                CmbBrans.ValueMember = "BRANSID";

            }
            else
            {
                CmbBrans.Text = LblOgrtBrans.Text;
                CmbBrans.Enabled = false;
                ogrnotlistele();
  

            }
            temizle();
        }


        private void CmbSinif_SelectedIndexChanged(object sender, EventArgs e)
        {
            ogrgetir();
        }



        double dogrusayisi, yanlissayisi, toplamnet;

        //Araçlara Veri Aktarma
        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {


            TxtID.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NOTID").ToString();
            CmbSinif.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NOTSINIF").ToString();
            lookUpEdit1.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NOTADSOYAD").ToString();
            LblTC.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NOTTC").ToString();
            CmbBrans.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NOTBRANS").ToString();
            TxtSinav1.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DOGRUSAYİSİ").ToString();
            TxtSinav2.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "YANLİSSAYİSİ").ToString();
            // Ortalama ve Geçti kaldı kısmına gerek yok, tekrar hesaplansın
            TxtOrtalama.Text = "";
            durum = false;


        }

        private void gridView2_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            TxtID.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "NOTID").ToString();
            CmbSinif.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "NOTSINIF").ToString();
            lookUpEdit1.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "NOTADSOYAD").ToString();
            LblTC.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "NOTTC").ToString();
            CmbBrans.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "NOTBRANS").ToString();
            TxtSinav1.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "DOGRUSAYİSİ").ToString();
            TxtSinav2.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "YANLİSSAYİSİ").ToString();
            // Ortalama ve Geçti kaldı kısmına gerek yok, tekrar hesaplansın
            TxtOrtalama.Text = "";
            durum = false;
        }

        private void gridView3_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            TxtID.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "NOTID").ToString();
            CmbSinif.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "NOTSINIF").ToString();
            lookUpEdit1.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "NOTADSOYAD").ToString();
            LblTC.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "NOTTC").ToString();
            CmbBrans.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "NOTBRANS").ToString();
            TxtSinav1.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "DOGRUSAYİSİ").ToString();
            TxtSinav2.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "YANLİSSAYİSİ").ToString();
            // Ortalama ve Geçti kaldı kısmına gerek yok, tekrar hesaplansın
            TxtOrtalama.Text = "";
            durum = false;
        }

        private void gridView4_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            TxtID.Text = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "NOTID").ToString();
            CmbSinif.Text = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "NOTSINIF").ToString();
            lookUpEdit1.Text = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "NOTADSOYAD").ToString();
            LblTC.Text = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "NOTTC").ToString();
            CmbBrans.Text = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "NOTBRANS").ToString();
            TxtSinav1.Text = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "DOGRUSAYİSİ").ToString();
            TxtSinav2.Text = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "YANLİSSAYİSİ").ToString();
            // Ortalama ve Geçti kaldı kısmına gerek yok, tekrar hesaplansın
            TxtOrtalama.Text = "";
            durum = false;
        }


        //ders netleri guncelleme
        void guncelle()
        {
            int id = Convert.ToInt32(TxtID.Text);
            byte dogrusayisi = Convert.ToByte(TxtSinav1.Text);
            byte yanlissayisi = Convert.ToByte(TxtSinav2.Text);
            decimal toplamnet = Convert.ToDecimal(TxtOrtalama.Text);


            MySqlCommand command = new MySqlCommand("UPDATE TBL_NOTLAR SET DOGRUSAYİSİ = @dogrusayisi, YANLİSSAYİSİ = @yanlissayisi, TOPLAMNET = @toplamnet WHERE NOTID = @id", bgl.baglanti());

            command.Parameters.AddWithValue("@dogrusayisi", dogrusayisi);
            command.Parameters.AddWithValue("@yanlissayisi", yanlissayisi);
            command.Parameters.AddWithValue("@toplamnet", toplamnet);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }


        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            if (LblOgrtBrans.Text == "MÜDÜR" || LblOgrtBrans.Text == "MÜDÜR YARDIMCISI")
            {

                guncelle();
                mudurnotlistele();

            }
            else
            {

                guncelle();
                ogrnotlistele();
            }

        }
        //secilen ad ve soyada ait ögrencinin TC numarasını goruntuler
        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            string selectedAdSoyad = lookUpEdit1.Text;
            MySqlCommand command = new MySqlCommand("SELECT tc FROM öğrenci WHERE CONCAT(öğrenci.ad, ' ', öğrenci.soyad) = @adSoyad", bgl.baglanti());
            command.Parameters.AddWithValue("@adSoyad", selectedAdSoyad);

            string ogrenciTC = command.ExecuteScalar()?.ToString();

            LblTC.Text = ogrenciTC;
        }



        private void BtnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TxtID.Text);
            MySqlCommand command2 = new MySqlCommand("Delete FROM tbl_notlar WHERE NOTID = @id", bgl.baglanti());
            command2.Parameters.AddWithValue("@id", id);

            command2.ExecuteNonQuery();
            mudurnotlistele();

        }

        //not kaydetme
        void kaydet()
        {
            string tc = LblTC.Text;
            string brans = CmbBrans.Text;

            MySqlCommand kmt = new MySqlCommand("SELECT COUNT(*) FROM TBL_NOTLAR WHERE NOTTC = @p1 AND NOTBRANS = @p2", bgl.baglanti());

            kmt.Parameters.AddWithValue("@p1", tc);
            kmt.Parameters.AddWithValue("@p2", brans);

            int count = Convert.ToInt32(kmt.ExecuteScalar());

            if (count > 0)
            {
                MessageBox.Show("Öğrencinin " + brans + " net bilgileri vardır");
            }
            else
            {
                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO TBL_NOTLAR (NOTID,NOTSINIF, NOTADSOYAD, NOTTC, NOTBRANS, DOGRUSAYİSİ, YANLİSSAYİSİ, TOPLAMNET) VALUES (@notid,@sinif, @adsoyad, @tc, @brans, @dogrusayisi, @yanlissayisi, @toplamnet)", bgl.baglanti());
                insertCommand.Parameters.AddWithValue("@notid", TxtID.Text);
                insertCommand.Parameters.AddWithValue("@sinif", CmbSinif.Text);
                insertCommand.Parameters.AddWithValue("@adsoyad", lookUpEdit1.Text);
                insertCommand.Parameters.AddWithValue("@tc", tc);
                insertCommand.Parameters.AddWithValue("@brans", CmbBrans.Text);
                insertCommand.Parameters.AddWithValue("@dogrusayisi", Convert.ToByte(TxtSinav1.Text));
                insertCommand.Parameters.AddWithValue("@yanlissayisi", Convert.ToByte(TxtSinav2.Text));
        
                insertCommand.Parameters.AddWithValue("@toplamnet", Convert.ToDecimal(TxtOrtalama.Text));

                insertCommand.ExecuteNonQuery();
            }
        }
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (LblOgrtBrans.Text == "MÜDÜR" || LblOgrtBrans.Text == "MÜDÜR YARDIMCISI")
            {

                kaydet();
                mudurnotlistele();

            }
            else
            {

                kaydet();
                ogrnotlistele();

            }
        }

        public bool durum;
        private void BtnHesapla_Click(object sender, EventArgs e)
        {
            if (TxtSinav1.Text == "" || TxtSinav2.Text == "")
            {
                MessageBox.Show("Eksik Sınav Bilgisi Girdiniz!!!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                dogrusayisi = Convert.ToDouble(TxtSinav1.Text);
                yanlissayisi = Convert.ToDouble(TxtSinav2.Text);

               toplamnet = (dogrusayisi - (yanlissayisi / 4));
               TxtOrtalama.Text = toplamnet.ToString("0.00");

            }


        }
    }
}

