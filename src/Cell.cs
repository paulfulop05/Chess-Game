using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    internal class Cell : Button
    {
        public bool Occupied { get; set; } = false;
        public PieceTypes PieceType { get; set; } = PieceTypes.None;
        public PieceColors PieceColor { get; set; } = PieceColors.None;
        public int Row { get; set; }
        public int Column { get; set; }

        public Cell()
        {
            Dock = DockStyle.Fill;
            FlatStyle = FlatStyle.Flat;
            BackgroundImageLayout = ImageLayout.Zoom;
            Margin = new Padding(0);
            Enabled = false;
        }

        public void PaintCell(Color color)
        {
            BackColor = color;
            ForeColor = color;
            FlatAppearance.MouseDownBackColor = color;
            FlatAppearance.MouseOverBackColor = color;
        }

    }
}
