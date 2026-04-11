namespace ObjectsRecognition.Pages
{
    partial class FilesPage
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
            btnChooseFiles = new Button();
            RtxtResults = new RichTextBox();
            pBar = new ProgressBar();
            SuspendLayout();
            // 
            // btnChooseFiles
            // 
            btnChooseFiles.FlatStyle = FlatStyle.Flat;
            btnChooseFiles.Location = new Point(12, 19);
            btnChooseFiles.Name = "btnChooseFiles";
            btnChooseFiles.Size = new Size(152, 43);
            btnChooseFiles.TabIndex = 0;
            btnChooseFiles.Text = "Choose Files";
            btnChooseFiles.UseVisualStyleBackColor = true;
            btnChooseFiles.Click += btnChooseFiles_Click;
            // 
            // RtxtResults
            // 
            RtxtResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RtxtResults.Location = new Point(12, 82);
            RtxtResults.Name = "RtxtResults";
            RtxtResults.Size = new Size(1157, 433);
            RtxtResults.TabIndex = 1;
            RtxtResults.Text = "";
            // 
            // pBar
            // 
            pBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pBar.Location = new Point(170, 19);
            pBar.Name = "pBar";
            pBar.Size = new Size(999, 43);
            pBar.TabIndex = 2;
            // 
            // FilesPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1181, 527);
            Controls.Add(pBar);
            Controls.Add(RtxtResults);
            Controls.Add(btnChooseFiles);
            Name = "FilesPage";
            Text = "Files";
            Load += FilesPage_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnChooseFiles;
        private RichTextBox RtxtResults;
        private ProgressBar pBar;
    }
}