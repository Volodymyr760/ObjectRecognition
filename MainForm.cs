using ObjectsRecognition.Pages;

namespace ObjectsRecognition
{
    public partial class MainForm : Form
    {
        private Form? activePage;

        public MainForm()
        {
            InitializeComponent();
            SetActivePage(BtnCamera.Name);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ToolTip MainPageTooltip = new ToolTip();
            MainPageTooltip.SetToolTip(BtnCamera, "Camera Capture");
            MainPageTooltip.SetToolTip(BtnVideo, "Video Viwer");
            MainPageTooltip.SetToolTip(BtnFiles, "Files");
            MainPageTooltip.SetToolTip(BtnSettings, "Settings");
        }

        private void btnFiles_Click(object sender, EventArgs e) =>
            SetActivePage(BtnFiles.Name);

        private void btnPlayVideo_Click(object sender, EventArgs e) =>
            SetActivePage(BtnVideo.Name);

        private void btnCamera_Click(object sender, EventArgs e) =>
            SetActivePage(BtnCamera.Name);

        private void btnSettings_Click(object sender, EventArgs e) =>
            SetActivePage(BtnSettings.Name);

        private void SetActivePage(string buttonName)
        {
            Form? formToOpen = null;

            switch (buttonName)
            {
                case "BtnFiles":
                    BtnFiles.BackColor = Color.Orchid;
                    BtnFiles.ForeColor = Color.WhiteSmoke;
                    BtnVideo.BackColor = BtnCamera.BackColor = BtnSettings.BackColor = Color.Purple;
                    BtnVideo.ForeColor = BtnCamera.ForeColor = BtnSettings.ForeColor = Color.Gainsboro;
                    formToOpen = new FilesPage();
                    break;
                case "BtnVideo":
                    BtnVideo.BackColor = Color.Orchid;
                    BtnVideo.ForeColor = Color.WhiteSmoke;
                    BtnFiles.BackColor = BtnCamera.BackColor = BtnSettings.BackColor = Color.Purple;
                    BtnFiles.ForeColor = BtnCamera.ForeColor = BtnSettings.ForeColor = Color.Gainsboro;
                    formToOpen = new VideoPage();
                    break;
                case "BtnCamera":
                    BtnCamera.BackColor = Color.Orchid;
                    BtnCamera.ForeColor = Color.WhiteSmoke;
                    BtnVideo.BackColor = BtnFiles.BackColor = BtnSettings.BackColor = Color.Purple;
                    BtnVideo.ForeColor = BtnFiles.ForeColor = BtnSettings.ForeColor = Color.Gainsboro;
                    formToOpen = new CameraPage();
                    break;
                default:
                    BtnSettings.BackColor = Color.Orchid;
                    BtnSettings.ForeColor = Color.WhiteSmoke;
                    BtnVideo.BackColor = BtnCamera.BackColor = BtnFiles.BackColor = Color.Purple;
                    BtnVideo.ForeColor = BtnCamera.ForeColor = BtnFiles.ForeColor = Color.Gainsboro;
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
