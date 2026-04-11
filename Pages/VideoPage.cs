using Emgu.CV;
using Emgu.CV.Structure;
using ObjectsRecognition.Models;
using ObjectsRecognition.Services;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace ObjectsRecognition.Pages
{
    public partial class VideoPage : Form
    {
        Bitmap? currentImage = null;
        string speedLimit = "";
        DateTime speedLimitChanged = DateTime.Now;

        VideoCapture? capture;
        CommonService commonService = new();
        double duration;
        int delay;
        int filesLimit;
        int framesPerSecond = 0;
        int frequency;
        ImageToRecognize imageToRecognize = new();
        bool isAutoMode;
        bool onPause = false;
        YoloScorer<YoloSignModel> scorer = new YoloScorer<YoloSignModel>("Assets/Models/" +
                Properties.Settings.Default["CurrentModel"].ToString());
        List<YoloPrediction> yoloPredictions = [];

        private System.Timers.Timer aTimer;

        public VideoPage()
        {
            InitializeComponent();
        }

        private void VideoPage_Load(object sender, EventArgs e)
        {
            delay = int.Parse(Properties.Settings.Default["Delay"].ToString());

            filesLimit = int.Parse(Properties.Settings.Default["FilesLimit"].ToString());

            frequency = Properties.Settings.Default["Frequency"].ToString() switch
            {
                "Once/sec" => 1000,
                "Twice/sec" => 500,
                _ => 0,
            };
            var test = Properties.Settings.Default["IsAutoMode"].ToString();
            isAutoMode = bool.Parse(Properties.Settings.Default["IsAutoMode"].ToString());
            Text = isAutoMode ? "Video: Auto Save Mode"
                : "Video: Play Mode";
            trackBarVideo.Scroll += new EventHandler(trackBarVideo_Scroll);

            ToolTip playVideoPageTooltip = new ToolTip();
            playVideoPageTooltip.SetToolTip(btnPlay, "Choose *.mp4 file & Play");
            playVideoPageTooltip.SetToolTip(btnPause, "Pause | Play");
            playVideoPageTooltip.SetToolTip(btnStop, "Stop");
            SetTimer();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Video files | *mp4";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    capture = new VideoCapture(openFileDialog.FileName);
                    capture.ImageGrabbed += VideoCapture_ImageGrabbed;// MakePicturesFromVideo;
                    framesPerSecond = (int)capture.Get(Emgu.CV.CvEnum.CapProp.Fps);
                    duration = capture.Get(Emgu.CV.CvEnum.CapProp.FrameCount) / framesPerSecond;
                    capture.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong.");
            }
        }

        private void VideoCapture_ImageGrabbedOld(object? sender, EventArgs e)
        {
            try
            {
                Mat m = new Mat();
                capture?.Retrieve(m);
                var image = m.ToImage<Bgr, byte>().ToBitmap();
                if (isAutoMode)
                {
                    var images = new List<Image>();

                    int frameInterval = (int)Math.Round((float)framesPerSecond * frequency / 1000, 0);
                    int frameCounter = 0;
                    int? totalFrames = (int?)capture?.Get(Emgu.CV.CvEnum.CapProp.FrameCount);

                    while (images.Count < filesLimit && frameCounter <= totalFrames)
                    {
                        image = capture?.QueryFrame().ToBitmap();
                        if (!imageToRecognize.InProgress)
                        {
                            imageToRecognize.Image = new Bitmap(image);
                            imageToRecognize.InProgress = true;
                            Task.Run(() => GetPredictions(imageToRecognize.Image))
                            .ContinueWith(t => imageToRecognize.InProgress = false);
                        }

                        if (yoloPredictions.Count > 0) images.Add(commonService.DrawBoundingBox(image, yoloPredictions, 0, 0, 0, 0));
                        capture?.Set(Emgu.CV.CvEnum.CapProp.PosFrames, frameCounter);
                        capture?.Read(m);
                        frameCounter += frameInterval;
                        SetTimeOnForm();
                        pbVideo.Image = image;
                    }

                    if (images.Count > 0)
                    {
                        string outputFolder = Path.Combine(commonService.GetAbsolutePath("Assets"), "Output");
                        if (Directory.Exists(outputFolder)) Directory.Delete(outputFolder, true);
                        if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

                        string screenshotFilename = string.Empty;

                        foreach (var img in images)
                        {
                            screenshotFilename = $"Screen_{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}" +
                            $"_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}_{images.IndexOf(img)}.jpg";
                            img.Save(Path.Combine(outputFolder, Path.GetFileName(screenshotFilename)));
                        }

                        btnStop_Click(sender, new EventArgs());

                        // Show folder
                        var psi = new ProcessStartInfo() { FileName = outputFolder, UseShellExecute = true };
                        Process.Start(psi);
                    }
                    else
                    {
                        MessageBox.Show("Nothing detected.");
                    }
                }
                else
                {
                    //if (!imageToRecognize.InProgress)
                    //{
                    //    imageToRecognize.Image = new Bitmap(image);
                    //    imageToRecognize.InProgress = true;
                    //    Task.Run(() => GetPredictions(imageToRecognize.Image))
                    //    .ContinueWith(t => imageToRecognize.InProgress = false);
                    //}
                    //pbVideo.Image = yoloPredictions.Count > 0 ? commonService.DrawBoundingBox(image, yoloPredictions, 0, 0, 0, 0) : image;
                    pbVideo.Image = image;
                    SetTimeOnForm();
                    //if (delay > 0 && !imageToRecognize.InProgress) Thread.Sleep(delay);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Invoke")) VideoPage_Deactivate(sender, e);
            }
        }

        private void VideoCapture_ImageGrabbed(object? sender, EventArgs e)
        {
            try
            {
                Mat m = new Mat();
                capture?.Retrieve(m);
                currentImage = m.ToImage<Bgr, byte>().ToBitmap();

                int frameInterval = (int)Math.Round((float)framesPerSecond * frequency / 1000, 0);
                int frameCounter = 0;
                int? totalFrames = (int?)capture?.Get(Emgu.CV.CvEnum.CapProp.FrameCount);
                var imageHasPredictions = false;

                if (isAutoMode)
                {
                    var images = new List<Image>();
                    while (images.Count < filesLimit && frameCounter <= totalFrames)
                    {
                        currentImage = capture?.QueryFrame().ToBitmap();
                        if (!imageToRecognize.InProgress)
                        {
                            imageToRecognize.Image = new Bitmap(currentImage);
                            imageToRecognize.InProgress = true;
                            Task.Run(() => GetPredictions(imageToRecognize.Image))
                            .ContinueWith(t => imageToRecognize.InProgress = false);
                        }

                        if (yoloPredictions.Count > 0) images.Add(commonService.DrawBoundingBox(currentImage, yoloPredictions, 0, 0, 0, 0));
                        capture?.Set(Emgu.CV.CvEnum.CapProp.PosFrames, frameCounter);
                        capture?.Read(m);
                        frameCounter += frameInterval;
                        SetTimeOnForm();
                        pbVideo.Image = currentImage;
                    }

                    if (images.Count > 0)
                    {
                        string outputFolder = Path.Combine(commonService.GetAbsolutePath("Assets"), "Output");
                        if (Directory.Exists(outputFolder)) Directory.Delete(outputFolder, true);
                        if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

                        string screenshotFilename = string.Empty;

                        foreach (var img in images)
                        {
                            screenshotFilename = $"Screen_{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}" +
                            $"_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}_{images.IndexOf(img)}.jpg";
                            img.Save(Path.Combine(outputFolder, Path.GetFileName(screenshotFilename)));
                        }

                        btnStop_Click(sender, new EventArgs());

                        // Show folder
                        var psi = new ProcessStartInfo() { FileName = outputFolder, UseShellExecute = true };
                        Process.Start(psi);
                    }
                    else
                    {
                        MessageBox.Show("Nothing detected.");
                    }
                }
                else
                {
                    //if (!imageToRecognize.InProgress)
                    //{
                    //    imageToRecognize.Image = new Bitmap(image);
                    //    imageToRecognize.InProgress = true;
                    //    Task.Run(() => GetPredictions(imageToRecognize.Image))
                    //    .ContinueWith(t => imageToRecognize.InProgress = false);
                    //}
                    //pbVideo.Image = yoloPredictions.Count > 0 ? commonService.DrawBoundingBox(image, yoloPredictions, 0, 0, 0, 0) : image;
                    pbVideo.Image = currentImage;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Invoke")) VideoPage_Deactivate(sender, e);
            }
        }

        // Event handler that is called every time the interval elapses.
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (currentImage == null) return;

            TimeSpan duration = DateTime.Now - speedLimitChanged;
            if (duration.TotalSeconds < 8)
            {
                return;
            }
            else
            {
                speedLimit = "";
                Invoke(new Action(() => { lblSpeedLimit.Text = speedLimit; }));
            }

            // Divide image to frames 640x640 and predict objects on each frame
            Bitmap sourceBitmap = currentImage;
            int rows = Convert.ToInt32(Math.Floor((decimal)sourceBitmap.Height / 640));
            int columns = Convert.ToInt32(Math.Floor((decimal)sourceBitmap.Width / 640));
            int paddingLeft = (sourceBitmap.Width - columns * 640) / 2; // x axle
            int paddingTop = (sourceBitmap.Height - rows * 640) / 2; // y axle
            bool hasPredection = false;

            SetTimeOnForm();

            for (int r = 1; r <= rows; r++)
            {
                if (hasPredection)
                {
                    if (speedLimit != yoloPredictions[0].Label.Category)
                    {
                        speedLimit = yoloPredictions[0].Label.Category;
                        Invoke(new Action(() => { lblSpeedLimit.Text = speedLimit; }));
                    }
                    yoloPredictions.Clear();
                    speedLimitChanged = DateTime.Now;
                    return;
                }
                for (int c = 1; c <= columns; c++)
                {
                    Rectangle cropRectangle = new((c - 1) * 640 + paddingLeft, (r - 1) * 640 + paddingTop, 640, 640);
                    Bitmap? croppedBitmap = sourceBitmap.Clone(cropRectangle, sourceBitmap.PixelFormat);
                    yoloPredictions = scorer.Predict(croppedBitmap);
                    // Draw labels and bounding boxes
                    if (yoloPredictions.Count > 0)
                    {
                        hasPredection = true;
                        croppedBitmap.Dispose();
                        break;
                    }
                }
            }
        }

        private void SetTimeOnForm()
        {
            var position = capture.Get(Emgu.CV.CvEnum.CapProp.PosMsec);
            var trackValue = (int)Math.Floor(trackBarVideo.Maximum * position * 1.05 / 1000 / duration);
            Invoke(new Action(() => { if (trackValue >= trackBarVideo.Value) trackBarVideo.Value = trackValue; }));
            TimeSpan time = TimeSpan.FromMilliseconds(position);
            Invoke(new Action(() => { txtVideo.Text = $"{time:hh\\:mm\\:ss}"; }));
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            try
            {
                if (onPause) capture?.Start(); else capture?.Pause();
                onPause = !onPause;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

                framesPerSecond = 0;
            }
            catch
            {
                MessageBox.Show("Oops! Something went wrong.");
            }
        }

        private void trackBarVideo_Scroll(object sender, EventArgs e)
        {
            if (capture == null) return;
            try
            {
                capture.Pause();
                var timeFor1Element = duration * 1000 / trackBarVideo.Maximum;
                capture.Set(Emgu.CV.CvEnum.CapProp.PosMsec, trackBarVideo.Value * timeFor1Element);
                capture.Start();
            }
            catch { }
        }

        private void GetPredictions(Bitmap image)
        {
            //var startDate = DateTime.Now;
            yoloPredictions = scorer.Predict(image);
            if (!isAutoMode) Thread.Sleep(frequency);
            //var endDate = DateTime.Now;
            //var execTime = endDate.Subtract(startDate).TotalSeconds;
            //statusBarMessage = $"Mode: CPU | Objects: {yoloPredictions.Count} | Recognition time: {Math.Round(execTime, 3)} sec";
        }

        private void btnScreenShot_Click(object sender, EventArgs e)
        {
            if (pbVideo.Image == null) return;
            try
            {
                string outputFolder = Path.Combine(commonService.GetAbsolutePath("Assets"), "Output");
                //if (Directory.Exists(outputFolder)) Directory.Delete(outputFolder, true);
                if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

                string screenshotFilename = $"Screen_{DateTime.Now.Day}__{DateTime.Now.Month}_{DateTime.Now.Year}" +
                    $"_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}.jpg";

                //Mat m = new();
                //capture?.Retrieve(m);
                //var image = m.ToImage<Bgr, byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal).ToBitmap();

                //pbVideo.Image = yoloPredictions.Count > 0 ?
                //    commonService.DrawBoundingBox(image, yoloPredictions, 0, 0, 0, 0) : image;

                pbVideo.Image.Save(Path.Combine(outputFolder, Path.GetFileName(screenshotFilename)));
                //if (!isAutoMode) MessageBox.Show("Image has been saved to ./Assets/Output folder.");
            }
            catch
            {
                MessageBox.Show("Oops! Something went wrong.");
            }
        }

        private void MakePicturesFromVideo(object? sender, EventArgs e)
        {
            Mat m = new Mat();
            capture?.Retrieve(m);
            var images = new List<Image>();

            var image = m.ToImage<Bgr, byte>().ToBitmap();

            int frameInterval = (int)Math.Round((float)framesPerSecond * frequency / 1000, 0);
            int frameCounter = 0;
            int? totalFrames = (int?)capture?.Get(Emgu.CV.CvEnum.CapProp.FrameCount);
            int imageCounter = 0;

            string outputFolder = Path.Combine(commonService.GetAbsolutePath("Assets"), "Output");
            if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

            string screenshotFilename = string.Empty;

            while (frameCounter <= totalFrames)
            {
                image = capture?.QueryFrame().ToBitmap();

                images.Add(image);
                var rotatedImage = new Bitmap(image);

                rotatedImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                images.Add(rotatedImage);

                foreach (var img in images)
                {
                    screenshotFilename = $"Screen_{imageCounter}.jpg";
                    img.Save(Path.Combine(outputFolder, Path.GetFileName(screenshotFilename)));
                    imageCounter++;
                }

                bool r = capture.Set(Emgu.CV.CvEnum.CapProp.PosFrames, frameCounter);
                capture.Read(m);
                frameCounter += frameInterval;
                images.Clear();

                SetTimeOnForm();
            }

            btnStop_Click(sender, new EventArgs());

            // Show folder
            var psi = new ProcessStartInfo() { FileName = outputFolder, UseShellExecute = true };
            Process.Start(psi);
        }

        private void VideoPage_Deactivate(object sender, EventArgs e)
        {
            try
            {
                capture?.Stop();
                capture?.Dispose();
                capture = null;

                pbVideo.Image?.Dispose();
                pbVideo.Image = null;

                framesPerSecond = 0;
                this.Dispose();
            }
            catch
            {
                MessageBox.Show("Oops! Something went wrong.");
            }
        }

        private void SetTimer()
        {
            // Create a timer with a two second interval (2000 milliseconds).
            aTimer = new System.Timers.Timer(2000);

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += OnTimedEvent;

            // Set AutoReset to true to make the timer run repeatedly.
            aTimer.AutoReset = true;

            // Start the timer.
            aTimer.Enabled = true;
        }
    }
}
