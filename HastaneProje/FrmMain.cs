using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneProje
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        
        private void btnDoc_Click(object sender, EventArgs e)
        {
            FrmDocGiris frmDgiris = new FrmDocGiris(this);
            frmDgiris.Show();
            this.Hide();
        }

        private void btnSekreter_Click(object sender, EventArgs e)
        {
            FrmSekreterGiris frmSgiris = new FrmSekreterGiris(this);
            frmSgiris.Show();
            this.Hide();
        }

        private void btnHasta_Click(object sender, EventArgs e)
        {
            FrmHastaGiris frmHgiris = new FrmHastaGiris(this);
            frmHgiris.Show();
            this.Hide();
        }
    }
}
