using Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Runtime.InteropServices;
using System.Data.Common;
using ChessGame.Properties;

namespace Chess
{
    internal class ChessBoard : TableLayoutPanel
    {
        private Cell[,] cell;
        private Cell checkerPiece;
        private Pawn pawn;
        private Rook rook;
        private Bishop bishop;
        private Knight knight;
        private Queen queen;
        private King king;
        public Color whiteSquareColor = Color.FromArgb(224, 204, 164),
            blackSquareColor = Color.FromArgb(130, 97, 66),
            blackSquareLegalColor = Color.FromArgb(199, 68, 48),
            whiteSquareLegalColor = Color.FromArgb(201, 90, 73),
            timerWhitePauseBackColor = Color.FromArgb(152, 151, 149),
            timerWhitePauseForeColor = Color.FromArgb(97, 96, 93),
            timerBlackPauseBackColor = Color.FromArgb(43, 41, 38),
            timerBlackPauseForeColor = Color.FromArgb(130, 129, 127),
            timerBlackBackColor = Color.FromArgb(20, 19, 19);

        public int whiteScore, blackScore;
        private GameEndForm gameEndForm;
        private bool firstSecondClick, kingInCheck, gameEnded;
        private PieceColors turns;
        public ListView _listView;
        private static readonly string soundEffectsFilesPath = Application.StartupPath.Replace(@"bin\Debug", "resurse");

        public ChessBoard(ListView listView)
        {
            cell = new Cell[8, 8];

            pawn = new Pawn();
            rook = new Rook();
            bishop = new Bishop();
            knight = new Knight();
            queen = new Queen();
            king = new King();

            RowCount = 8;
            ColumnCount = 8;
            Dock = DockStyle.Fill;

            gameEnded = false;
            firstSecondClick = false;
            turns = PieceColors.White;
            whiteScore = blackScore = 0;
            _listView = listView;
        }

        public void CreateBoard(Panel mainPanel)
        {
            float cellSize = Width * Height / 64;
            for (int i = 0; i < 8; ++i)
            {
                ColumnStyles.Add(new ColumnStyle(SizeType.Percent, cellSize));
                RowStyles.Add(new RowStyle(SizeType.Percent, cellSize));

                for (int j = 0; j < 8; ++j)
                {
                    cell[i, j] = new Cell()
                    {
                        Row = i, Column = j
                    };

                    if ((i + j) % 2 == 0) cell[i, j].PaintCell(whiteSquareColor);
                    else cell[i, j].PaintCell(blackSquareColor);

                    Controls.Add(cell[i, j]);
                }

                mainPanel.Controls.Add(this);
            }

            ArrangePieces();
        }

        public void ClearBoard()
        {
            gameEnded = firstSecondClick = false;
            turns = PieceColors.White;
            for (int i = 0; i < 8; ++i)
                for (int j = 0; j < 8; ++j)
                {
                    cell[i, j].BackgroundImage = null;
                    cell[i, j].Enabled = false;
                    cell[i, j].Occupied = false;
                    cell[i, j].PieceColor = PieceColors.None;
                    cell[i, j].PieceType = PieceTypes.None;

                    if ((i + j) % 2 == 0) cell[i, j].PaintCell(whiteSquareColor);
                    else cell[i, j].PaintCell(blackSquareColor);
                }

            ArrangePieces();
        }

        public void CanPlayOnBoard(bool enabled)
        {
            for (int i = 0; i < 8; ++i)
                for (int j = 0; j < 8; ++j)
                    cell[i, j].Enabled = enabled;
        }

