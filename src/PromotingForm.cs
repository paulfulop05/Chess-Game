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
    public partial class PromotingForm : Form
    {
        private Color backColor = Color.FromArgb(33, 32, 30);
        public PieceTypes pieceType;
        public Image pieceImage;

        public PromotingForm(PieceColors pieceColor)
        {
            InitializeComponent();

            BackColor = backColor;
            StyleButtons(pieceColor);
        }

        private void StyleButtons(PieceColors pieceColor)
        {
            if (pieceColor == PieceColors.White)
            {
                btnKnight.BackgroundImage = ChessGame.Properties.Resources.wN;
                btnBishop.BackgroundImage = ChessGame.Properties.Resources.wB;
                btnQueen.BackgroundImage = ChessGame.Properties.Resources.wQ;
                btnRook.BackgroundImage = ChessGame.Properties.Resources.wR;
            }
            else
            {
                btnKnight.BackgroundImage = ChessGame.Properties.Resources.bN;
                btnBishop.BackgroundImage = ChessGame.Properties.Resources.bB;
                btnQueen.BackgroundImage = ChessGame.Properties.Resources.bQ;
                btnRook.BackgroundImage = ChessGame.Properties.Resources.bR;
            }

            btnKnight.BackColor = backColor;
            btnKnight.FlatStyle = FlatStyle.Flat;
            btnKnight.FlatAppearance.BorderColor = backColor;
            btnKnight.BackgroundImageLayout = ImageLayout.Zoom;

            btnKnight.FlatAppearance.MouseOverBackColor = backColor;
            btnKnight.FlatAppearance.MouseDownBackColor = backColor;

            btnKnight.Click += (sender, e) => {
                var btn = (Button)sender;
                pieceType = PieceTypes.Knight;
                pieceImage = btn.BackgroundImage;
                this.Close();
            }; 
            
            btnBishop.BackColor = backColor;
            btnBishop.FlatStyle = FlatStyle.Flat;
            btnBishop.FlatAppearance.BorderColor = backColor;
            btnBishop.BackgroundImageLayout = ImageLayout.Zoom;

            btnBishop.FlatAppearance.MouseOverBackColor = backColor;
            btnBishop.FlatAppearance.MouseDownBackColor = backColor;

            btnBishop.Click += (sender, e) => {
                var btn = (Button)sender;
                pieceType = PieceTypes.Bishop;
                pieceImage = btn.BackgroundImage;
                this.Close();
            }; 
            
            btnQueen.BackColor = backColor;
            btnQueen.FlatStyle = FlatStyle.Flat;
            btnQueen.FlatAppearance.BorderColor = backColor;
            btnQueen.BackgroundImageLayout = ImageLayout.Zoom;

            btnQueen.FlatAppearance.MouseOverBackColor = backColor;
            btnQueen.FlatAppearance.MouseDownBackColor = backColor;

            btnQueen.Click += (sender, e) => {
                var btn = (Button)sender;
                pieceType = PieceTypes.Queen;
                pieceImage = btn.BackgroundImage;
                this.Close();
            };
            
            btnRook.BackColor = backColor;
            btnRook.FlatStyle = FlatStyle.Flat;
            btnRook.FlatAppearance.BorderColor = backColor;
            btnRook.BackgroundImageLayout = ImageLayout.Zoom;

            btnRook.FlatAppearance.MouseOverBackColor = backColor;
            btnRook.FlatAppearance.MouseDownBackColor = backColor;

            btnRook.Click += (sender, e) => {
                var btn = (Button)sender;
                pieceType = PieceTypes.Rook;
                pieceImage = btn.BackgroundImage;
                this.Close();
            };
        }
    }
}
