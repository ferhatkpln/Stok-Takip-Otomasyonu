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
    public partial class frmUrunListeleme : Form
    {
        public frmUrunListeleme()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=FerhatKaplan\SQLEXPRESS;Initial Catalog=Stok_Takip;Integrated Security=True");
        DataSet dataset = new DataSet();

        private void frmUrunListeleme_Load(object sender, EventArgs e)
        {
            UrunGöster();
        }
        private void UrunGöster()
        {
            baglanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from urun", baglanti);
            adapter.Fill(dataset, "urun");
            dataGridView1.DataSource = dataset.Tables["urun"];
            baglanti.Close();
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBarkodNo1.Text = dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString();
            txtKategori.Text = dataGridView1.CurrentRow.Cells["kategori"].Value.ToString();
            txtMarka.Text = dataGridView1.CurrentRow.Cells["marka"].Value.ToString();
            txtUrunAdi1.Text = dataGridView1.CurrentRow.Cells["urunadi"].Value.ToString();
            txtMiktari1.Text = dataGridView1.CurrentRow.Cells["miktarı"].Value.ToString();
            txtAlisFiyati1.Text = dataGridView1.CurrentRow.Cells["alisfiyati"].Value.ToString();
            txtSatisFiyati1.Text = dataGridView1.CurrentRow.Cells["satisfiyati"].Value.ToString();
        }
        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update urun set urunadi=@urunadi,miktarı=@miktarı,alisfiyati=@alisfiyati,satisfiyati=@satisfiyati where barkodno=@barkodno", baglanti);
            komut.Parameters.AddWithValue("@barkodno", txtBarkodNo1.Text);
            komut.Parameters.AddWithValue("@urunadi", txtUrunAdi1.Text);
            komut.Parameters.AddWithValue("@miktarı", int.Parse(txtMiktari1.Text));
            komut.Parameters.AddWithValue("@alisfiyati", double.Parse(txtAlisFiyati1.Text));
            komut.Parameters.AddWithValue("@satisfiyati", double.Parse(txtSatisFiyati1.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            dataset.Tables["urun"].Clear();
            UrunGöster();
            MessageBox.Show("Ürün Kaydı Güncellendi...");

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
            SqlCommand komut = new SqlCommand("delete from urun where barkodno='" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            dataset.Tables["urun"].Clear();
            UrunGöster();
            MessageBox.Show("Ürün Kaydı Başarılı Bir Şekilde Silindi..");
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from urun where barkodno like '%" + txtAra.Text + "%'", baglanti);
            adapter.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void btnMarkaGüncelle_Click(object sender, EventArgs e)
        {
            if (txtBarkodNo1.Text !="")
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update urun set kategori=@kategori,marka=@marka where barkodno=@barkodno", baglanti);
                komut.Parameters.AddWithValue("@barkodno", txtBarkodNo1.Text);
                komut.Parameters.AddWithValue("@kategori", comboKategori.Text);
                komut.Parameters.AddWithValue("@marka", comboMarka.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                dataset.Tables["urun"].Clear();
                UrunGöster();
                MessageBox.Show("Ürün Kaydı Güncellendi...");
            }
            else
                MessageBox.Show("Barkod No Yazılı Değil","Uyarı");


            foreach (Control item in this.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }
    }
}