        public void ArrangePieces()
        {
            for (int i = 0; i < 8; i++)
            {
                cell[1, i].BackgroundImage = ChessGame.Properties.Resources.bP;
                cell[6, i].BackgroundImage = ChessGame.Properties.Resources.wP;

                cell[1, i].PieceType = cell[6, i].PieceType = PieceTypes.Pawn;

                cell[1, i].PieceColor = cell[0, i].PieceColor = PieceColors.Black;
                cell[6, i].PieceColor = cell[7, i].PieceColor = PieceColors.White;

                cell[0, i].Occupied = cell[1, i].Occupied = cell[6, i].Occupied = cell[7, i].Occupied = true;
            }

            cell[0, 0].BackgroundImage = cell[0, 7].BackgroundImage = ChessGame.Properties.Resources.bR;
            cell[7, 0].BackgroundImage = cell[7, 7].BackgroundImage = ChessGame.Properties.Resources.wR;
            cell[0, 0].PieceType = cell[0, 7].PieceType = cell[7, 0].PieceType = cell[7, 7].PieceType = PieceTypes.Rook;

            cell[0, 1].BackgroundImage = cell[0, 6].BackgroundImage = ChessGame.Properties.Resources.bN;
            cell[7, 1].BackgroundImage = cell[7, 6].BackgroundImage = ChessGame.Properties.Resources.wN;
            cell[0, 1].PieceType = cell[0, 6].PieceType = cell[7, 1].PieceType = cell[7, 6].PieceType = PieceTypes.Knight;

            cell[0, 2].BackgroundImage = cell[0, 5].BackgroundImage = ChessGame.Properties.Resources.bB;
            cell[7, 2].BackgroundImage = cell[7, 5].BackgroundImage = ChessGame.Properties.Resources.wB;
            cell[0, 2].PieceType = cell[0, 5].PieceType = cell[7, 2].PieceType = cell[7, 5].PieceType = PieceTypes.Bishop;

            cell[0, 3].BackgroundImage = ChessGame.Properties.Resources.bQ;
            cell[7, 3].BackgroundImage = ChessGame.Properties.Resources.wQ;
            cell[0, 3].PieceType = cell[7, 3].PieceType = PieceTypes.Queen;

            cell[0, 4].BackgroundImage = ChessGame.Properties.Resources.bK;
            cell[7, 4].BackgroundImage = ChessGame.Properties.Resources.wK;
            cell[0, 4].PieceType = cell[7, 4].PieceType = PieceTypes.King;
        }

