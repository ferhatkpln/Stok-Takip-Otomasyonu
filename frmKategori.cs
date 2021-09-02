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

namespace Stok_Takip_Otomasyonu
{
    public partial class frmKategori : Form
    {
        public frmKategori()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=FerhatKaplan\SQLEXPRESS;Initial Catalog=Stok_Takip;Integrated Security=True");
        bool durum;

        private void kategoriKontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kategoribilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                if (txtKategoriEkleme.Text == read["kategori"].ToString() || txtKategoriEkleme.Text =="")
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }
        private void btnYeniKategoriEkle_Click(object sender, EventArgs e)
        {
            kategoriKontrol();
            if (durum == true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into kategoribilgileri(kategori) values ('" + txtKategoriEkleme.Text + "') ", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kategori Başarılı Şekilde Eklendi..");
            }
            else
            {
                MessageBox.Show("Böyle Bir Kategori Var!!","Uyarı");
            }
            txtKategoriEkleme.Text = "";

        }
    }
}
