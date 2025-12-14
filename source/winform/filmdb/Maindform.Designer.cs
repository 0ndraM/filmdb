namespace filmdb
{
    partial class Maindform
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        /// 

        private System.Windows.Forms.DataGridView dataGridViewFilms;
        private System.Windows.Forms.PictureBox picturePoster;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox comboOrderBy;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtPopis;
        private System.Windows.Forms.Label lblTime;
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
            this.components = new System.ComponentModel.Container();
            this.dataGridViewFilms = new System.Windows.Forms.DataGridView();
            this.picturePoster = new System.Windows.Forms.PictureBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.comboOrderBy = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtPopis = new System.Windows.Forms.TextBox();
            this.lblTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFilms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturePoster)).BeginInit();
            this.SuspendLayout();

            this.dataGridViewFilms.Location = new System.Drawing.Point(12, 41);
            this.dataGridViewFilms.Size = new System.Drawing.Size(500, 300);
            this.dataGridViewFilms.SelectionChanged += new System.EventHandler(this.dataGridViewFilms_SelectionChanged);

            this.picturePoster.Location = new System.Drawing.Point(530, 41);
            this.picturePoster.Size = new System.Drawing.Size(200, 300);
            this.picturePoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            this.txtSearch.Location = new System.Drawing.Point(12, 12);
            this.comboOrderBy.Location = new System.Drawing.Point(200, 12);
            this.btnSearch.Location = new System.Drawing.Point(360, 10);
            this.btnSearch.Text = "Hledat";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            this.lblTitle.Location = new System.Drawing.Point(12, 350);
            this.lblTitle.Size = new System.Drawing.Size(500, 20);

            this.txtPopis.Location = new System.Drawing.Point(12, 375);
            this.txtPopis.Size = new System.Drawing.Size(718, 60);
            this.txtPopis.Multiline = true;
            this.txtPopis.ReadOnly = true;

            this.lblTime.Location = new System.Drawing.Point(650, 450);

            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);

            this.ClientSize = new System.Drawing.Size(742, 480);
            this.Controls.Add(this.dataGridViewFilms);
            this.Controls.Add(this.picturePoster);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.comboOrderBy);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtPopis);
            this.Controls.Add(this.lblTime);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Text = "FilmDB Desktop";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