        public void BoardActivity(Timer timerWhite, Timer timerBlack, Label lblTimerWhite, Label lblWhiteScore, Label lblTimerBlack, Label lblBlackScore, ComboBox cmbTime)
        {
            Cell previousCell = null;
            List<Point> legalMoves = new List<Point>();

            for (int i = 0; i < 8; ++i)
                for (int j = 0; j < 8; ++j)
                {
                    cell[i, j].Click += (sender, e) =>
                    {
                        Cell currentCell = (Cell)sender;

                        switch (firstSecondClick)
                        {
                            case false:
                                ErasePieceLegalMoves(legalMoves);

                                if(gameEnded)
                                    CanPlayOnBoard(false);
                                else
                                {
                                    if (turns == currentCell.PieceColor)
                                    {
                                        Cell kingCell = FindKing(OppositePieceColor(currentCell.PieceColor));

                                        if (!kingInCheck)
                                        {
                                            GeneratePieceLegalMoves(ref legalMoves, currentCell.Row, currentCell.Column, currentCell);
                                            RemoveEventualIlegalMoves(ref legalMoves, currentCell, kingCell);
                                            DrawPieceLegalMoves(legalMoves);
                                            previousCell = currentCell;
                                            firstSecondClick = true;
                                        }
                                        else
                                        {
                                            legalMoves.Clear();
                                            GenerateInCheckLegalMoves(ref legalMoves, currentCell, kingCell);

                                            if (legalMoves.Count > 0)
                                            {
                                                DrawPieceLegalMoves(legalMoves);
                                                previousCell = currentCell;
                                                firstSecondClick = true;
                                            }
                                        }
                                    }
                                }

                                break;

                            case true:
                                ErasePieceLegalMoves(legalMoves);

                                if (turns == previousCell.PieceColor && legalMoves.Any(e1 => e1.X == currentCell.Row && e1.Y == currentCell.Column))
                                {
                                    MovePiece(previousCell, currentCell);

                                    if (Checkmate(currentCell))
                                    {
                                        var gameEndSoundEffect = new WMPLib.WindowsMediaPlayer();
                                        gameEndSoundEffect.URL = soundEffectsFilesPath + @"\game-end.mp3";
                                        gameEndSoundEffect.controls.play();
                                        cmbTime.Enabled = true;

                                        gameEndForm = new GameEndForm();
                                        gameEndForm.SetWinnerText(currentCell.PieceColor);
                                        gameEndForm.Show();

                                        if (currentCell.PieceColor == PieceColors.White)
                                        {
                                            timerWhite.Stop();
                                            lblTimerWhite.BackColor = timerWhitePauseBackColor;
                                            lblTimerWhite.ForeColor = timerWhitePauseForeColor;

                                            ++whiteScore;
                                            lblWhiteScore.Text = whiteScore.ToString();
                                        }
                                        else
                                        {
                                            timerBlack.Stop();
                                            lblTimerBlack.BackColor = timerWhitePauseBackColor;
                                            lblTimerBlack.ForeColor = timerWhitePauseForeColor;

                                            ++blackScore;
                                            lblBlackScore.Text = blackScore.ToString();
                                        }

                                        gameEnded = true;
                                    }
                                    else
                                    {
                                        if (Stalemate(OppositePieceColor(currentCell.PieceColor)) || FewPiecesDraw())
                                        {
                                            var gameEndSoundEffect = new WMPLib.WindowsMediaPlayer();
                                            gameEndSoundEffect.URL = soundEffectsFilesPath + @"\game-end.mp3";
                                            gameEndSoundEffect.controls.play();
                                            cmbTime.Enabled = true;

                                            gameEndForm = new GameEndForm();
                                            gameEndForm.SetDrawText();
                                            gameEndForm.Show();

                                            if (currentCell.PieceColor == PieceColors.White)
                                            {
                                                timerWhite.Stop();
                                                lblTimerWhite.BackColor = timerWhitePauseBackColor;
                                                lblTimerWhite.ForeColor = timerWhitePauseForeColor;
                                            }
                                            else
                                            {
                                                timerBlack.Stop();
                                                lblTimerBlack.BackColor = timerWhitePauseBackColor;
                                                lblTimerBlack.ForeColor = timerWhitePauseForeColor;
                                            }

                                            gameEnded = true;
                                        }
                                        else
                                        {
                                            if(PawnPromote(currentCell))
                                            {
                                                PromotingForm promotingForm = new PromotingForm(currentCell.PieceColor);
                                                promotingForm.Show();

                                                promotingForm.FormClosing += (senderPForm, ePForm) =>
                                                {
                                                    var form = (PromotingForm)senderPForm;
                                                    currentCell.PieceType = form.pieceType;
                                                    currentCell.BackgroundImage = form.pieceImage;
                                                };
                                            }

                                            if (turns == PieceColors.Black)
                                            {
                                                turns = PieceColors.White;
                                                timerBlack.Stop();
                                                timerWhite.Start();

                                                lblTimerBlack.BackColor = timerBlackPauseBackColor;
                                                lblTimerBlack.ForeColor = timerBlackPauseForeColor;

                                                lblTimerWhite.BackColor = Color.White;
                                                lblTimerWhite.ForeColor = Color.Black;
                                            }
                                            else
                                            {
                                                turns = PieceColors.Black;
                                                timerWhite.Stop();
                                                timerBlack.Start();

                                                lblTimerWhite.BackColor = timerWhitePauseBackColor;
                                                lblTimerWhite.ForeColor = timerWhitePauseForeColor;

                                                lblTimerBlack.BackColor = timerBlackBackColor;
                                                lblTimerBlack.ForeColor = Color.White;
                                            }
                                        }
                                    }
                                    firstSecondClick = false;
                                }
                                else
                                {
                                    if (previousCell.PieceColor != currentCell.PieceColor)
                                    {
                                        var illegalMoveSoundEffect = new WMPLib.WindowsMediaPlayer();
                                        illegalMoveSoundEffect.URL = soundEffectsFilesPath + @"\illegal.mp3";
                                        illegalMoveSoundEffect.controls.play();
                                        firstSecondClick = false;
                                    }
                                    else
                                    {
                                        Cell kingCell = FindKing(OppositePieceColor(currentCell.PieceColor));

                                        ErasePieceLegalMoves(legalMoves);
                                        if (kingInCheck)
                                        {
                                            legalMoves.Clear();
                                            GenerateInCheckLegalMoves(ref legalMoves, currentCell, kingCell);

                                            if (legalMoves.Count > 0)
                                            {
                                                DrawPieceLegalMoves(legalMoves);
                                                previousCell = currentCell;
                                            }
                                        }
                                        else
                                        {
                                            GeneratePieceLegalMoves(ref legalMoves, currentCell.Row, currentCell.Column, currentCell);
                                            RemoveEventualIlegalMoves(ref legalMoves, currentCell, kingCell);
                                            DrawPieceLegalMoves(legalMoves);
                                            previousCell = currentCell;
                                        }
                                    }
                                }
                                break;
                        }
                    };
                }
        }

