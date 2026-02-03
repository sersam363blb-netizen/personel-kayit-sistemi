using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Personel_Kayit
{
    public partial class frmistatistik : Form
    {
        public frmistatistik()
        {
            InitializeComponent();
        }

        SqlConnection baglantı = new SqlConnection("Data Source = DESKTOP-19VKL11;Initial Catalog = personelVeriTabani; Integrated Security = True; Encrypt=False");

        private void frmistatistik_Load(object sender, EventArgs e)
        {
            //Toplam Personel Sayısı
            baglantı.Open();
            SqlCommand komut1 = new SqlCommand(" Select  Count(*) From Tbl_Personel", baglantı);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lbltoplam.Text = dr1[0].ToString();
            }
            baglantı.Close();

            //Evli Personel Sayısı
            baglantı.Open();
            SqlCommand komut2 = new SqlCommand("SELECT COUNT(*) FROM Tbl_Personel WHERE Perdurum = 1", baglantı);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblevli.Text = dr2[0].ToString();
            }
            baglantı.Close();

            //Bekar Personel Sayısı
            baglantı.Open();
            SqlCommand komut3 = new SqlCommand("SELECT COUNT(*) FROM Tbl_Personel WHERE Perdurum = 0", baglantı);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblbekar.Text = dr3[0].ToString();
            }
            baglantı.Close();

            //Farklı Şehir Sayısı
            baglantı.Open();
            SqlCommand komut4 = new SqlCommand("SELECT COUNT(DISTINCT PerSehir) FROM Tbl_Personel", baglantı);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblsehir.Text = dr4[0].ToString();
            }
            baglantı.Close();

            //Ortalama Maaş Miktarı
            baglantı.Open();
            SqlCommand komut5 = new SqlCommand("SELECT AVG(PerMaas) FROM Tbl_Personel", baglantı);
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lblortmaas.Text = dr5[0].ToString();
            }
            baglantı.Close();

        }
            

        
    }
}
