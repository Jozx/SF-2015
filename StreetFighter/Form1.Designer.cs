namespace StreetFighter
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.PBplayer1 = new System.Windows.Forms.ProgressBar();
            this.PBplayer2 = new System.Windows.Forms.ProgressBar();
            this.RYU = new System.Windows.Forms.PictureBox();
            this.KEN = new System.Windows.Forms.PictureBox();
            this.screen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.RYU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KEN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.screen)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PBplayer1
            // 
            this.PBplayer1.Location = new System.Drawing.Point(326, 8);
            this.PBplayer1.Name = "PBplayer1";
            this.PBplayer1.Size = new System.Drawing.Size(269, 23);
            this.PBplayer1.TabIndex = 6;
            // 
            // PBplayer2
            // 
            this.PBplayer2.Location = new System.Drawing.Point(12, 8);
            this.PBplayer2.Name = "PBplayer2";
            this.PBplayer2.Size = new System.Drawing.Size(269, 23);
            this.PBplayer2.TabIndex = 4;
            // 
            // RYU
            // 
            this.RYU.BackColor = System.Drawing.Color.Transparent;
            this.RYU.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RYU.Location = new System.Drawing.Point(388, 168);
            this.RYU.Name = "RYU";
            this.RYU.Size = new System.Drawing.Size(58, 109);
            this.RYU.TabIndex = 5;
            this.RYU.TabStop = false;
            // 
            // KEN
            // 
            this.KEN.BackColor = System.Drawing.Color.Transparent;
            this.KEN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.KEN.Location = new System.Drawing.Point(173, 168);
            this.KEN.Name = "KEN";
            this.KEN.Size = new System.Drawing.Size(58, 109);
            this.KEN.TabIndex = 3;
            this.KEN.TabStop = false;
            // 
            // screen
            // 
            this.screen.BackColor = System.Drawing.Color.Transparent;
            this.screen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.screen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screen.Location = new System.Drawing.Point(0, 0);
            this.screen.Name = "screen";
            this.screen.Size = new System.Drawing.Size(607, 303);
            this.screen.TabIndex = 7;
            this.screen.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 303);
            this.Controls.Add(this.PBplayer1);
            this.Controls.Add(this.PBplayer2);
            this.Controls.Add(this.RYU);
            this.Controls.Add(this.KEN);
            this.Controls.Add(this.screen);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.RYU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KEN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.screen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar PBplayer1;
        private System.Windows.Forms.ProgressBar PBplayer2;
        private System.Windows.Forms.PictureBox RYU;
        private System.Windows.Forms.PictureBox KEN;
        private System.Windows.Forms.PictureBox screen;
    }
}

