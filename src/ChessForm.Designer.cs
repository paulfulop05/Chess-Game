namespace Chess
{
    partial class ChessForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChessForm));
            this.boardPanel = new System.Windows.Forms.Panel();
            this.menuPanel = new System.Windows.Forms.Panel();
            this.grpMoves = new System.Windows.Forms.GroupBox();
            this.lvMoves = new System.Windows.Forms.ListView();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.cmbTime = new System.Windows.Forms.ComboBox();
            this.lblBlackTime = new System.Windows.Forms.Label();
            this.lblWhiteTime = new System.Windows.Forms.Label();
            this.lblBlackScore = new System.Windows.Forms.Label();
            this.lblWhiteScore = new System.Windows.Forms.Label();
            this.menuPanel.SuspendLayout();
            this.grpMoves.SuspendLayout();
            this.SuspendLayout();
            // 
            // boardPanel
            // 
            this.boardPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boardPanel.Location = new System.Drawing.Point(12, 12);
            this.boardPanel.Name = "boardPanel";
            this.boardPanel.Size = new System.Drawing.Size(717, 717);
            this.boardPanel.TabIndex = 2;
            // 
            // menuPanel
            // 
            this.menuPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menuPanel.Controls.Add(this.grpMoves);
            this.menuPanel.Controls.Add(this.btnRestart);
            this.menuPanel.Controls.Add(this.btnPlay);
            this.menuPanel.Controls.Add(this.cmbTime);
            this.menuPanel.Location = new System.Drawing.Point(992, 12);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(393, 717);
            this.menuPanel.TabIndex = 3;
            // 
            // grpMoves
            // 
            this.grpMoves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMoves.Controls.Add(this.lvMoves);
            this.grpMoves.Font = new System.Drawing.Font("Bauhaus 93", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMoves.ForeColor = System.Drawing.Color.White;
            this.grpMoves.Location = new System.Drawing.Point(51, 312);
            this.grpMoves.Name = "grpMoves";
            this.grpMoves.Size = new System.Drawing.Size(294, 391);
            this.grpMoves.TabIndex = 3;
            this.grpMoves.TabStop = false;
            this.grpMoves.Text = "MOVES";
            // 
            // lvMoves
            // 
            this.lvMoves.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvMoves.Font = new System.Drawing.Font("Bahnschrift", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvMoves.ForeColor = System.Drawing.Color.Transparent;
            this.lvMoves.HideSelection = false;
            this.lvMoves.Location = new System.Drawing.Point(6, 33);
            this.lvMoves.Name = "lvMoves";
            this.lvMoves.Size = new System.Drawing.Size(282, 352);
            this.lvMoves.TabIndex = 0;
            this.lvMoves.UseCompatibleStateImageBehavior = false;
            // 
            // btnRestart
            // 
            this.btnRestart.Font = new System.Drawing.Font("Bauhaus 93", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestart.Location = new System.Drawing.Point(51, 241);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(294, 50);
            this.btnRestart.TabIndex = 2;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            // 
            // btnPlay
            // 
            this.btnPlay.Font = new System.Drawing.Font("Bauhaus 93", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.Location = new System.Drawing.Point(51, 140);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(294, 50);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            // 
            // cmbTime
            // 
            this.cmbTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTime.Font = new System.Drawing.Font("Bauhaus 93", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTime.FormattingEnabled = true;
            this.cmbTime.Items.AddRange(new object[] {
            "10 min",
            "5 min",
            "3 min",
            "1 min"});
            this.cmbTime.Location = new System.Drawing.Point(51, 47);
            this.cmbTime.Name = "cmbTime";
            this.cmbTime.Size = new System.Drawing.Size(294, 44);
            this.cmbTime.TabIndex = 0;
            // 
            // lblBlackTime
            // 
            this.lblBlackTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBlackTime.AutoSize = true;
            this.lblBlackTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlackTime.Location = new System.Drawing.Point(735, 12);
            this.lblBlackTime.Name = "lblBlackTime";
            this.lblBlackTime.Size = new System.Drawing.Size(158, 55);
            this.lblBlackTime.TabIndex = 4;
            this.lblBlackTime.Text = "label1";
            // 
            // lblWhiteTime
            // 
            this.lblWhiteTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWhiteTime.AutoSize = true;
            this.lblWhiteTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWhiteTime.Location = new System.Drawing.Point(735, 674);
            this.lblWhiteTime.Name = "lblWhiteTime";
            this.lblWhiteTime.Size = new System.Drawing.Size(158, 55);
            this.lblWhiteTime.TabIndex = 5;
            this.lblWhiteTime.Text = "label2";
            // 
            // lblBlackScore
            // 
            this.lblBlackScore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBlackScore.AutoSize = true;
            this.lblBlackScore.Font = new System.Drawing.Font("Verdana", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlackScore.Location = new System.Drawing.Point(735, 67);
            this.lblBlackScore.Name = "lblBlackScore";
            this.lblBlackScore.Size = new System.Drawing.Size(79, 78);
            this.lblBlackScore.TabIndex = 6;
            this.lblBlackScore.Text = "0";
            // 
            // lblWhiteScore
            // 
            this.lblWhiteScore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWhiteScore.AutoSize = true;
            this.lblWhiteScore.Font = new System.Drawing.Font("Verdana", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWhiteScore.Location = new System.Drawing.Point(732, 596);
            this.lblWhiteScore.Name = "lblWhiteScore";
            this.lblWhiteScore.Size = new System.Drawing.Size(79, 78);
            this.lblWhiteScore.TabIndex = 7;
            this.lblWhiteScore.Text = "0";
            // 
            // ChessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1397, 741);
            this.Controls.Add(this.lblWhiteScore);
            this.Controls.Add(this.lblBlackScore);
            this.Controls.Add(this.lblWhiteTime);
            this.Controls.Add(this.lblBlackTime);
            this.Controls.Add(this.menuPanel);
            this.Controls.Add(this.boardPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChessForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chess";
            this.menuPanel.ResumeLayout(false);
            this.grpMoves.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel boardPanel;
        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.ComboBox cmbTime;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.GroupBox grpMoves;
        private System.Windows.Forms.Label lblBlackTime;
        private System.Windows.Forms.Label lblWhiteTime;
        private System.Windows.Forms.Label lblBlackScore;
        private System.Windows.Forms.Label lblWhiteScore;
        private System.Windows.Forms.ListView lvMoves;
    }
}