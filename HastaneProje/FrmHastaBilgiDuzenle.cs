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
    public partial class FrmHastaBilgiDuzenle : Form
    {
        public FrmHastaBilgiDuzenle()
        {
            InitializeComponent();
        }
        sqlBaglantisi conn = new sqlBaglantisi();
        public bool cinsiyet;
        public string hTC;
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = false;
        }


        private void FrmHastaBilgiDuzenle_Load(object sender, EventArgs e)
        {
            SqlCommand cmd0 = new SqlCommand("SELECT * FROM tbl_Hasta WHERE HastaTC=@p0",conn.baglanti());
            cmd0.Parameters.AddWithValue("@p0", hTC);
            SqlDataReader rdr0 = cmd0.ExecuteReader();
            while(rdr0.Read())
            {
                txtAd.Text = rdr0[1].ToString();
                txtSoyad.Text = rdr0[2].ToString();
                mskTCKN.Text = rdr0[3].ToString();
                mskTel.Text = rdr0[4].ToString();
                txtPassword.Text = rdr0[5].ToString();
                if (Convert.ToBoolean(rdr0[6]) == true)
                {
                    radioButton1.Checked = true;
                }
                else { radioButton2.Checked = true;}
            }
            conn.baglanti().Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd1 = new SqlCommand("Update Tbl_Hasta SET HastaAd=@p1,HastaSoyad=@p2,HastaTelefon=@p4,HastaPassword=@p5,HastaCinsiyet=@p6 WHERE HastaTC=@t0", conn.baglanti());
            cmd1.Parameters.AddWithValue("@p1",txtAd.Text);
            cmd1.Parameters.AddWithValue("@p2",txtSoyad.Text);
            cmd1.Parameters.AddWithValue("@p4",mskTel.Text);
            cmd1.Parameters.AddWithValue("@p5",txtPassword.Text);
            cmd1.Parameters.AddWithValue("@p6",cinsiyet);
            cmd1.Parameters.AddWithValue("@t0",mskTCKN.Text);
            cmd1.ExecuteNonQuery();
            conn.baglanti().Close();
            this.Close();
            MessageBox.Show("Bilgiler başarıyla güncellendi!", "Bilgileri Güncelle", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