        private bool PawnPromote(Cell currentCell)
        {
            if (currentCell.PieceType != PieceTypes.Pawn) return false;

            if (currentCell.PieceColor == PieceColors.White && currentCell.Row == 0)
                return true;

            if(currentCell.PieceColor == PieceColors.Black && currentCell.Row == 7)
                return true;

            return false;
        }

        private bool Checkmate(Cell targetCell)
        {
            if (!kingInCheck) return false;

            List<Point> pieceLegalMoves = new List<Point>();
            PieceColors oppositeTargetCellColor = OppositePieceColor(targetCell.PieceColor);

            Cell kingCell = FindKing(targetCell.PieceColor);

            bool ok = false;
            for(int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    if (cell[i, j].Occupied && cell[i, j].PieceColor == oppositeTargetCellColor)
                        GenerateInCheckLegalMoves(ref pieceLegalMoves, cell[i, j], kingCell);

                    if (pieceLegalMoves.Count > 0)
                    {
                        ok = true;
                        break;
                    }
                }

                if (ok) break;
            }

            if (!ok) return true;
            return false;
        }

        private bool Stalemate(PieceColors pieceColor)
        {
            bool isStalemate = true;
            List<Point> pieceLegalMoves = new List<Point>();

            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                    if (cell[i, j].Occupied && cell[i, j].PieceColor == pieceColor)
                    {
                        GeneratePieceLegalMoves(ref pieceLegalMoves, i, j, cell[i, j]);
                        if (pieceLegalMoves.Count > 0)
                        {
                            isStalemate = false;
                            break;
                        }
                    }

                if (!isStalemate) break;
            }

