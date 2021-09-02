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

namespace Stok_Takip_Otomasyonu
{
    public partial class frmMarka : Form
    {
        public frmMarka()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=FerhatKaplan\SQLEXPRESS;Initial Catalog=Stok_Takip;Integrated Security=True");

        bool durum;
        private void markaKontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from markabilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                if (cmbKategori.Text ==read["kategori"].ToString() && txtmarka.Text == read["marka"].ToString() || cmbKategori.Text == "" ||txtmarka.Text == "")
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }
        private void btnYeniMarkaEkle_Click(object sender, EventArgs e)
        {
            markaKontrol();
            if (durum == true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into markabilgileri(kategori,marka) values ('" + cmbKategori.Text + "','" + txtmarka.Text + "') ", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kategori ve Marka Başarılı Şekilde Eklendi..");
            }
            else
                MessageBox.Show("Böyle Bir Marka ve Kategori Vardır!","Uyarı");

            cmbKategori.Text = "";
            txtmarka.Text = "";

            
        }

        private void KategoriGetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kategoribilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                cmbKategori.Items.Add(read["kategori"].ToString());

            }
            baglanti.Close();
        }
        private void frmMarka_Load(object sender, EventArgs e)
        {
            KategoriGetir();
        }
    }
}
