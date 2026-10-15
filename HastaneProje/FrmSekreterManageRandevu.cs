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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace HastaneProje
{
    public partial class FrmSekreterManageRandevu : Form
    {
        public FrmSekreterManageRandevu()
        {
            InitializeComponent();
        }
        sqlBaglantisi conn = new sqlBaglantisi();
        bool durumKontrol = false;
        int bransid, docid;
        private void TabloDoldur()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT r.Randevuid as id, r.RandevuDate as Tarih, r.RandevuSaat as Saat, b.BransAd as Polikinlik, d.DocAd + ' ' + d.DocSoyad as Doktor, r.RandevuDurum as Durum, r.HastaTC FROM Tbl_Randevu r INNER JOIN Tbl_Brans as b ON r.Randevubrans=b.Bransid INNER JOIN Tbl_Doc d ON d.Docid=r.RandevuDoc", conn.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void otoIdDoldur()
        {
            SqlCommand cmdid = new SqlCommand("SELECT TOP 1 Randevuid FROM Tbl_Randevu ORDER BY Randevuid desc", conn.baglanti());
            SqlDataReader drid = cmdid.ExecuteReader();
            while (drid.Read())
            {
                txtRandevuid.Text = (Convert.ToInt16(drid[0]) + 1).ToString();
            }
            drid.Close();
            conn.baglanti().Close();
        }
        private void FrmSekreterManageRandevu_Load(object sender, EventArgs e)
        {
            //oto id doldur
            otoIdDoldur();
            //tablo doldur
            TabloDoldur();

            //bransları doldur
            SqlCommand cmd0 = new SqlCommand("SELECT BransAd FROM Tbl_Brans", conn.baglanti());
            SqlDataReader dr0 = cmd0.ExecuteReader();
            while (dr0.Read())
            {
                cmbBrans.Items.Add(dr0[0]);
            }
            dr0.Close();
            conn.baglanti().Close();

            //doktorları doldur
            SqlCommand cmd00 = new SqlCommand("SELECT DoctorAd + ' ' + DoctorSoyad FROM Tbl_Brans", conn.baglanti());
            SqlDataReader dr00 = cmd0.ExecuteReader();
            while (dr00.Read())
            {
                cmbBrans.Items.Add(dr00[0]);
            }
            dr00.Close();
            conn.baglanti().Close();

            //hasta tc'leri doldur
            SqlCommand cmd000 = new SqlCommand("SELECT HastaTC FROM Tbl_Hasta", conn.baglanti());
            SqlDataReader dr000 = cmd000.ExecuteReader();
            while (dr000.Read())
            {
                cmbTC.Items.Add(dr000[0]);
            }
            dr000.Close();
            conn.baglanti().Close();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                durumKontrol = true;
            }
            else
            {
                durumKontrol = false;
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoc.Text = "";
            string bra = cmbBrans.Text;
            SqlCommand cmdB = new SqlCommand("SELECT Bransid FROM Tbl_Brans WHERE BransAd='" + bra + "'", conn.baglanti());
            SqlDataReader drB = cmdB.ExecuteReader();
            while (drB.Read())
            {
                bransid = Convert.ToInt16(drB[0]);
            }
            drB.Close();
            conn.baglanti().Close();

            cmbDoc.Items.Clear();
            SqlCommand cmdD = new SqlCommand("SELECT DocAd ,DocSoyad FROM tbl_Doc WHERE DocBrans='" + bransid + "'", conn.baglanti());
            SqlDataReader rdrD = cmdD.ExecuteReader();
            while (rdrD.Read())
            {
                cmbDoc.Items.Add(rdrD[0] + " " + rdrD[1]);
            }
            conn.baglanti().Close();
            cmbDoc.Enabled = true;
        }
        private void cmbDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmdD = new SqlCommand("SELECT Docid FROM Tbl_Doc WHERE (DocAd + ' ' + DocSoyad) ='" + cmbDoc.Text + "'", conn.baglanti());
            SqlDataReader drD = cmdD.ExecuteReader();
            while (drD.Read())
            {
                docid = Convert.ToInt16(drD[0]);
            }
            drD.Close();
            conn.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmdRolustur = new SqlCommand("INSERT INTO Tbl_Randevu(RandevuDate,RandevuSaat,RandevuBrans,RandevuDoc,RandevuDurum,HastaTC) VALUES (@p1,@p2,@p3,@p4,@p5,@p6)", conn.baglanti());
            cmdRolustur.Parameters.Add("@p1", SqlDbType.SmallDateTime).Value = DateTime.Parse(mskDate.Text);
            cmdRolustur.Parameters.AddWithValue("@p2", mskTime.Text);
            cmdRolustur.Parameters.AddWithValue("@p3", bransid);
            cmdRolustur.Parameters.AddWithValue("@p4", docid);
            cmdRolustur.Parameters.AddWithValue("@p5", durumKontrol);
            cmdRolustur.Parameters.AddWithValue("@p6", cmbTC.Text);
            cmdRolustur.ExecuteNonQuery();
            conn.baglanti().Close();
            MessageBox.Show("Randevu başarıyla oluşturuldu!", "Randevu Oluştur", MessageBoxButtons.OK);
            TabloDoldur();
        }


        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmdRguncelle = new SqlCommand("UPDATE Tbl_Randevu SET RandevuDate=@p1, RandevuSaat=@p2, RandevuBrans=@p3, RandevuDoc=@p4, RandevuDurum=@p5, HastaTC=@p6 WHERE Randevuid=" + txtRandevuid.Text.ToString().Trim(), conn.baglanti());
            cmdRguncelle.Parameters.Add("@p1", SqlDbType.SmallDateTime).Value = DateTime.Parse(mskDate.Text);
            cmdRguncelle.Parameters.AddWithValue("@p2", mskTime.Text);
            cmdRguncelle.Parameters.AddWithValue("@p3", bransid);
            cmdRguncelle.Parameters.AddWithValue("@p4", docid);
            cmdRguncelle.Parameters.AddWithValue("@p5", durumKontrol);
            cmdRguncelle.Parameters.AddWithValue("@p6", cmbTC.Text);
            cmdRguncelle.ExecuteNonQuery();
            conn.baglanti().Close();
            MessageBox.Show("Randevu başarıyla güncellendi!", "Randevuyu Güncelle", MessageBoxButtons.OK);
            TabloDoldur();
        }


        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bu randevuyu silmek istediğine emin misin?", "Randevuyu Kaldır", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SqlCommand cmdRsil = new SqlCommand("DELETE FROM Tbl_Randevu WHERE Randevuid=" + txtRandevuid.Text, conn.baglanti());
                cmdRsil.ExecuteNonQuery();
                conn.baglanti().Close();
                MessageBox.Show("Randevu başarıyla silindi!", "Randevuyu Sil", MessageBoxButtons.OK);
                TabloDoldur();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected = dataGridView1.SelectedCells[0].RowIndex;
            txtRandevuid.Text = dataGridView1.Rows[selected].Cells[0].Value.ToString().Trim();
            mskDate.Text = dataGridView1.Rows[selected].Cells[1].Value.ToString().Trim();
            mskTime.Text = dataGridView1.Rows[selected].Cells[2].Value.ToString().Trim();
            cmbBrans.Text = dataGridView1.Rows[selected].Cells[3].Value.ToString().Trim();
            cmbDoc.Text = dataGridView1.Rows[selected].Cells[4].Value.ToString().Trim();
            cmbDoc.Enabled = true;
            cmbTC.Text = dataGridView1.Rows[selected].Cells[6].Value.ToString().Trim();
            if (Convert.ToBoolean(dataGridView1.Rows[selected].Cells[5].Value))
            {
                checkBox1.Checked = true;
            }
            else { checkBox1.Checked = false; }
        }

        private void btnRes_Click(object sender, EventArgs e)
        {
            otoIdDoldur();
            mskDate.Text = "";
            mskTime.Text = "";
            cmbBrans.Text = "";
            cmbDoc.Text = "";
            cmbTC.Text = "";
            cmbDoc.Enabled = false;
            TabloDoldur();
        }

    }
}
