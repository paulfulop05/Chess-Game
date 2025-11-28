using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class Bishop : Piece
    {
        public Bishop()
        {}

        public override List<Point> LegalMoves(int row, int column, Cell targetCell, Cell[,] cell)
        {
            var bishopLegalMoves = new List<Point>();

            for (int i = 1; !OutOfBoard(row + i, column + i)
                && !PieceInTheWay(targetCell.PieceColor, cell[row + i, column + i].PieceColor); ++i)
            {
                bishopLegalMoves.Add(new Point(row + i, column + i));

                if (targetCell.PieceColor != cell[row + i, column + i].PieceColor &&
                    cell[row + i, column + i].PieceColor != PieceColors.None)
                    break;
            }

            for (int i = 1; !OutOfBoard(row + i, column - i)
                && !PieceInTheWay(targetCell.PieceColor, cell[row + i, column - i].PieceColor); ++i)
            {
                bishopLegalMoves.Add(new Point(row + i, column - i));

                if (targetCell.PieceColor != cell[row + i, column - i].PieceColor &&
                    cell[row + i, column - i].PieceColor != PieceColors.None)
                    break;
            }  

            for (int i = 1; !OutOfBoard(row - i, column + i)
                && !PieceInTheWay(targetCell.PieceColor, cell[row - i, column + i].PieceColor); ++i)
            {
                bishopLegalMoves.Add(new Point(row - i, column + i));

                if (targetCell.PieceColor != cell[row - i, column + i].PieceColor &&
                    cell[row - i, column + i].PieceColor != PieceColors.None)
                    break;
            }   

            for (int i = 1; !OutOfBoard(row - i, column - i)
                && !PieceInTheWay(targetCell.PieceColor, cell[row - i, column - i].PieceColor); ++i)
            {
                bishopLegalMoves.Add(new Point(row - i, column - i));

                if (targetCell.PieceColor != cell[row - i, column - i].PieceColor &&
                    cell[row - i, column - i].PieceColor != PieceColors.None)
                    break;
            }


            return bishopLegalMoves;
        }

        public bool VerifyCheck(Cell kingCell, List<Point> bishopLegalMoves)
        {
            if (bishopLegalMoves.Any(move => move.X == kingCell.Row && move.Y == kingCell.Column))
                return true;

            return false;
        }
    }
}
