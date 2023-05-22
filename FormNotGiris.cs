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
    public partial class FormNotGiris : Form
    {
        public FormNotGiris()
        {
            InitializeComponent();
        }

        DbOkulEntities db = new DbOkulEntities();

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
        void ogrtgetir()
        {
            var sorgu = db.TBL_OGRETMENLER.Where(x => x.OGRTTC == TC).SingleOrDefault();
            LblOgrtAdSoyad.Text = sorgu.OGRTAD + " " + sorgu.OGRTSOYAD;
            LblOgrtBrans.Text = sorgu.OGRTBRANS;
        }

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
        }

        void mudurnotlistele()
        {
            gridControl1.DataSource = db.NotListele5();
            gridControl2.DataSource = db.NotListele6();
            gridControl3.DataSource = db.NotListele7();
            gridControl4.DataSource = db.NotListele8();
        }
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
            CmbBrans.Text = gridView1.GetRowCellValue(gridView4.FocusedRowHandle, "NOTBRANS").ToString();
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

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            /*var liste = from item in db.TBL_OGRENCILER
                        where lookUpEdit1.Text == (item.OGRAD + " " + item.OGRSOYAD)
                        select new
                        {
                            TCKimlik = item.OGRTC
                        };
            LblTC.Text = liste.FirstOrDefault().TCKimlik;*/

            var liste = from item in db.TBL_OGRENCILER
                        select item;

            LblTC.Text = liste.FirstOrDefault(x => (x.OGRAD + " " + x.OGRSOYAD) == lookUpEdit1.Text).OGRTC;
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
            var item = db.TBL_NOTLAR.FirstOrDefault(x => x.NOTID == id);
            db.TBL_NOTLAR.Remove(item);
            db.SaveChanges();
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
        void kaydet()
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

