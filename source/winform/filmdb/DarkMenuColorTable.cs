using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override Color MenuItemSelected => Color.FromArgb(60, 60, 60);
        public override Color MenuItemBorder => back;
        public override Color MenuBorder => back;
        public override Color ToolStripDropDownBackground => back;
        public override Color ImageMarginGradientBegin => back;
        public override Color ImageMarginGradientMiddle => back;
        public override Color ImageMarginGradientEnd => back;
    }
}
