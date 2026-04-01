using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using filmdb.Models; // Kde je definována SimpleApiResponse
using filmdb.Services;
using filmdb;

namespace filmdb
{
    public partial class SettingsForm : Form
    {
        private readonly string _currentUsername;
        private readonly string _authToken;
        private const string ApiUrl = "https://0ndra.maweb.eu/FilmDB/api_settings.php";
        // V SettingsForm.cs
        public SettingsForm()
        {
            InitializeComponent();
            ThemeManager.Apply(this);

            // Získání aktuálního stavu z AppContextu
            _authToken = LoginForm.AppContext.AuthToken;

            // PŮVODNÍ ŘÁDEK (Způsoboval chybu): 
            // _currentUsername = (Application.OpenForms["Maindform"] as Maindform)?.LoggedInUsername;

            // NOVÝ ZPŮSOB: Získání jména ze statického AppContextu
            _currentUsername = LoginForm.AppContext.LoggedInUsername;

            // Nastavení UI
            lblCurrentUsername.Text = $"Aktuální uživatel: {_currentUsername}";
        }

        // --- ZMĚNA UŽIVATELSKÉHO JMÉNA ---

        private async void btnChangeUsername_Click(object sender, EventArgs e)
        {
            string newUsername = txtNewUsername.Text.Trim();
            string confirmUsername = txtConfirmUsername.Text.Trim();

            if (string.IsNullOrEmpty(newUsername) || string.IsNullOrEmpty(confirmUsername))
            {
                DisplayStatus("Nové jméno a potvrzení jsou povinné.", true);
                return;
            }
            if (newUsername != confirmUsername)
            {
                DisplayStatus("Nové jméno se neshoduje s potvrzením.", true);
                return;
            }
            if (newUsername.Equals(_currentUsername, StringComparison.OrdinalIgnoreCase))
            {
                DisplayStatus("Nové jméno je stejné jako stávající.", true);
                return;
            }

            // Potvrzení s uživatelem
            if (MessageBox.Show("Opravdu chcete změnit uživatelské jméno? Budete odhlášeni!",
                                "Potvrzení změny", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            var data = new { action = "change_username", new_username = newUsername };
            var result = await SendChangeRequest(data);

            if (result != null && result.success)
            {
                // Po úspěšné změně jména musíme uživatele odhlásit, 
                // protože JWT token je platný pro staré jméno.

                // Vyčištění tokenu
                LoginForm.AppContext.SetToken(null, null);

                // Informace Maindformu o odhlášení (pokud je potřeba)
                // (Můžete přidat public metodu do MainFormu pro řízení odhlášení)

                DisplayStatus("Jméno úspěšně změněno. Pro pokračování se přihlaste znovu.", false);

                // Můžeme zavřít SettingsForm a vyzvat uživatele k restartu/relogu
                Close();
                // Spusťte dialog pro přihlášení nebo zavřete aplikaci.
            }
        }

        // --- ZMĚNA HESLA ---

        private async void btnChangePassword_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                DisplayStatus("Nové heslo a potvrzení jsou povinné.", true);
                return;
            }
            if (newPassword != confirmPassword)
            {
                DisplayStatus("Nové heslo se neshoduje s potvrzením.", true);
                return;
            }

            var data = new { action = "change_password", new_password = newPassword, confirm_password = confirmPassword };
            var result = await SendChangeRequest(data);

            if (result != null && result.success)
            {
                DisplayStatus(result.message ?? "Heslo úspěšně změněno.", false);
                // Vyčistíme pole formuláře
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
            }
        }

        // --- UNIVERZÁLNÍ METODA PRO VOLÁNÍ API ZMĚN ---

        private async Task<SimpleApiResponse> SendChangeRequest(object data)
        {
            if (string.IsNullOrEmpty(_authToken))
            {
                DisplayStatus("Nejste přihlášeni. Chybí autorizační token.", true);
                return null;
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                    // Používáme POST, jak je definováno v api_settings.php
                    var response = await client.PostAsync(ApiUrl, jsonContent);
                    var json = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<SimpleApiResponse>(json);

                    if (!response.IsSuccessStatusCode || !result.success)
                    {
                        // Zobrazení chyby z API
                        string message = result?.message ?? $"Chyba HTTP: {response.StatusCode}";
                        DisplayStatus(message, true);
                        return null;
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                DisplayStatus($"Došlo k chybě připojení: {ex.Message}", true);
                return null;
            }
        }

        // --- POMOCNÁ METODA PRO ZOBRAZENÍ ZPRÁV ---

        private void DisplayStatus(string message, bool isError)
        {
            lblStatus.Text = message;
            // Nastavte barvu a viditelnost labelu lblStatus
            lblStatus.ForeColor = isError ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            lblStatus.Visible = true;
        }

        // Při načtení formuláře skryjeme zprávu
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            lblStatus.Visible = false;
        }
    }
}