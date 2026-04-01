using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using filmdb;

namespace filmdb
{
    public partial class AdminForm : Form
    {
        private const string ApiUrl = "https://0ndra.maweb.eu/FilmDB/approve_api.php";

        public AdminForm()
        {
            InitializeComponent();
            ThemeManager.Apply(this);
            // Spustíme načítání dat hned po otevření formuláře
            _ = LoadUnapprovedFilms();
        }


        // Metoda pro načtení neschválených filmů z API
        private async Task LoadUnapprovedFilms()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Přidáme JWT token pro autorizaci
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", LoginForm.AppContext.AuthToken);

                    var response = await client.GetAsync(ApiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<dynamic>(json);

                        // Naplníme DataGridView daty z pole "films" v JSONu
                        dgvUnapproved.DataSource = result.films.ToObject<List<object>>();

                        // Volitelné: Formátování tabulky po načtení
                       // if (dgvUnapproved.Columns["id"] != null) dgvUnapproved.Columns["id"].Visible = false;
                    }
                    else
                    {
                        MessageBox.Show($"Chyba při načítání dat: {response.StatusCode}", "Chyba API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Došlo k chybě připojení: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- TATO METODA CHYBĚLA ---
        // Obsluha tlačítka pro ruční aktualizaci seznamu
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadUnapprovedFilms();
        }

        // Obsluha tlačítka pro schválení vybraného filmu
        private async void btnApprove_Click(object sender, EventArgs e)
        {
            if (dgvUnapproved.CurrentRow == null)
            {
                MessageBox.Show("Prosím, vyberte film ze seznamu.");
                return;
            }

            // Získáme ID z vybraného řádku
            var filmId = dgvUnapproved.CurrentRow.Cells["id"].Value;

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", LoginForm.AppContext.AuthToken);

                    // Vytvoříme JSON tělo s ID filmu
                    var payload = new { id = filmId };
                    var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(ApiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Film byl úspěšně schválen.", "Úspěch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Po schválení znovu načteme seznam (zmizí schválený film)
                        await LoadUnapprovedFilms();
                    }
                    else
                    {
                        string errorMsg = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Film se nepodařilo schválit: {errorMsg}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při komunikaci: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Upravené načítání pro podporu filtru
        private async Task LoadFilms(bool showAll = false)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", LoginForm.AppContext.AuthToken);

                    // Voláme API s parametrem type
                    string url = showAll ? $"{ApiUrl}?type=all" : ApiUrl;
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<dynamic>(json);
                        dgvUnapproved.DataSource = result.films.ToObject<List<object>>();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Chyba: " + ex.Message); }
        }

        // Společná metoda pro změnu stavu
        private async Task ChangeFilmStatus(object filmId, string action)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", LoginForm.AppContext.AuthToken);
                    var payload = new { id = filmId, action = action };
                    var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(ApiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(action == "approve" ? "Film schválen." : "Schválení zrušeno.");
                        await LoadFilms(chkShowAll.Checked); // Refresh podle aktuálního filtru
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Chyba: " + ex.Message); }
        }

        // Handler pro nové tlačítko "Zrušit schválení"
        private async void btnReject_Click(object sender, EventArgs e)
        {
            if (dgvUnapproved.CurrentRow != null)
            {
                var filmId = dgvUnapproved.CurrentRow.Cells["id"].Value;
                await ChangeFilmStatus(filmId, "reject");
            }
        }

        // Handler pro CheckBox (pokud ho přidáte do designeru)
        private async void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            await LoadFilms(chkShowAll.Checked);
        }
    }
}