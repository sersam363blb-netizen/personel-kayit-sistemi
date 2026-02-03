using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personel_Kayit
{
    public partial class Frm_Grafikler : Form
    {
        public Frm_Grafikler()
        {
            InitializeComponent();
        }

        SqlConnection baglantı = new SqlConnection("Data Source = DESKTOP-19VKL11;Initial Catalog = personelVeriTabani; Integrated Security = True; Encrypt=False");

        private void Frm_Grafikler_Load(object sender, EventArgs e)
        {
            // Evli ve bekar sayısı
            int evli = 0, bekar = 0;
            baglantı.Open();
            using (SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM Tbl_Personel WHERE Perdurum = 1", baglantı))
            {
                evli = Convert.ToInt16(komut.ExecuteScalar());
            }
            using (SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM Tbl_Personel WHERE Perdurum = 0", baglantı))
            {
                bekar = Convert.ToInt16(komut.ExecuteScalar());
            }
            baglantı.Close();

            // Pie Chart: Personel Durumu
            chart1.Series.Clear();
            chart1.Series.Add("PersonelDurum");
            chart1.Series["PersonelDurum"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            chart1.Series["PersonelDurum"].Points.AddXY("Evli", evli);
            chart1.Series["PersonelDurum"].Points.AddXY("Bekar", bekar);

            // Ortalama maaşlar
            int evliMaas = 0, bekarMaas = 0, toplamMaas = 0;
            baglantı.Open();
            using (SqlCommand komut = new SqlCommand("SELECT AVG(PerMaas) FROM Tbl_Personel WHERE Perdurum = 1", baglantı))
            {
                evliMaas = Convert.ToInt32(komut.ExecuteScalar());
            }
            using (SqlCommand komut = new SqlCommand("SELECT AVG(PerMaas) FROM Tbl_Personel WHERE Perdurum = 0", baglantı))
            {
                bekarMaas = Convert.ToInt32(komut.ExecuteScalar());
            }
            using (SqlCommand komut = new SqlCommand("SELECT AVG(PerMaas) FROM Tbl_Personel", baglantı))
            {
                toplamMaas = Convert.ToInt32(komut.ExecuteScalar());
            }
            baglantı.Close();

            // Column Chart: Ortalama Maaş
            chart2.Series.Clear();
            chart2.Series.Add("OrtalamaMaas");
            chart2.Series["OrtalamaMaas"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart2.Series["OrtalamaMaas"].Points.AddXY("Evli", evliMaas);
            chart2.Series["OrtalamaMaas"].Points.AddXY("Bekar", bekarMaas);
            chart2.Series["OrtalamaMaas"].Points.AddXY("Toplam", toplamMaas);

            // Şehirler ve personel sayısı
            baglantı.Open();
            SqlCommand komut2 = new SqlCommand("SELECT PerSehir, COUNT(*) AS PersonelSayisi FROM Tbl_Personel GROUP BY PerSehir", baglantı);
            SqlDataReader dr = komut2.ExecuteReader();



            baglantı.Close();





        }
    }
}
