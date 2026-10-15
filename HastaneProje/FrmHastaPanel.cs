using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneProje
{
    public partial class FrmHastaPanel : Form
    {
        Form AnaForm;
        public FrmHastaPanel(Form gelenForm)
        {
            InitializeComponent();
            AnaForm = gelenForm;
        }
        sqlBaglantisi conn = new sqlBaglantisi();
        public string hastaTCNO, hastAdSoyad;
        bool pKapat=true;
        public int BransIDbul(string bransad)
        {
            int bransid=0;
            SqlCommand cmduWu = new SqlCommand("Select Bransid FROM Tbl_Brans WHERE BransAd='"+bransad+"'",conn.baglanti());
            SqlDataReader rdruWu = cmduWu.ExecuteReader();
            while (rdruWu.Read())
            {
                bransid = Convert.ToInt16(rdruWu[0]);
            }
            return bransid;
            conn.baglanti().Close();
        }
        private int DocIDbul(string docad)
        {
            int docid=0;
            SqlCommand cmdfufu = new SqlCommand("Select Docid FROM Tbl_Doc WHERE (DocAd +' '+ DocSoyad)='" + docad + "'", conn.baglanti());
            SqlDataReader rdrfufu = cmdfufu.ExecuteReader();
            while (rdrfufu.Read())
            {
                docid = Convert.ToInt16(rdrfufu[0]);
            }
                return docid;
            conn.baglanti().Close();
        }
        void tablo1doldur()
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT r.randevuid as id,r.randevudate as Tarih,r.randevusaat as Saat,b.bransad as Brans,(d.DocAd+' '+d.DocSoyad) as Doktor,r.Sikayet FROM tbl_Randevu r INNER JOIN Tbl_Brans b ON b.bransid=r.randevubrans INNER JOIN Tbl_Doc d ON d.Docid=r.RandevuDoc WHERE HastaTC='" + hastaTCNO + "'", conn.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
        }
        private void FrmHastaPanel_Load(object sender, EventArgs e)
        {
            txtTC.Text = hastaTCNO;
            txtAdSoyad.Text = hastAdSoyad;
            cmbBrans.Focus();

            tablo1doldur();

            //bransları doldurma
            SqlCommand query2 = new SqlCommand("SELECT BransAd FROM tbl_Brans",conn.baglanti());
            SqlDataReader rdrB = query2.ExecuteReader();
            while(rdrB.Read())
            {
                cmbBrans.Items.Add(rdrB[0].ToString());
            }
            conn.baglanti().Close();
        }


        private void bilgileriGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHastaBilgiDuzenle frmDuzenle = new FrmHastaBilgiDuzenle();
            frmDuzenle.hTC=txtTC.Text;
            frmDuzenle.Show();
            this.Enabled = false;
            frmDuzenle.FormClosed += (s, args) => this.Enabled = true;
        }
        private void çıkışYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dgResut = MessageBox.Show(
                "Çıkış yapmak istediğinize emin misiniz?\n***Giriş ekranına yönlendirileceksiniz.",
                "Çıkış Yap",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dgResut == DialogResult.Yes)
            {
                pKapat = false;
                FrmHastaGiris FrmHgiris = new FrmHastaGiris(AnaForm);
                FrmHgiris.Show();
                FrmHgiris.Focus();
                this.Close();
            }
        }

        private void FrmHastaPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (pKapat)
            {
                Application.Exit();
            }
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pol = cmbBrans.Text;
            cmbDoc.Items.Clear();
            SqlCommand cmd1 = new SqlCommand("SELECT d.DocAd, d.DocSoyad FROM tbl_Doc d INNER JOIN tbl_Brans b ON d.DocBrans =  b.Bransid WHERE b.BransAd=@p1",conn.baglanti());
            cmd1.Parameters.AddWithValue("@p1", pol);
            SqlDataReader rdr1 = cmd1.ExecuteReader();
            while (rdr1.Read())
            {
                cmbDoc.Items.Add(rdr1[0] + " " +  rdr1[1]);
            }
            conn.baglanti().Close();
            cmbDoc.Enabled = true;
        }
        private void aktfRandGetir()
        {
            DataTable dtD = new DataTable();
            SqlDataAdapter dp = new SqlDataAdapter("Select r.randevuid as id, r.randevudate as Tarih, r.randevusaat as Saat, b.bransad as Brans,(d.Docad+' '+d.DocSoyad) as Doktor From Tbl_Randevu r INNER JOIN Tbl_Brans b ON r.RandevuBrans=b.Bransid INNER JOIN Tbl_Doc d ON d.Docid=r.RandevuDoc WHERE r.RandevuDurum=0 AND b.BransAd='" + cmbBrans.Text + "' AND (d.DocAd+' '+d.DocSoyad)='"+cmbDoc.Text+"'", conn.baglanti()); 
            dp.Fill(dtD);
            dataGridView2.DataSource = dtD;
        }
        private void cmbDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            aktfRandGetir();
        }

        private void hesabıSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dgResult = MessageBox.Show("Hesabınızı silmek istediğinize emin misiniz?", "Hesabı Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgResult == DialogResult.Yes)
            {
                SqlCommand cmd000 = new SqlCommand("DELETE FROM Tbl_Hasta WHERE HastaTC="+txtTC,conn.baglanti());
                cmd000.ExecuteNonQuery();
                MessageBox.Show("Hesabınız Başarıyla Silindi!", "Hesap Silindi", MessageBoxButtons.OK);
                this.Close();
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView2.Rows[selected].Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmdR = new SqlCommand("UPDATE Tbl_Randevu SET RandevuBrans=@p1,RandevuDoc=@p2,Sikayet=@p3,HastaTC=@p4,RandevuDurum=1 WHERE Randevuid=@p5",conn.baglanti());
            cmdR.Parameters.AddWithValue("@p1",BransIDbul(cmbBrans.Text));
            cmdR.Parameters.AddWithValue("@p2",DocIDbul(cmbDoc.Text));
            cmdR.Parameters.AddWithValue("@p3",richTextBox1.Text);
            cmdR.Parameters.AddWithValue("@p4",txtTC.Text);
            cmdR.Parameters.AddWithValue("@p5",txtID.Text);
            cmdR.ExecuteNonQuery();
            conn.baglanti();
            MessageBox.Show("Randevunuz alınmıştır!","Randevu Al",MessageBoxButtons.OK);
            aktfRandGetir();
            txtID.Text="";
            cmbBrans.Text=" ";
            cmbDoc.Text = " ";cmbDoc.Enabled=false;
            richTextBox1.Text = "";
            tablo1doldur();

        }

        private void FrmHastaPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pKapat)
            {
                DialogResult result = MessageBox.Show(
                    "Kapatmak istediğinize emin misiniz?\n***Program sonlandırılacak.",
                    "Kapat",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            
        }
    }
}
