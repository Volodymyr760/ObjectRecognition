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
            btnPlayVideo = new Button();
            btnFiles = new Button();
            btnLeftMenu = new Button();
            panelTitle = new Panel();
            lblTitle = new Label();
            panelDesktop = new Panel();
            panelMenu.SuspendLayout();
            panelTitle.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.Purple;
            panelMenu.Controls.Add(btnSettings);
            panelMenu.Controls.Add(btnCamera);
            panelMenu.Controls.Add(btnPlayVideo);
            panelMenu.Controls.Add(btnFiles);
            panelMenu.Controls.Add(btnLeftMenu);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(250, 581);
            panelMenu.TabIndex = 0;
            // 
            // btnSettings
            // 
            btnSettings.Dock = DockStyle.Top;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnSettings.ForeColor = Color.WhiteSmoke;
            btnSettings.Image = (Image)resources.GetObject("btnSettings.Image");
            btnSettings.ImageAlign = ContentAlignment.MiddleLeft;
            btnSettings.Location = new Point(0, 226);
            btnSettings.Name = "btnSettings";
            btnSettings.Padding = new Padding(10, 0, 0, 0);
            btnSettings.Size = new Size(250, 60);
            btnSettings.TabIndex = 6;
            btnSettings.Text = "   Settings";
            btnSettings.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnCamera
            // 
            btnCamera.Dock = DockStyle.Top;
            btnCamera.FlatAppearance.BorderSize = 0;
            btnCamera.FlatStyle = FlatStyle.Flat;
            btnCamera.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnCamera.ForeColor = Color.WhiteSmoke;
            btnCamera.Image = (Image)resources.GetObject("btnCamera.Image");
            btnCamera.ImageAlign = ContentAlignment.MiddleLeft;
            btnCamera.Location = new Point(0, 166);
            btnCamera.Name = "btnCamera";
            btnCamera.Padding = new Padding(10, 0, 0, 0);
            btnCamera.Size = new Size(250, 60);
            btnCamera.TabIndex = 5;
            btnCamera.Text = "   Camera";
            btnCamera.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCamera.UseVisualStyleBackColor = true;
            btnCamera.Click += btnCamera_Click;
            // 
            // btnPlayVideo
            // 
            btnPlayVideo.Dock = DockStyle.Top;
            btnPlayVideo.FlatAppearance.BorderSize = 0;
            btnPlayVideo.FlatStyle = FlatStyle.Flat;
            btnPlayVideo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnPlayVideo.ForeColor = Color.WhiteSmoke;
            btnPlayVideo.Image = (Image)resources.GetObject("btnPlayVideo.Image");
            btnPlayVideo.ImageAlign = ContentAlignment.MiddleLeft;
            btnPlayVideo.Location = new Point(0, 106);
            btnPlayVideo.Name = "btnPlayVideo";
            btnPlayVideo.Padding = new Padding(10, 0, 0, 0);
            btnPlayVideo.Size = new Size(250, 60);
            btnPlayVideo.TabIndex = 3;
            btnPlayVideo.Text = "   Video";
            btnPlayVideo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnPlayVideo.UseVisualStyleBackColor = true;
            btnPlayVideo.Click += btnPlayVideo_Click;
            // 
            // btnFiles
            // 
            btnFiles.Dock = DockStyle.Top;
            btnFiles.FlatAppearance.BorderSize = 0;
            btnFiles.FlatStyle = FlatStyle.Flat;
            btnFiles.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnFiles.ForeColor = Color.WhiteSmoke;
            btnFiles.Image = Properties.Resources.folder_open;
            btnFiles.ImageAlign = ContentAlignment.MiddleLeft;
            btnFiles.Location = new Point(0, 46);
            btnFiles.Name = "btnFiles";
            btnFiles.Padding = new Padding(10, 0, 0, 0);
            btnFiles.Size = new Size(250, 60);
            btnFiles.TabIndex = 2;
            btnFiles.Text = "   Files";
            btnFiles.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnFiles.UseVisualStyleBackColor = true;
            btnFiles.Click += btnFiles_Click;
            // 
            // btnLeftMenu
            // 
            btnLeftMenu.BackColor = Color.White;
            btnLeftMenu.Dock = DockStyle.Top;
            btnLeftMenu.FlatAppearance.BorderSize = 0;
            btnLeftMenu.FlatStyle = FlatStyle.Flat;
            btnLeftMenu.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnLeftMenu.ForeColor = Color.Gainsboro;
            btnLeftMenu.Image = (Image)resources.GetObject("btnLeftMenu.Image");
            btnLeftMenu.ImageAlign = ContentAlignment.MiddleLeft;
            btnLeftMenu.Location = new Point(0, 0);
            btnLeftMenu.Name = "btnLeftMenu";
            btnLeftMenu.Padding = new Padding(10, 0, 0, 0);
            btnLeftMenu.Size = new Size(250, 46);
            btnLeftMenu.TabIndex = 1;
            btnLeftMenu.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLeftMenu.UseVisualStyleBackColor = false;
            btnLeftMenu.Click += btnLeftMenu_Click;
            // 
            // panelTitle
            // 
            panelTitle.BackColor = Color.DarkGray;
            panelTitle.Controls.Add(lblTitle);
            panelTitle.Dock = DockStyle.Top;
            panelTitle.Location = new Point(250, 0);
            panelTitle.Name = "panelTitle";
            panelTitle.Size = new Size(976, 46);
            panelTitle.TabIndex = 2;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblTitle.ForeColor = Color.WhiteSmoke;
            lblTitle.Location = new Point(23, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(65, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Home";
            // 
            // panelDesktop
            // 
            panelDesktop.Dock = DockStyle.Fill;
            panelDesktop.Location = new Point(250, 46);
            panelDesktop.Name = "panelDesktop";
            panelDesktop.Size = new Size(976, 535);
            panelDesktop.TabIndex = 3;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1226, 581);
            Controls.Add(panelDesktop);
            Controls.Add(panelTitle);
            Controls.Add(panelMenu);
            Name = "MainForm";
            Text = "Objects Recognition";
            panelMenu.ResumeLayout(false);
            panelTitle.ResumeLayout(false);
            panelTitle.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMenu;
        private Panel panelTitle;
        private Button btnLeftMenu;
        private Label lblTitle;
        private Button btnFiles;
        private Button btnSettings;
        private Button btnCamera;
        private Button btnPlayVideo;
        private Panel panelDesktop;
    }
}
