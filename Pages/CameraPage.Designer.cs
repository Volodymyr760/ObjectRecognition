namespace ObjectsRecognition.Pages
{
    partial class CameraPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraPage));
            pbVideo = new PictureBox();
            cmbCameras = new ComboBox();
            btnPlay = new Button();
            btnPause = new Button();
            btnStop = new Button();
            btnScreenShot = new Button();
            ((System.ComponentModel.ISupportInitialize)pbVideo).BeginInit();
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
            // cmbCameras
            // 
            cmbCameras.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmbCameras.FormattingEnabled = true;
            cmbCameras.Location = new Point(10, 422);
            cmbCameras.Name = "cmbCameras";
            cmbCameras.Size = new Size(280, 28);
            cmbCameras.TabIndex = 1;
            cmbCameras.SelectedIndexChanged += cmbCameras_SelectedIndexChanged;
            // 
            // btnPlay
            // 
            btnPlay.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnPlay.FlatAppearance.BorderSize = 0;
            btnPlay.FlatStyle = FlatStyle.Flat;
            btnPlay.Image = Properties.Resources.play_circle_outline;
            btnPlay.Location = new Point(300, 420);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(40, 33);
            btnPlay.TabIndex = 2;
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += btnPlay_Click;
            // 
            // btnPause
            // 
            btnPause.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnPause.FlatAppearance.BorderSize = 0;
            btnPause.FlatStyle = FlatStyle.Flat;
            btnPause.Image = Properties.Resources.pause;
            btnPause.Location = new Point(350, 420);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(39, 33);
            btnPause.TabIndex = 3;
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += btnPause_Click;
            // 
            // btnStop
            // 
            btnStop.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnStop.FlatAppearance.BorderSize = 0;
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.Image = Properties.Resources.stop;
            btnStop.Location = new Point(400, 420);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(39, 33);
            btnStop.TabIndex = 4;
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnScreenShot
            // 
            btnScreenShot.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnScreenShot.FlatAppearance.BorderSize = 0;
            btnScreenShot.FlatStyle = FlatStyle.Flat;
            btnScreenShot.Image = (Image)resources.GetObject("btnScreenShot.Image");
            btnScreenShot.Location = new Point(950, 420);
            btnScreenShot.Name = "btnScreenShot";
            btnScreenShot.Size = new Size(40, 33);
            btnScreenShot.TabIndex = 5;
            btnScreenShot.UseVisualStyleBackColor = true;
            btnScreenShot.Click += btnScreenShot_Click;
            // 
            // CameraPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1001, 462);
            Controls.Add(btnScreenShot);
            Controls.Add(btnStop);
            Controls.Add(btnPause);
            Controls.Add(btnPlay);
            Controls.Add(cmbCameras);
            Controls.Add(pbVideo);
            Name = "CameraPage";
            Text = "Camera";
            Load += CameraPage_Load;
            ((System.ComponentModel.ISupportInitialize)pbVideo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbVideo;
        private ComboBox cmbCameras;
        private Button btnPlay;
        private Button btnPause;
        private Button btnStop;
        private Button btnScreenShot;
    }
}