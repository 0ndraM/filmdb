namespace filmdb
{
    partial class AdminForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dgvUnapproved = new System.Windows.Forms.DataGridView();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnApprove = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnapproved)).BeginInit();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();
            

            this.btnReject = new System.Windows.Forms.Button();
            this.chkShowAll = new System.Windows.Forms.CheckBox();

            // Nastavení btnReject
            this.btnReject.BackColor = System.Drawing.Color.LightCoral;
            this.btnReject.Location = new System.Drawing.Point(466, 11); // Umístit vedle btnApprove
            this.btnReject.Size = new System.Drawing.Size(150, 30);
            this.btnReject.Text = "Odschválit film";
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);

            // Nastavení chkShowAll
            this.chkShowAll.Location = new System.Drawing.Point(150, 15);
            this.chkShowAll.Text = "Zobrazit vše";
            this.chkShowAll.CheckedChanged += new System.EventHandler(this.chkShowAll_CheckedChanged);

            this.panelActions.Controls.Add(this.btnReject);
            this.panelActions.Controls.Add(this.chkShowAll);
            // 
            // dgvUnapproved (Tabulka s filmy)
            // 
            this.dgvUnapproved.AllowUserToAddRows = false;
            this.dgvUnapproved.AllowUserToDeleteRows = false;
            this.dgvUnapproved.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUnapproved.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnapproved.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUnapproved.Location = new System.Drawing.Point(0, 40);
            this.dgvUnapproved.MultiSelect = false;
            this.dgvUnapproved.Name = "dgvUnapproved";
            this.dgvUnapproved.ReadOnly = true;
            this.dgvUnapproved.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUnapproved.Size = new System.Drawing.Size(784, 371);
            this.dgvUnapproved.TabIndex = 0;
            // 
            // panelActions (Horní lišta s tlačítky)
            // 
            this.panelActions.Controls.Add(this.btnRefresh);
            this.panelActions.Controls.Add(this.btnApprove);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Location = new System.Drawing.Point(0, 411);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(784, 50);
            this.panelActions.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 11);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 30);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Aktualizovat";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnApprove
            // 
            this.btnApprove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApprove.BackColor = System.Drawing.Color.LightGreen;
            this.btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApprove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnApprove.Location = new System.Drawing.Point(622, 11);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(150, 30);
            this.btnApprove.TabIndex = 0;
            this.btnApprove.Text = "Schválit vybraný film";
            this.btnApprove.UseVisualStyleBackColor = false;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.SystemColors.Info;
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblInfo.Location = new System.Drawing.Point(0, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblInfo.Size = new System.Drawing.Size(784, 40);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "Seznam neschválených filmů čekajících na kontrolu:";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.dgvUnapproved);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.panelActions);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Administrace - Schvalování filmů";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnapproved)).EndInit();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUnapproved;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.CheckBox chkShowAll;
    }
}