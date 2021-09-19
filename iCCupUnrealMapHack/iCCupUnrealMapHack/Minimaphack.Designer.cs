namespace iCCupUnrealMapHack
{
    partial class Minimaphack
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ( )
        {
            this.components = new System.ComponentModel.Container();
            this.DragTimer = new System.Windows.Forms.Timer(this.components);
            this.MinihackTimer = new System.Windows.Forms.Timer(this.components);
            this.ClickabledTimer = new System.Windows.Forms.Timer(this.components);
            this.MyTimerFix = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // DragTimer
            // 
            this.DragTimer.Enabled = true;
            this.DragTimer.Interval = 120;
            this.DragTimer.Tick += new System.EventHandler(this.DragTimer_Tick);
            // 
            // MinihackTimer
            // 
            this.MinihackTimer.Enabled = true;
            this.MinihackTimer.Interval = 150;
            this.MinihackTimer.Tick += new System.EventHandler(this.MinihackTimer_Tick);
            // 
            // ClickabledTimer
            // 
            this.ClickabledTimer.Enabled = true;
            this.ClickabledTimer.Interval = 50;
            this.ClickabledTimer.Tick += new System.EventHandler(this.ClickabledTimer_Tick);
            // 
            // MyTimerFix
            // 
            this.MyTimerFix.Enabled = true;
            this.MyTimerFix.Tick += new System.EventHandler(this.MyTimerFix_Tick);
            // 
            // Minimaphack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(260, 260);
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(4000, 4000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(128, 128);
            this.Name = "Minimaphack";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(123)))), ((int)(((byte)(123)))));
            this.Load += new System.EventHandler(this.Minimaphack_Load);
            this.LocationChanged += new System.EventHandler(this.Minimaphack_LocationChanged);
            this.DoubleClick += new System.EventHandler(this.MiniMapImg_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MiniMapImg_MouseDown);
            this.MouseEnter += new System.EventHandler(this.MiniMapImg_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.MiniMapImg_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Minimaphack_MouseMove);
            this.Resize += new System.EventHandler(this.MiniMapImg_Resize);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer DragTimer;
        private System.Windows.Forms.Timer MinihackTimer;
        private System.Windows.Forms.Timer ClickabledTimer;
        private System.Windows.Forms.Timer MyTimerFix;
    }
}