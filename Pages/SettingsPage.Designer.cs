namespace ObjectsRecognition.Pages
{
    partial class SettingsPage
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
            lblModels = new Label();
            cmbModels = new ComboBox();
            cmbConfidence = new ComboBox();
            lblConfidence = new Label();
            cmbFrameDelays = new ComboBox();
            lblDelay = new Label();
            cmbColors = new ComboBox();
            lblColor = new Label();
            cmbFrequency = new ComboBox();
            lblFrequency = new Label();
            gbAvtomation = new GroupBox();
            cmbFilesLimit = new ComboBox();
            lblFilesLimit = new Label();
            cbAvtoScreenshotsEnabled = new CheckBox();
            lblAvoScreenshotsEnabled = new Label();
            btnReset = new Button();
            gbAvtomation.SuspendLayout();
            SuspendLayout();
            // 
            // lblModels
            // 
            lblModels.AutoSize = true;
            lblModels.Location = new Point(49, 28);
            lblModels.Name = "lblModels";
            lblModels.Size = new Size(174, 20);
            lblModels.TabIndex = 0;
            lblModels.Text = "TensorFlow Model In Use";
            // 
            // cmbModels
            // 
            cmbModels.FlatStyle = FlatStyle.Flat;
            cmbModels.FormattingEnabled = true;
            cmbModels.Location = new Point(577, 20);
            cmbModels.Name = "cmbModels";
            cmbModels.Size = new Size(151, 28);
            cmbModels.TabIndex = 1;
            cmbModels.SelectedIndexChanged += cmbModels_SelectedIndexChanged;
            // 
            // cmbConfidence
            // 
            cmbConfidence.FlatStyle = FlatStyle.Flat;
            cmbConfidence.FormattingEnabled = true;
            cmbConfidence.Location = new Point(577, 66);
            cmbConfidence.Name = "cmbConfidence";
            cmbConfidence.Size = new Size(151, 28);
            cmbConfidence.TabIndex = 3;
            cmbConfidence.SelectedIndexChanged += cmbConfidence_SelectedIndexChanged;
            // 
            // lblConfidence
            // 
            lblConfidence.AutoSize = true;
            lblConfidence.Location = new Point(49, 74);
            lblConfidence.Name = "lblConfidence";
            lblConfidence.Size = new Size(289, 20);
            lblConfidence.TabIndex = 2;
            lblConfidence.Text = "Minimal Confidence While Recognizing, %";
            // 
            // cmbFrameDelays
            // 
            cmbFrameDelays.FlatStyle = FlatStyle.Flat;
            cmbFrameDelays.FormattingEnabled = true;
            cmbFrameDelays.Location = new Point(577, 112);
            cmbFrameDelays.Name = "cmbFrameDelays";
            cmbFrameDelays.Size = new Size(151, 28);
            cmbFrameDelays.TabIndex = 5;
            cmbFrameDelays.SelectedIndexChanged += cmbFrameDelays_SelectedIndexChanged;
            // 
            // lblDelay
            // 
            lblDelay.AutoSize = true;
            lblDelay.Location = new Point(49, 120);
            lblDelay.Name = "lblDelay";
            lblDelay.Size = new Size(118, 20);
            lblDelay.TabIndex = 4;
            lblDelay.Text = "Frame Delay, ms";
            // 
            // cmbColors
            // 
            cmbColors.FlatStyle = FlatStyle.Flat;
            cmbColors.FormattingEnabled = true;
            cmbColors.Location = new Point(577, 158);
            cmbColors.Name = "cmbColors";
            cmbColors.Size = new Size(151, 28);
            cmbColors.TabIndex = 7;
            cmbColors.SelectedIndexChanged += cmbColors_SelectedIndexChanged;
            // 
            // lblColor
            // 
            lblColor.AutoSize = true;
            lblColor.Location = new Point(49, 166);
            lblColor.Name = "lblColor";
            lblColor.Size = new Size(84, 20);
            lblColor.TabIndex = 6;
            lblColor.Text = "Draw Color";
            // 
            // cmbFrequency
            // 
            cmbFrequency.FlatStyle = FlatStyle.Flat;
            cmbFrequency.FormattingEnabled = true;
            cmbFrequency.Location = new Point(577, 204);
            cmbFrequency.Name = "cmbFrequency";
            cmbFrequency.Size = new Size(151, 28);
            cmbFrequency.TabIndex = 9;
            cmbFrequency.SelectedIndexChanged += cmbFrequency_SelectedIndexChanged;
            // 
            // lblFrequency
            // 
            lblFrequency.AutoSize = true;
            lblFrequency.Location = new Point(49, 212);
            lblFrequency.Name = "lblFrequency";
            lblFrequency.Size = new Size(160, 20);
            lblFrequency.TabIndex = 8;
            lblFrequency.Text = "Recognition Frequency";
            // 
            // gbAvtomation
            // 
            gbAvtomation.Controls.Add(cmbFilesLimit);
            gbAvtomation.Controls.Add(lblFilesLimit);
            gbAvtomation.Controls.Add(cbAvtoScreenshotsEnabled);
            gbAvtomation.Controls.Add(lblAvoScreenshotsEnabled);
            gbAvtomation.Location = new Point(40, 252);
            gbAvtomation.Name = "gbAvtomation";
            gbAvtomation.Size = new Size(697, 143);
            gbAvtomation.TabIndex = 10;
            gbAvtomation.TabStop = false;
            gbAvtomation.Text = "Avto Screenshots Setup";
            // 
            // cmbFilesLimit
            // 
            cmbFilesLimit.FlatStyle = FlatStyle.Flat;
            cmbFilesLimit.FormattingEnabled = true;
            cmbFilesLimit.Location = new Point(535, 86);
            cmbFilesLimit.Name = "cmbFilesLimit";
            cmbFilesLimit.Size = new Size(151, 28);
            cmbFilesLimit.TabIndex = 3;
            cmbFilesLimit.SelectedIndexChanged += cmbFilesLimit_SelectedIndexChanged;
            // 
            // lblFilesLimit
            // 
            lblFilesLimit.AutoSize = true;
            lblFilesLimit.Location = new Point(116, 94);
            lblFilesLimit.Name = "lblFilesLimit";
            lblFilesLimit.Size = new Size(81, 20);
            lblFilesLimit.TabIndex = 2;
            lblFilesLimit.Text = "Up To Files";
            // 
            // cbAvtoScreenshotsEnabled
            // 
            cbAvtoScreenshotsEnabled.AutoSize = true;
            cbAvtoScreenshotsEnabled.FlatStyle = FlatStyle.Flat;
            cbAvtoScreenshotsEnabled.Location = new Point(536, 40);
            cbAvtoScreenshotsEnabled.Name = "cbAvtoScreenshotsEnabled";
            cbAvtoScreenshotsEnabled.Size = new Size(14, 13);
            cbAvtoScreenshotsEnabled.TabIndex = 1;
            cbAvtoScreenshotsEnabled.UseVisualStyleBackColor = true;
            cbAvtoScreenshotsEnabled.CheckedChanged += cbAvtoScreenshotsEnabled_CheckedChanged;
            // 
            // lblAvoScreenshotsEnabled
            // 
            lblAvoScreenshotsEnabled.AutoSize = true;
            lblAvoScreenshotsEnabled.Location = new Point(116, 36);
            lblAvoScreenshotsEnabled.Name = "lblAvoScreenshotsEnabled";
            lblAvoScreenshotsEnabled.Size = new Size(180, 20);
            lblAvoScreenshotsEnabled.TabIndex = 0;
            lblAvoScreenshotsEnabled.Text = "Avto Screenshots Enabled";
            // 
            // btnReset
            // 
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Location = new Point(577, 421);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(151, 29);
            btnReset.TabIndex = 11;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            // 
            // SettingsPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1001, 462);
            Controls.Add(btnReset);
            Controls.Add(gbAvtomation);
            Controls.Add(cmbFrequency);
            Controls.Add(lblFrequency);
            Controls.Add(cmbColors);
            Controls.Add(lblColor);
            Controls.Add(cmbFrameDelays);
            Controls.Add(lblDelay);
            Controls.Add(cmbConfidence);
            Controls.Add(lblConfidence);
            Controls.Add(cmbModels);
            Controls.Add(lblModels);
            Name = "SettingsPage";
            Text = "Settings";
            Load += SettingsPage_Load;
            gbAvtomation.ResumeLayout(false);
            gbAvtomation.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblModels;
        private ComboBox cmbModels;
        private ComboBox cmbConfidence;
        private Label lblConfidence;
        private ComboBox cmbFrameDelays;
        private Label lblDelay;
        private ComboBox cmbColors;
        private Label lblColor;
        private ComboBox cmbFrequency;
        private Label lblFrequency;
        private GroupBox gbAvtomation;
        private ComboBox cmbFilesLimit;
        private Label lblFilesLimit;
        private CheckBox cbAvtoScreenshotsEnabled;
        private Label lblAvoScreenshotsEnabled;
        private Button btnReset;
    }
}