namespace filmdb
{
    partial class SettingsForm
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
            this.lblCurrentUsername = new System.Windows.Forms.Label();
            this.groupBoxUsername = new System.Windows.Forms.GroupBox();
            this.btnChangeUsername = new System.Windows.Forms.Button();
            this.txtConfirmUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNewUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxPassword = new System.Windows.Forms.GroupBox();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBoxUsername.SuspendLayout();
            this.groupBoxPassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCurrentUsername
            // 
            this.lblCurrentUsername.AutoSize = true;
            this.lblCurrentUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblCurrentUsername.Location = new System.Drawing.Point(12, 9);
            this.lblCurrentUsername.Name = "lblCurrentUsername";
            this.lblCurrentUsername.Size = new System.Drawing.Size(130, 16);
            this.lblCurrentUsername.TabIndex = 0;
            this.lblCurrentUsername.Text = "Aktuální uživatel: ";
            // 
            // groupBoxUsername
            // 
            this.groupBoxUsername.Controls.Add(this.btnChangeUsername);
            this.groupBoxUsername.Controls.Add(this.txtConfirmUsername);
            this.groupBoxUsername.Controls.Add(this.label2);
            this.groupBoxUsername.Controls.Add(this.txtNewUsername);
            this.groupBoxUsername.Controls.Add(this.label1);
            this.groupBoxUsername.Location = new System.Drawing.Point(15, 48);
            this.groupBoxUsername.Name = "groupBoxUsername";
            this.groupBoxUsername.Size = new System.Drawing.Size(350, 140);
            this.groupBoxUsername.TabIndex = 1;
            this.groupBoxUsername.TabStop = false;
            this.groupBoxUsername.Text = "Změna uživatelského jména";
            // 
            // btnChangeUsername
            // 
            this.btnChangeUsername.Location = new System.Drawing.Point(19, 98);
            this.btnChangeUsername.Name = "btnChangeUsername";
            this.btnChangeUsername.Size = new System.Drawing.Size(310, 28);
            this.btnChangeUsername.TabIndex = 4;
            this.btnChangeUsername.Text = "Změnit jméno";
            this.btnChangeUsername.UseVisualStyleBackColor = true;
            this.btnChangeUsername.Click += new System.EventHandler(this.btnChangeUsername_Click);
            // 
            // txtConfirmUsername
            // 
            this.txtConfirmUsername.Location = new System.Drawing.Point(154, 59);
            this.txtConfirmUsername.Name = "txtConfirmUsername";
            this.txtConfirmUsername.Size = new System.Drawing.Size(175, 20);
            this.txtConfirmUsername.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Potvrzení nového jm.:";
            // 
            // txtNewUsername
            // 
            this.txtNewUsername.Location = new System.Drawing.Point(154, 26);
            this.txtNewUsername.Name = "txtNewUsername";
            this.txtNewUsername.Size = new System.Drawing.Size(175, 20);
            this.txtNewUsername.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nové uživ. jméno:";
            // 
            // groupBoxPassword
            // 
            this.groupBoxPassword.Controls.Add(this.btnChangePassword);
            this.groupBoxPassword.Controls.Add(this.txtConfirmPassword);
            this.groupBoxPassword.Controls.Add(this.label3);
            this.groupBoxPassword.Controls.Add(this.txtNewPassword);
            this.groupBoxPassword.Controls.Add(this.label4);
            this.groupBoxPassword.Location = new System.Drawing.Point(15, 204);
            this.groupBoxPassword.Name = "groupBoxPassword";
            this.groupBoxPassword.Size = new System.Drawing.Size(350, 140);
            this.groupBoxPassword.TabIndex = 2;
            this.groupBoxPassword.TabStop = false;
            this.groupBoxPassword.Text = "Změna hesla";
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Location = new System.Drawing.Point(19, 98);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(310, 28);
            this.btnChangePassword.TabIndex = 4;
            this.btnChangePassword.Text = "Změnit heslo";
            this.btnChangePassword.UseVisualStyleBackColor = true;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(154, 59);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(175, 20);
            this.txtConfirmPassword.TabIndex = 3;
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Potvrzení nového:";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(154, 26);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(175, 20);
            this.txtNewPassword.TabIndex = 1;
            this.txtNewPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Nové heslo:";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(12, 360);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(353, 35);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status message";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.Visible = false;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 404);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.groupBoxPassword);
            this.Controls.Add(this.groupBoxUsername);
            this.Controls.Add(this.lblCurrentUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nastavení účtu";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBoxUsername.ResumeLayout(false);
            this.groupBoxUsername.PerformLayout();
            this.groupBoxPassword.ResumeLayout(false);
            this.groupBoxPassword.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCurrentUsername;
        private System.Windows.Forms.GroupBox groupBoxUsername;
        private System.Windows.Forms.Button btnChangeUsername;
        private System.Windows.Forms.TextBox txtConfirmUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNewUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxPassword;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblStatus;
    }
}