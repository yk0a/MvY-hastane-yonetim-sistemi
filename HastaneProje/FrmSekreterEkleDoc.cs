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
    public partial class FrmSekreterEkleDoc : Form
    {
        public FrmSekreterEkleDoc()
        {
            InitializeComponent();
        }
        sqlBaglantisi conn = new sqlBaglantisi();
        int bransid;
        private void FrmSekreterEkleDoc_Load(object sender, EventArgs e)
        {
            SqlCommand query0 = new SqlCommand("SELECT * FROM tbl_Brans", conn.baglanti());
            SqlDataReader rdrB = query0.ExecuteReader();
            while (rdrB.Read())
            {
                cmbBrans.Items.Add(rdrB[1].ToString());
            }
            conn.baglanti().Close();
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Tbl_Doc(DocAd,DocSoyad,DocTC,DocBrans,DocPassword) VALUES (@p1,@p2,@p3,@p4,@p5)", conn.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", mskTCKN.Text);
            cmd.Parameters.AddWithValue("@p4", bransid);
            cmd.Parameters.AddWithValue("@p5", txtPassword.Text);
            cmd.ExecuteNonQuery();
            conn.baglanti().Close();
            MessageBox.Show("Doktor '" + txtAd.Text + " " + txtSoyad.Text + "' Başarıyla Eklendi!","Doktor Ekleme",MessageBoxButtons.OK);
            
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
    }
}
