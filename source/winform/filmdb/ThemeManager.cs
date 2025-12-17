using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace filmdb
{
     public static class ThemeManager
    {
        public static bool DarkMode { get; private set; } = true;

        public static void Set(bool darkMode)
        {
            DarkMode = darkMode;
        }
        // V ThemeManager.cs přidej tuto metodu pro snadnější volání:
        public static void ApplyToForm(Form form)
        {
            // Aplikuje barvy na samotný formulář a všechny jeho prvky
            Apply(form);

            // Zabezpečí, aby se barvy aplikovaly i na titulkový pruh (u některých verzí Windows)
            // a nastaví startovní pozici, pokud je potřeba
            form.BackColor = DarkMode ? Color.FromArgb(30, 30, 30) : SystemColors.Control;
        }
        public static void Apply(Form form)
        {
            Color formBack = DarkMode ? Color.FromArgb(30, 30, 30) : SystemColors.Control;
            Color textFore = DarkMode ? Color.White : Color.Black;
            Color textboxBack = DarkMode ? Color.FromArgb(45, 45, 45) : Color.White;
            Color menuBack = DarkMode ? Color.FromArgb(25, 25, 25) : SystemColors.Control;
            Color buttonBack = DarkMode ? Color.FromArgb(50, 50, 50) : SystemColors.Control;

            form.BackColor = formBack;

            foreach (Control c in form.Controls)
                ApplyToControl(c, formBack, textFore, textboxBack, menuBack, buttonBack);
        }

        private static void ApplyToControl(
            Control c,
            Color formBack,
            Color textFore,
            Color textboxBack,
            Color menuBack,
            Color buttonBack)
        {
            if (c is MenuStrip menu)
            {
                menu.BackColor = menuBack;
                menu.ForeColor = textFore;
                menu.Renderer = new ToolStripProfessionalRenderer(
                    new DarkMenuColorTable(menuBack, textFore)
                );
            }
            else if (c is ToolStrip tool)
            {
                tool.BackColor = menuBack;
                tool.ForeColor = textFore;
            }
            else if (c is Button btn)
            {
                btn.BackColor = buttonBack;
                btn.ForeColor = textFore;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderColor = DarkMode ? Color.Gray : SystemColors.ActiveBorder;
                btn.UseVisualStyleBackColor = false;
            }
            else if (c is Label)
            {
                c.ForeColor = textFore;
                c.BackColor = Color.Transparent;
            }
            else if (c is TextBox tb)
            {
                tb.BackColor = textboxBack;
                tb.ForeColor = textFore;
            }
            else if (c is DataGridView dgv)
            {
                dgv.BackgroundColor = formBack;
                dgv.DefaultCellStyle.BackColor = formBack;
                dgv.DefaultCellStyle.ForeColor = textFore;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = menuBack;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = textFore;
                dgv.EnableHeadersVisualStyles = false;
            }
            else if (c is GroupBox gb)
            {
                gb.ForeColor = textFore; // Text GroupBoxu
                                         // GroupBoxy mají v tmavém režimu problém s barvou čáry, 
                                         // ale nastavení ForeColor pomůže čitelnosti.
            }
            else if (c is Panel p)
            {
                // Panely by měly mít stejnou barvu jako pozadí formuláře
                p.BackColor = formBack;
            }
            else if (c is CheckBox cb)
            {
                cb.ForeColor = textFore;
                cb.BackColor = Color.Transparent;
            }

            foreach (Control child in c.Controls)
                ApplyToControl(child, formBack, textFore, textboxBack, menuBack, buttonBack);
        }
    }
}