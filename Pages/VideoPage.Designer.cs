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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoPage));
            PbVideo = new PictureBox();
            BtnOpenFolder = new Button();
            BtnPause = new Button();
            BtnStop = new Button();
            TrackBarVideo = new TrackBar();
            BtnScreenShot = new Button();
            LblSpeedLimit = new Label();
            ChbAI = new CheckBox();
            LblTimeCounter = new Label();
            ((System.ComponentModel.ISupportInitialize)PbVideo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TrackBarVideo).BeginInit();
            SuspendLayout();
            // 
            // PbVideo
            // 
            PbVideo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PbVideo.BackColor = SystemColors.Control;
            PbVideo.Location = new Point(0, 43);
            PbVideo.Margin = new Padding(3, 2, 3, 2);
            PbVideo.Name = "PbVideo";
            PbVideo.Size = new Size(896, 504);
            PbVideo.SizeMode = PictureBoxSizeMode.StretchImage;
            PbVideo.TabIndex = 0;
            PbVideo.TabStop = false;
            // 
            // BtnOpenFolder
            // 
            BtnOpenFolder.FlatAppearance.BorderSize = 0;
            BtnOpenFolder.FlatStyle = FlatStyle.Flat;
            BtnOpenFolder.Image = (Image)resources.GetObject("BtnOpenFolder.Image");
            BtnOpenFolder.Location = new Point(10, 9);
            BtnOpenFolder.Margin = new Padding(3, 2, 3, 2);
            BtnOpenFolder.Name = "BtnOpenFolder";
            BtnOpenFolder.Size = new Size(35, 25);
            BtnOpenFolder.TabIndex = 1;
            BtnOpenFolder.UseVisualStyleBackColor = true;
            BtnOpenFolder.Click += BtnOpenFolder_Click;
            // 
            // BtnPause
            // 
            BtnPause.FlatAppearance.BorderSize = 0;
            BtnPause.FlatStyle = FlatStyle.Flat;
            BtnPause.Image = Properties.Resources.pause;
            BtnPause.Location = new Point(55, 9);
            BtnPause.Margin = new Padding(3, 2, 3, 2);
            BtnPause.Name = "BtnPause";
            BtnPause.Size = new Size(35, 25);
            BtnPause.TabIndex = 2;
            BtnPause.UseVisualStyleBackColor = true;
            BtnPause.Click += BtnPlayPause_Click;
            // 
            // BtnStop
            // 
            BtnStop.FlatAppearance.BorderSize = 0;
            BtnStop.FlatStyle = FlatStyle.Flat;
            BtnStop.Image = Properties.Resources.stop;
            BtnStop.Location = new Point(100, 9);
            BtnStop.Margin = new Padding(3, 2, 3, 2);
            BtnStop.Name = "BtnStop";
            BtnStop.Size = new Size(35, 25);
            BtnStop.TabIndex = 3;
            BtnStop.UseVisualStyleBackColor = true;
            BtnStop.Click += BtnStop_Click;
            // 
            // TrackBarVideo
            // 
            TrackBarVideo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TrackBarVideo.AutoSize = false;
            TrackBarVideo.BackColor = Color.Purple;
            TrackBarVideo.Enabled = false;
            TrackBarVideo.LargeChange = 2;
            TrackBarVideo.Location = new Point(291, 3);
            TrackBarVideo.Maximum = 100;
            TrackBarVideo.Name = "TrackBarVideo";
            TrackBarVideo.Size = new Size(555, 27);
            TrackBarVideo.TabIndex = 5;
            TrackBarVideo.TickFrequency = 20;
            TrackBarVideo.TickStyle = TickStyle.Both;
            // 
            // BtnScreenShot
            // 
            BtnScreenShot.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnScreenShot.FlatAppearance.BorderSize = 0;
            BtnScreenShot.FlatStyle = FlatStyle.Flat;
            BtnScreenShot.Image = Properties.Resources.screenshot;
            BtnScreenShot.Location = new Point(858, 7);
            BtnScreenShot.Margin = new Padding(3, 2, 3, 2);
            BtnScreenShot.Name = "BtnScreenShot";
            BtnScreenShot.Size = new Size(35, 25);
            BtnScreenShot.TabIndex = 6;
            BtnScreenShot.UseVisualStyleBackColor = true;
            BtnScreenShot.Click += BtnScreenShot_Click;
            // 
            // LblSpeedLimit
            // 
            LblSpeedLimit.AutoSize = true;
            LblSpeedLimit.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            LblSpeedLimit.Location = new Point(210, 9);
            LblSpeedLimit.MinimumSize = new Size(42, 21);
            LblSpeedLimit.Name = "LblSpeedLimit";
            LblSpeedLimit.Size = new Size(42, 25);
            LblSpeedLimit.TabIndex = 7;
            LblSpeedLimit.TextAlign = ContentAlignment.TopCenter;
            // 
            // ChbAI
            // 
            ChbAI.AutoSize = true;
            ChbAI.ForeColor = SystemColors.Control;
            ChbAI.Location = new Point(210, 13);
            ChbAI.Name = "ChbAI";
            ChbAI.Size = new Size(71, 19);
            ChbAI.TabIndex = 8;
            ChbAI.Text = "AI Mode";
            ChbAI.UseVisualStyleBackColor = true;
            ChbAI.CheckedChanged += ChbAI_CheckedChanged;
            // 
            // LblTimeCounter
            // 
            LblTimeCounter.AutoSize = true;
            LblTimeCounter.FlatStyle = FlatStyle.Flat;
            LblTimeCounter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LblTimeCounter.ForeColor = Color.WhiteSmoke;
            LblTimeCounter.Location = new Point(145, 14);
            LblTimeCounter.Name = "LblTimeCounter";
            LblTimeCounter.Size = new Size(55, 15);
            LblTimeCounter.TabIndex = 9;
            LblTimeCounter.Text = "00:00:00";
            // 
            // VideoPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Purple;
            ClientSize = new Size(895, 545);
            Controls.Add(LblTimeCounter);
            Controls.Add(ChbAI);
            Controls.Add(LblSpeedLimit);
            Controls.Add(BtnScreenShot);
            Controls.Add(TrackBarVideo);
            Controls.Add(BtnStop);
            Controls.Add(BtnPause);
            Controls.Add(BtnOpenFolder);
            Controls.Add(PbVideo);
            ForeColor = Color.Red;
            Margin = new Padding(3, 2, 3, 2);
            Name = "VideoPage";
            Text = "Video";
            FormClosing += VideoPage_FormClosing;
            Load += VideoPage_Load;
            ((System.ComponentModel.ISupportInitialize)PbVideo).EndInit();
            ((System.ComponentModel.ISupportInitialize)TrackBarVideo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox PbVideo;
        private Button BtnOpenFolder;
        private Button BtnPause;
        private Button BtnStop;
        private TrackBar TrackBarVideo;
        private Button BtnScreenShot;
        private Label LblSpeedLimit;
        private CheckBox ChbAI;
        private Label LblTimeCounter;
    }
}