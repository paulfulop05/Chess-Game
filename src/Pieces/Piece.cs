using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public enum PieceColors { None, White, Black };
    public enum PieceTypes {None, Pawn, Rook, Knight, Bishop, Queen, King};
    abstract class Piece
    {
        public Piece() { }

        public abstract List<Point> LegalMoves(int row, int column, Cell targetCell, Cell[,] cell);

        public bool OutOfBoard(int row, int column)
        {
            if(column < 0 || column > 7 || row < 0 || row > 7) return true;
            return false;
        }

        public bool PieceInTheWay(PieceColors pieceColor1, PieceColors pieceColor2)
        {
            return pieceColor1 == pieceColor2 ? true : false;
        }

    }
}
