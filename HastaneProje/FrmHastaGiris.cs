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
    public partial class FrmHastaGiris : Form
    {
        Form AnaForm;
        public FrmHastaGiris(Form gelenForm)
        {
            InitializeComponent();
            AnaForm = gelenForm;
        }
        sqlBaglantisi conn = new sqlBaglantisi();
        bool cikis = true;
        private void FrmHastaGiris_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaRegister frmReg = new FrmHastaRegister();
            frmReg.Show();
            this.Enabled = false;
            frmReg.FormClosed += (s, args) => this.Enabled = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string tcno;
            tcno = mskTC.Text;
            SqlCommand query = new SqlCommand("SELECT * FROM tbl_Hasta WHERE HastaTC=@p1", conn.baglanti());
            query.Parameters.AddWithValue("@p1", tcno);
            SqlDataReader rdr = query.ExecuteReader();

            if (rdr.Read())
            {
                rdr.Close();
                SqlCommand query2 = new SqlCommand("SELECT * FROM tbl_Hasta WHERE HastaTC=@u1 and HastaPassword=@u2", conn.baglanti());
                query2.Parameters.AddWithValue("@u1", tcno);
                query2.Parameters.AddWithValue("@u2", txtPassword.Text);
                SqlDataReader rdr2 = query2.ExecuteReader();
                if (rdr2.Read())
                {
                    string has = rdr2[1] + " " + rdr2[2];
                    MessageBox.Show("Kimlik doğrulandı: " + has, "Başarılı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rdr2.Close();
                    conn.baglanti().Close();
                    FrmHastaPanel formHasta = new FrmHastaPanel(AnaForm);
                    formHasta.hastaTCNO = tcno;
                    formHasta.hastAdSoyad = has;
                    formHasta.Show();
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
                MessageBox.Show("**" + tcno + "**" + " TCNO'lu hasta kaydı bulunamadı!", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmHastaGiris_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (cikis)
            {
                AnaForm.Show();
                AnaForm.Focus();
            }
        }
    }
}
