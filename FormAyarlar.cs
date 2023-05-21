﻿using System;
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
    public partial class FormAyarlar : Form
    {
        public FormAyarlar()
        {
            InitializeComponent();
        }

        mysqlbaglantisi bgl = new mysqlbaglantisi();

        //ADO.NET ile öğretmen sifre bilgileri
        void listele()
        {
            DataTable dt1 = new DataTable();
            MySqlDataAdapter da1 = new MySqlDataAdapter("CALL AyarlarOgretmenler", bgl.baglanti());
            da1.Fill(dt1);
            gridControl1.DataSource = dt1;
        }

        void ogrencilistele()
        {
            DataTable dt2 = new DataTable();
            MySqlDataAdapter da2 = new MySqlDataAdapter("CALL AyarlarOgrenciler", bgl.baglanti());
            da2.Fill(dt2);
            gridControl2.DataSource = dt2;

        }

        void temizle()
        {
            txtOgrtID.Text = "";
            txtBrans.Text = "";
            txtOgrtSifre.Text = "";
            mskOgrtDogTar.Text = "";
            lookUpEdit1.Text = "";
            lookUpEdit1.Properties.NullText = "Öğretmen Seçiniz";
        }


        //ADO.NET ile LookUpEdit Araci Veri Getirme

        void ogretmenlistesi()
        {
            DataTable dt2 = new DataTable();
            MySqlDataAdapter da2 = new MySqlDataAdapter("Select ogr_id,CONCAT(tbl_ogretmenler.ad, ' ', tbl_ogretmenler.soyad) AS adsoyad, brans from tbl_ogretmenler", bgl.baglanti());
            da2.Fill(dt2);
            lookUpEdit1.Properties.ValueMember = "ogr_id";
            lookUpEdit1.Properties.DisplayMember = "adsoyad";
            lookUpEdit1.Properties.NullText = "Öğretmen Seçiniz:";
           
            lookUpEdit1.Properties.DataSource = dt2;
        }

        void ogrencilistesi()
        {
            DataTable dt3 = new DataTable();
            MySqlDataAdapter da3 = new MySqlDataAdapter("Select id,CONCAT(öğrenci.ad, ' ', öğrenci.soyad) AS adsoyad, ogr_sinif from öğrenci", bgl.baglanti());
            da3.Fill(dt3);
            lookUpEdit2.Properties.ValueMember = "id";
            lookUpEdit2.Properties.DisplayMember = "adsoyad";
            lookUpEdit2.Properties.NullText = "Öğrenci Seçiniz:";
            lookUpEdit2.Properties.DataSource = dt3;

        }

        private void FormAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            ogretmenlistesi();
            ogrencilistele();
            ogrencilistesi();
            temizle();
            
        }

        //ADO.NET ile GridControl verilerini araclara tasima
        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtOgrtID.Text = dr["AYARLAROGRID"].ToString();
                lookUpEdit1.Text = dr["adsoyad"].ToString();
                txtBrans.Text = dr["brans"].ToString();
                mskOgrtDogTar.Text = dr["dogum_tarihi"].ToString();
                txtOgrtSifre.Text = dr["OGRT_SIFRE"].ToString();


            }
        }

        //ADO.NET ile LookUpEdit secimi sonrasi verilerin düzeltilmesi
        private void lookUpEdit1_Properties_EditValueChanged(object sender, EventArgs e)
        {
            txtOgrtSifre.Text = "";
            MySqlCommand komut = new MySqlCommand("Select * from tbl_ogretmenler where ogr_id=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lookUpEdit1.ItemIndex + 1);
            MySqlDataReader dr3 = komut.ExecuteReader();
            while (dr3.Read())
            {
                txtOgrtID.Text = dr3["ogr_id"].ToString();      
                txtBrans.Text = dr3["brans"].ToString();
                mskOgrtDogTar.Text = dr3["dogum_tarihi"].ToString();
            }
            bgl.baglanti().Close();
        }

        //ogretmenler sifre kaydetme
        private void BtnOgrtKaydet_Click(object sender, EventArgs e)
        {
            MySqlCommand komut2 = new MySqlCommand("insert into tbl_ayarlar (AYARLAROGRID,OGRT_SIFRE) values(@p1,@p2)", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", txtOgrtID.Text);
            komut2.Parameters.AddWithValue("@p2", txtOgrtSifre.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Şifre oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        //ADO.NET ögretmen sifre guncelle
        private void BtnOgrtGuncelle_Click(object sender, EventArgs e)
        {
            MySqlCommand komut3 = new MySqlCommand("Update tbl_ayarlar set OGRT_SIFRE=@p1 where AYARLAROGRID=@p2", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", txtOgrtSifre.Text);
            komut3.Parameters.AddWithValue("@p2", txtOgrtID.Text); 
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Şifre Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void BtnOgrtTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
