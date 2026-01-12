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

        // Reference na položku "Upravit vybraný film"
        private ToolStripMenuItem editFilmToolStripMenuItem;

        public Maindform()
        {
            InitializeComponent();

            // Synchronizace: Nastaví ThemeManager podle toho, co je v Designeru v MenuStripu
            ThemeManager.Set(menuDarkMode.Checked);

            // Aplikuje motiv na hlavní okno, aby bylo jasno, v jakém stavu začínáme
            ThemeManager.Apply(this);

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

            // 4. Vytvoření nové položky "Upravit vybraný film"
            editFilmToolStripMenuItem = new ToolStripMenuItem("Upravit vybraný film");
            editFilmToolStripMenuItem.Click += new EventHandler(this.EditFilmToolStripMenuItem_Click);
          
            // 5. Vložení položek do menuStrip (předpokládáme vložení za položku "Odhlásit se")

            // Index 2: Přidat film
            menuStrip1.Items.Insert(2, actualAddFilmToolStripMenuItem);

            // Index 3: Nastavení
            menuStrip1.Items.Insert(3, settingsToolStripMenuItem);

            // Index 4: NOVÉ: Administrace
            menuStrip1.Items.Insert(4, administrationToolStripMenuItem);

            // Index 5: Upravit vybraný film
            menuStrip1.Items.Insert(3, editFilmToolStripMenuItem);
            // 5. Nastavení počátečního stavu UI
            UpdateLoginState();
        }

        private List<Film> films;

        private void menuDarkMode_Click(object sender, EventArgs e)
        {
            // Nastaví globální stav (true/false) podle zaškrtnutí v menu
            ThemeManager.Set(menuDarkMode.Checked);
            // Aplikuje motiv na hlavní okno
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
            // 1. Získání dat z API
            films = await ApiService.GetFilmsAsync(comboOrderBy.Text, txtSearch.Text);

            // 2. Převod Listu na BindingList (podporuje řazení a notifikace v UI)
            var bindingList = new BindingList<Film>(films);

            // 3. Přiřazení zdroje dat
            dataGridViewFilms.DataSource = bindingList;

            FormatGrid();
        }

        private void FormatGrid()
        {
            // Skrytí sloupců
            if (dataGridViewFilms.Columns["id"] != null)
                dataGridViewFilms.Columns["id"].Visible = false;

            if (dataGridViewFilms.Columns["rok"] != null)
            {
                dataGridViewFilms.Columns["rok"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            if (dataGridViewFilms.Columns["hodnoceni"] != null)
            {
                dataGridViewFilms.Columns["hodnoceni"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            if (dataGridViewFilms.Columns["popis"] != null)
                dataGridViewFilms.Columns["popis"].Visible = false;

            if (dataGridViewFilms.Columns["schvaleno"] != null)
                dataGridViewFilms.Columns["schvaleno"].Visible = false;

            if (dataGridViewFilms.Columns["poster"] != null)
                dataGridViewFilms.Columns["poster"].Visible = false;

            if (dataGridViewFilms.Columns["hodnoceni"] != null)
            {
                dataGridViewFilms.Columns["hodnoceni"].HeaderText = "Hodnocení";
                // "N1" zajistí formát jako 8.5, 7.0 atd.
                dataGridViewFilms.Columns["hodnoceni"].DefaultCellStyle.Format = "N1";
            }
            // Nastavení hlaviček (zkráceno pro přehlednost)
            if (dataGridViewFilms.Columns["reziser"] != null)
                dataGridViewFilms.Columns["reziser"].HeaderText = "Režisér";
            if (dataGridViewFilms.Columns["autor"] != null)
            {
                dataGridViewFilms.Columns["autor"].Visible = true;
                dataGridViewFilms.Columns["autor"].HeaderText = "Přidal uživatel";
            }

            foreach (DataGridViewColumn column in dataGridViewFilms.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
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
                string currentRole = LoginForm.AppContext.UserRole;
                if (currentRole == "admin" || currentRole == "owner")
                {
                    // Otevření admin sekce
                    var adminForm = new AdminForm();
                    adminForm.ShowDialog();

                    // Po zavření admin sekce obnovíme hlavní seznam filmů
                    _ = LoadFilms();
                }
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
                settingsToolStripMenuItem.Visible = isLoggedIn;
            }

            // 4. NOVÉ: Zobrazení/Skrytí položky "Administrace" POUZE pro role 'admin' nebo 'owner'
            if (administrationToolStripMenuItem != null)
            {
                administrationToolStripMenuItem.Visible = isLoggedIn && isAdminOrOwner;
            }

            // 5. Zobrazení/Skrytí položky "Upravit vybraný film" POUZE pro přihlášené uživatele    
            if (editFilmToolStripMenuItem != null)
            {
                editFilmToolStripMenuItem.Visible = isLoggedIn;
            }
        }
        private void EditFilmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewFilms.CurrentRow?.DataBoundItem is Film selectedFilm)
            {
                // Kontrola práv: Upravit může jen autor nebo admin/owner
                bool isAuthor = string.Equals(selectedFilm.Autor, loggedInUsername, StringComparison.OrdinalIgnoreCase);
                bool isAdmin = string.Equals(LoginForm.AppContext.UserRole, "admin", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(LoginForm.AppContext.UserRole, "owner", StringComparison.OrdinalIgnoreCase);

                if (isAuthor || isAdmin)
                {
                    var editForm = new EditFilmForm(selectedFilm);
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        _ = LoadFilms(); // Refresh seznamu
                    }
                }
                else
                {
                    MessageBox.Show("Můžete upravovat pouze své vlastní filmy.");
                }
            }
            else
            {
                MessageBox.Show("Nejprve vyberte film, který chcete upravit.");
            }
        }
    }
}