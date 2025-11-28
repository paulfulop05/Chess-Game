using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class Knight : Piece
    {
        public Knight()
        {}

        public override List<Point> LegalMoves(int row, int column, Cell targetCell, Cell[,] cell)
        {
            var knightLegalMoves = new List<Point>();
            int[] dirRow = {-1, -1, 1, 1, -2, -2, 2, 2};
            int[] dirColumn = { 2, -2, 2, -2, 1, -1, 1, -1 };

            for (int i = 0; i < 8; ++i)
                if (!OutOfBoard(row + dirRow[i], column + dirColumn[i]) &&
                    !PieceInTheWay(targetCell.PieceColor, cell[row + dirRow[i], column + dirColumn[i]].PieceColor))
                    knightLegalMoves.Add(new Point(row + dirRow[i], column + dirColumn[i]));

            return knightLegalMoves;
        }

        public bool VerifyCheck(Cell kingCell, List<Point> knightLegalMoves)
        {
            if (knightLegalMoves.Any(move => move.X == kingCell.Row && move.Y == kingCell.Column))
                return true;

            return false;
        }
    }
}
