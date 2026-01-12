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
        private System.Windows.Forms.Button btnCancel; // Přidáno tlačítko zrušit
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;

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
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(0, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(460, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Přidání nového filmu";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNazev
            // 
            this.txtNazev.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNazev.Location = new System.Drawing.Point(30, 83);
            this.txtNazev.Name = "txtNazev";
            this.txtNazev.Size = new System.Drawing.Size(400, 25);
            this.txtNazev.TabIndex = 1;
            // 
            // txtRok
            // 
            this.txtRok.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRok.Location = new System.Drawing.Point(30, 138);
            this.txtRok.Name = "txtRok";
            this.txtRok.Size = new System.Drawing.Size(190, 25);
            this.txtRok.TabIndex = 2;
            // 
            // txtZanr
            // 
            this.txtZanr.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtZanr.Location = new System.Drawing.Point(240, 138);
            this.txtZanr.Name = "txtZanr";
            this.txtZanr.Size = new System.Drawing.Size(190, 25);
            this.txtZanr.TabIndex = 3;
            // 
            // txtReziser
            // 
            this.txtReziser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReziser.Location = new System.Drawing.Point(30, 193);
            this.txtReziser.Name = "txtReziser";
            this.txtReziser.Size = new System.Drawing.Size(400, 25);
            this.txtReziser.TabIndex = 4;
            // 
            // txtHodnoceni
            // 
            this.txtHodnoceni.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHodnoceni.Location = new System.Drawing.Point(30, 248);
            this.txtHodnoceni.Name = "txtHodnoceni";
            this.txtHodnoceni.Size = new System.Drawing.Size(110, 25);
            this.txtHodnoceni.TabIndex = 5;
            // 
            // txtPopis
            // 
            this.txtPopis.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPopis.Location = new System.Drawing.Point(30, 303);
            this.txtPopis.Multiline = true;
            this.txtPopis.Name = "txtPopis";
            this.txtPopis.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPopis.Size = new System.Drawing.Size(400, 80);
            this.txtPopis.TabIndex = 6;
            // 
            // btnPoster
            // 
            this.btnPoster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPoster.Location = new System.Drawing.Point(30, 395);
            this.btnPoster.Name = "btnPoster";
            this.btnPoster.Size = new System.Drawing.Size(120, 35);
            this.btnPoster.TabIndex = 7;
            this.btnPoster.Text = "🖼 Plakát";
            this.btnPoster.UseVisualStyleBackColor = true;
            this.btnPoster.Click += new System.EventHandler(this.btnPoster_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(290, 395);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 35);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "📤 ODESLAT";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(160, 395);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Zrušit";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(27, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 15;
            this.label1.Text = "Název filmu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Rok";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(237, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Žánr";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Režisér";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 230);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Hodnocení (např. 8.5)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 285);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Popis filmu";
            // 
            // AddFilmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(460, 460);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtNazev);
            this.Controls.Add(this.txtRok);
            this.Controls.Add(this.txtZanr);
            this.Controls.Add(this.txtReziser);
            this.Controls.Add(this.txtHodnoceni);
            this.Controls.Add(this.txtPopis);
            this.Controls.Add(this.btnPoster);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AddFilmForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Přidat nový film";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}