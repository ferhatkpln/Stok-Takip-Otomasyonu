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
    public partial class frmÜrünEkle : Form
    {
        public frmÜrünEkle()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=FerhatKaplan\SQLEXPRESS;Initial Catalog=Stok_Takip;Integrated Security=True");

        bool durum;
        private void barkodKontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from urun", baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                if (txtBarkodNo.Text == read["barkodno"].ToString() || txtBarkodNo.Text =="" )
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }
        private void KategoriGetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kategoribilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                cmbKategori1.Items.Add(read["kategori"].ToString());

            }
            baglanti.Close();
        }
        private void frmÜrünEkle_Load(object sender, EventArgs e)
        {
            KategoriGetir();
        }

        private void cmbKategori1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMarka1.Items.Clear();
            cmbMarka1.Text = "";
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from markabilgileri where kategori= '"+cmbKategori1.SelectedItem+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                cmbMarka1.Items.Add(read["marka"].ToString());

            }
            baglanti.Close();
        }

        private void btnYeniÜrünEkle_Click(object sender, EventArgs e)
        {
            barkodKontrol();
            if (durum == true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into urun(barkodno,kategori,marka,urunadi,miktarı,alisfiyati,satisfiyati,tarih) values (@barkodno,@kategori,@marka,@urunadi,@miktarı,@alisfiyati,@satisfiyati,@tarih)", baglanti);

                komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
                komut.Parameters.AddWithValue("@kategori", cmbKategori1.Text);
                komut.Parameters.AddWithValue("@marka", cmbMarka1.Text);
                komut.Parameters.AddWithValue("@urunadi", txtUrunAdi.Text);
                komut.Parameters.AddWithValue("@miktarı", int.Parse(txtMiktari.Text));
                komut.Parameters.AddWithValue("@alisfiyati", txtAlisFiyati.Text);
                komut.Parameters.AddWithValue("@satisfiyati", txtSatisFiyati.Text);
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Yeni Ürün Başarılı Bir Şekilde Eklendi...");
            }
            else
                MessageBox.Show("Böyle Bir BarkonNo Bulunmaktatır!","Uyarı");

            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";
                }

            }
        }

        private void txtBarkodNo1_TextChanged(object sender, EventArgs e)
        {
            if (txtBarkodNo1.Text=="")
            {
                lblToplamMiktar.Text = "";
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from urun where barkodno like '"+txtBarkodNo1.Text+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtKategori.Text = read["kategori"].ToString();
                txtMarka.Text = read["marka"].ToString();
                txtUrunAdi1.Text = read["urunadi"].ToString();
                lblToplamMiktar.Text = read["miktarı"].ToString();
                txtAlisFiyati1.Text = read["alisfiyati"].ToString();
                txtSatisFiyati1.Text = read["satisfiyati"].ToString();
   
            }
            baglanti.Close();
        }

        private void btnVarOlanaEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update urun set miktarı=miktarı+'"+int.Parse(txtMiktari1.Text)+"'where barkodno='"+txtBarkodNo1.Text+"' ",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            MessageBox.Show("Var Olan Ürüne Ekleme Başarılı Şekilde Yapıldı..");
        }
    }
}
