namespace Chess
{
    partial class PromotingForm
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
            this.btnKnight = new System.Windows.Forms.Button();
            this.btnBishop = new System.Windows.Forms.Button();
            this.btnQueen = new System.Windows.Forms.Button();
            this.btnRook = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnKnight
            // 
            this.btnKnight.Location = new System.Drawing.Point(12, 33);
            this.btnKnight.Name = "btnKnight";
            this.btnKnight.Size = new System.Drawing.Size(123, 78);
            this.btnKnight.TabIndex = 3;
            this.btnKnight.UseVisualStyleBackColor = true;
            // 
            // btnBishop
            // 
            this.btnBishop.Location = new System.Drawing.Point(141, 33);
            this.btnBishop.Name = "btnBishop";
            this.btnBishop.Size = new System.Drawing.Size(123, 78);
            this.btnBishop.TabIndex = 4;
            this.btnBishop.UseVisualStyleBackColor = true;
            // 
            // btnQueen
            // 
            this.btnQueen.Location = new System.Drawing.Point(270, 33);
            this.btnQueen.Name = "btnQueen";
            this.btnQueen.Size = new System.Drawing.Size(123, 78);
            this.btnQueen.TabIndex = 5;
            this.btnQueen.UseVisualStyleBackColor = true;
            // 
            // btnRook
            // 
            this.btnRook.Location = new System.Drawing.Point(399, 33);
            this.btnRook.Name = "btnRook";
            this.btnRook.Size = new System.Drawing.Size(123, 78);
            this.btnRook.TabIndex = 6;
            this.btnRook.UseVisualStyleBackColor = true;
            // 
            // PromotingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 146);
            this.Controls.Add(this.btnRook);
            this.Controls.Add(this.btnQueen);
            this.Controls.Add(this.btnBishop);
            this.Controls.Add(this.btnKnight);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PromotingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PromotingForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnKnight;
        private System.Windows.Forms.Button btnBishop;
        private System.Windows.Forms.Button btnQueen;
        private System.Windows.Forms.Button btnRook;
    }
}