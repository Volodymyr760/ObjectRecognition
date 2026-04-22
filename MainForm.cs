using ObjectsRecognition.Pages;

namespace ObjectsRecognition
{
    public partial class MainForm : Form
    {
        private Form? activePage;

        public MainForm()
        {
            InitializeComponent();
            SetActivePage(btnCamera.Name);
        }

        private void btnFiles_Click(object sender, EventArgs e) =>
            SetActivePage(btnFiles.Name);

        private void btnPlayVideo_Click(object sender, EventArgs e) =>
            SetActivePage(btnVideo.Name);

        private void btnCamera_Click(object sender, EventArgs e) =>
            SetActivePage(btnCamera.Name);

        private void btnSettings_Click(object sender, EventArgs e) =>
            SetActivePage(btnSettings.Name);

        private void SetActivePage(string buttonName)
        {
            Form? formToOpen = null;

            switch (buttonName)
            {
                case "btnFiles":
                    btnFiles.BackColor = Color.Orchid;
                    btnFiles.ForeColor = Color.WhiteSmoke;
                    btnVideo.BackColor = btnCamera.BackColor = btnSettings.BackColor = Color.Purple;
                    btnVideo.ForeColor = btnCamera.ForeColor = btnSettings.ForeColor = Color.Gainsboro;
                    formToOpen = new FilesPage();
                    break;
                case "btnVideo":
                    btnVideo.BackColor = Color.Orchid;
                    btnVideo.ForeColor = Color.WhiteSmoke;
                    btnFiles.BackColor = btnCamera.BackColor = btnSettings.BackColor = Color.Purple;
                    btnFiles.ForeColor = btnCamera.ForeColor = btnSettings.ForeColor = Color.Gainsboro;
                    formToOpen = new VideoPage();
                    break;
                case "btnCamera":
                    btnCamera.BackColor = Color.Orchid;
                    btnCamera.ForeColor = Color.WhiteSmoke;
                    btnVideo.BackColor = btnFiles.BackColor = btnSettings.BackColor = Color.Purple;
                    btnVideo.ForeColor = btnFiles.ForeColor = btnSettings.ForeColor = Color.Gainsboro;
                    formToOpen = new CameraPage();
                    break;
                default:
                    btnSettings.BackColor = Color.Orchid;
                    btnSettings.ForeColor = Color.WhiteSmoke;
                    btnVideo.BackColor = btnCamera.BackColor = btnFiles.BackColor = Color.Purple;
                    btnVideo.ForeColor = btnCamera.ForeColor = btnFiles.ForeColor = Color.Gainsboro;
                    formToOpen = new SettingsPage();
                    break;
            }

            if (activePage != null)
            {
                activePage.Close();
                activePage.Dispose();
            }

            activePage = formToOpen;
            formToOpen.TopLevel = false;
            formToOpen.FormBorderStyle = FormBorderStyle.None;
            formToOpen.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(formToOpen);
            formToOpen.BringToFront();
            formToOpen.Show();
        }
    }
}
