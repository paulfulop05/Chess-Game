using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    internal class Pawn : Piece
    {
        public Pawn()
        {}

        override public List<Point> LegalMoves(int row, int column, Cell targetCell, Cell[,] cell)
        {
            var pawnLegalMoves = new List<Point>();

            switch(cell[row, column].PieceColor)
            {
                case PieceColors.White:
                    if(!OutOfBoard(row - 1, column) && cell[row - 1, column].Occupied == false)
                        pawnLegalMoves.Add(new Point(row  - 1, column));

                    if (!OutOfBoard(row - 1, column - 1) && cell[row - 1, column - 1].PieceColor == PieceColors.Black)
                        pawnLegalMoves.Add(new Point(row - 1, column - 1));

                    if(!OutOfBoard(row - 1, column + 1) && cell[row - 1, column + 1].PieceColor == PieceColors.Black)
                        pawnLegalMoves.Add(new Point(row - 1, column + 1));

                    if (row == 6 && cell[row - 2, column].Occupied == false && cell[row - 1, column].Occupied == false)
                        pawnLegalMoves.Add(new Point(row - 2, column));
                    break;

                case PieceColors.Black:
                    if(!OutOfBoard(row + 1, column) && cell[row + 1, column].Occupied == false)
                        pawnLegalMoves.Add(new Point(row + 1, column));

                    if (!OutOfBoard(row + 1, column - 1) && cell[row + 1, column - 1].PieceColor == PieceColors.White)
                        pawnLegalMoves.Add(new Point(row + 1, column - 1));

                    if (!OutOfBoard(row + 1, column + 1) && cell[row + 1, column + 1].PieceColor == PieceColors.White)
                        pawnLegalMoves.Add(new Point(row + 1, column + 1));

                    if (row == 1 && cell[row + 2, column].Occupied == false && cell[row + 1, column].Occupied == false)
                        pawnLegalMoves.Add(new Point(row + 2, column));
                    break;
            }

            return pawnLegalMoves;
        }

        public bool VerifyCheck(Cell kingCell, int row, int column)
        {
            if (kingCell.PieceColor == PieceColors.White && kingCell.Row == row + 1
                && (kingCell.Column == column + 1 || kingCell.Column == column + 1))
                return true;

            if (kingCell.PieceColor == PieceColors.Black && kingCell.Row == row - 1
                && (kingCell.Column == column + 1 || kingCell.Column == column + 1))
                return true;

            return false;
        }
    }
}
