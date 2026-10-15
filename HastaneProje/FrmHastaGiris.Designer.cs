namespace HastaneProje
{
    partial class FrmHastaGiris
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.mskTC = new System.Windows.Forms.MaskedTextBox();
            this.linkLblForgot = new System.Windows.Forms.LinkLabel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.linkLblRegister = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Ivory;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Font = new System.Drawing.Font("Monotype Corsiva", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(65, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(440, 74);
            this.label4.TabIndex = 8;
            this.label4.Text = "Hasta Giriş Paneli";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Himalaya", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(156, 103);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 30);
            this.label1.TabIndex = 9;
            this.label1.Text = "TCKN:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Himalaya", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(164, 145);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 30);
            this.label2.TabIndex = 10;
            this.label2.Text = "Şifre:";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.Ivory;
            this.txtPassword.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtPassword.Location = new System.Drawing.Point(243, 138);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(187, 35);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // mskTC
            // 
            this.mskTC.BackColor = System.Drawing.Color.Ivory;
            this.mskTC.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.mskTC.HidePromptOnLeave = true;
            this.mskTC.Location = new System.Drawing.Point(243, 98);
            this.mskTC.Margin = new System.Windows.Forms.Padding(4);
            this.mskTC.Mask = "00000000000";
            this.mskTC.Name = "mskTC";
            this.mskTC.Size = new System.Drawing.Size(187, 35);
            this.mskTC.SkipLiterals = false;
            this.mskTC.TabIndex = 1;
            this.mskTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskTC.ValidatingType = typeof(int);
            // 
            // linkLblForgot
            // 
            this.linkLblForgot.AutoSize = true;
            this.linkLblForgot.Location = new System.Drawing.Point(439, 158);
            this.linkLblForgot.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLblForgot.Name = "linkLblForgot";
            this.linkLblForgot.Size = new System.Drawing.Size(99, 16);
            this.linkLblForgot.TabIndex = 4;
            this.linkLblForgot.TabStop = true;
            this.linkLblForgot.Text = "Şifremi Unuttum";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Honeydew;
            this.btnLogin.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnLogin.Location = new System.Drawing.Point(243, 181);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(188, 50);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Giriş Yap";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // linkLblRegister
            // 
            this.linkLblRegister.AutoSize = true;
            this.linkLblRegister.BackColor = System.Drawing.Color.MediumTurquoise;
            this.linkLblRegister.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.linkLblRegister.LinkColor = System.Drawing.Color.Blue;
            this.linkLblRegister.Location = new System.Drawing.Point(16, 219);
            this.linkLblRegister.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLblRegister.Name = "linkLblRegister";
            this.linkLblRegister.Size = new System.Drawing.Size(94, 18);
            this.linkLblRegister.TabIndex = 5;
            this.linkLblRegister.TabStop = true;
            this.linkLblRegister.Text = "Hesap Oluştur";
            this.linkLblRegister.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // FrmHastaGiris
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumTurquoise;
            this.ClientSize = new System.Drawing.Size(613, 246);
            this.Controls.Add(this.linkLblRegister);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.linkLblForgot);
            this.Controls.Add(this.mskTC);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmHastaGiris";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hasta Giriş";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmHastaGiris_FormClosed);
            this.Load += new System.EventHandler(this.FrmHastaGiris_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.MaskedTextBox mskTC;
        private System.Windows.Forms.LinkLabel linkLblForgot;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.LinkLabel linkLblRegister;
    }
}