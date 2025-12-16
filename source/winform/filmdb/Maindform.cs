using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using filmdb.Models;
using filmdb.Services;
using filmdb;

namespace filmdb
{
    public partial class Maindform : Form
    {
        public Maindform()
        {
            InitializeComponent();
        }
        private List<Film> films;

        private void menuDarkMode_Click(object sender, EventArgs e)
        {
            ThemeManager.Set(menuDarkMode.Checked);
            ThemeManager.Apply(this);
        }


        private async void MainForm_Load(object sender, EventArgs e)
        {
            comboOrderBy.Items.AddRange(new string[] { "nazev", "rok", "zanr", "hodnoceni" });
            comboOrderBy.SelectedIndex = 0;
            await LoadFilms();
            timerClock.Start();
    
           
        }

        private async System.Threading.Tasks.Task LoadFilms()
        {
            films = await ApiService.GetFilmsAsync(comboOrderBy.Text, txtSearch.Text);
            dataGridViewFilms.DataSource = films;
            FormatGrid();

        }
        private void FormatGrid()
        {
            // Skrytí sloupců
            if (dataGridViewFilms.Columns["id"] != null)
                dataGridViewFilms.Columns["id"].Visible = false;

            if (dataGridViewFilms.Columns["popis"] != null)
                dataGridViewFilms.Columns["popis"].Visible = false;

            if (dataGridViewFilms.Columns["schvaleno"] != null)
                dataGridViewFilms.Columns["schvaleno"].Visible = false;

            if (dataGridViewFilms.Columns["autor"] != null)
                dataGridViewFilms.Columns["autor"].Visible = false;

            if (dataGridViewFilms.Columns["poster"] != null)
                dataGridViewFilms.Columns["poster"].Visible = false;

            // Formát hodnocení na 1 desetinné místo
            if (dataGridViewFilms.Columns["hodnoceni"] != null)
            {
                dataGridViewFilms.Columns["hodnoceni"].DefaultCellStyle.Format = "0.0";
                dataGridViewFilms.Columns["hodnoceni"].HeaderText = "Hodnocení";
            }

            // Volitelně: hezčí názvy sloupců
            if (dataGridViewFilms.Columns["nazev"] != null)
                dataGridViewFilms.Columns["nazev"].HeaderText = "Název filmu";

            if (dataGridViewFilms.Columns["rok"] != null)
                dataGridViewFilms.Columns["rok"].HeaderText = "Rok";

            if (dataGridViewFilms.Columns["zanr"] != null)
                dataGridViewFilms.Columns["zanr"].HeaderText = "Žánr";

            if (dataGridViewFilms.Columns["reziser"] != null)
                dataGridViewFilms.Columns["reziser"].HeaderText = "Režisér";
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await LoadFilms();
        }

        private void dataGridViewFilms_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewFilms.CurrentRow?.DataBoundItem is Film film)
            {
                lblTitle.Text = $"{film.Nazev} ({film.Rok})";
                txtPopis.Text = film.Popis;
                picturePoster.Load(ApiService.GetPosterUrl(film.Poster));
            }
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void dataGridViewFilms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Film selectedFilm = (Film)dataGridViewFilms.Rows[e.RowIndex].DataBoundItem;

            FilmDetailForm detail = new FilmDetailForm(selectedFilm);
            detail.ShowDialog();
        }
    }
}
