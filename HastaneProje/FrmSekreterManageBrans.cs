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

namespace HastaneProje
{
    public partial class FrmSekreterManageBrans : Form
    {
        public FrmSekreterManageBrans()
        {
            InitializeComponent();
        }
        public int bransid;
        public string bransad;
        sqlBaglantisi conn = new sqlBaglantisi();
        public void bransDoldur()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT Bransid as id,Bransad as BransAd FROM tbl_Brans", conn.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void FrmSekreterManageBrans_Load(object sender, EventArgs e)
        {
            if (bransad != null)
            {
                txtBransid.Text = bransid.ToString();
                txtBransAd.Text = bransad.ToString();
            }


            bransDoldur();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmdGuncelle = new SqlCommand("UPDATE Tbl_Brans SET BransAd='" + txtBransAd.Text + "' WHERE Bransid=@p1", conn.baglanti());
            cmdGuncelle.Parameters.AddWithValue("@p1", Convert.ToInt16(txtBransid.Text));
            cmdGuncelle.ExecuteNonQuery();
            conn.baglanti().Close();
            MessageBox.Show("Brans başarıyla güncellendi!", "Branşı Güncelle", MessageBoxButtons.OK);
            bransDoldur();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("'" + txtBransAd.Text + "' adlı branşı kaldırmak istediğine emin misin?", "Branşı Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SqlCommand cmdSil = new SqlCommand("DELETE FROM Tbl_Brans WHERE Bransid=" + txtBransid.Text, conn.baglanti());
                cmdSil.ExecuteNonQuery();
                conn.baglanti().Close();
                bransDoldur();
                txtBransid.Text = "";
                txtBransAd.Text = "";
                MessageBox.Show("Brans başarıyla kaldırıldı!", "Branşı Sil", MessageBoxButtons.OK);
            }
            bransDoldur();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected = dataGridView1.SelectedCells[0].RowIndex;
            txtBransid.Text = dataGridView1.Rows[selected].Cells[0].Value.ToString().Trim();
            txtBransAd.Text = dataGridView1.Rows[selected].Cells[1].Value.ToString().Trim();
        }

        private void btnRes_Click(object sender, EventArgs e)
        {
            txtBransid.Text = "";
            txtBransAd.Text = "";
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmdG = new SqlCommand("INSERT INTO Tbl_Brans(BransAd) VALUES (@p1)", conn.baglanti());
            cmdG.Parameters.AddWithValue("@p1", txtBransAd.Text);
            cmdG.ExecuteNonQuery();
            conn.baglanti().Close();
            MessageBox.Show("Branş başarıyla eklendi!", "Branş ekle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bransDoldur();
        }
    }
}
