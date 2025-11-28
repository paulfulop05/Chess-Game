using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Media;

namespace Chess
{
    public partial class ChessForm : Form
    {
        private Color backColor = Color.FromArgb(48, 46, 43),
            menuColor = Color.FromArgb(38, 37, 34),
            lightBrown = Color.FromArgb(224, 204, 164),
            darkBrown = Color.FromArgb(130, 97, 66),
            darkerBrown = Color.FromArgb(84, 62, 42),
            lowOnTimeColor = Color.FromArgb(158, 28, 33);

        private ChessBoard chessBoard;
        private int whiteScore = 0, blackScore = 0;
        private Timer countdownTimerW, countdownTimerB;
        private int secW, minW, secB, minB;

        private static readonly string soundEffectsFilesPath = Application.StartupPath.Replace(@"bin\Debug", "resurse");

        public ChessForm()
        {
            InitializeComponent();
            HandleButtons();
            HandleTimers();
            CreateChessBoard();
            StyleControls();
        }

        private void HandleTimers()
        {
            countdownTimerW = new Timer();
            countdownTimerW.Interval = 1000;
            countdownTimerW.Tick += (sender, e) =>
            {
                if (secW == 0)
                {
                    --minW;
                    secW = 60;
                }
                --secW;

                if(minW == 0)
                {
                    if(secW == 20)
                    {
                        var outOfTimeSoundEffect = new WMPLib.WindowsMediaPlayer();
                        outOfTimeSoundEffect.URL = soundEffectsFilesPath + @"\20seconds.mp3";
                        outOfTimeSoundEffect.controls.play();
                    }

                    if(secW <= 20)
                    {
                        lblWhiteTime.BackColor = lowOnTimeColor;
                        lblWhiteTime.ForeColor = Color.White;
                    }

                    if(secW == 0)
                    {
                        countdownTimerW.Stop();

                        lblWhiteTime.BackColor = Color.White;
                        lblWhiteTime.ForeColor = Color.Black;

                        var gameEndSoundEffect = new WMPLib.WindowsMediaPlayer();
                        gameEndSoundEffect.URL = soundEffectsFilesPath + @"\game-end.mp3";
                        gameEndSoundEffect.controls.play();
                        cmbTime.Enabled = true;

                        chessBoard.CanPlayOnBoard(false);

                        GameEndForm gameEndForm = new GameEndForm();
                        gameEndForm.SetWinnerText(PieceColors.Black);
                        gameEndForm.Show();

                        ++blackScore;
                        lblBlackScore.Text = blackScore.ToString();
                    }
                }

                if (secW < 10)
                    lblWhiteTime.Text = minW.ToString() + ":0" + secW.ToString();
                else
                    lblWhiteTime.Text = minW.ToString() + ":" + secW.ToString();
            };

            countdownTimerB = new Timer();
            countdownTimerB.Interval = 1000;
            countdownTimerB.Tick += (sender, e) =>
            {
                if (secB == 0)
                {
                    --minB;
                    secB = 60;
                }
                --secB;

                if (minB == 0)
                {
                    if(secB == 20)
                    {
                        var outOfTimeSoundEffect = new WMPLib.WindowsMediaPlayer();
                        outOfTimeSoundEffect.URL = soundEffectsFilesPath + @"\20seconds.mp3";
                        outOfTimeSoundEffect.controls.play();
                    }

                    if(secB <= 20)
                    {
                        lblBlackTime.BackColor = lowOnTimeColor;
                        lblBlackTime.ForeColor = Color.White;
                    }

                    if(secB == 0)
                    {
                        countdownTimerB.Stop();

                        lblBlackTime.BackColor = chessBoard.timerBlackBackColor;
                        lblBlackTime.ForeColor = Color.White;

                        var gameEndSoundEffect = new WMPLib.WindowsMediaPlayer();
                        gameEndSoundEffect.URL = soundEffectsFilesPath + @"\game-end.mp3";
                        gameEndSoundEffect.controls.play();
                        cmbTime.Enabled = true;

                        chessBoard.CanPlayOnBoard(false);

                        GameEndForm gameEndForm = new GameEndForm();
                        gameEndForm.SetWinnerText(PieceColors.White);
                        gameEndForm.Show();

                        ++whiteScore;
                        lblWhiteScore.Text = whiteScore.ToString();
                    }
                }

                if (secB < 10)
                    lblBlackTime.Text = minB.ToString() + ":0" + secB.ToString();
                else
                    lblBlackTime.Text = minB.ToString() + ":" + secB.ToString();
            };
        }

