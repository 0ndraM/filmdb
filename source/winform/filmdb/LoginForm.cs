using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using filmdb.Models;
using System.Windows.Forms;

namespace filmdb
{
    public partial class LoginForm : Form
    {
        public string LoggedUser { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
        }

        public static class AppContext
        {
            public static string AuthToken { get; private set; }
            public static string UserRole { get; private set; } // NOVÉ: Role

            public static void SetToken(string token, string role) // Změna: Nyní přijímá i roli
            {
                AuthToken = token;
                UserRole = role;
            }
        }
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                var data = new Dictionary<string, string>
        {
            { "username", txtUsername.Text },
            { "password", txtPassword.Text }
        };

                // Krok 1: Odeslání přihlašovacích údajů
                var response = await client.PostAsync(
                    "https://0ndra.maweb.eu/FilmDB/login_api.php",
                    new FormUrlEncodedContent(data)
                );

                var json = await response.Content.ReadAsStringAsync();

                // Krok 2: Deserializace na silně typovou třídu
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponse>(json);

                // Krok 3: Kontrola úspěchu a uložení tokenu
                if (result.success && !string.IsNullOrEmpty(result.token))
                {
                    // Uložení JWT tokenu pro budoucí požadavky
                    AppContext.SetToken(result.token, result.role); // Předáváme i result.role!

                    // Nastavení přihlášeného uživatele (pokud je potřeba)
                    LoggedUser = result.username;

                    MessageBox.Show($"Přihlášení úspěšné. Vítejte, {result.username}!");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    // Zobrazíme chybovou zprávu z API, pokud existuje (např. "Špatné heslo.")
                    string message = result.message ?? "Neplatné přihlašovací údaje nebo chyba serveru.";
                    MessageBox.Show(message);
                }
            }
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(
                "https://0ndra.maweb.eu/FilmDB/register.php"
            );
        }
    }
}
