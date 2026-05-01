using Emgu.CV;
using Emgu.CV.Structure;
using ObjectsRecognition.Models;
using ObjectsRecognition.Services;
using System.Data.Common;
using System.Diagnostics;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace ObjectsRecognition.Pages
{
    public partial class VideoPage : Form
    {
        #region Private Members

        VideoCapture? capture;
        Mat mat;
        Bitmap? currentImage;

        bool isAiMode;
        bool isPredicting;

        readonly CommonService commonService;
        double duration;
        int framesPerSecond;
        int frequency;
        bool isPlaying = false;
        YoloScorer<YoloSignModel> scorer;
        List<PredictionData> predictions;

        #endregion

        #region Ctor

        public VideoPage()
        {
            InitializeComponent();
            mat = new Mat();
            commonService = new CommonService();
            framesPerSecond = 15;
            scorer = new YoloScorer<YoloSignModel>("Assets/Models/" +
                Properties.Settings.Default["CurrentModel"].ToString());
            predictions = [];
        }

        #endregion

        #region UI Events

        void VideoPage_Load(object sender, EventArgs e)
        {
            ToolTip VideoPageTooltip = new ToolTip();
            VideoPageTooltip.SetToolTip(BtnOpenFolder, "Open Folder and Choose *.mp4 file");
            VideoPageTooltip.SetToolTip(BtnPause, "Pause | Play");
            VideoPageTooltip.SetToolTip(BtnStop, "Stop");
            BtnPause.Image = Properties.Resources.play;
            LblTimeCounter.Text = "00:00:00";
            LblTimeCounter.Tag = null;
        }

        private void VideoPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            capture?.Stop();
            capture?.Dispose();
        }

        void VideoCapture_ImageGrabbed(object? sender, EventArgs e)
        {
            try
            {
                if (capture == null) return;
                capture.Retrieve(mat);
                currentImage = mat.ToImage<Bgr, byte>().ToBitmap();
                int delay = (int)(1000 / framesPerSecond);
                SetTimeOnForm(capture.Get(Emgu.CV.CvEnum.CapProp.PosMsec));

                if (isAiMode)
                {
                    if (predictions.Count > 0)
                        currentImage = (Bitmap)commonService.DrawBoundingBox(currentImage, predictions);
                    if (!isPredicting)
                    {
                        isPredicting = true;
                        delay = 1;
                        predictions.Clear();

                        var imageToPredict = currentImage.Width <= 640 || currentImage.Height <= 640 ?
                            commonService.OverlayImages(new Bitmap(640, 640), currentImage, new Point(0, 0))
                            : currentImage;

                        Task.Run(() =>
                        {
                            GetPredictions(imageToPredict);
                        })
                        .ContinueWith(async t =>
                            {
                                await Task.Delay(1000);
                                isPredicting = false;
                            });
                    }
                }

                //todo: optimize the Thread.Sleep(delay) conditionally on isPredicting;
                Thread.Sleep(delay);
                PbVideo.Image = currentImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops! Something went wrong." + ex.Message);
            }
        }

        private void ChbAI_CheckedChanged(object sender, EventArgs e) => isAiMode = ChbAI.Checked;

        void BtnOpenFolder_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Video files | *mp4";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    capture = new VideoCapture(openFileDialog.FileName);
                    capture.ImageGrabbed += VideoCapture_ImageGrabbed;
                    framesPerSecond = (int)capture.Get(Emgu.CV.CvEnum.CapProp.Fps);
                    duration = capture.Get(Emgu.CV.CvEnum.CapProp.FrameCount) / framesPerSecond;
                    capture.Start();
                    isPlaying = true;
                    BtnPause.Image = Properties.Resources.pause;
                    LblTimeCounter.Text = "00:00:00";
                    LblTimeCounter.Tag = null;
                    TrackBarVideo.Value = 0;
                }
            }
            catch
            {
                MessageBox.Show("Oops! Something went wrong. Unable to get video.");
            }
        }

        void BtnPlayPause_Click(object sender, EventArgs e)
        {
            try
            {
                if (capture == null)
                {
                    MessageBox.Show("No choosed video. Click Open Folder button and select .mp4 file.");
                    return;
                }

                if (isPlaying) capture.Pause(); else capture.Start();
                isPlaying = !isPlaying;
                BtnPause.Image = isPlaying ? Properties.Resources.pause : Properties.Resources.play;
            }
            catch
            {
                MessageBox.Show("Oops! Something went wrong while playing.");
            }
        }

        void BtnStop_Click(object? sender, EventArgs e)
        {
            try
            {
                capture?.Stop();
                capture?.Dispose();
                capture = null;

                PbVideo.Image?.Dispose();
                PbVideo.Image = null;

                isPlaying = false;
                BtnPause.Image = Properties.Resources.play;
            }
            catch
            {
                MessageBox.Show("Oops! Something went wrong.");
            }
        }

        void BtnScreenShot_Click(object sender, EventArgs e)
        {
            if (PbVideo.Image == null) return;
            try
            {
                string outputFolder = Path.Combine(commonService.GetAbsolutePath("Assets"), "Output");
                if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

                string screenshotFilename = $"Screen_{DateTime.Now.Day}__{DateTime.Now.Month}_{DateTime.Now.Year}" +
                    $"_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}.jpg";

                //Mat m = new();
                //capture?.Retrieve(m);
                //var image = m.ToImage<Bgr, byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal).ToBitmap();

                //pbVideo.Image = yoloPredictions.Count > 0 ?
                //    commonService.DrawBoundingBox(image, yoloPredictions, 0, 0, 0, 0) : image;

                PbVideo.Image.Save(Path.Combine(outputFolder, Path.GetFileName(screenshotFilename)));
                //if (!isAutoMode) MessageBox.Show("Image has been saved to ./Assets/Output folder.");
            }
            catch
            {
                MessageBox.Show("Oops! Something went wrong while screen is processing.");
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// 1080p (1920x1080): Сучасний стандарт для професійних відеодзвінків, створення контенту та потокової передачі, що пропонує збалансоване, чітке зображення.
        /// 720p(1280x720) : Підходить для повсякденного використання, зменшує пропускну здатність та навантаження на процесор на старих комп'ютерах.
        /// 4K(3840x2160) : Орієнтовано на професійних стрімерів або тих, кому потрібно обрізати/масштабувати без втрати якості
        /// </summary>
        /// <param name="image"></param>
        void GetPredictions(Bitmap image)
        {
            var startDate = DateTime.Now;
            try
            {
                if (image.Width <= 640 || image.Height <= 640)
                {
                    var yoloPredictions = scorer.Predict(image);
                    if (yoloPredictions.Count > 0)
                    {
                        foreach (var partialYoloPrediction in yoloPredictions)
                        {
                            var predictionData = new PredictionData(partialYoloPrediction.Label.Name,
                                partialYoloPrediction.Label.Category,
                                partialYoloPrediction.Rectangle.X,
                                partialYoloPrediction.Rectangle.Y,
                                partialYoloPrediction.Rectangle.Width,
                                partialYoloPrediction.Rectangle.Height,
                                partialYoloPrediction.Score);
                            predictions.Add(predictionData);
                        }
                    }
                }
                else
                {
                    int rows = Convert.ToInt32(Math.Floor((decimal)image.Height / 640));
                    int columns = Convert.ToInt32(Math.Floor((decimal)image.Width / 640));
                    int paddingLeft = (image.Width - columns * 640) / 2; // x axle
                    int paddingTop = (image.Height - rows * 640) / 2; // y axle

                    for (int r = 1; r <= rows; r++)
                    {
                        for (int c = 1; c <= columns; c++)
                        {
                            Rectangle cropRectangle = new((c - 1) * 640 + paddingLeft, (r - 1) * 640 + paddingTop, 640, 640);
                            Bitmap? croppedBitmap = image.Clone(cropRectangle, image.PixelFormat);

                            var yoloPredictions = scorer.Predict(croppedBitmap);

                            if (yoloPredictions.Count > 0)
                            {
                                foreach (var yoloPrediction in yoloPredictions)
                                {
                                    var predictionData = new PredictionData(yoloPrediction.Label.Name,
                                        yoloPrediction.Label.Category,
                                        paddingLeft + yoloPrediction.Rectangle.X + (c - 1) * 640,
                                        paddingTop + yoloPrediction.Rectangle.Y + (r - 1) * 640,
                                        yoloPrediction.Rectangle.Width,
                                        yoloPrediction.Rectangle.Height,
                                        yoloPrediction.Score);
                                    predictions.Add(predictionData);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops! Something went wrong while predicting." + ex.Message);
            }
            var endDate = DateTime.Now;
            var execTime = endDate.Subtract(startDate).TotalMilliseconds;
            Debug.WriteLine($"Recognition time, ms: {execTime}");
        }

        void MakePicturesFromVideo(object? sender, EventArgs e)
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

                SetTimeOnForm(capture.Get(Emgu.CV.CvEnum.CapProp.PosMsec));
            }

            BtnStop_Click(null, new EventArgs());

            // Show folder
            var psi = new ProcessStartInfo() { FileName = outputFolder, UseShellExecute = true };
            Process.Start(psi);
        }

        void SetTimeOnForm(double position)
        {
            if (LblTimeCounter.Tag == null)
            {
                LblTimeCounter.Tag = position;
                return;
            }
            if (position > (double)LblTimeCounter.Tag)
            {
                var trackValue = (int)Math.Floor(TrackBarVideo.Maximum * position / 1000 / duration);
                Invoke(new Action(() => { TrackBarVideo.Value = trackValue; }));

                TimeSpan time = TimeSpan.FromMilliseconds(position + 1000);
                Invoke(new Action(() => { LblTimeCounter.Text = $"{time:hh\\:mm\\:ss}"; }));
                LblTimeCounter.Tag = position + 1000;
            }
        }

        #endregion
    }
}
