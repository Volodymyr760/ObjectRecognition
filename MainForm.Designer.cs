namespace ObjectsRecognition
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            panelMenu = new Panel();
            BtnSettings = new Button();
            BtnCamera = new Button();
            BtnVideo = new Button();
            BtnFiles = new Button();
            panelDesktop = new Panel();
            panelMenu.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.Purple;
            panelMenu.Controls.Add(BtnSettings);
            panelMenu.Controls.Add(BtnCamera);
            panelMenu.Controls.Add(BtnVideo);
            panelMenu.Controls.Add(BtnFiles);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(61, 546);
            panelMenu.TabIndex = 0;
            // 
            // BtnSettings
            // 
            BtnSettings.FlatAppearance.BorderSize = 0;
            BtnSettings.FlatStyle = FlatStyle.Flat;
            BtnSettings.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            BtnSettings.ForeColor = Color.WhiteSmoke;
            BtnSettings.Image = (Image)resources.GetObject("BtnSettings.Image");
            BtnSettings.Location = new Point(0, 135);
            BtnSettings.Name = "BtnSettings";
            BtnSettings.Size = new Size(61, 45);
            BtnSettings.TabIndex = 4;
            BtnSettings.TextAlign = ContentAlignment.BottomCenter;
            BtnSettings.TextImageRelation = TextImageRelation.ImageAboveText;
            BtnSettings.UseVisualStyleBackColor = true;
            BtnSettings.Click += btnSettings_Click;
            // 
            // BtnCamera
            // 
            BtnCamera.FlatAppearance.BorderSize = 0;
            BtnCamera.FlatStyle = FlatStyle.Flat;
            BtnCamera.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            BtnCamera.ForeColor = Color.WhiteSmoke;
            BtnCamera.Image = (Image)resources.GetObject("BtnCamera.Image");
            BtnCamera.Location = new Point(0, 0);
            BtnCamera.Name = "BtnCamera";
            BtnCamera.Size = new Size(61, 45);
            BtnCamera.TabIndex = 1;
            BtnCamera.TextAlign = ContentAlignment.BottomCenter;
            BtnCamera.TextImageRelation = TextImageRelation.ImageAboveText;
            BtnCamera.UseVisualStyleBackColor = true;
            BtnCamera.Click += btnCamera_Click;
            // 
            // BtnVideo
            // 
            BtnVideo.FlatAppearance.BorderSize = 0;
            BtnVideo.FlatStyle = FlatStyle.Flat;
            BtnVideo.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            BtnVideo.ForeColor = Color.WhiteSmoke;
            BtnVideo.Image = (Image)resources.GetObject("BtnVideo.Image");
            BtnVideo.Location = new Point(0, 45);
            BtnVideo.Name = "BtnVideo";
            BtnVideo.Size = new Size(61, 45);
            BtnVideo.TabIndex = 2;
            BtnVideo.TextAlign = ContentAlignment.MiddleRight;
            BtnVideo.TextImageRelation = TextImageRelation.ImageAboveText;
            BtnVideo.UseVisualStyleBackColor = true;
            BtnVideo.Click += btnPlayVideo_Click;
            // 
            // BtnFiles
            // 
            BtnFiles.BackColor = Color.Purple;
            BtnFiles.FlatAppearance.BorderSize = 0;
            BtnFiles.FlatStyle = FlatStyle.Flat;
            BtnFiles.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            BtnFiles.ForeColor = Color.WhiteSmoke;
            BtnFiles.Image = Properties.Resources.folder_open;
            BtnFiles.Location = new Point(0, 90);
            BtnFiles.Name = "BtnFiles";
            BtnFiles.Size = new Size(61, 45);
            BtnFiles.TabIndex = 3;
            BtnFiles.TextAlign = ContentAlignment.BottomCenter;
            BtnFiles.TextImageRelation = TextImageRelation.ImageAboveText;
            BtnFiles.UseVisualStyleBackColor = false;
            BtnFiles.Click += btnFiles_Click;
            // 
            // panelDesktop
            // 
            panelDesktop.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelDesktop.Location = new Point(62, 0);
            panelDesktop.Margin = new Padding(0);
            panelDesktop.Name = "panelDesktop";
            panelDesktop.Size = new Size(896, 547);
            panelDesktop.TabIndex = 3;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(956, 546);
            Controls.Add(panelDesktop);
            Controls.Add(panelMenu);
            IsMdiContainer = true;
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainForm";
            Text = "Objects Recognition";
            Load += MainForm_Load;
            panelMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMenu;
        private Button BtnSettings;
        private Button BtnCamera;
        private Button BtnVideo;
        private Panel panelDesktop;
        private Button BtnFiles;
    }
}
