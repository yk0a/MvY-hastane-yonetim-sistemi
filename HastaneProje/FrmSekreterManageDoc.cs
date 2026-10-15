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
    public partial class FrmSekreterManageDoc : Form
    {
        public FrmSekreterManageDoc()
        {
            InitializeComponent();
        }
        sqlBaglantisi conn = new sqlBaglantisi();
        public int bransid;
        public string doctorTC;

        private void doktorTablosuDoldur()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT d.Docid,d.DocAd,d.DocSoyad,d.DocTC,b.Bransad,d.DocPassword FROM tbl_Doc d INNER JOIN tbl_Brans b ON d.DocBrans=b.Bransid", conn.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void branslaridoldur()
        {
            SqlCommand queryB = new SqlCommand("SELECT BransAd FROM tbl_Brans", conn.baglanti());
            SqlDataReader rdrB = queryB.ExecuteReader();
            while (rdrB.Read())
            {
                cmbBrans.Items.Add(rdrB[0].ToString());
            }
            conn.baglanti().Close();
        }

        private void FrmSekreterManageDoc_Load(object sender, EventArgs e)
        {
            doktorTablosuDoldur();
            branslaridoldur();

            if (doctorTC != null)
            {
                SqlCommand cmd = new SqlCommand("SELECT d.DocAd,d.DocSoyad,b.Bransad,d.DocTC,d.DocPassword FROM Tbl_Doc d INNER JOIN Tbl_Brans b ON d.DocBrans=b.Bransid WHERE DocTC=" + doctorTC, conn.baglanti());
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtAd.Text = dr[0].ToString();
                    txtSoyad.Text = dr[1].ToString();
                    cmbBrans.Text = dr[2].ToString();
                    mskTCKN.Text = dr[3].ToString();
                    txtPassword.Text = dr[4].ToString();

                }
                dr.Close();
                conn.baglanti().Close();
            }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd1 = new SqlCommand("UPDATE Tbl_Doc SET DocAD=@p1,DocSoyad=@p2,DocBrans=@p3,DocTC=@p4,DocPassword=@p5 WHERE DocTC='" + doctorTC + "'", conn.baglanti());
            cmd1.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd1.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd1.Parameters.AddWithValue("@p3", bransid);
            cmd1.Parameters.AddWithValue("@p4", mskTCKN.Text);
            cmd1.Parameters.AddWithValue("@p5", txtPassword.Text);
            cmd1.ExecuteNonQuery();
            conn.baglanti().Close();
            MessageBox.Show("Doktor Başarıyla Güncellendi!", "Doktor Bilgileri Güncelle", MessageBoxButtons.OK);
            doktorTablosuDoldur();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Doktor '" + txtAd.Text + " " + txtSoyad.Text + "'nın kaydını kaldırmak istediğine emin misin?", "Doktoru Kaldır", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {

                SqlCommand cmd1 = new SqlCommand("DELETE FROM Tbl_Doc WHERE DocTC=" + doctorTC, conn.baglanti());
                cmd1.ExecuteNonQuery();
                conn.baglanti().Close();
                doktorTablosuDoldur();
                txtAd.Text = "";
                txtSoyad.Text = "";
                txtPassword.Text = "";
                mskTCKN.Text = "";
                cmbBrans.Text = "";
                MessageBox.Show("Kayıt Başarıyla Silindi!", "Doktoru Kaldır", MessageBoxButtons.OK);
                doktorTablosuDoldur();
            }
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            string bra = cmbBrans.Text;
            SqlCommand cmdB = new SqlCommand("SELECT Bransid FROM Tbl_Brans WHERE BransAd='" + bra + "'", conn.baglanti());
            SqlDataReader drB = cmdB.ExecuteReader();
            while (drB.Read())
            {
                bransid = Convert.ToInt16(drB[0]);
            }
            drB.Close();
            conn.baglanti().Close();
        }

        private void btnRes_Click(object sender, EventArgs e)
        {
            doktorTablosuDoldur();
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtPassword.Text = "";
            mskTCKN.Text = "";
            cmbBrans.Text = "";
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected = dataGridView1.SelectedCells[0].RowIndex;

            txtAd.Text = dataGridView1.Rows[selected].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[selected].Cells[2].Value.ToString();
            mskTCKN.Text = dataGridView1.Rows[selected].Cells[3].Value.ToString();
            cmbBrans.Text = dataGridView1.Rows[selected].Cells[4].Value.ToString();
            txtPassword.Text = dataGridView1.Rows[selected].Cells[5].Value.ToString();
        }
    }
}
