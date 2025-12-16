using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace filmdb
{
    public partial class PosterFullScreenForm : Form
    {
        public PosterFullScreenForm(Image poster)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;
            this.KeyPreview = true;

            PictureBox pb = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = poster,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Black
            };

            this.Controls.Add(pb);

            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            };

            pb.Click += (s, e) => this.Close();
        }
    }
}