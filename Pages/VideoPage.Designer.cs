namespace ObjectsRecognition.Pages
{
    partial class VideoPage
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
            pbVideo = new PictureBox();
            btnPlay = new Button();
            btnPause = new Button();
            btnStop = new Button();
            txtVideo = new TextBox();
            trackBarVideo = new TrackBar();
            btnScreenShot = new Button();
            lblSpeedLimit = new Label();
            ((System.ComponentModel.ISupportInitialize)pbVideo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarVideo).BeginInit();
            SuspendLayout();
            // 
            // pbVideo
            // 
            pbVideo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pbVideo.Location = new Point(0, 0);
            pbVideo.Name = "pbVideo";
            pbVideo.Size = new Size(998, 405);
            pbVideo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbVideo.TabIndex = 0;
            pbVideo.TabStop = false;
            // 
            // btnPlay
            // 
            btnPlay.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnPlay.FlatAppearance.BorderSize = 0;
            btnPlay.FlatStyle = FlatStyle.Flat;
            btnPlay.Image = Properties.Resources.play_circle_outline;
            btnPlay.Location = new Point(10, 417);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(40, 33);
            btnPlay.TabIndex = 1;
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += btnPlay_Click;
            // 
            // btnPause
            // 
            btnPause.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnPause.FlatAppearance.BorderSize = 0;
            btnPause.FlatStyle = FlatStyle.Flat;
            btnPause.Image = Properties.Resources.pause;
            btnPause.Location = new Point(60, 417);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(40, 33);
            btnPause.TabIndex = 2;
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += btnPause_Click;
            // 
            // btnStop
            // 
            btnStop.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnStop.FlatAppearance.BorderSize = 0;
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.Image = Properties.Resources.stop;
            btnStop.Location = new Point(110, 417);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(40, 33);
            btnStop.TabIndex = 3;
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // txtVideo
            // 
            txtVideo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtVideo.Enabled = false;
            txtVideo.Location = new Point(168, 420);
            txtVideo.Name = "txtVideo";
            txtVideo.Size = new Size(66, 27);
            txtVideo.TabIndex = 4;
            txtVideo.Text = "0:00:00";
            // 
            // trackBarVideo
            // 
            trackBarVideo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            trackBarVideo.LargeChange = 2;
            trackBarVideo.Location = new Point(303, 408);
            trackBarVideo.Name = "trackBarVideo";
            trackBarVideo.Size = new Size(641, 56);
            trackBarVideo.TabIndex = 5;
            trackBarVideo.TickFrequency = 20;
            trackBarVideo.TickStyle = TickStyle.Both;
            // 
            // btnScreenShot
            // 
            btnScreenShot.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnScreenShot.FlatAppearance.BorderSize = 0;
            btnScreenShot.FlatStyle = FlatStyle.Flat;
            btnScreenShot.Image = Properties.Resources.screenshot;
            btnScreenShot.Location = new Point(950, 417);
            btnScreenShot.Name = "btnScreenShot";
            btnScreenShot.Size = new Size(40, 33);
            btnScreenShot.TabIndex = 6;
            btnScreenShot.UseVisualStyleBackColor = true;
            btnScreenShot.Click += btnScreenShot_Click;
            // 
            // lblSpeedLimit
            // 
            lblSpeedLimit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblSpeedLimit.AutoSize = true;
            lblSpeedLimit.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblSpeedLimit.Location = new Point(240, 417);
            lblSpeedLimit.MinimumSize = new Size(48, 28);
            lblSpeedLimit.Name = "lblSpeedLimit";
            lblSpeedLimit.Size = new Size(48, 32);
            lblSpeedLimit.TabIndex = 7;
            lblSpeedLimit.TextAlign = ContentAlignment.TopCenter;
            // 
            // VideoPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1001, 462);
            Controls.Add(lblSpeedLimit);
            Controls.Add(btnScreenShot);
            Controls.Add(trackBarVideo);
            Controls.Add(txtVideo);
            Controls.Add(btnStop);
            Controls.Add(btnPause);
            Controls.Add(btnPlay);
            Controls.Add(pbVideo);
            ForeColor = Color.Red;
            Name = "VideoPage";
            Text = "Video";
            Deactivate += VideoPage_Deactivate;
            Load += VideoPage_Load;
            ((System.ComponentModel.ISupportInitialize)pbVideo).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarVideo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbVideo;
        private Button btnPlay;
        private Button btnPause;
        private Button btnStop;
        private TextBox txtVideo;
        private TrackBar trackBarVideo;
        private Button btnScreenShot;
        private Label lblSpeedLimit;
    }
}