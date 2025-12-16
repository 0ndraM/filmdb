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

        private void InitializeComponent()
        {
            this.pictureBoxPoster = new System.Windows.Forms.PictureBox();
            this.lblNazev = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblReziser = new System.Windows.Forms.Label();
            this.lblHodnoceni = new System.Windows.Forms.Label();
            this.txtPopis = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPoster)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxPoster
            // 
            this.pictureBoxPoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPoster.Location = new System.Drawing.Point(20, 20);
            this.pictureBoxPoster.Name = "pictureBoxPoster";
            this.pictureBoxPoster.Size = new System.Drawing.Size(200, 300);
            this.pictureBoxPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPoster.TabIndex = 0;
            this.pictureBoxPoster.TabStop = false;
            // 
            // lblNazev
            // 
            this.lblNazev.AutoSize = true;
            this.lblNazev.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblNazev.Location = new System.Drawing.Point(240, 20);
            this.lblNazev.MaximumSize = new System.Drawing.Size(520, 0);
            this.lblNazev.Name = "lblNazev";
            this.lblNazev.Size = new System.Drawing.Size(114, 25);
            this.lblNazev.TabIndex = 1;
            this.lblNazev.Text = "Název filmu";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblInfo.Location = new System.Drawing.Point(242, 55);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(102, 19);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "Rok • Žánr";
            // 
            // lblReziser
            // 
            this.lblReziser.AutoSize = true;
            this.lblReziser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblReziser.Location = new System.Drawing.Point(242, 80);
            this.lblReziser.Name = "lblReziser";
            this.lblReziser.Size = new System.Drawing.Size(60, 19);
            this.lblReziser.TabIndex = 3;
            this.lblReziser.Text = "Režisér:";
            // 
            // lblHodnoceni
            // 
            this.lblHodnoceni.AutoSize = true;
            this.lblHodnoceni.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHodnoceni.Location = new System.Drawing.Point(242, 110);
            this.lblHodnoceni.Name = "lblHodnoceni";
            this.lblHodnoceni.Size = new System.Drawing.Size(98, 19);
            this.lblHodnoceni.TabIndex = 4;
            this.lblHodnoceni.Text = "⭐ 0.0";
            // 
            // txtPopis
            // 
            this.txtPopis.Location = new System.Drawing.Point(245, 145);
            this.txtPopis.Multiline = true;
            this.txtPopis.Name = "txtPopis";
            this.txtPopis.ReadOnly = true;
            this.txtPopis.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPopis.Size = new System.Drawing.Size(520, 175);
            this.txtPopis.TabIndex = 5;
            // 
            // FilmDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 360);
            this.Controls.Add(this.txtPopis);
            this.Controls.Add(this.lblHodnoceni);
            this.Controls.Add(this.lblReziser);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblNazev);
            this.Controls.Add(this.pictureBoxPoster);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilmDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Detail filmu";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPoster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}