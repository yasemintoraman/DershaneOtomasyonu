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
    public partial class FormOgrenciAnaModul : Form
    {
        public FormOgrenciAnaModul()
        {
            InitializeComponent();
        }

        mysqlbaglantisi bgl = new mysqlbaglantisi();

        public string OgrTC;

        void ogrgetir()
        {
            MySqlCommand komut = new MySqlCommand("Select ad,soyad,ogr_sinif from öğrenci where tc = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", OgrTC);

            MySqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                string ad = dr.GetString(0);
                string soyad = dr.GetString(1);
                string ogr_sinif = dr.GetString(2);

                LblAdSoyad.Text = ad + " " + soyad;
                LblTC.Text = OgrTC;
                LblSinif.Text = ogr_sinif;
            }
            dr.Close();
        }
        void notgetir()
        {
            MySqlCommand komut2 = new MySqlCommand("SELECT NOTBRANS AS DERSADI, DOGRUSAYİSİ, YANLİSSAYİSİ, TOPLAMNET FROM TBL_NOTLAR WHERE NOTTC = @OgrTC", bgl.baglanti());
            komut2.Parameters.AddWithValue("@OgrTC", OgrTC);
            MySqlDataAdapter da = new MySqlDataAdapter(komut2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }

        private void FormOgrenciAnaModul_Load(object sender, EventArgs e)
        {
            ogrgetir();
            notgetir();

        }
    }
}
