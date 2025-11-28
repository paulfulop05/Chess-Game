using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class GameEndForm : Form
    {
        private Color backColor = Color.FromArgb(33, 32, 30),
            darkBrown = Color.FromArgb(130, 97, 66),
            lightBrown = Color.FromArgb(224, 204, 164),
            btnExitColor = Color.FromArgb(116, 117, 116),
            btnExitMouseEnterColor = Color.FromArgb(162, 163, 162);

        public GameEndForm()
        {
            InitializeComponent();
            StyleForm();
        }

        private void StyleForm()
        {
            BackColor = backColor;

            btnExit.BackColor = backColor;
            btnExit.ForeColor = btnExitColor;

            btnExit.FlatAppearance.MouseOverBackColor = backColor;
            btnExit.FlatAppearance.MouseDownBackColor = backColor;

            btnExit.MouseEnter += (sender, e) => { btnExit.ForeColor = btnExitMouseEnterColor; };
            btnExit.MouseLeave += (sender, e) => { btnExit.ForeColor = btnExitColor; };
            btnExit.Click += (sender, e) => { Close(); };
        }

        public void SetWinnerText(PieceColors winnerColor)
        {
            if(winnerColor == PieceColors.White)
            {
                lblWinnerText.Text = "White Won";
                lblWinnerText.ForeColor = lightBrown;
            }
            else
            {
                lblWinnerText.Text = "Black Won";
                lblWinnerText.ForeColor = darkBrown;
            }
        }

        public void SetDrawText()
        {
            lblWinnerText.Text = "It's a draw";
            lblWinnerText.ForeColor = Color.White;
        }
    }
}
