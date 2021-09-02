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
    public partial class frmMüşteriEkle : Form
    {
        public frmMüşteriEkle()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=FerhatKaplan\SQLEXPRESS;Initial Catalog=Stok_Takip;Integrated Security=True");
        private void btnKayıtEkle_Click(object sender, EventArgs e)
        {
            if (txtKayıtTc.Text == "" || txtKayıtAdSoyad.Text == "" || txtKayıtTelefon.Text == "" || txtKayıtAdres.Text =="" || txtKayıtMail.Text == "" ||
                    txtKayıtTc.Text == String.Empty || txtKayıtAdSoyad.Text == String.Empty || txtKayıtTelefon.Text == String.Empty || txtKayıtAdres.Text == String.Empty || txtKayıtMail.Text == String.Empty)
            {
                txtKayıtTc.BackColor = Color.Yellow;
                txtKayıtAdSoyad.BackColor = Color.Yellow;
                txtKayıtTelefon.BackColor = Color.Yellow;
                txtKayıtAdres.BackColor = Color.Yellow;
                txtKayıtMail.BackColor = Color.Yellow;
                MessageBox.Show("Sarı Rekli Alanları Boş Geçemezsiniz", "Boş Alan Hatası");
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into müşteri(tc,adsoyad,telefon,adres,mail) values (@tc,@adsoyad,@telefon,@adres,@mail)", baglanti);

                komut.Parameters.AddWithValue("@tc", txtKayıtTc.Text);
                komut.Parameters.AddWithValue("@adsoyad", txtKayıtAdSoyad.Text);
                komut.Parameters.AddWithValue("@telefon", txtKayıtTelefon.Text);
                komut.Parameters.AddWithValue("@adres", txtKayıtAdres.Text);
                komut.Parameters.AddWithValue("@mail", txtKayıtMail.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Müşteri Başarılı Bir Şekilde Eklendi...");

                foreach (Control item in this.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }

            
        }

        private void txtKayıtTc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtKayıtTelefon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtKayıtAdSoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }
    }
}
