namespace filmdb
{
    partial class Mainform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainform));
            this.dataGridViewFilms = new System.Windows.Forms.DataGridView();
            this.picturePoster = new System.Windows.Forms.PictureBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.comboOrderBy = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtPopis = new System.Windows.Forms.TextBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.zobrazeníToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDarkMode = new System.Windows.Forms.ToolStripMenuItem();
            this.přidatFilmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFilms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturePoster)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewFilms
            // 
            this.dataGridViewFilms.AllowUserToAddRows = false;
            this.dataGridViewFilms.AllowUserToDeleteRows = false;
            this.dataGridViewFilms.AllowUserToResizeColumns = false;
            this.dataGridViewFilms.AllowUserToResizeRows = false;
            this.dataGridViewFilms.ColumnHeadersHeight = 30;
            this.dataGridViewFilms.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewFilms.Location = new System.Drawing.Point(12, 64);
            this.dataGridViewFilms.Name = "dataGridViewFilms";
            this.dataGridViewFilms.ReadOnly = true;
            this.dataGridViewFilms.RowHeadersVisible = false;
            this.dataGridViewFilms.RowHeadersWidth = 82;
            this.dataGridViewFilms.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewFilms.Size = new System.Drawing.Size(563, 300);
            this.dataGridViewFilms.TabIndex = 0;
            this.dataGridViewFilms.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFilms_CellDoubleClick);
            this.dataGridViewFilms.SelectionChanged += new System.EventHandler(this.dataGridViewFilms_SelectionChanged);
            // 
            // picturePoster
            // 
            this.picturePoster.Location = new System.Drawing.Point(595, 64);
            this.picturePoster.Name = "picturePoster";
            this.picturePoster.Size = new System.Drawing.Size(200, 300);
            this.picturePoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picturePoster.TabIndex = 1;
            this.picturePoster.TabStop = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(12, 35);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 20);
            this.txtSearch.TabIndex = 2;
            // 
            // comboOrderBy
            // 
            this.comboOrderBy.Location = new System.Drawing.Point(200, 35);
            this.comboOrderBy.Name = "comboOrderBy";
            this.comboOrderBy.Size = new System.Drawing.Size(121, 21);
            this.comboOrderBy.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(360, 33);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Hledat";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // timerClock
            // 
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(12, 373);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(500, 20);
            this.lblTitle.TabIndex = 5;
            // 
            // txtPopis
            // 
            this.txtPopis.Location = new System.Drawing.Point(12, 396);
            this.txtPopis.Multiline = true;
            this.txtPopis.Name = "txtPopis";
            this.txtPopis.ReadOnly = true;
            this.txtPopis.Size = new System.Drawing.Size(783, 60);
            this.txtPopis.TabIndex = 6;
            // 
            // lblTime
            // 
            this.lblTime.Location = new System.Drawing.Point(695, 471);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(100, 23);
            this.lblTime.TabIndex = 7;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zobrazeníToolStripMenuItem,
            this.přidatFilmToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(828, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // zobrazeníToolStripMenuItem
            // 
            this.zobrazeníToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDarkMode});
            this.zobrazeníToolStripMenuItem.Name = "zobrazeníToolStripMenuItem";
            this.zobrazeníToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.zobrazeníToolStripMenuItem.Text = "Zobrazení";
            // 
            // menuDarkMode
            // 
            this.menuDarkMode.CheckOnClick = true;
            this.menuDarkMode.Name = "menuDarkMode";
            this.menuDarkMode.Size = new System.Drawing.Size(143, 22);
            this.menuDarkMode.Text = "Tmavý režim ";
            this.menuDarkMode.Click += new System.EventHandler(this.menuDarkMode_Click);
            // 
            // přidatFilmToolStripMenuItem
            // 
            this.přidatFilmToolStripMenuItem.Name = "přidatFilmToolStripMenuItem";
            this.přidatFilmToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.přidatFilmToolStripMenuItem.Text = "Přidat film";
            this.přidatFilmToolStripMenuItem.Click += new System.EventHandler(this.přidatFilmToolStripMenuItem_Click);
            // 
            // Maindform
            // 
            this.ClientSize = new System.Drawing.Size(828, 506);
            this.Controls.Add(this.dataGridViewFilms);
            this.Controls.Add(this.picturePoster);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.comboOrderBy);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtPopis);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Maindform";
            this.Text = "FilmDB Desktop";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFilms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturePoster)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem zobrazeníToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuDarkMode;
        private System.Windows.Forms.ToolStripMenuItem přidatFilmToolStripMenuItem;
    }
}

