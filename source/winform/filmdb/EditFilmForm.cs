using filmdb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using filmdb;

namespace filmdb
{
    public partial class EditFilmForm : Form
    {
        private Film _film;

        public EditFilmForm(Film film)
        {
            InitializeComponent();
            ThemeManager.Apply(this);
            _film = film;
            FillData();
        }

        private void FillData()
        {
            txtNazev.Text = _film.Nazev;
            txtRok.Text = _film.Rok.ToString();
            txtZanr.Text = _film.Zanr;
            txtReziser.Text = _film.Reziser;
            txtHodnoceni.Text = _film.Hodnoceni?.ToString("0.0");
            txtPopis.Text = _film.Popis;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", LoginForm.AppContext.AuthToken);

                    var form = new MultipartFormDataContent();
                    form.Add(new StringContent(_film.Id.ToString()), "id");
                    form.Add(new StringContent(txtNazev.Text), "nazev");
                    form.Add(new StringContent(txtRok.Text), "rok");
                    form.Add(new StringContent(txtZanr.Text), "zanr");
                    form.Add(new StringContent(txtReziser.Text), "reziser");
                    form.Add(new StringContent(txtHodnoceni.Text.Replace(',', '.')), "hodnoceni");
                    form.Add(new StringContent(txtPopis.Text), "popis");

                    // POZOR: Zkontrolujte, zda se soubor jmenuje edit_api.php nebo edit_film_api.php
                    var response = await client.PostAsync("https://0ndra.maweb.eu/FilmDB/edit_api.php", form);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Film byl úspěšně upraven.");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        // Pokud server vrátí chybu (např. 401, 404, 500), uvidíte ji zde
                        string errorDetail = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Chyba serveru ({response.StatusCode}): {errorDetail}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Došlo k chybě při odesílání: {ex.Message}");
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
