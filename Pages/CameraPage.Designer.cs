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
            btnRecord = new Button();
            btnScreenShot = new Button();
            lblTimeCounter = new Label();
            CmbMode = new ComboBox();
            CmbLoop = new ComboBox();
            LblLoop = new Label();
            ((System.ComponentModel.ISupportInitialize)pbVideo).BeginInit();
            SuspendLayout();
            // 
            // pbVideo
            // 
            pbVideo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pbVideo.Location = new Point(1, 54);
            pbVideo.Name = "pbVideo";
            pbVideo.Size = new Size(1024, 576);
            pbVideo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbVideo.TabIndex = 0;
            pbVideo.TabStop = false;
            // 
            // cmbCameras
            // 
            cmbCameras.FormattingEnabled = true;
            cmbCameras.Location = new Point(12, 12);
            cmbCameras.Name = "cmbCameras";
            cmbCameras.Size = new Size(280, 28);
            cmbCameras.TabIndex = 1;
            cmbCameras.SelectedIndexChanged += cmbCameras_SelectedIndexChanged;
            // 
            // btnRecord
            // 
            btnRecord.FlatAppearance.BorderSize = 0;
            btnRecord.FlatStyle = FlatStyle.Flat;
            btnRecord.Image = Properties.Resources.movie_24;
            btnRecord.Location = new Point(555, 9);
            btnRecord.Name = "btnRecord";
            btnRecord.Size = new Size(40, 33);
            btnRecord.TabIndex = 2;
            btnRecord.UseVisualStyleBackColor = true;
            btnRecord.Click += btnRecord_Click;
            // 
            // btnScreenShot
            // 
            btnScreenShot.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnScreenShot.FlatAppearance.BorderSize = 0;
            btnScreenShot.FlatStyle = FlatStyle.Flat;
            btnScreenShot.Image = (Image)resources.GetObject("btnScreenShot.Image");
            btnScreenShot.Location = new Point(984, 9);
            btnScreenShot.Name = "btnScreenShot";
            btnScreenShot.Size = new Size(40, 33);
            btnScreenShot.TabIndex = 5;
            btnScreenShot.UseVisualStyleBackColor = true;
            btnScreenShot.Click += btnScreenShot_Click;
            // 
            // lblTimeCounter
            // 
            lblTimeCounter.AutoSize = true;
            lblTimeCounter.FlatStyle = FlatStyle.Flat;
            lblTimeCounter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTimeCounter.ForeColor = Color.WhiteSmoke;
            lblTimeCounter.Location = new Point(615, 15);
            lblTimeCounter.Name = "lblTimeCounter";
            lblTimeCounter.Size = new Size(49, 20);
            lblTimeCounter.TabIndex = 6;
            lblTimeCounter.Text = "00:00";
            // 
            // CmbMode
            // 
            CmbMode.FormattingEnabled = true;
            CmbMode.Location = new Point(305, 12);
            CmbMode.Name = "CmbMode";
            CmbMode.Size = new Size(91, 28);
            CmbMode.TabIndex = 7;
            CmbMode.SelectedIndexChanged += CmbMode_SelectedIndexChanged;
            // 
            // CmbLoop
            // 
            CmbLoop.FormattingEnabled = true;
            CmbLoop.Location = new Point(487, 12);
            CmbLoop.Name = "CmbLoop";
            CmbLoop.Size = new Size(53, 28);
            CmbLoop.TabIndex = 8;
            CmbLoop.SelectedIndexChanged += CmbLoop_SelectedIndexChanged;
            // 
            // LblLoop
            // 
            LblLoop.AutoSize = true;
            LblLoop.FlatStyle = FlatStyle.Flat;
            LblLoop.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LblLoop.ForeColor = Color.WhiteSmoke;
            LblLoop.Location = new Point(402, 15);
            LblLoop.Name = "LblLoop";
            LblLoop.Size = new Size(79, 20);
            LblLoop.TabIndex = 9;
            LblLoop.Text = "Loop, min";
            // 
            // CameraPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Purple;
            ClientSize = new Size(1026, 626);
            Controls.Add(LblLoop);
            Controls.Add(CmbLoop);
            Controls.Add(CmbMode);
            Controls.Add(lblTimeCounter);
            Controls.Add(btnScreenShot);
            Controls.Add(btnRecord);
            Controls.Add(cmbCameras);
            Controls.Add(pbVideo);
            Name = "CameraPage";
            Text = "Camera";
            FormClosing += CameraPage_FormClosing;
            Load += CameraPage_Load;
            ((System.ComponentModel.ISupportInitialize)pbVideo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbVideo;
        private ComboBox cmbCameras;
        private Button btnRecord;
        private Button btnScreenShot;
        private Label lblTimeCounter;
        private ComboBox CmbMode;
        private ComboBox CmbLoop;
        private Label LblLoop;
    }
}