        private void HandleButtons()
        {
            btnPlay.Click += (sender, e) =>
            {
                SetTime();
                chessBoard.ClearBoard();
                chessBoard.Invalidate();
                SetLockedTimersColor();

                chessBoard.CanPlayOnBoard(true);
                countdownTimerW.Start();

                lblWhiteTime.BackColor = Color.White;
                lblWhiteTime.ForeColor = Color.Black;

                lvMoves.Items.Clear();

                var gameStartSoundEffect = new WMPLib.WindowsMediaPlayer();
                gameStartSoundEffect.URL = soundEffectsFilesPath + @"\game-start.mp3";
                gameStartSoundEffect.controls.play();

                cmbTime.Enabled = false;
            };

            btnRestart.Click += (sender, e) =>
            {
                SetTime();
                lvMoves.Items.Clear();
                chessBoard.ClearBoard();
                chessBoard.Invalidate();
                cmbTime.Enabled = true;
                SetLockedTimersColor();
            };

            cmbTime.SelectedIndexChanged += (sender, e) =>
            {
                if((minW == 0 && secW == 0) || (minB == 0 && secB == 0))
                {
                    chessBoard.ClearBoard();
                    chessBoard.Invalidate();
                    SetLockedTimersColor();
                }

                SetTime();
            };
        }

        private void StyleControls()
        {

            BackColor = backColor;

            menuPanel.BackColor = menuColor;

            cmbTime.BackColor = darkBrown;
            cmbTime.ForeColor = lightBrown;
            cmbTime.FlatStyle = FlatStyle.Flat;
            cmbTime.SelectedIndex = 0;

            btnPlay.BackColor = darkBrown;
            btnPlay.ForeColor = lightBrown;
            btnPlay.FlatStyle = FlatStyle.Flat;
            btnPlay.FlatAppearance.BorderColor = darkBrown;

            btnRestart.BackColor = darkBrown;
            btnRestart.ForeColor = lightBrown;
            btnRestart.FlatStyle = FlatStyle.Flat;
            btnRestart.FlatAppearance.BorderColor = darkBrown;

            grpMoves.ForeColor = darkBrown;

            lblWhiteScore.ForeColor = Color.White;
            lblBlackScore.ForeColor = Color.Black;

            lvMoves.BackColor = menuColor;

            SetLockedTimersColor();
        }

        private void SetLockedTimersColor()
        {
            lblWhiteTime.BackColor = chessBoard.timerWhitePauseBackColor;
            lblWhiteTime.ForeColor = chessBoard.timerWhitePauseForeColor;

            lblBlackTime.BackColor = chessBoard.timerBlackPauseBackColor;
            lblBlackTime.ForeColor = chessBoard.timerBlackPauseForeColor;
        }

        private void SetTime()
        {
            countdownTimerW?.Stop();
            countdownTimerB?.Stop();

            secW = secB = 0;

            if (cmbTime.SelectedIndex == 0)
                minW = minB = 10;

            if (cmbTime.SelectedIndex == 1)
                minW = minB = 5;

            if (cmbTime.SelectedIndex == 2)
                minW = minB = 3;

            if (cmbTime.SelectedIndex == 3)
                minW = minB = 1;

            lblWhiteTime.Text = minW.ToString() + ":00";
            lblBlackTime.Text = minB.ToString() + ":00";
        }

        private void CreateChessBoard()
        {
            chessBoard = new ChessBoard(lvMoves);
            chessBoard.CreateBoard(boardPanel);
            chessBoard.BoardActivity(countdownTimerW, countdownTimerB, lblWhiteTime, lblWhiteScore, lblBlackTime, lblBlackScore, cmbTime);
        }
    }
}
