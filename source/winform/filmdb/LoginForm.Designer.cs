namespace filmdb
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.LinkLabel linkRegister;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.linkRegister = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Size = new System.Drawing.Size(360, 30);
            this.lblTitle.Text = "Přihlášení do FilmDB";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblUsername
            this.lblUsername.Location = new System.Drawing.Point(30, 60);
            this.lblUsername.Text = "Uživatelské jméno:";

            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(30, 80);
            this.txtUsername.Size = new System.Drawing.Size(300, 23);

            // lblPassword
            this.lblPassword.Location = new System.Drawing.Point(30, 115);
            this.lblPassword.Text = "Heslo:";

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(30, 135);
            this.txtPassword.Size = new System.Drawing.Size(300, 23);
            this.txtPassword.UseSystemPasswordChar = true;

            // btnLogin
            this.btnLogin.Location = new System.Drawing.Point(30, 180);
            this.btnLogin.Size = new System.Drawing.Size(300, 30);
            this.btnLogin.Text = "🔐 Přihlásit se";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // linkRegister
            this.linkRegister.Location = new System.Drawing.Point(30, 220);
            this.linkRegister.Size = new System.Drawing.Size(300, 23);
            this.linkRegister.Text = "Nemáš účet? Zaregistruj se na webu";
            this.linkRegister.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkRegister.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRegister_LinkClicked);

            // LoginForm
            this.ClientSize = new System.Drawing.Size(360, 270);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.linkRegister);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
