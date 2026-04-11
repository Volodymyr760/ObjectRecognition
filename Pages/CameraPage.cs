using Emgu.CV;
using ObjectsRecognition.Models;
using ObjectsRecognition.Services;
using DirectShowLib;
using Emgu.CV.Structure;

namespace ObjectsRecognition.Pages
{
    public partial class CameraPage : Form
    {
        VideoCapture? capture;
        DsDevice[] cams;
        CommonService commonService;
        int delay;
        int filesCount = 0;
        int filesLimit;
        int frequency;
        ImageToRecognize imageToRecognize = new ImageToRecognize();
        bool isAutoMode;
        bool onPause = false;
        //YoloScorer<YoloCocoModel1> scorer;
        YoloScorer<YoloSignModel> scorer;
        int selectedCameraId = 0;
        List<YoloPrediction> yoloPredictions = [];

        public CameraPage()
        {
            InitializeComponent();
        }

        private void CameraPage_Load(object sender, EventArgs e)
        {
            cams = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            foreach (var cam in cams) cmbCameras.Items.Add(cam.Name);

            commonService = new CommonService();

            delay = int.Parse(Properties.Settings.Default["Delay"].ToString());

            filesLimit = int.Parse(Properties.Settings.Default["FilesLimit"].ToString());

            frequency = Properties.Settings.Default["Frequency"].ToString() switch
            {
                "Once/sec" => 1000,
                "Twice/sec" => 500,
                _ => 0,
            };

            isAutoMode = bool.Parse(Properties.Settings.Default["IsAutoMode"].ToString());

            scorer = new YoloScorer<YoloSignModel>("Assets/Models/" +
                            Properties.Settings.Default["CurrentModel"].ToString());

            ToolTip playVideoPageTooltip = new ToolTip();
            playVideoPageTooltip.SetToolTip(btnPlay, "Select Camera & Play");
            playVideoPageTooltip.SetToolTip(btnPause, "Pause | Play");
            playVideoPageTooltip.SetToolTip(btnStop, "Stop");
            playVideoPageTooltip.SetToolTip(btnScreenShot, "Save ScreenShot");
        }

        private void cmbCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCameraId = cmbCameras.SelectedIndex;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                if (cams.Length == 0)
                {
                    throw new Exception("No available cameras.");
                }
                else if (cmbCameras.SelectedItem == null)
                {
                    throw new Exception("Choose the camera.");
                }
                else
                {
                    capture = new VideoCapture(selectedCameraId);
                    capture.ImageGrabbed += VideoCapture_ImageGrabbed;
                    capture.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void VideoCapture_ImageGrabbed(object? sender, EventArgs e)
        {
            try
            {
                Mat m = new Mat();
                capture?.Retrieve(m);
                Bitmap? image = m.ToImage<Bgr, byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal).ToBitmap();

                string outputFolder = Path.Combine(commonService.GetAbsolutePath("Assets"), "Output");
                if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

                if (!imageToRecognize.InProgress)
                {
                    imageToRecognize.Image = new Bitmap(image);
                    imageToRecognize.InProgress = true;
                    Task.Run(() => GetPredictions(imageToRecognize.Image))
                    .ContinueWith(t => imageToRecognize.InProgress = false);
                }

                if (yoloPredictions.Count > 0)
                {
                    if (isAutoMode && filesCount < filesLimit)
                    {
                        var imageToSave = commonService.DrawBoundingBox(image, yoloPredictions, 0, 0, 0, 0);
                        string screenshotFilename = $"Screen_{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}" +
                            $"_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}.jpg";
                        Task.Run(() => imageToSave.Save(Path.Combine(outputFolder, Path.GetFileName(screenshotFilename))))
                            .ContinueWith(t =>
                                {
                                    pbVideo.Image = imageToSave;
                                    Task.Delay(frequency);
                                });
                        filesCount++;
                    }
                    pbVideo.Image = commonService.DrawBoundingBox(image, yoloPredictions, 0, 0, 0, 0);
                }
                else
                {
                    pbVideo.Image = image;
                }

                if (delay > 0 && !imageToRecognize.InProgress) Thread.Sleep(delay);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            try
            {
                if (onPause) capture?.Start(); else capture?.Pause();
                onPause = !onPause;
            }
            catch
            {
                MessageBox.Show("Oops! Something went wrong.");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                capture?.Stop();
                capture?.Dispose();
                capture = null;

                pbVideo.Image?.Dispose();
                pbVideo.Image = null;
            }
            catch
            {
                MessageBox.Show("Oops! Something went wrong.");
            }
        }

        private void btnScreenShot_Click(object sender, EventArgs e)
        {
            if (pbVideo.Image == null) return;
            try
            {
                string outputFolder = Path.Combine(commonService.GetAbsolutePath("Assets"), "Output");
                if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

                string screenshotFilename = $"Screen_{DateTime.Now.Day}__{DateTime.Now.Month}_{DateTime.Now.Year}" +
                    $"_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}.jpg";

                Mat m = new();
                capture?.Retrieve(m);
                var image = m.ToImage<Bgr, byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal).ToBitmap();

                pbVideo.Image = yoloPredictions.Count > 0 ?
                    commonService.DrawBoundingBox(image, yoloPredictions, 0, 0, 0, 0) : image;

                pbVideo.Image.Save(Path.Combine(outputFolder, Path.GetFileName(screenshotFilename)));
                MessageBox.Show("Image has been saved to ./Assets/Output folder.");
            }
            catch
            {
                MessageBox.Show("Oops! Something went wrong.");
            }
        }

        private void GetPredictions(Bitmap image) => yoloPredictions = scorer.Predict(image);
    }
}
