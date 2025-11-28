using ChessGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    internal class Rook : Piece
    {
        public Rook()
        {}

        public override List<Point> LegalMoves(int row, int column, Cell targetCell, Cell[,] cell)
        {
            var rookLegalMoves = new List<Point>();

            for (int i = 1; !OutOfBoard(row + i, column)
                && !PieceInTheWay(targetCell.PieceColor, cell[row + i, column].PieceColor); ++i)
            {
                rookLegalMoves.Add(new Point(row + i, column));

                if (targetCell.PieceColor != cell[row + i, column].PieceColor &&
                    cell[row + i, column].PieceColor != PieceColors.None)
                    break;
            }

            for (int i = 1; !OutOfBoard(row - i, column)
                && !PieceInTheWay(targetCell.PieceColor, cell[row - i, column].PieceColor); ++i)
            {
                rookLegalMoves.Add(new Point(row - i, column));

                if (targetCell.PieceColor != cell[row - i, column].PieceColor &&
                    cell[row - i, column].PieceColor != PieceColors.None)
                    break;
            }

            for (int i = 1; !OutOfBoard(row, column + i)
                && !PieceInTheWay(targetCell.PieceColor, cell[row, column + i].PieceColor); ++i)
            {
                rookLegalMoves.Add(new Point(row, column + i));

                if (targetCell.PieceColor != cell[row, column + i].PieceColor &&
                    cell[row, column + i].PieceColor != PieceColors.None)
                    break;
            }

            for (int i = 1; !OutOfBoard(row, column - i)
                && !PieceInTheWay(targetCell.PieceColor, cell[row, column - i].PieceColor); ++i)
            {
                rookLegalMoves.Add(new Point(row, column - i));

                if (targetCell.PieceColor != cell[row, column - i].PieceColor &&
                    cell[row, column - i].PieceColor != PieceColors.None)
                    break;
            }

            return rookLegalMoves;
        }

        public bool VerifyCheck(Cell kingCell, List<Point> rookLegalMoves)
        {
            if (rookLegalMoves.Any(move => move.X == kingCell.Row && move.Y == kingCell.Column))
                return true;

            return false;
        }
    }
}
