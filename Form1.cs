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
using System.Security.Cryptography;

namespace Personel_Kayit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglantı = new SqlConnection("Data Source = DESKTOP-19VKL11;Initial Catalog = personelVeriTabani; Integrated Security = True; Encrypt=False");

        void temizle()
        {
            txtad.Text = "";
            txtmeslek.Text = "";
            txtsehir.Text = "";
            txtsoyad.Text = "";
            Txtid.Text = "";
            mskmaas.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            txtad.Focus();

        } 
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'personelVeriTabaniDataSet.Tbl_Personel' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);

        }

        // Personel Listeleme, Tazeleme
        private void btnlistele_Click(object sender, EventArgs e)
        {
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);
        }

        // Personel Ekle
        private void btnkaydet_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Personel (PerAd,PerSoyad,PerSehir,PerMaas,PerMeslek,PerDurum) values (@p1,@p2,@p3,@p4,@p5,@p6)",baglantı);
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", txtsehir.Text);
            komut.Parameters.AddWithValue("@p4", mskmaas.Text);
            komut.Parameters.AddWithValue("@p5", txtmeslek.Text);
            komut.Parameters.AddWithValue("@p6", radioButton1.Checked ? "True" : "False"); 
            komut.ExecuteNonQuery();
            baglantı.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (label8.Text == "True")
            {
                radioButton1.Checked = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (label8.Text == "False")
            {
                radioButton2.Checked = true;
            }
        }

        // Temizle Butonu
        private void btntemizle_Click(object sender, EventArgs e)
        {
            Txtid.Text = ("");
            txtad.Text = ("");
            txtmeslek.Text = ("");
            txtsehir.Text = ("");
            txtsoyad.Text = ("");
            mskmaas.Text = ("");
            radioButton2.Checked = true;
            txtad.Focus();
        }

        // DataGridView'den verileri textboxlara aktarma
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            Txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtsoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtsehir.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mskmaas.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            label8.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();

            txtmeslek.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        
        private void label8_TextChanged(object sender, EventArgs e)
        {
            if (label8.Text == "True")
            {
                radioButton1.Checked = true;
            }
            if (label8.Text == "False")
            {
                radioButton2.Checked = true;            }
        }

        // Personel Sil
        private void btnsil_Click(object sender, EventArgs e)
        {
            baglantı.Open();

            SqlCommand komutsil = new SqlCommand("Delete from Tbl_Personel Where Perid=@k1", baglantı);
            komutsil.Parameters.AddWithValue("@k1",Txtid.Text);
            komutsil.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Kayıt Silindi");
        }
        
        // Personel Güncelle
        private void btnguncelle_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand guncelleme = new SqlCommand("Update Tbl_Personel Set PerAd=@a1, PerSoyad=@a2,PerSehir=@a3, PerMaas= @a4, PerDurum= @a5, PerMeslek=@a6 where Perid=@a7 " ,baglantı);
            guncelleme.Parameters.AddWithValue("@a1",txtad.Text);
            guncelleme.Parameters.AddWithValue("@a2", txtsoyad.Text);
            guncelleme.Parameters.AddWithValue("@a3", txtsehir.Text);
            guncelleme.Parameters.AddWithValue("@a4", mskmaas.Text);
            guncelleme.Parameters.AddWithValue("@a5", label8.Text);
            guncelleme.Parameters.AddWithValue("@a6", txtmeslek.Text);
            guncelleme.Parameters.AddWithValue("@a7", Txtid.Text);
            guncelleme.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Personel güncellendi");
        }

        // İstatistik Butonu
        private void btnistatistik_Click(object sender, EventArgs e)
        {
            frmistatistik fr = new frmistatistik();
            fr.Show();
        }

     

        // Grafikler Butonu
        private void btnGrafikler_Click(object sender, EventArgs e)
        {
            Frm_Grafikler fr = new Frm_Grafikler();
            fr.Show();
        }
    }
}
