using System;
using System.Drawing;
using System.Windows.Forms;
using filmdb.Models;
using filmdb.Services;

namespace filmdb
{
    public partial class FilmDetailForm : Form
    {
        private Film film;

        public FilmDetailForm(Film selectedFilm)
        {
            InitializeComponent();
            ApplyDarkMode();
            this.KeyPreview = true;        // ⬅️ formulář chytá klávesy
            this.KeyDown += FilmDetailForm_KeyDown;
            this.pictureBoxPoster.Click += pictureBoxPoster_Click;
            this.pictureBoxPoster.Cursor = Cursors.Hand;
            film = selectedFilm;
            ThemeManager.Apply(this);
            LoadFilm();
        }

        private void ApplyDarkMode()
        {
            this.BackColor = Color.FromArgb(30, 30, 30);

            foreach (Control c in this.Controls)
            {
                if (c is Label)
                {
                    c.ForeColor = Color.White;
                    c.BackColor = Color.Transparent;
                }
                else if (c is TextBox)
                {
                    c.BackColor = Color.FromArgb(45, 45, 45);
                    c.ForeColor = Color.White;
                    ((TextBox)c).BorderStyle = BorderStyle.FixedSingle;
                }
            }

            lblNazev.ForeColor = Color.White;
            lblHodnoceni.ForeColor = Color.Gold;
        }
        private void LoadFilm()
        {
            if (film == null)
                return;

            lblNazev.Text = film.Nazev ?? "-";
            //lblRok.Text = "Rok: " + film.Rok;
            lblInfo.Text = "Žánr: " + (film.Zanr ?? "-");
            lblReziser.Text = "Režisér: " + (film.Reziser ?? "-");

            // Hodnocení – může být null
            if (film.Hodnoceni > 0)
                lblHodnoceni.Text = "Hodnocení: " + (film.Hodnoceni.HasValue ? film.Hodnoceni.Value.ToString("0.0") : "N/A");
            else
                lblHodnoceni.Text = "Hodnocení: -";

            txtPopis.Text = string.IsNullOrWhiteSpace(film.Popis)
                ? "Popis není k dispozici."
                : film.Popis;

            // Plakát
            if (!string.IsNullOrEmpty(film.Poster))
            {
                try
                {
                    pictureBoxPoster.Load(ApiService.GetPosterUrl(film.Poster));
                }
                catch
                {
                    pictureBoxPoster.Image = null;
                }
            }
            else
            {
                pictureBoxPoster.Image = null;
            }
        }
        private void pictureBoxPoster_Click(object sender, EventArgs e)
        {
            if (pictureBoxPoster.Image == null) return;

            using (var fs = new PosterFullScreenForm(pictureBoxPoster.Image))
            {
                fs.ShowDialog();
            }
        }


        private void btnZavrit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FilmDetailForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
