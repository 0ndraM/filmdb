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
        // Pole pro sledování přihlášeného uživatele (null, pokud nikdo není přihlášen)
        private string loggedInUsername = null;

        // Reference na položku "Přidat film"
        private ToolStripMenuItem actualAddFilmToolStripMenuItem;

        // Reference na položku "Nastavení"
        private ToolStripMenuItem settingsToolStripMenuItem;

        // NOVÉ: Reference na položku "Administrace"
        private ToolStripMenuItem administrationToolStripMenuItem;

        public Maindform()
        {
            InitializeComponent();
            InitializeLoginLogic();
        }

        // Metoda, která nastaví menu a počáteční stav
        private void InitializeLoginLogic()
        {
            // 1. Vytvoření nové položky "Přidat film"
            actualAddFilmToolStripMenuItem = new ToolStripMenuItem("Přidat film");
            actualAddFilmToolStripMenuItem.Click += new EventHandler(this.AddFilmToolStripMenuItem_Click);

            // 2. Vytvoření nové položky "Nastavení"
            settingsToolStripMenuItem = new ToolStripMenuItem("Nastavení");
            settingsToolStripMenuItem.Click += new EventHandler(this.SettingsToolStripMenuItem_Click);

            // 3. NOVÉ: Vytvoření položky "Administrace"
            administrationToolStripMenuItem = new ToolStripMenuItem("Administrace");
            administrationToolStripMenuItem.Click += new EventHandler(this.AdministrationToolStripMenuItem_Click); // Nový handler

            // 4. Vložení položek do menuStrip (předpokládáme vložení za položku "Odhlásit se")

            // Index 2: Přidat film
            menuStrip1.Items.Insert(2, actualAddFilmToolStripMenuItem);

            // Index 3: Nastavení
            menuStrip1.Items.Insert(3, settingsToolStripMenuItem);

            // Index 4: NOVÉ: Administrace
            menuStrip1.Items.Insert(4, administrationToolStripMenuItem);

            // 5. Nastavení počátečního stavu UI
            UpdateLoginState();
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

            // Nastavení hlaviček (zkráceno pro přehlednost)
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
                // Přidejte kontrolu pro null Poster (např. pokud ještě není schváleno)
                if (!string.IsNullOrEmpty(film.Poster))
                {
                    picturePoster.Load(ApiService.GetPosterUrl(film.Poster));
                }
                else
                {
                    picturePoster.Image = null; // Zobrazit prázdný obrázek nebo placeholder
                }
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

            // POUZE schválené filmy
            // Můžete přidat kontrolu: if (!selectedFilm.Schvaleno) return;

            FilmDetailForm detail = new FilmDetailForm(selectedFilm);
            detail.ShowDialog();
        }

        // Změněný handler pro původní přidatFilmToolStripMenuItem (nyní slouží jako LOGIN/LOGOUT)
        private void přidatFilmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // LOGOUT logika
            if (!string.IsNullOrEmpty(loggedInUsername))
            {
                // 1. Vyčistíme globální stav
                LoginForm.AppContext.SetToken(null, null, null);

                // 2. Vyčistíme lokální stav
                loggedInUsername = null; // TOTO MUSÍ ZŮSTAT!

                MessageBox.Show("Byl jste úspěšně odhlášen.");

                // 3. Aktualizujeme UI
                UpdateLoginState(); // TOTO TAKÉ MUSÍ ZŮSTAT!

                return;
            }

            // LOGIN logika
            var login = new LoginForm();
            if (login.ShowDialog() == DialogResult.OK)
            {
                // Po úspěšném přihlášení získáme jméno a uložíme stav
                loggedInUsername = login.LoggedUser;
                UpdateLoginState();
            }
        }

        // Handler pro nově vytvořenou položku "Přidat film"
        private void AddFilmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(loggedInUsername))
            {
                // Spustíme formulář pro přidání filmu s přihlášeným jménem
                var addFilm = new AddFilmForm(loggedInUsername);
                addFilm.ShowDialog();
                // Po přidání filmu, obnovíme seznam, aby se zobrazil nový film
                Task.Run(async () => await LoadFilms());
            }
        }


        // NOVÝ: Handler pro položku "Nastavení"
        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(loggedInUsername))
            {
                var settingsForm = new SettingsForm();
                settingsForm.ShowDialog();

                // **KRITICKÁ ZMĚNA:** Po zavření SettingsForm ověříme globální stav:

                // Pokud AppContext neobsahuje token (což se stane po změně jména/odhlášení)
                if (string.IsNullOrEmpty(LoginForm.AppContext.AuthToken))
                {
                    // Vynutíme vyčištění lokálního stavu v MainFormu
                    loggedInUsername = null;

                    // Vynutíme vizuální aktualizaci menu
                    UpdateLoginState();
                }
            }
        }


        // NOVÝ: Handler pro položku "Administrace"
        private void AdministrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(loggedInUsername))
            {
                MessageBox.Show($"Otevřít administrační panel pro {loggedInUsername} (Role: {LoginForm.AppContext.UserRole})", "Administrace");
                // Zde bys spustil: AdminForm adminForm = new AdminForm();
                // adminForm.ShowDialog();
            }
        }

        // Nová metoda pro aktualizaci stavu UI
        private void UpdateLoginState()
        {
            // Zjistíme, jestli je uživatel přihlášený
            bool isLoggedIn = !string.IsNullOrEmpty(loggedInUsername);

            // Zjistíme role
            string currentRole = LoginForm.AppContext.UserRole;
            bool isUserRole = string.Equals(currentRole, "user", StringComparison.OrdinalIgnoreCase);
            bool isAdminOrOwner = string.Equals(currentRole, "admin", StringComparison.OrdinalIgnoreCase) ||
                                  string.Equals(currentRole, "owner", StringComparison.OrdinalIgnoreCase);

            // 1. Změna textu pro přihlášení/odhlášení
            if (isLoggedIn)
            {
                přidatFilmToolStripMenuItem.Text = "Odhlásit se (" + loggedInUsername + ")";
            }
            else
            {
                přidatFilmToolStripMenuItem.Text = "Přihlásit se";
            }

            // 2. Zobrazení/Skrytí položky "Přidat film"
            if (actualAddFilmToolStripMenuItem != null)
            {
                actualAddFilmToolStripMenuItem.Visible = isLoggedIn;
            }

            // 3. Zobrazení/Skrytí položky "Nastavení" POUZE pro roli 'user'
            if (settingsToolStripMenuItem != null)
            {
                settingsToolStripMenuItem.Visible = isLoggedIn && isUserRole;
            }

            // 4. NOVÉ: Zobrazení/Skrytí položky "Administrace" POUZE pro role 'admin' nebo 'owner'
            if (administrationToolStripMenuItem != null)
            {
                administrationToolStripMenuItem.Visible = isLoggedIn && isAdminOrOwner;
            }
        }
    }
}