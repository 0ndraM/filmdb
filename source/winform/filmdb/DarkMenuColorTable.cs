using System;
using System.Drawing;
using System.Windows.Forms;

namespace filmdb
{
    public class DarkMenuColorTable : ProfessionalColorTable
    {
        private readonly Color back;
        private readonly Color fore;

        public DarkMenuColorTable(Color back, Color fore)
        {
            this.back = back;
            this.fore = fore;
        }

        // DYNAMICKÝ HOVER: Barva se změní podle toho, zda je zapnutý DarkMode
        public override Color MenuItemSelected => ThemeManager.DarkMode
            ? Color.FromArgb(60, 60, 60)       // Tmavě šedá pro Dark režim
            : Color.FromArgb(200, 200, 200);    // Světle šedá pro Light režim

        // Odstranění gradientů pro čistý vzhled
        public override Color MenuItemSelectedGradientBegin => MenuItemSelected;
        public override Color MenuItemSelectedGradientEnd => MenuItemSelected;

        public override Color MenuItemBorder => back;
        public override Color MenuBorder => back;
        public override Color ToolStripDropDownBackground => back;
        public override Color ImageMarginGradientBegin => back;
        public override Color ImageMarginGradientMiddle => back;
        public override Color ImageMarginGradientEnd => back;
    }
}