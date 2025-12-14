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

namespace filmdb
{
    public partial class Maindform : Form
    {
        public Maindform()
        {
            InitializeComponent();
        }
        private List<Film> films;

      

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
    }
}
