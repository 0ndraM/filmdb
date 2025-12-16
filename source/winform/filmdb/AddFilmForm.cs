using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Headers; // Potřebné pro AuthenticationHeaderValue

namespace filmdb
{
    public partial class AddFilmForm : Form
    {
        // Původní pole _autor již není potřeba pro API, protože se bere z tokenu, 
        // ale ponecháváme ho zde pro případ, že ho používáš pro UI nebo logiku
        private readonly string _autor;
        private string selectedPosterPath;

        public AddFilmForm(string autor)
        {
            InitializeComponent();
            _autor = autor;
        }

        private void btnPoster_Click(object sender, EventArgs e)
        {
            // ... (Kód pro výběr plakátu je beze změny) ...
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedPosterPath = ofd.FileName;
                    MessageBox.Show("Plakát vybrán: " + Path.GetFileName(selectedPosterPath));
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            // Získání JWT tokenu uloženého po přihlášení
            string token = LoginForm.AppContext.AuthToken;

            if (string.IsNullOrEmpty(token))
            {
                MessageBox.Show("Chyba autorizace. Uživatelský token nebyl nalezen. Zkuste se přihlásit znovu.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var client = new HttpClient())
            {
                // 1. PŘIDÁNÍ JWT TOKENU DO HLAVIČKY AUTORIZACE
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                using (var form = new MultipartFormDataContent())
                {
                    // 2. Přidání dat (pole 'autor' bylo ODEBRÁNO)
                    form.Add(new StringContent(txtNazev.Text), "nazev");
                    form.Add(new StringContent(txtRok.Text), "rok");
                    form.Add(new StringContent(txtZanr.Text), "zanr");
                    form.Add(new StringContent(txtReziser.Text), "reziser");
                    form.Add(new StringContent(txtHodnoceni.Text), "hodnoceni");
                    form.Add(new StringContent(txtPopis.Text), "popis");
                    // form.Add(new StringContent(_autor), "autor"); // TOTO JE ZDE ODEBRÁNO!

                    // 3. Přidání plakátu
                    if (!string.IsNullOrEmpty(selectedPosterPath))
                    {
                        var bytes = File.ReadAllBytes(selectedPosterPath);
                        // Zde je poster.jpg název souboru, který se pošle serveru (PHP ho ignoruje a použije ID filmu)
                        form.Add(new ByteArrayContent(bytes), "plakat", "poster.jpg");
                    }

                    // 4. Odeslání požadavku
                    var response = await client.PostAsync(
                        "https://0ndra.maweb.eu/FilmDB/add_api.php", // Cílový URL
                        form
                    );

                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Film odeslán ke schválení", "Úspěch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        // Zkusíme deserializovat chybu z JSON pro lepší diagnostiku
                        try
                        {
                            dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);
                            string errorMessage = result.message ?? "Neznámá chyba.";
                            MessageBox.Show($"Chyba při odeslání filmu: {errorMessage}", "Chyba API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch
                        {
                            MessageBox.Show($"Chyba při odeslání filmu. HTTP Status: {response.StatusCode} ({jsonResponse})", "Chyba API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}