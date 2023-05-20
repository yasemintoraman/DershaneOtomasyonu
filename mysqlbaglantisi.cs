using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DershaneOtomasyonu
{
    class mysqlbaglantisi
    {
        public MySqlConnection baglanti()
        {
            //bu sekilde yaptigimiz icin her form icin yapmamiz gerekmeyecek
            MySqlConnection baglan = new MySqlConnection(@"server=localhost;user id=root;database=dershane_otomasyonu");
            baglan.Open();
            return baglan;
        }
    }
}
