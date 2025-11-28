using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class King : Piece
    {
        public King()
        {}
        public override List<Point> LegalMoves(int row, int column, Cell targetCell, Cell[,] cell)
        {
            var kingLegalMoves = new List<Point>();
            int[] dirRow = {1, 1, 1, -1, -1, -1, 0, 0};
            int[] dirColumn = {-1, 0, 1, -1, 0, 1, -1, 1};

            for (int i = 0; i < 8; ++i)
                if (!OutOfBoard(row + dirRow[i], column + dirColumn[i]) &&
                    !PieceInTheWay(targetCell.PieceColor, cell[row + dirRow[i], column + dirColumn[i]].PieceColor))
                    kingLegalMoves.Add(new Point(row + dirRow[i], column + dirColumn[i]));

            return kingLegalMoves;
        }
    }
}
