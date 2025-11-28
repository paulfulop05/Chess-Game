using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class Queen : Piece
    {
        public Queen()
        {}

        public override List<Point> LegalMoves(int row, int column, Cell targetCell, Cell[,] cell)
        {
            Rook rook = new Rook();
            Bishop bishop = new Bishop();

            var rookLegalMoves = rook.LegalMoves(row, column, targetCell, cell);
            var bishopLegalMoves = bishop.LegalMoves(row, column, targetCell, cell);

            var queenLegalMoves = new List<Point>();
            queenLegalMoves.AddRange(bishopLegalMoves);
            queenLegalMoves.AddRange(rookLegalMoves);

            return queenLegalMoves;
        }

        public bool VerifyCheck(Cell kingCell, List<Point> queenLegalMoves)
        {
            if (queenLegalMoves.Any(move => move.X == kingCell.Row && move.Y == kingCell.Column))
                return true;

            return false;
        }
    }
}
