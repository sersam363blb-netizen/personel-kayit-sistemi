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
    public partial class ftm_giris : Form
    {
        public ftm_giris()
        {
            InitializeComponent();
        }

        SqlConnection baglantı = new SqlConnection("Data Source = DESKTOP-19VKL11;Initial Catalog = personelVeriTabani; Integrated Security = True; Encrypt=False");

        private void button1_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("Select * From Tbl_Yonetici where KullaniciAd=@a1 and kullanicisifre=@a2",baglantı);
            komut.Parameters.AddWithValue("@a1",textBox1.Text);
            komut.Parameters.AddWithValue("@a2", textBox2.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Form1 fr = new Form1();
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Yanlış kullanıcı adı veya Yanlış Şifre Girdiniz");
            }
                baglantı.Close();
        }
    }
}
