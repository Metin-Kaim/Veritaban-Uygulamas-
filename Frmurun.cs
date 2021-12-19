using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace postgreurunproje
{
    public partial class Frmurun : Form
    {
        public Frmurun()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=dburunler;user Id=postgres;password=Laboratuvar26");

        private void Frmurun_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from kategoriler", baglanti);
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter("select * from firmalar", baglanti);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            da.Fill(dt);
            da1.Fill(dt1);
            ComboUrunKat.DisplayMember = "kat_ad";
            ComboUrunKat.ValueMember = "kat_id";
            cmbxfirmaad.DisplayMember = "firma_ad";
            cmbxfirmaad.ValueMember = "firma_id";
            ComboUrunKat.DataSource = dt;
            cmbxfirmaad.DataSource = dt1;
            baglanti.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into urunler (urun_id,urun_ad,urun_stok,urun_alis_fiyat,urun_satis_fiyat,urun_kategori,urun_firma_id) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglanti);
            komut1.Parameters.AddWithValue("@p1", int.Parse(txtUrunID.Text));
            komut1.Parameters.AddWithValue("@p2", txtUrunAd.Text);
            komut1.Parameters.AddWithValue("@p3", int.Parse(numUrunStok.Value.ToString()));
            komut1.Parameters.AddWithValue("@p4", double.Parse(txtUrunAlisF.Text));
            komut1.Parameters.AddWithValue("@p5", double.Parse(txtUrunSatisF.Text));
            komut1.Parameters.AddWithValue("@p6", int.Parse(ComboUrunKat.SelectedValue.ToString()));
            komut1.Parameters.AddWithValue("@p7", int.Parse(cmbxfirmaad.SelectedValue.ToString()));
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("ekleme yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("Delete from urunler where urun_id=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", int.Parse(txtUrunID.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("silme gerçekleşti");
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("update urunler set urun_stok=@p1,urun_alis_fiyat=@p2,urun_satis_fiyat=@p3 where urun_id=@p4", baglanti);

            komut3.Parameters.AddWithValue("@p1", int.Parse(numUrunStok.Value.ToString()));
            komut3.Parameters.AddWithValue("@p2", double.Parse(txtUrunAlisF.Text));
            komut3.Parameters.AddWithValue("@p3", double.Parse(txtUrunSatisF.Text));
            komut3.Parameters.AddWithValue("@p4", int.Parse(txtUrunID.Text));
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("güncellendi");
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut4 = new NpgsqlCommand("select * from view_urunler_listele", baglanti);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut4);
            DataSet dt = new DataSet();
            da.Fill(dt);
            dataGridView1.DataSource = dt.Tables[0];
            baglanti.Close();
        }

        private void txtUfirmaid_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
