using System;

namespace filmdb
{
    partial class AddFilmForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtNazev;
        private System.Windows.Forms.TextBox txtRok;
        private System.Windows.Forms.TextBox txtZanr;
        private System.Windows.Forms.TextBox txtReziser;
        private System.Windows.Forms.TextBox txtHodnoceni;
        private System.Windows.Forms.TextBox txtPopis;
        private System.Windows.Forms.Button btnPoster;
        private System.Windows.Forms.Button btnSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtNazev = new System.Windows.Forms.TextBox();
            this.txtRok = new System.Windows.Forms.TextBox();
            this.txtZanr = new System.Windows.Forms.TextBox();
            this.txtReziser = new System.Windows.Forms.TextBox();
            this.txtHodnoceni = new System.Windows.Forms.TextBox();
            this.txtPopis = new System.Windows.Forms.TextBox();
            this.btnPoster = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Size = new System.Drawing.Size(460, 30);
            this.lblTitle.Text = "Přidání nového filmu";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // txtNazev
            this.txtNazev.Location = new System.Drawing.Point(30, 60);
            this.txtNazev.Size = new System.Drawing.Size(400, 23);
            this.txtNazev.Text = "Název filmu";

            // txtRok
            this.txtRok.Location = new System.Drawing.Point(30, 95);
            this.txtRok.Text = "Rok";

            // txtZanr
            this.txtZanr.Location = new System.Drawing.Point(150, 95);
            this.txtZanr.Text = "Žánr";

            // txtReziser
            this.txtReziser.Location = new System.Drawing.Point(30, 130);
            this.txtReziser.Size = new System.Drawing.Size(400, 23);
            this.txtReziser.Text = "Režisér";

            // txtHodnoceni
            this.txtHodnoceni.Location = new System.Drawing.Point(30, 165);
            this.txtHodnoceni.Text = "Hodnocení (např. 8.5)";

            // txtPopis
            this.txtPopis.Location = new System.Drawing.Point(30, 200);
            this.txtPopis.Multiline = true;
            this.txtPopis.Size = new System.Drawing.Size(400, 100);
            this.txtPopis.Text = "Popis filmu";

            // btnPoster
            this.btnPoster.Location = new System.Drawing.Point(30, 315);
            this.btnPoster.Size = new System.Drawing.Size(180, 30);
            this.btnPoster.Text = "🖼 Vybrat plakát";
            this.btnPoster.Click += new System.EventHandler(this.btnPoster_Click);

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(250, 315);
            this.btnSave.Size = new System.Drawing.Size(180, 30);
            this.btnSave.Text = "📤 Odeslat film";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // AddFilmForm
            this.ClientSize = new System.Drawing.Size(500, 370);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtNazev);
            this.Controls.Add(this.txtRok);
            this.Controls.Add(this.txtZanr);
            this.Controls.Add(this.txtReziser);
            this.Controls.Add(this.txtHodnoceni);
            this.Controls.Add(this.txtPopis);
            this.Controls.Add(this.btnPoster);
            this.Controls.Add(this.btnSave);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Přidat film";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}