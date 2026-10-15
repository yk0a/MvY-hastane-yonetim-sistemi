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
    public partial class FrmSekreterGiris : Form
    {
        private Form Anaform;
        public FrmSekreterGiris(Form gelenForm)
        {
            InitializeComponent();
            Anaform = gelenForm;
        }
        string tcno;
        sqlBaglantisi conn = new sqlBaglantisi();
        public bool cikis=true;
        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            tcno = mskTC.Text;
            SqlCommand query = new SqlCommand("SELECT * FROM tbl_Sekreter WHERE SekreterTC=@p1", conn.baglanti());
            query.Parameters.AddWithValue("@p1", tcno);
            SqlDataReader rdr = query.ExecuteReader();

            if (rdr.Read())
            {
                rdr.Close();
                SqlCommand query2 = new SqlCommand("SELECT * FROM tbl_Sekreter WHERE SekreterTC=@u1 and SekreterPassword=@u2", conn.baglanti());
                query2.Parameters.AddWithValue("@u1", tcno);
                query2.Parameters.AddWithValue("@u2", txtPassword.Text);
                SqlDataReader rdr2 = query2.ExecuteReader();
                if (rdr2.Read())
                {
                    string Sas = rdr2[1].ToString();
                    MessageBox.Show("Kimlik doğrulandı: " + Sas, "Başarılı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rdr2.Close();
                    conn.baglanti().Close();
                    FrmSekreterPanel formSekreter = new FrmSekreterPanel(Anaform);
                    formSekreter.SekreterTC = tcno;
                    formSekreter.SekreterAdSoyad = Sas;
                    formSekreter.Show();
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
                MessageBox.Show("**" + tcno + "**" + " TCNO'lu Sekreter kaydı bulunamadı!", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmSekreterGiris_Load(object sender, EventArgs e)
        {
            
        }

        private void FrmSekreterGiris_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(cikis)
            {
                Anaform.Show();
            }
        }
    }
}
