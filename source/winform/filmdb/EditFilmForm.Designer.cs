namespace filmdb
{
    partial class EditFilmForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtNazev = new System.Windows.Forms.TextBox();
            this.txtRok = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtZanr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReziser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHodnoceni = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPopis = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 13;
            this.label1.Text = "Název filmu:";
            // 
            // txtNazev
            // 
            this.txtNazev.Location = new System.Drawing.Point(118, 12);
            this.txtNazev.Name = "txtNazev";
            this.txtNazev.Size = new System.Drawing.Size(250, 20);
            this.txtNazev.TabIndex = 12;
            // 
            // txtRok
            // 
            this.txtRok.Location = new System.Drawing.Point(118, 38);
            this.txtRok.Name = "txtRok";
            this.txtRok.Size = new System.Drawing.Size(100, 20);
            this.txtRok.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 11;
            this.label2.Text = "Rok vydání:";
            // 
            // txtZanr
            // 
            this.txtZanr.Location = new System.Drawing.Point(118, 64);
            this.txtZanr.Name = "txtZanr";
            this.txtZanr.Size = new System.Drawing.Size(250, 20);
            this.txtZanr.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 9;
            this.label3.Text = "Žánr:";
            // 
            // txtReziser
            // 
            this.txtReziser.Location = new System.Drawing.Point(118, 90);
            this.txtReziser.Name = "txtReziser";
            this.txtReziser.Size = new System.Drawing.Size(250, 20);
            this.txtReziser.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "Režisér:";
            // 
            // txtHodnoceni
            // 
            this.txtHodnoceni.Location = new System.Drawing.Point(118, 116);
            this.txtHodnoceni.Name = "txtHodnoceni";
            this.txtHodnoceni.Size = new System.Drawing.Size(100, 20);
            this.txtHodnoceni.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 5;
            this.label5.Text = "Hodnocení (0-10):";
            // 
            // txtPopis
            // 
            this.txtPopis.Location = new System.Drawing.Point(118, 142);
            this.txtPopis.Multiline = true;
            this.txtPopis.Name = "txtPopis";
            this.txtPopis.Size = new System.Drawing.Size(250, 100);
            this.txtPopis.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 3;
            this.label6.Text = "Popis filmu:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(215, 278);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Uložit změny";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(296, 278);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Zrušit";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(59, 245);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label8.Size = new System.Drawing.Size(234, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "!!Úprava plakátu pouze přes webovou aplikaci!!";
            // 
            // EditFilmForm
            // 
            this.ClientSize = new System.Drawing.Size(384, 324);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPopis);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtHodnoceni);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtReziser);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtZanr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRok);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNazev);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditFilmForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Upravit film";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNazev;
        private System.Windows.Forms.TextBox txtRok;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtZanr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReziser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtHodnoceni;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPopis;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label8;
    }
}