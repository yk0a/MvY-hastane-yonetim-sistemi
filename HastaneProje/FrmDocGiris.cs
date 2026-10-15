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
    public partial class FrmDocGiris : Form
    {
        Form AnaForm;
        public FrmDocGiris(Form gelenForm)
        {
            InitializeComponent();
            AnaForm = gelenForm;
        }
        sqlBaglantisi conn = new sqlBaglantisi();
        string tcno;
        bool cikis = true;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            tcno = mskTC.Text;
            SqlCommand query = new SqlCommand("SELECT * FROM Tbl_DOC WHERE DocTC=@p1", conn.baglanti());
            query.Parameters.AddWithValue("@p1", tcno);
            SqlDataReader rdr = query.ExecuteReader();

            if (rdr.Read())
            {
                rdr.Close();
                SqlCommand query2 = new SqlCommand("SELECT * FROM tbl_Doc WHERE DocTC=@u1 and DocPassword=@u2", conn.baglanti());
                query2.Parameters.AddWithValue("@u1", tcno);
                query2.Parameters.AddWithValue("@u2", txtPassword.Text);
                SqlDataReader rdr2 = query2.ExecuteReader();
                if (rdr2.Read())
                {
                    string Sas = rdr2[1].ToString() + " " + rdr2[2].ToString();
                    MessageBox.Show("Kimlik doğrulandı: Dr." + Sas, "Başarılı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rdr2.Close();
                    conn.baglanti().Close();
                    FrmDocPanel formDoc = new FrmDocPanel(AnaForm);
                    formDoc.DocTC = tcno;
                    formDoc.DocAdsoyad = Sas;
                    formDoc.Show();
                    cikis = false;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Şifre Yanlış!", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("**" + tcno + "**" + " TCNO'lu Doktor kaydı bulunamadı!", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmDocGiris_Load(object sender, EventArgs e)
        {

        }

        private void FrmDocGiris_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (cikis)
            {
                AnaForm.Show();
                AnaForm.Focus();
            }
        }
    }
}
