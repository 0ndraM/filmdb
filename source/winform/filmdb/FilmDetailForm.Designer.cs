using System;

namespace filmdb
{
    partial class FilmDetailForm
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

        private System.Windows.Forms.PictureBox pictureBoxPoster;
        private System.Windows.Forms.Label lblNazev;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblReziser;
        private System.Windows.Forms.Label lblHodnoceni;
        private System.Windows.Forms.TextBox txtPopis;
        private System.Windows.Forms.Panel panelLine;
        private System.Windows.Forms.Button btnClose;

        private void InitializeComponent()
        {
            this.pictureBoxPoster = new System.Windows.Forms.PictureBox();
            this.lblNazev = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblReziser = new System.Windows.Forms.Label();
            this.lblHodnoceni = new System.Windows.Forms.Label();
            this.txtPopis = new System.Windows.Forms.TextBox();
            this.panelLine = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPoster)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxPoster
            // 
            this.pictureBoxPoster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.pictureBoxPoster.Location = new System.Drawing.Point(25, 25);
            this.pictureBoxPoster.Name = "pictureBoxPoster";
            this.pictureBoxPoster.Size = new System.Drawing.Size(210, 310);
            this.pictureBoxPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPoster.TabIndex = 0;
            this.pictureBoxPoster.TabStop = false;
            // 
            // lblNazev
            // 
            this.lblNazev.AutoSize = true;
            this.lblNazev.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblNazev.Location = new System.Drawing.Point(255, 20);
            this.lblNazev.MaximumSize = new System.Drawing.Size(500, 0);
            this.lblNazev.Name = "lblNazev";
            this.lblNazev.Size = new System.Drawing.Size(143, 32);
            this.lblNazev.TabIndex = 1;
            this.lblNazev.Text = "Název filmu";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblInfo.Location = new System.Drawing.Point(257, 57);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(91, 20);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "2024 • Sci-Fi";
            // 
            // lblReziser
            // 
            this.lblReziser.AutoSize = true;
            this.lblReziser.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblReziser.Location = new System.Drawing.Point(257, 85);
            this.lblReziser.Name = "lblReziser";
            this.lblReziser.Size = new System.Drawing.Size(54, 19);
            this.lblReziser.TabIndex = 3;
            this.lblReziser.Text = "Režisér:";
            // 
            // lblHodnoceni
            // 
            this.lblHodnoceni.AutoSize = true;
            this.lblHodnoceni.Font = new System.Drawing.Font("Segoe UI Black", 14F, System.Drawing.FontStyle.Bold);
            this.lblHodnoceni.ForeColor = System.Drawing.Color.Gold;
            this.lblHodnoceni.Location = new System.Drawing.Point(256, 115);
            this.lblHodnoceni.Name = "lblHodnoceni";
            this.lblHodnoceni.Size = new System.Drawing.Size(62, 25);
            this.lblHodnoceni.TabIndex = 4;
            this.lblHodnoceni.Text = "⭐ 8.5";
            // 
            // txtPopis
            // 
            this.txtPopis.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPopis.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPopis.Location = new System.Drawing.Point(260, 165);
            this.txtPopis.Multiline = true;
            this.txtPopis.Name = "txtPopis";
            this.txtPopis.ReadOnly = true;
            this.txtPopis.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPopis.Size = new System.Drawing.Size(510, 140);
            this.txtPopis.TabIndex = 5;
            this.txtPopis.Text = "Popis filmu...";
            // 
            // panelLine
            // 
            this.panelLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.panelLine.Location = new System.Drawing.Point(260, 150);
            this.panelLine.Name = "panelLine";
            this.panelLine.Size = new System.Drawing.Size(500, 1);
            this.panelLine.TabIndex = 6;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(670, 320);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Zavřít";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FilmDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 370);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panelLine);
            this.Controls.Add(this.txtPopis);
            this.Controls.Add(this.lblHodnoceni);
            this.Controls.Add(this.lblReziser);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblNazev);
            this.Controls.Add(this.pictureBoxPoster);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilmDetailForm";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "O filmu";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPoster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}