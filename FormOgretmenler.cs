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
    public partial class FormOgretmenler : Form
    {
        public FormOgretmenler()
        {
            InitializeComponent();
        }
        mysqlbaglantisi bgl =  new mysqlbaglantisi(); //mysqlbaglantisi sinifimizi cagirarak baglantiyi sagladik

        void listele()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from TBL_OGRETMENLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void bransgetir()
        {
            MySqlCommand komut = new MySqlCommand("Select * from Ders_Bilgileri", bgl.baglanti());
            MySqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbBrans.Properties.Items.Add(dr[1]);
            }
            bgl.baglanti().Close();

        }


        private void FormOgretmenler_Load(object sender, EventArgs e)
        {
            listele();
            bransgetir();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            MySqlCommand komut = new MySqlCommand("insert into TBL_OGRETMENLER (ogr_id, ad, soyad, dogum_tarihi,brans) values(@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtID.Text);
            komut.Parameters.AddWithValue("@p2", txtAd.Text);
            komut.Parameters.AddWithValue("@p3", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p4", maskedDogTar.Text);
            komut.Parameters.AddWithValue("@p5", cmbBrans.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close(); //baglantiyi kapattik
            MessageBox.Show("Personel Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        //gridView bilgilerini yandaki tabloda(toolda) goruntulemek
        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                txtID.Text = dr["ogr_id"].ToString();
                txtAd.Text = dr["ad"].ToString();
                txtSoyad.Text = dr["soyad"].ToString();
                maskedDogTar.Text = dr["dogum_tarihi"].ToString();
                cmbBrans.Text = dr["brans"].ToString();
            }
        }
    }
}
