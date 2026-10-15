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
    public partial class FrmSekreterPanel : Form
    {
        private Form Anaform;
        public FrmSekreterPanel(Form gelenForm)
        {
            InitializeComponent();
            Anaform = gelenForm;
        }
        bool pKapat = true;
        sqlBaglantisi conn = new sqlBaglantisi();
        public string SekreterTC, SekreterAdSoyad;
        public void bransTablosuDoldur()
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT Bransid as id,Bransad as BransAd FROM tbl_Brans", conn.baglanti());
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;
        }
        public void doktorTablosuDoldur()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT d.Docid as id,(d.DocAd + ' ' + d.DocSoyad) as AdSoyad,b.Bransad as Brans,d.DocTC as TCNO FROM tbl_Doc d INNER JOIN tbl_Brans b ON d.DocBrans=b.Bransid", conn.baglanti());
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
        }
        private void docUptAfter()
        {
            this.Enabled = true;
            doktorTablosuDoldur();
            this.Focus();
        }
        private void bransUpdAfter()
        {
            this.Enabled = true;
            bransTablosuDoldur();
            this.Focus();
        }
        private void FrmSekreterPanel_Load(object sender, EventArgs e)
        {
            txtAdSoyad.Text = SekreterAdSoyad;
            txtTC.Text = SekreterTC;

            //tabloları doldur
            bransTablosuDoldur();
            doktorTablosuDoldur();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string duyuru = richTextBox1.Text;
            SqlCommand queryD = new SqlCommand("INSERT INTO Tbl_Duyurular(DuyuruMesaj) VALUES (@p1)", conn.baglanti());
            queryD.Parameters.AddWithValue("@p1",duyuru);
            queryD.ExecuteNonQuery();
            conn.baglanti();
            richTextBox1.Clear();
            MessageBox.Show("Duyuru Başarıyla Gönderildi", "Duyuru", MessageBoxButtons.OK);
        }
        private void doktorEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSekreterEkleDoc frmED = new FrmSekreterEkleDoc();
            frmED.Show();

            frmED.FormClosed += (s, args) => docUptAfter();
        }
        private void doktorlarıDenetleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSekreterManageDoc frmMD = new FrmSekreterManageDoc();
            frmMD.Show();
            frmMD.FormClosed += (s, args) => docUptAfter();
        }
        private void randevularıYönetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmSekreterManageRandevu frmMR = new FrmSekreterManageRandevu();
            frmMR.Show();
        }
        private void çıkışYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dgResut = MessageBox.Show("Çıkış yapmak istediğinize emin misiniz?\n***Giriş ekranına yönlendirileceksiniz.", "Çıkış Yap", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgResut == DialogResult.Yes)
            {
                pKapat = false;
                FrmSekreterGiris frmSG = new FrmSekreterGiris(Anaform);
                frmSG.cikis = true;
                frmSG.Show();
                this.Close();
            }
        }
        private void FrmSekreterPanel_FormClosing(object sender, FormClosingEventArgs e)
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
        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected = dataGridView2.SelectedCells[0].RowIndex;
            int secilenBransid = Convert.ToInt16(dataGridView2.Rows[selected].Cells[0].Value);
            string secilenBransAd = dataGridView2.Rows[selected].Cells[1].Value.ToString();
            FrmSekreterManageBrans frmManBrans = new FrmSekreterManageBrans();
            frmManBrans.bransid = secilenBransid;
            frmManBrans.bransad = secilenBransAd;
            frmManBrans.Show();
            this.Enabled = false;

            frmManBrans.FormClosed += (s, args) => bransUpdAfter();
        }
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected = dataGridView1.SelectedCells[0].RowIndex;
            string secilenDocTC = dataGridView1.Rows[selected].Cells[3].Value.ToString();
            FrmSekreterManageDoc frmManDoc = new FrmSekreterManageDoc();
            frmManDoc.doctorTC = secilenDocTC;
            frmManDoc.Show();
            this.Enabled = false;

            frmManDoc.FormClosed += (s, args) => docUptAfter();
        }
        private void bransDenetleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmSekreterManageBrans frmMB = new FrmSekreterManageBrans();
            frmMB.Show();
            frmMB.FormClosed += (s, args) => bransUpdAfter();
        }
        private void btnRes_Click(object sender, EventArgs e)
        {
            doktorTablosuDoldur();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            bransTablosuDoldur();
        }

        private void duyurularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDuyurular FrmDuyuru = new FrmDuyurular();
            FrmDuyuru.Show();

            FrmDuyuru.FormClosed += (s, args) => this.Focus();
        }

        private void FrmSekreterPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (pKapat) { Application.Exit(); }
        }
    }
}
