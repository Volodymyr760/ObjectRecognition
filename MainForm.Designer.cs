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
            btnSettings = new Button();
            btnCamera = new Button();
            btnVideo = new Button();
            btnFiles = new Button();
            panelDesktop = new Panel();
            panelMenu.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.Purple;
            panelMenu.Controls.Add(btnSettings);
            panelMenu.Controls.Add(btnCamera);
            panelMenu.Controls.Add(btnVideo);
            panelMenu.Controls.Add(btnFiles);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(70, 669);
            panelMenu.TabIndex = 0;
            // 
            // btnSettings
            // 
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSettings.ForeColor = Color.WhiteSmoke;
            btnSettings.Image = (Image)resources.GetObject("btnSettings.Image");
            btnSettings.ImageAlign = ContentAlignment.TopCenter;
            btnSettings.Location = new Point(0, 198);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(70, 60);
            btnSettings.TabIndex = 6;
            btnSettings.Text = "Settings";
            btnSettings.TextAlign = ContentAlignment.BottomCenter;
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnCamera
            // 
            btnCamera.FlatAppearance.BorderSize = 0;
            btnCamera.FlatStyle = FlatStyle.Flat;
            btnCamera.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnCamera.ForeColor = Color.WhiteSmoke;
            btnCamera.Image = (Image)resources.GetObject("btnCamera.Image");
            btnCamera.ImageAlign = ContentAlignment.TopCenter;
            btnCamera.Location = new Point(0, 0);
            btnCamera.Name = "btnCamera";
            btnCamera.Padding = new Padding(0, 5, 0, 0);
            btnCamera.Size = new Size(70, 60);
            btnCamera.TabIndex = 5;
            btnCamera.Text = "Camera";
            btnCamera.TextAlign = ContentAlignment.BottomCenter;
            btnCamera.UseVisualStyleBackColor = true;
            btnCamera.Click += btnCamera_Click;
            // 
            // btnVideo
            // 
            btnVideo.FlatAppearance.BorderSize = 0;
            btnVideo.FlatStyle = FlatStyle.Flat;
            btnVideo.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnVideo.ForeColor = Color.WhiteSmoke;
            btnVideo.Image = (Image)resources.GetObject("btnVideo.Image");
            btnVideo.ImageAlign = ContentAlignment.TopCenter;
            btnVideo.Location = new Point(0, 66);
            btnVideo.Name = "btnVideo";
            btnVideo.Size = new Size(70, 60);
            btnVideo.TabIndex = 3;
            btnVideo.Text = "Video";
            btnVideo.TextAlign = ContentAlignment.BottomCenter;
            btnVideo.UseVisualStyleBackColor = true;
            btnVideo.Click += btnPlayVideo_Click;
            // 
            // btnFiles
            // 
            btnFiles.BackColor = Color.Purple;
            btnFiles.FlatAppearance.BorderSize = 0;
            btnFiles.FlatStyle = FlatStyle.Flat;
            btnFiles.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnFiles.ForeColor = Color.WhiteSmoke;
            btnFiles.Image = Properties.Resources.folder_open;
            btnFiles.ImageAlign = ContentAlignment.TopCenter;
            btnFiles.Location = new Point(0, 132);
            btnFiles.Name = "btnFiles";
            btnFiles.Size = new Size(70, 60);
            btnFiles.TabIndex = 2;
            btnFiles.Text = "Files";
            btnFiles.TextAlign = ContentAlignment.BottomCenter;
            btnFiles.UseVisualStyleBackColor = false;
            btnFiles.Click += btnFiles_Click;
            // 
            // panelDesktop
            // 
            panelDesktop.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelDesktop.Location = new Point(70, 0);
            panelDesktop.Name = "panelDesktop";
            panelDesktop.Size = new Size(1024, 673);
            panelDesktop.TabIndex = 3;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1096, 669);
            Controls.Add(panelDesktop);
            Controls.Add(panelMenu);
            Name = "MainForm";
            Text = "Objects Recognition";
            panelMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMenu;
        private Button btnSettings;
        private Button btnCamera;
        private Button btnVideo;
        private Panel panelDesktop;
        private Button btnFiles;
    }
}
