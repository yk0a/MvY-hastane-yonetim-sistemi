using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HastaneProje
{
    public partial class FrmHastaRegister : Form
    {
        public FrmHastaRegister()
        {
            InitializeComponent();
        }
        sqlBaglantisi conn = new sqlBaglantisi();
         bool cinsiyet;

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand query0 = new SqlCommand("SELECT HastaTC FROM tbl_Hasta WHERE HastaTC=@p0",conn.baglanti());
            query0.Parameters.AddWithValue("@p0", mskTCKN.Text);
            SqlDataReader rd0 = query0.ExecuteReader();
            if (rd0.Read())
            {
                MessageBox.Show("Bu TCNO'lu hasta zaten kayıtlı: " + mskTCKN.Text, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand query = new SqlCommand("INSERT INTO tbl_Hasta(HastaAd, HastaSoyad, HastaTC, HastaTelefon, HastaPassword, HastaCinsiyet) VALUES (@p1, @p2, @p3, @p4, @p5, @p6)", conn.baglanti());
                query.Parameters.AddWithValue("@p1", txtAd.Text);
                query.Parameters.AddWithValue("@p2", txtSoyad.Text);
                query.Parameters.AddWithValue("@p3", mskTCKN.Text);
                query.Parameters.AddWithValue("@p4", mskTel.Text);
                query.Parameters.AddWithValue("@p5", txtPassword.Text);
                query.Parameters.AddWithValue("@p6", cinsiyet);
                query.ExecuteNonQuery();
                conn.baglanti().Close();
                MessageBox.Show("Hasta Kaydı Başarılı!", "Hasta Kaydı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            
        }
    }
}
