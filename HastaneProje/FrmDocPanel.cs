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
    public partial class FrmDocPanel : Form
    {
        Form AnaForm;
        public FrmDocPanel(Form gelenForm)
        {
            InitializeComponent();
            AnaForm = gelenForm;
        }
        sqlBaglantisi conn = new sqlBaglantisi();
        bool pKapat = true;
        public string DocTC, DocAdsoyad;
        private void FrmDocPanel_Load(object sender, EventArgs e)
        {
            DocFormLoad();
        }
        private void DocFormLoad()
        {
            txtTC.Text = DocTC;
            txtAdSoyad.Text = "Dr." + DocAdsoyad;

            RandevularıYukle();
        }
        private void RandevularıYukle()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT r.Randevuid as id, r.RandevuDate as Tarih, r.RandevuSaat as Saat, r.RandevuDurum, (h.HastaAd +' '+ h.HastaSoyad) as Hasta FROM Tbl_Randevu r INNER JOIN Tbl_Hasta h ON h.HastaTC=r.HastaTC INNER JOIN Tbl_Doc d ON d.Docid=r.RandevuDoc WHERE d.DocTC='" + DocTC + "'", conn.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.baglanti().Close();
        }

        private void bilgilerimiGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDocBilgiDuzenle frmDuzenle = new FrmDocBilgiDuzenle(this);
            frmDuzenle.dTC = txtTC.Text;
            frmDuzenle.Show();
            this.Enabled = false;

            frmDuzenle.FormClosed += (s, args) => DocFormLoad();
        }

        private void duyurularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDuyurular frmDuyuru = new FrmDuyurular();
            frmDuyuru.Show();

            frmDuyuru.FormClosed += (s, args) => this.Focus();
        }

        private void çıkışYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dgResut = MessageBox.Show("Çıkış yapmak istediğinize emin misiniz?\n***Giriş ekranına yönlendirileceksiniz.", "Çıkış Yap", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgResut == DialogResult.Yes)
            {
                pKapat = false;
                FrmDocGiris frmDG = new FrmDocGiris(AnaForm);
                frmDG.Show();
                frmDG.Focus();
                this.Close();
            }
        }

        private void FrmDocPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pKapat)
            {
                DialogResult dgResult = MessageBox.Show("Kapatmak istediğinize emin misiniz?\n***Program sonlandırılacak.", "Kapat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            string sikayetid = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            if (sikayetid != null)
            {
                SqlCommand cmdSkyt = new SqlCommand("SELECT Sikayet FROM Tbl_Randevu WHERE Randevuid=" + sikayetid, conn.baglanti());
                SqlDataReader drSkyt = cmdSkyt.ExecuteReader();
                while (drSkyt.Read())
                {
                    richTextBox1.Text = drSkyt[0].ToString();
                }
                conn.baglanti().Close();
            }

        }

        private void FrmDocPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (pKapat) { Application.Exit(); }
        }
    }
}
