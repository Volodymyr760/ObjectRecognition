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
            PbVideo = new PictureBox();
            CmbCameras = new ComboBox();
            BtnRecord = new Button();
            BtnScreenShot = new Button();
            LblTimeCounter = new Label();
            CmbMode = new ComboBox();
            CmbLoop = new ComboBox();
            LblLoop = new Label();
            ((System.ComponentModel.ISupportInitialize)PbVideo).BeginInit();
            SuspendLayout();
            // 
            // PbVideo
            // 
            PbVideo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PbVideo.BackColor = SystemColors.Control;
            PbVideo.Location = new Point(0, 43);
            PbVideo.Margin = new Padding(0);
            PbVideo.Name = "PbVideo";
            PbVideo.Size = new Size(896, 504);
            PbVideo.SizeMode = PictureBoxSizeMode.StretchImage;
            PbVideo.TabIndex = 0;
            PbVideo.TabStop = false;
            // 
            // CmbCameras
            // 
            CmbCameras.FormattingEnabled = true;
            CmbCameras.Location = new Point(10, 9);
            CmbCameras.Margin = new Padding(3, 2, 3, 2);
            CmbCameras.Name = "CmbCameras";
            CmbCameras.Size = new Size(246, 23);
            CmbCameras.TabIndex = 1;
            CmbCameras.SelectedIndexChanged += CmbCameras_SelectedIndexChanged;
            // 
            // BtnRecord
            // 
            BtnRecord.FlatAppearance.BorderSize = 0;
            BtnRecord.FlatStyle = FlatStyle.Flat;
            BtnRecord.Image = Properties.Resources.movie_24;
            BtnRecord.Location = new Point(484, 7);
            BtnRecord.Margin = new Padding(3, 2, 3, 2);
            BtnRecord.Name = "BtnRecord";
            BtnRecord.Size = new Size(35, 25);
            BtnRecord.TabIndex = 2;
            BtnRecord.UseVisualStyleBackColor = true;
            BtnRecord.Click += BtnRecord_Click;
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
            BtnScreenShot.TabIndex = 5;
            BtnScreenShot.UseVisualStyleBackColor = true;
            BtnScreenShot.Click += BtnScreenShot_Click;
            // 
            // LblTimeCounter
            // 
            LblTimeCounter.AutoSize = true;
            LblTimeCounter.FlatStyle = FlatStyle.Flat;
            LblTimeCounter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LblTimeCounter.ForeColor = Color.WhiteSmoke;
            LblTimeCounter.Location = new Point(529, 11);
            LblTimeCounter.Margin = new Padding(3, 2, 3, 0);
            LblTimeCounter.Name = "LblTimeCounter";
            LblTimeCounter.Size = new Size(38, 15);
            LblTimeCounter.TabIndex = 6;
            LblTimeCounter.Text = "00:00";
            // 
            // CmbMode
            // 
            CmbMode.FormattingEnabled = true;
            CmbMode.Location = new Point(266, 9);
            CmbMode.Margin = new Padding(3, 2, 3, 2);
            CmbMode.Name = "CmbMode";
            CmbMode.Size = new Size(80, 23);
            CmbMode.TabIndex = 7;
            CmbMode.SelectedIndexChanged += CmbMode_SelectedIndexChanged;
            // 
            // CmbLoop
            // 
            CmbLoop.FormattingEnabled = true;
            CmbLoop.Location = new Point(427, 9);
            CmbLoop.Margin = new Padding(3, 2, 3, 2);
            CmbLoop.Name = "CmbLoop";
            CmbLoop.Size = new Size(47, 23);
            CmbLoop.TabIndex = 8;
            CmbLoop.SelectedIndexChanged += CmbLoop_SelectedIndexChanged;
            // 
            // LblLoop
            // 
            LblLoop.AutoSize = true;
            LblLoop.FlatStyle = FlatStyle.Flat;
            LblLoop.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LblLoop.ForeColor = Color.WhiteSmoke;
            LblLoop.Location = new Point(356, 11);
            LblLoop.Name = "LblLoop";
            LblLoop.Size = new Size(61, 15);
            LblLoop.TabIndex = 9;
            LblLoop.Text = "Loop, min";
            LblLoop.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CameraPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Purple;
            ClientSize = new Size(895, 545);
            Controls.Add(LblLoop);
            Controls.Add(CmbLoop);
            Controls.Add(CmbMode);
            Controls.Add(LblTimeCounter);
            Controls.Add(BtnScreenShot);
            Controls.Add(BtnRecord);
            Controls.Add(CmbCameras);
            Controls.Add(PbVideo);
            Margin = new Padding(3, 2, 3, 2);
            Name = "CameraPage";
            Text = "Camera";
            FormClosing += CameraPage_FormClosing;
            Load += CameraPage_Load;
            ((System.ComponentModel.ISupportInitialize)PbVideo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox PbVideo;
        private ComboBox CmbCameras;
        private Button BtnRecord;
        private Button BtnScreenShot;
        private Label LblTimeCounter;
        private ComboBox CmbMode;
        private ComboBox CmbLoop;
        private Label LblLoop;
    }
}