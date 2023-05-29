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

        //ekledik
        public string TC;

        void temizle()
        {
            TxtID.Text = "";
            CmbSinif.Text = "";
            lookUpEdit1.Text = "";
            LblTC.Text = "";
            //CmbBrans.Text= "";
            TxtSinav1.Text = "";
            TxtSinav2.Text = "";
            TxtSozlu1.Text = "";
            TxtSozlu2.Text = "";
            TxtSozlu3.Text = "Girilmedi";
            // Ortalama ve Geçti kaldı kısmına gerek yok, tekrar hesaplansın
            TxtOrtalama.Text = "";
            durum = false;
            chksozlu3.Checked = true;
        }
        /*void ogrtgetir()
        {
            var sorgu = db.TBL_OGRETMENLER.Where(x => x.OGRTTC == TC).SingleOrDefault();
            LblOgrtAdSoyad.Text = sorgu.OGRTAD + " " + sorgu.OGRTSOYAD;
            LblOgrtBrans.Text = sorgu.OGRTBRANS;
        }
        */
        void ogrtgetir()
        {
            MySqlCommand komut = new MySqlCommand("Select ad, soyad, brans from tbl_ogretmenler where ogrt_tc = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TC); //burasi TC idi

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

        /*
        void ogrgetir()
        {
            var sorgu = from item in db.TBL_OGRENCILER
                        where CmbSinif.Text == item.OGRSINIF
                        select new
                        {
                            ID = item.OGRID,
                            ADSOYAD = item.OGRAD + " " + item.OGRSOYAD,
                            TCKIMLIK = item.OGRTC,
                        };

            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "ADSOYAD";
            lookUpEdit1.Properties.NullText = "Öğrenci Seçiniz";
            lookUpEdit1.Properties.DataSource = sorgu.ToList();
        }
        */

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



        /*
         void ogrnotlistele()
        {
            var liste1 = from item in db.TBL_NOTLAR
                         where item.NOTBRANS == LblOgrtBrans.Text &&
                               item.NOTSINIF == "5.SINIF"
                         select item;

            gridControl1.DataSource = liste1.ToList();

            var liste2 = from item in db.TBL_NOTLAR
                         where item.NOTBRANS == LblOgrtBrans.Text &&
                               item.NOTSINIF == "6.SINIF"
                         select item;

            gridControl2.DataSource = liste2.ToList();

            var liste3 = from item in db.TBL_NOTLAR
                         where item.NOTBRANS == LblOgrtBrans.Text &&
                               item.NOTSINIF == "7.SINIF"
                         select item;

            gridControl3.DataSource = liste3.ToList();

            var liste4 = from item in db.TBL_NOTLAR
                         where item.NOTBRANS == LblOgrtBrans.Text &&
                               item.NOTSINIF == "8.SINIF"
                         select item;

            gridControl4.DataSource = liste4.ToList();
        }*/

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

        /*
        void mudurnotlistele()
        {
            gridControl1.DataSource = NotListele5();
            gridControl2.DataSource = NotListele6();
            gridControl3.DataSource = db.NotListele7();
            gridControl4.DataSource = db.NotListele8();
        }
        */

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
                BtnSil.Visible = true;

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
                BtnSil.Visible = false;

            }
            temizle();

        }


        /*
        private void FrmNotGiris_Load(object sender, EventArgs e)
        {

            LblOgrtTC.Text = TC;
            ogrtgetir();


            if (LblOgrtBrans.Text == "MÜDÜR" || LblOgrtBrans.Text == "MÜDÜR YARDIMCISI")
            {
                mudurnotlistele();
                BtnSil.Visible = true;

                var sorgu = from item in db.TBL_BRANSLAR
                            select new
                            {
                                ID = item.BRANSID,
                                BRANS = item.BRANSAD,
                            };
                CmbBrans.DataSource = sorgu.ToList();
                CmbBrans.DisplayMember = "BRANS";
                CmbBrans.ValueMember = "ID";
            }
            else
            {
                CmbBrans.Text = LblOgrtBrans.Text;
                CmbBrans.Enabled = false;
                ogrnotlistele();
                BtnSil.Visible = false;
            }

            temizle();

        }
        */


        private void CmbSinif_SelectedIndexChanged(object sender, EventArgs e)
        {
            ogrgetir();
        }




        double sinav1, sinav2, sozlu1, sozlu2, sozlu3, ortalama;

        //Araçlara Veri Aktarma
        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {


            TxtID.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NOTID").ToString();
            CmbSinif.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NOTSINIF").ToString();
            lookUpEdit1.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NOTADSOYAD").ToString();
            LblTC.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NOTTC").ToString();
            CmbBrans.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NOTBRANS").ToString();
            TxtSinav1.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SINAV1").ToString();
            TxtSinav2.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SINAV2").ToString();
            TxtSozlu1.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SOZLU1").ToString();
            TxtSozlu2.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SOZLU2").ToString();
            if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SOZLU3")?.ToString() == null)
            {
                TxtSozlu3.Text = "Girilmedi";
                chksozlu3.Checked = true;
            }
            else
            {
                TxtSozlu3.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SOZLU3")?.ToString();
                chksozlu3.Checked = false;
            }
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
            TxtSinav1.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SINAV1").ToString();
            TxtSinav2.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SINAV2").ToString();
            TxtSozlu1.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SOZLU1").ToString();
            TxtSozlu2.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SOZLU2").ToString();
            if (gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SOZLU3")?.ToString() == null)
            {
                //TxtSozlu3.Text = "Girilmedi";
                chksozlu3.Checked = true;
            }
            else
            {
                TxtSozlu3.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SOZLU3")?.ToString();
                chksozlu3.Checked = false;
            }
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
            TxtSinav1.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "SINAV1").ToString();
            TxtSinav2.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "SINAV2").ToString();
            TxtSozlu1.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "SOZLU1").ToString();
            TxtSozlu2.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "SOZLU2").ToString();
            if (gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "SOZLU3")?.ToString() == null)
            {
                //TxtSozlu3.Text = "Girilmedi" ;
                chksozlu3.Checked = true;
            }
            else
            {
                TxtSozlu3.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "SOZLU3")?.ToString();
                chksozlu3.Checked = false;
            }
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
            TxtSinav1.Text = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "SINAV1").ToString();
            TxtSinav2.Text = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "SINAV2").ToString();
            TxtSozlu1.Text = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "SOZLU1").ToString();
            TxtSozlu2.Text = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "SOZLU2").ToString();
            if (gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "SOZLU3")?.ToString() == null)
            {
                //TxtSozlu3.Text = "Girilmedi";
                chksozlu3.Checked = true;
            }
            else
            {
                TxtSozlu3.Text = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "SOZLU3")?.ToString();
                chksozlu3.Checked = false;
            }
            // Ortalama ve Geçti kaldı kısmına gerek yok, tekrar hesaplansın
            TxtOrtalama.Text = "";
            durum = false;
        }

        /*void guncellesozlu3var()
        {
            int id = Convert.ToInt32(TxtID.Text);
            var item = db.TBL_NOTLAR.FirstOrDefault(x => x.NOTID == id);
            item.SINAV1 = Convert.ToByte(TxtSinav1.Text);
            item.SINAV2 = Convert.ToByte(TxtSinav2.Text);
            item.SOZLU1 = Convert.ToByte(TxtSozlu1.Text);
            item.SOZLU2 = Convert.ToByte(TxtSozlu2.Text);
            
            item.ORTALAMA = Convert.ToDecimal(TxtOrtalama.Text);
            item.DURUM = durum;
            db.SaveChanges();
        }*/

        /*
        void guncelle()
        {
            int id = Convert.ToInt32(TxtID.Text);
            var item = db.TBL_NOTLAR.FirstOrDefault(x => x.NOTID == id);
            item.SINAV1 = Convert.ToByte(TxtSinav1.Text);
            item.SINAV2 = Convert.ToByte(TxtSinav2.Text);
            item.SOZLU1 = Convert.ToByte(TxtSozlu1.Text);
            item.SOZLU2 = Convert.ToByte(TxtSozlu2.Text);
            if (TxtSozlu3.Text == "Girilmedi" || TxtSozlu3.Text == "")
            {
                item.SOZLU3 = null;
            }
            else
            {
                item.SOZLU3 = Convert.ToByte(TxtSozlu3.Text);
            }
            item.ORTALAMA = Convert.ToDecimal(TxtOrtalama.Text);
            item.DURUM = durum;
            db.SaveChanges();
        }
        */

        //sinav notu guncelleme
        void guncelle()
        {
            int id = Convert.ToInt32(TxtID.Text);
            byte sinav1 = Convert.ToByte(TxtSinav1.Text);
            byte sinav2 = Convert.ToByte(TxtSinav2.Text);
            byte sozlu1 = Convert.ToByte(TxtSozlu1.Text);
            byte sozlu2 = Convert.ToByte(TxtSozlu2.Text);
            byte? sozlu3 = null;
            if (!string.IsNullOrEmpty(TxtSozlu3.Text) && TxtSozlu3.Text != "Girilmedi")
            {
                sozlu3 = Convert.ToByte(TxtSozlu3.Text);
            }
            decimal ortalama = Convert.ToDecimal(TxtOrtalama.Text);


            MySqlCommand command = new MySqlCommand("UPDATE TBL_NOTLAR SET SINAV1 = @sinav1, SINAV2 = @sinav2, SOZLU1 = @sozlu1, SOZLU2 = @sozlu2, SOZLU3 = @sozlu3, ORTALAMA = @ortalama, DURUM = @durum WHERE NOTID = @id", bgl.baglanti());

            command.Parameters.AddWithValue("@sinav1", sinav1);
            command.Parameters.AddWithValue("@sinav2", sinav2);
            command.Parameters.AddWithValue("@sozlu1", sozlu1);
            command.Parameters.AddWithValue("@sozlu2", sozlu2);
            command.Parameters.AddWithValue("@sozlu3", (object)sozlu3 ?? DBNull.Value);
            command.Parameters.AddWithValue("@ortalama", ortalama);
            command.Parameters.AddWithValue("@durum", durum);
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

        private void chksozlu3_CheckedChanged(object sender, EventArgs e)
        {
            if (chksozlu3.Checked == true)
            {
                TxtSozlu3.Text = "Girilmedi";
                TxtSozlu3.Enabled = false;
            }
            else
            {
                TxtSozlu3.Enabled = true;

            }
        }




        private void BtnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TxtID.Text);
            MySqlCommand command2 = new MySqlCommand("Delete FROM tbl_notlar WHERE NOTID = @id", bgl.baglanti());
            command2.Parameters.AddWithValue("@id", id);

            command2.ExecuteNonQuery();
            mudurnotlistele();

        }

        /*void kaydetsozlu3var()
        {
            TBL_NOTLAR komut = new TBL_NOTLAR();
            komut.NOTSINIF = CmbSinif.Text;
            komut.NOTADSOYAD = lookUpEdit1.Text;
            komut.NOTTC = LblTC.Text;
            komut.NOTBRANS = CmbBrans.Text;
            komut.SINAV1 = Convert.ToByte(TxtSinav1.Text);
            komut.SINAV2 = Convert.ToByte(TxtSinav2.Text);
            komut.SOZLU1 = Convert.ToByte(TxtSozlu1.Text);
            komut.SOZLU2 = Convert.ToByte(TxtSozlu2.Text);
            
            komut.ORTALAMA = Convert.ToDecimal(TxtOrtalama.Text);
            komut.DURUM = durum;
            db.TBL_NOTLAR.Add(komut);
            db.SaveChanges();
        }*/
       /* void kaydet()
        {
            var sorgu = from item in db.TBL_NOTLAR
                        where item.NOTTC == LblTC.Text &&
                              item.NOTBRANS == CmbBrans.Text
                        select item;
            if (sorgu.Any())
            {
                MessageBox.Show("Öğrencinin " + CmbBrans.Text + " ders notu vardır");
            }
            else
            {
                TBL_NOTLAR komut = new TBL_NOTLAR();
                komut.NOTSINIF = CmbSinif.Text;
                komut.NOTADSOYAD = lookUpEdit1.Text;
                komut.NOTTC = LblTC.Text;
                komut.NOTBRANS = CmbBrans.Text;
                komut.SINAV1 = Convert.ToByte(TxtSinav1.Text);
                komut.SINAV2 = Convert.ToByte(TxtSinav2.Text);
                komut.SOZLU1 = Convert.ToByte(TxtSozlu1.Text);
                komut.SOZLU2 = Convert.ToByte(TxtSozlu2.Text);
                if (TxtSozlu3.Text == "Girilmedi" || TxtSozlu3.Text != "")
                {
                    komut.SOZLU3 = null;
                }
                else
                {
                    komut.SOZLU3 = Convert.ToByte(TxtSozlu3.Text);
                }
                komut.DURUM = durum;
                komut.ORTALAMA = Convert.ToDecimal(TxtOrtalama.Text);
                db.TBL_NOTLAR.Add(komut);
                db.SaveChanges();
            }
        }

        */

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
                MessageBox.Show("Öğrencinin " + brans + " ders notu vardır");
            }
            else
            {
                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO TBL_NOTLAR (NOTID,NOTSINIF, NOTADSOYAD, NOTTC, NOTBRANS, SINAV1, SINAV2, SOZLU1, SOZLU2, SOZLU3, DURUM, ORTALAMA) VALUES (@notid,@sinif, @adsoyad, @tc, @brans, @sinav1, @sinav2, @sozlu1, @sozlu2, @sozlu3, @durum, @ortalama)", bgl.baglanti());
                insertCommand.Parameters.AddWithValue("@notid", TxtID.Text);
                insertCommand.Parameters.AddWithValue("@sinif", CmbSinif.Text);
                insertCommand.Parameters.AddWithValue("@adsoyad", lookUpEdit1.Text);
                insertCommand.Parameters.AddWithValue("@tc", tc);
                insertCommand.Parameters.AddWithValue("@brans", CmbBrans.Text);
                insertCommand.Parameters.AddWithValue("@sinav1", Convert.ToByte(TxtSinav1.Text));
                insertCommand.Parameters.AddWithValue("@sinav2", Convert.ToByte(TxtSinav2.Text));
                insertCommand.Parameters.AddWithValue("@sozlu1", Convert.ToByte(TxtSozlu1.Text));
                insertCommand.Parameters.AddWithValue("@sozlu2", Convert.ToByte(TxtSozlu2.Text));

                if (TxtSozlu3.Text == "Girilmedi" || string.IsNullOrEmpty(TxtSozlu3.Text))
                {
                    insertCommand.Parameters.AddWithValue("@sozlu3", DBNull.Value);
                }
                else
                {
                    insertCommand.Parameters.AddWithValue("@sozlu3", Convert.ToByte(TxtSozlu3.Text));
                }

                insertCommand.Parameters.AddWithValue("@durum", durum);
                insertCommand.Parameters.AddWithValue("@ortalama", Convert.ToDecimal(TxtOrtalama.Text));

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
            if (TxtSinav1.Text == "" || TxtSinav2.Text == "" || TxtSozlu1.Text == "" || TxtSozlu2.Text == "")
            {
                MessageBox.Show("Eksik Sınav Notu Girdiniz!!!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                sinav1 = Convert.ToDouble(TxtSinav1.Text);
                sinav2 = Convert.ToDouble(TxtSinav2.Text);
                sozlu1 = Convert.ToDouble(TxtSozlu1.Text);
                sozlu2 = Convert.ToDouble(TxtSozlu2.Text);

                if (TxtSozlu3.Text == "Girilmedi" || TxtSozlu3.Text == "")
                {
                    ortalama = (((sinav1 + sinav2) + ((sozlu1 + sozlu2) / 2)) / 3);
                    TxtOrtalama.Text = ortalama.ToString("0.00");
                }
                else
                {
                    sozlu3 = Convert.ToInt32(TxtSozlu3.Text);
                    ortalama = (((sinav1 + sinav2) + ((sozlu1 + sozlu2 + sozlu3) / 3)) / 3);
                    TxtOrtalama.Text = ortalama.ToString("0.00");
                }

                if (ortalama >= 45)
                {
                    checkEdit1.Checked = true;
                    checkEdit1.Text = "Geçti";
                    durum = true;
                }
                else
                {
                    checkEdit1.Checked = false;
                    checkEdit1.Text = "Kaldı";
                    durum = false;
                }

            }


        }
    }
}

