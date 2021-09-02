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
    public partial class frmMüşteriListele : Form
    {
        public frmMüşteriListele()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=FerhatKaplan\SQLEXPRESS;Initial Catalog=Stok_Takip;Integrated Security=True");
        DataSet dataset = new DataSet();
        private void frmMüşteriListele_Load(object sender, EventArgs e)
        {
            Kayıt_Göster();
        }

        private void Kayıt_Göster()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("spBilgileriGetir", baglanti);
            komut.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            //SqlDataAdapter adapter = new SqlDataAdapter("select * from müşteri", baglanti);
            adapter.Fill(dataset, "müşteri");
            dataGridView1.DataSource = dataset.Tables["müşteri"];
            baglanti.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtListeleTc.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            txtListeleAdSoyad.Text = dataGridView1.CurrentRow.Cells["adsoyad"].Value.ToString();
            txtListeleTelefon.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            txtListeleAdres.Text = dataGridView1.CurrentRow.Cells["adres"].Value.ToString();
            txtListeleMail.Text = dataGridView1.CurrentRow.Cells["mail"].Value.ToString();

        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update müşteri set adsoyad=@adsoyad,telefon=@telefon,adres=@adres,mail=@mail where tc=@tc", baglanti);

            komut.Parameters.AddWithValue("@tc", txtListeleTc.Text);
            komut.Parameters.AddWithValue("@adsoyad", txtListeleAdSoyad.Text);
            komut.Parameters.AddWithValue("@telefon", txtListeleTelefon.Text);
            komut.Parameters.AddWithValue("@adres", txtListeleAdres.Text);
            komut.Parameters.AddWithValue("@mail", txtListeleMail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            dataset.Tables["müşteri"].Clear();
            Kayıt_Göster();
            MessageBox.Show("Müşteri Kaydı Güncellendi...");

            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from müşteri where tc='"+dataGridView1.CurrentRow.Cells["tc"].Value.ToString()+"'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            dataset.Tables["müşteri"].Clear();
            Kayıt_Göster();
            MessageBox.Show("Müşteri Kaydı Başarılı Bir Şekilde Silindi..");
        }

        private void txtTcAra_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from müşteri where tc like '%"+txtTcAra.Text+"%'",baglanti);
            adapter.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();

        }
    }
}
