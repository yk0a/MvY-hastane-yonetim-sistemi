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
    public partial class FrmDocBilgiDuzenle : Form
    {
        FrmDocPanel DocPanel;
        public FrmDocBilgiDuzenle(FrmDocPanel FrmDocPanel)
        {
            InitializeComponent();
            DocPanel = FrmDocPanel;
        }
        public string dTC;
        sqlBaglantisi conn = new sqlBaglantisi();
        int BRANSid;
        private int BRANSidBul(string bransad)
        {
            int id = 0;
            SqlCommand cmdbib = new SqlCommand("SELECT bransid FROM Tbl_Brans WHERE Bransad='" + bransad + "'", conn.baglanti());
            SqlDataReader drbib = cmdbib.ExecuteReader();
            while (drbib.Read())
            {
                id = Convert.ToInt16(drbib[0]);
            }
            conn.baglanti().Close();
            return id;
        }
        private void FrmDocBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTCKN.Text= dTC;

            //brans doldur
            SqlCommand cmdB = new SqlCommand("SELECT BransAd FROM tbl_Brans", conn.baglanti());
            SqlDataReader rdrB = cmdB.ExecuteReader();
            while (rdrB.Read())
            {
                cmbBrans.Items.Add(rdrB[0].ToString());
            }
            conn.baglanti().Close();
            int docbransid;
            //doctor bilgilerini doldur
            SqlCommand cmd0 = new SqlCommand("SELECT d.docad,d.docsoyad,b.bransad,d.docpassword,b.bransid FROM Tbl_Doc d INNER JOIN Tbl_Brans b ON b.bransid=d.docbrans WHERE DocTC="+dTC,conn.baglanti());
            SqlDataReader rdr0 = cmd0.ExecuteReader();
            while(rdr0.Read())
            {
                txtAd.Text = rdr0[0].ToString();
                txtSoyad.Text = rdr0[1].ToString();
                cmbBrans.Text = rdr0[2].ToString();
                txtPassword.Text = rdr0[3].ToString();
                docbransid = Convert.ToInt16(rdr0[4]);
            }
            conn.baglanti().Close();
        }
        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            BRANSid = BRANSidBul(cmbBrans.Text);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd1 = new SqlCommand("UPDATE Tbl_Doc SET DocAD=@p1,DocSoyad=@p2,DocBrans=@p3,DocTC=@p4,DocPassword=@p5 WHERE DocTC="+dTC,conn.baglanti());
            cmd1.Parameters.AddWithValue("@p1",txtAd.Text);
            cmd1.Parameters.AddWithValue("@p2",txtSoyad.Text);
            cmd1.Parameters.AddWithValue("@p3",BRANSid);
            cmd1.Parameters.AddWithValue("@p4",mskTCKN.Text);
            cmd1.Parameters.AddWithValue("@p5",txtPassword.Text);
            cmd1.ExecuteNonQuery();
            conn.baglanti().Close();
            MessageBox.Show("Bilgiler başarıyla güncellendi!", "Bilgileri Güncelle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DocPanel.DocTC = mskTCKN.Text;
            DocPanel.DocAdsoyad = txtAd.Text + " " + txtSoyad.Text;
            DocPanel.Enabled = true;
            DocPanel.Focus();
            this.Close();
        }

    }
}