            return isStalemate;
        }

        private bool FewPiecesDraw()
        {
            List<PieceTypes> whitePieces = new List<PieceTypes>();
            List<PieceTypes> blackPieces = new List<PieceTypes>();

            for (int i = 0; i < 8; ++i)
                for (int j = 0; j < 8; ++j)
                    if (cell[i, j].Occupied && cell[i, j].PieceType != PieceTypes.King)
                    {
                        if (cell[i, j].PieceColor == PieceColors.White)
                            whitePieces.Add(cell[i, j].PieceType);
                        else
                            blackPieces.Add(cell[i, j].PieceType);
                    }

            if (whitePieces.Count > 1 || blackPieces.Count > 1) return false;
            if (whitePieces.Count == 0 && blackPieces.Count == 0) return true;

            if (whitePieces.Contains(PieceTypes.Bishop) || whitePieces.Contains(PieceTypes.Knight)
                || blackPieces.Contains(PieceTypes.Bishop) || blackPieces.Contains(PieceTypes.Knight))
                return true;

            return false;
        }

        private void RemoveEventualIlegalMoves(ref List<Point> legalMoves, Cell targetCell, Cell kingCell)
        {
            Cell attackerCell = new Cell();
            List<Point> pieceLegalMoves = new List<Point>(), attackerPieceLegalMoves = new List<Point>();
            GeneratePieceLegalMoves(ref pieceLegalMoves, targetCell.Row, targetCell.Column, targetCell);
            bool ok = false;
            PieceColors targetCellColor = targetCell.PieceColor, oppositePieceColor = OppositePieceColor(targetCellColor);


            cell[targetCell.Row, targetCell.Column].PieceColor = PieceColors.None;
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    if (cell[i, j].Occupied && cell[i, j].PieceColor == oppositePieceColor)
                    {
                        GeneratePieceLegalMoves(ref attackerPieceLegalMoves, i, j, cell[i, j]);
                        if (KingInCheck(attackerPieceLegalMoves, cell[i, j], kingCell))
                        {
                            ok = true;
                            attackerCell = cell[i, j];
                            break;
                        }
                    }
                }

                if (ok) break;
            }

            if (ok)
            {
                foreach(var pieceLegalMove in pieceLegalMoves)
                {
                    cell[pieceLegalMove.X, pieceLegalMove.Y].PieceColor = targetCellColor;

                    GeneratePieceLegalMoves(ref attackerPieceLegalMoves, attackerCell.Row, attackerCell.Column, attackerCell);
                    if(KingInCheck(attackerPieceLegalMoves, attackerCell, kingCell))
                        legalMoves.Remove(pieceLegalMove);

                    if (cell[pieceLegalMove.X, pieceLegalMove.Y].Occupied)
                        cell[pieceLegalMove.X, pieceLegalMove.Y].PieceColor = oppositePieceColor;
                    else
                        cell[pieceLegalMove.X, pieceLegalMove.Y].PieceColor = PieceColors.None;
                }
            }

            cell[targetCell.Row, targetCell.Column].PieceColor = targetCellColor;
        }

        private void GenerateInCheckLegalMoves(ref List<Point> legalMoves, Cell targetCell, Cell kingCell)
        {
            if(targetCell.PieceType == PieceTypes.King)
                GeneratePieceLegalMoves(ref legalMoves, targetCell.Row, targetCell.Column, targetCell);
            else
            {
                List<Point> pieceLegalMoves = new List<Point>(), attackerPieceLegalMoves = new List<Point>();
                GeneratePieceLegalMoves(ref pieceLegalMoves, targetCell.Row, targetCell.Column, targetCell);
                bool ok;
                PieceColors oppositeTargetCellColor = OppositePieceColor(targetCell.PieceColor);

                foreach (var pieceLegalMove in pieceLegalMoves)
                {
                    ok = true;
                    cell[pieceLegalMove.X, pieceLegalMove.Y].PieceColor = targetCell.PieceColor;

                    for (int i = 0; i < 8; ++i)
                    {
                        for (int j = 0; j < 8; ++j)
                            if (cell[i, j].Occupied && cell[i, j].PieceColor != targetCell.PieceColor)
                            {
                                GeneratePieceLegalMoves(ref attackerPieceLegalMoves, i, j, cell[i, j]);
                                if (KingInCheck(attackerPieceLegalMoves, cell[i, j], kingCell))
                                {
                                    ok = false;
                                    break;
                                }
                            }

                        if (!ok) break;
                    }

                    if (ok)
                        legalMoves.Add(pieceLegalMove);

                    if (cell[pieceLegalMove.X, pieceLegalMove.Y].Occupied)
                        cell[pieceLegalMove.X, pieceLegalMove.Y].PieceColor = oppositeTargetCellColor;
                    else
                        cell[pieceLegalMove.X, pieceLegalMove.Y].PieceColor = PieceColors.None;

                }
            }
        }

        private Cell FindKing(PieceColors pieceColor)
        {
            for (int i = 0; i < 8; ++i)
                for (int j = 0; j < 8; ++j)
                    if (cell[i, j].PieceType == PieceTypes.King && cell[i, j].PieceColor != pieceColor)
                        return cell[i, j];

            return null;
        }

        private void DrawPieceLegalMoves(List<Point> legalMoves)
        {
            foreach (var move in legalMoves)
                if ((move.X + move.Y) % 2 == 0) cell[move.X, move.Y].PaintCell(whiteSquareLegalColor);
                else cell[move.X, move.Y].PaintCell(blackSquareLegalColor);
        }

        private void ErasePieceLegalMoves(List<Point> legalMoves)
        {
            foreach (var move in legalMoves)
                if ((move.X + move.Y) % 2 == 0) cell[move.X, move.Y].PaintCell(whiteSquareColor);
                else cell[move.X, move.Y].PaintCell(blackSquareColor);
        }

        private void MovePiece(Cell previousCell, Cell currentCell)
        {
            bool pieceCaptured = false;
            kingInCheck = false;

            if (currentCell.Occupied) pieceCaptured = true;

            currentCell.BackgroundImage = previousCell.BackgroundImage;
            currentCell.Occupied = true;
            currentCell.PieceColor = previousCell.PieceColor;
            currentCell.PieceType = previousCell.PieceType;
            AddMovesToLV(currentCell);

            previousCell.BackgroundImage = null;
            previousCell.Occupied = false;
            previousCell.PieceColor = PieceColors.None;
            previousCell.PieceType = PieceTypes.None;

            List<Point> pieceLegalMoves = new List<Point>();
            Cell kingCell = null;

            kingCell = FindKing(currentCell.PieceColor);

            //pentru discovered check
            for(int i = 0; i < 8; ++i)
                for(int j = 0; j < 8; ++j)
                {
                    if (cell[i, j].Occupied && cell[i, j].PieceColor == currentCell.PieceColor)
                    {
                        GeneratePieceLegalMoves(ref pieceLegalMoves, i, j, cell[i, j]);
                        if(KingInCheck(pieceLegalMoves, cell[i, j], kingCell))
                            kingInCheck = true;
                    }
                }

            if (kingInCheck)
            {
                var checkSoundEffect = new WMPLib.WindowsMediaPlayer();
                checkSoundEffect.URL = soundEffectsFilesPath + @"\move-check.mp3";
                checkSoundEffect.controls.play();
            }
            else
            {
                if (!pieceCaptured)
                {
                    var legalMoveSoundEffect = new WMPLib.WindowsMediaPlayer();
                    legalMoveSoundEffect.URL = soundEffectsFilesPath + @"\move.mp3";
                    legalMoveSoundEffect.controls.play();
                }
                else
                {
                    var capturePieceSoundEffect = new WMPLib.WindowsMediaPlayer();
                    capturePieceSoundEffect.URL = soundEffectsFilesPath + @"\capture.mp3";
                    capturePieceSoundEffect.controls.play();
                }
            }
        }

        private void GeneratePieceLegalMoves(ref List<Point> legalMoves, int row, int column, Cell targetCell)
        {
            switch(targetCell.PieceType)
            {
                case PieceTypes.None:
                    legalMoves.Clear();
                    break;

                case PieceTypes.Pawn:
                    legalMoves = pawn.LegalMoves(row, column, targetCell, cell);
                    break;

                case PieceTypes.Rook:
                    legalMoves = rook.LegalMoves(row, column, targetCell, cell);
                    break;

                case PieceTypes.Knight:
                    legalMoves = knight.LegalMoves(row, column, targetCell, cell);
                    break;

                case PieceTypes.Bishop:
                    legalMoves = bishop.LegalMoves(row, column, targetCell, cell);
                    break;

                case PieceTypes.Queen:
                    legalMoves = queen.LegalMoves(row, column, targetCell, cell);
                    break;

                case PieceTypes.King:
                    legalMoves = king.LegalMoves(row, column, targetCell, cell);
                    RestrictKingMovement(ref legalMoves, targetCell);
                    break;
            }
        }
        private void RestrictKingMovement(ref List<Point> kingLegalMoves, Cell kingCell)
        {
            List<Point> restrictedMoves = new List<Point>(), pieceLegalMoves = new List<Point>();
            PieceColors oppositeKingPieceColor = OppositePieceColor(kingCell.PieceColor);

            foreach (var kingLegalMove in kingLegalMoves)
            {
                bool pieceInWay = false;
                if (cell[kingLegalMove.X, kingLegalMove.Y].PieceColor == oppositeKingPieceColor)
                {
                    cell[kingLegalMove.X, kingLegalMove.Y].PieceColor = kingCell.PieceColor;
                    pieceInWay = true;
                }

                for (int i = 0; i < 8; ++i)
                    for (int j = 0; j < 8; ++j)
                    {
                        if (cell[i, j].Occupied && cell[i, j].PieceColor == oppositeKingPieceColor)
                        {
                            if (cell[kingLegalMove.X, kingLegalMove.Y].PieceColor == oppositeKingPieceColor)
                                cell[kingLegalMove.X, kingLegalMove.Y].PieceColor = kingCell.PieceColor;

                            switch (cell[i, j].PieceType)
                            {
                                case PieceTypes.None:
                                    pieceLegalMoves.Clear();
                                    break;

                                case PieceTypes.Pawn:
                                    pieceLegalMoves.Clear();

                                    if(kingCell.PieceColor == PieceColors.White)
                                        if(kingLegalMove.X == i + 1 && (kingLegalMove.Y == j - 1 || kingLegalMove.Y == j + 1))
                                            restrictedMoves.Add(kingLegalMove);

                                    if(kingCell.PieceColor == PieceColors.Black)
                                        if (kingLegalMove.X == i - 1 && (kingLegalMove.Y == j - 1 || kingLegalMove.Y == j + 1))
                                            restrictedMoves.Add(kingLegalMove);
                                    break;

                                case PieceTypes.Rook:
                                    pieceLegalMoves = rook.LegalMoves(i, j, cell[i, j], cell);
                                    break;

                                case PieceTypes.Knight:
                                    pieceLegalMoves = knight.LegalMoves(i, j, cell[i, j], cell);
                                    break;

                                case PieceTypes.Bishop:
                                    pieceLegalMoves = bishop.LegalMoves(i, j, cell[i, j], cell);
                                    break;

                                case PieceTypes.Queen:
                                    pieceLegalMoves = queen.LegalMoves(i, j, cell[i, j], cell);
                                    break;

                                case PieceTypes.King:
                                    pieceLegalMoves = king.LegalMoves(i, j, cell[i, j], cell);
                                    break;
                            }

                            foreach (var pieceLegalMove in pieceLegalMoves)
                                if (kingLegalMoves.Contains(pieceLegalMove))
                                    restrictedMoves.Add(pieceLegalMove);
                        }
                    }

                if(pieceInWay)
                    cell[kingLegalMove.X, kingLegalMove.Y].PieceColor = oppositeKingPieceColor;
            }

            foreach (var restrictedMove in restrictedMoves)
                kingLegalMoves.Remove(restrictedMove);
        }

        private bool KingInCheck(List<Point> legalMoves, Cell targetCell, Cell kingCell)
        {
            switch (targetCell.PieceType)
            {
                case PieceTypes.Pawn:
                    if (pawn.VerifyCheck(kingCell, targetCell.Row, targetCell.Column))
                        return true;
                    break;

                case PieceTypes.Rook:
                    if (rook.VerifyCheck(kingCell, legalMoves))
                        return true;
                    break;

                case PieceTypes.Knight:
                    if (knight.VerifyCheck(kingCell, legalMoves))
                        return true;
                    break;

                case PieceTypes.Bishop:
                    if (bishop.VerifyCheck(kingCell, legalMoves))
                        return true;
                    break;

                case PieceTypes.Queen:
                    if (queen.VerifyCheck(kingCell, legalMoves))
                        return true;
                    break;
            }

            return false;
        }

        private void AddMovesToLV(Cell targetCell)
        {
            string moveCord = string.Empty;

            switch (targetCell.PieceType)
            {
                case PieceTypes.Rook:
                    moveCord += 'R';
                    break;

                case PieceTypes.Knight:
                    moveCord += 'N';
                    break;

                case PieceTypes.Bishop:
                    moveCord += 'B';
                    break;

                case PieceTypes.Queen:
                    moveCord += 'Q';
                    break;

                case PieceTypes.King:
                    moveCord += 'K';
                    break;
            }

            switch(targetCell.Column)
            {
                case 0:
                    moveCord += 'a';
                    break;

                case 1:
                    moveCord += 'b';
                    break;

                case 2:
                    moveCord += 'c';
                    break;

                case 3:
                    moveCord += 'd';
                    break;

                case 4:
                    moveCord += 'e';
                    break;

                case 5:
                    moveCord += 'f';
                    break;

                case 6:
                    moveCord += 'g';
                    break;

                case 7:
                    moveCord += 'h';
                    break;
            }

            moveCord += (8 - targetCell.Row).ToString();
            ListViewItem listViewItem = new ListViewItem(moveCord);
           
            switch (targetCell.PieceColor)
            {
                case PieceColors.Black:
                    listViewItem.ForeColor = Color.Black;
                    break;

                case PieceColors.White:
                    listViewItem.ForeColor = Color.White;
                    break;
            }
            _listView.Items.Add(listViewItem);
        }

        private PieceColors OppositePieceColor(PieceColors pieceColor)
        {
            if(pieceColor == PieceColors.Black) return PieceColors.White;
            return PieceColors.Black;
        }
    }
}
