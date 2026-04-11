using ObjectsRecognition.Services;

namespace ObjectsRecognition.Pages
{
    // Properties.Settings - https://stackoverflow.com/questions/453161/how-can-i-save-application-settings-in-a-windows-forms-application
    public partial class SettingsPage : Form
    {
        CommonService commonService = new CommonService();

        public SettingsPage()
        {
            InitializeComponent();
        }

        private void SettingsPage_Load(object sender, EventArgs e)
        {
            try
            {
                string? currentValue;

                // Load models list
                string modelFolder = Path.Combine(commonService.GetAbsolutePath("Assets"), "Models");
                string[] fileEntries = Directory.GetFiles(modelFolder);
                foreach (string fileName in fileEntries)
                    if (Path.GetExtension(fileName) == ".onnx") cmbModels.Items.Add(Path.GetFileName(fileName));

                if (cmbModels.Items.Count == 0)
                    throw new Exception("No available models found.");
                currentValue = Properties.Settings.Default["CurrentModel"].ToString();
                if (currentValue == string.Empty || currentValue == null)
                    throw new Exception("todo: обробити помилку, якщо юзер видалив модель з папки");
                cmbModels.SelectedItem = currentValue;

                // Minimal Confidence values
                string[] confidenceValues = ["20", "30", "40", "50", "60", "70", "80", "90"];
                foreach (string confidenceValue in confidenceValues) cmbConfidence.Items.Add(confidenceValue);
                cmbConfidence.SelectedItem = Properties.Settings.Default["Confidence"].ToString();

                // Frame delays
                string[] frameDelays = ["0", "5", "10", "15", "20", "25", "30"];
                foreach (string frameDelay in frameDelays) cmbFrameDelays.Items.Add(frameDelay);
                cmbFrameDelays.SelectedItem = Properties.Settings.Default["Delay"].ToString();

                // Draw colors
                string[] colors = ["Black", "Blue", "Green", "Indigo", "Orange", "Red", "Violet", "White", "Yellow"];
                foreach (string color in colors) cmbColors.Items.Add(color);
                cmbColors.SelectedItem = Properties.Settings.Default["Color"].ToString();

                // Minimal Confidence values
                string[] recFrequencies = ["Once/sec", "Twice/sec", "Each Frame"];
                foreach (string recFrequency in recFrequencies) cmbFrequency.Items.Add(recFrequency);
                cmbFrequency.SelectedItem = Properties.Settings.Default["Frequency"].ToString();

                // IsAutoMode 
                cbAvtoScreenshotsEnabled.Checked = bool.Parse(Properties.Settings.Default["IsAutoMode"].ToString());

                // Auto save files limit values
                string[] limits = ["100", "200", "300", "400", "500", "600", "700", "800", "900", "1000"];
                foreach (string limit in limits) cmbFilesLimit.Items.Add(limit);
                cmbFilesLimit.SelectedItem = Properties.Settings.Default["FilesLimit"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default["CurrentModel"] = cmbModels.SelectedItem;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbConfidence_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default["Confidence"] = cmbConfidence.SelectedItem;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbFrameDelays_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default["Delay"] = cmbFrameDelays.SelectedItem;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default["Color"] = cmbColors.SelectedItem;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default["Frequency"] = cmbFrequency.SelectedItem;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbFilesLimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default["FilesLimit"] = cmbFilesLimit.SelectedItem;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbAvtoScreenshotsEnabled_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default["IsAutoMode"] = cbAvtoScreenshotsEnabled.Checked.ToString();
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
