using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ObjectsRecognition
{
    public partial class MainForm : Form
    {
        bool showHideLeftMenu = true;
        //private Form? activePage;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLeftMenu_Click(object sender, EventArgs e)
        {
            panelMenu.Width = showHideLeftMenu ? 250 : 70;
            if (showHideLeftMenu)
            {
                btnFiles.Text = "   Files";
                btnPlayVideo.Text = "   Play Video";
                btnCamera.Text = "   Camera";
                btnSettings.Text = "   Settings";
            }
            else
            {
                btnFiles.Text = btnPlayVideo.Text = btnCamera.Text
                    = btnSettings.Text = string.Empty;
            }

            btnLeftMenu.Width = btnFiles.Width = btnPlayVideo.Width
                = btnCamera.Width = btnSettings.Width = panelMenu.Width;
            showHideLeftMenu = !showHideLeftMenu;
        }

        #region LeftMenu

        private void btnFiles_Click(object sender, EventArgs e) =>
            OpenChildForm(new Pages.FilesPage(), sender);

        private void btnPlayVideo_Click(object sender, EventArgs e) =>
            OpenChildForm(new Pages.VideoPage(), sender);

        private void btnCamera_Click(object sender, EventArgs e) =>
            OpenChildForm(new Pages.CameraPage(), sender);

        private void btnSettings_Click(object sender, EventArgs e) =>
            OpenChildForm(new Pages.SettingsPage(), sender);

        #endregion

        private void OpenChildForm(Form childForm, object btnSender)
        {
            activePage = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }
    }
}
