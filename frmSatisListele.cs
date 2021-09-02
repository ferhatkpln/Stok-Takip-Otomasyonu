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
    public partial class frmSatisListele : Form
    {
        public frmSatisListele()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=FerhatKaplan\SQLEXPRESS;Initial Catalog=Stok_Takip;Integrated Security=True");
        DataSet dataset = new DataSet();

        private void satisListele()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("spSatisListele", baglanti);
            komut.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            //SqlDataAdapter adapter = new SqlDataAdapter("select * from satis", baglanti);
            adapter.Fill(dataset, "satis");
            dataGridView1.DataSource = dataset.Tables["satis"];
            baglanti.Close();
        }
        private void frmSatisListele_Load(object sender, EventArgs e)
        {
            satisListele();
        }
    }
}
