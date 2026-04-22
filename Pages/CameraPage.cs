using DirectShowLib;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using ObjectsRecognition.Library.Enums;
using ObjectsRecognition.Models;
using ObjectsRecognition.Services;
using System.Diagnostics;
using System.Timers;

namespace ObjectsRecognition.Pages
{
    public partial class CameraPage : Form
    {
        VideoCapture? capture;
        VideoWriter writer;
        DsDevice[] cams;
        CommonService commonService;
        int delay;
        int frequency;
        ImageToRecognize imageToRecognize = new ImageToRecognize();
        bool isRecording;
        bool isAIModeFlag;
        bool isRegularModeFlag;
        int loopRecording;
        Mat mat;
        string outputFolder = string.Empty;
        YoloScorer<YoloSignModel> scorer;
        int selectedCameraId = 0;
        System.Timers.Timer DurationTimer = new System.Timers.Timer();
        System.Timers.Timer RecognitionTimer = new System.Timers.Timer();
        int durationSeconds = 0;

        List<YoloPrediction> yoloPredictions = [];

        public CameraPage()
        {
            InitializeComponent();

            cams = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            commonService = new CommonService();
            frequency = Properties.Settings.Default["Frequency"].ToString() switch
            {
                "Once/sec" => 1000,
                "Twice/sec" => 500,
                _ => 0,
            };

            isRecording = false;
            isAIModeFlag = false;

            mat = new Mat();

            loopRecording = (int)Properties.Settings.Default["Loop"]; // in minutes

            outputFolder = Path.Combine(commonService.GetAbsolutePath("Assets"), "Output");
            if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

            scorer = new YoloScorer<YoloSignModel>("Assets/Models/" +
                            Properties.Settings.Default["CurrentModel"].ToString());
        }

        void OnDurationTimerStart(bool startStop)
        {
            durationSeconds = 0;
            if (startStop)
            {
                DurationTimer.Elapsed += new ElapsedEventHandler(OnDurationTimerEvent);
                DurationTimer.Interval = 1000; // ~ 1 second
                DurationTimer.Enabled = true;
                lblTimeCounter.BackColor = Color.Red;
            }
            else
            {
                DurationTimer.Stop();
                DurationTimer.Elapsed -= OnDurationTimerEvent;
                lblTimeCounter.BackColor = Color.Transparent;
                lblTimeCounter.Text = "00:00";
            }
        }

        void OnDurationTimerEvent(object? source, ElapsedEventArgs e)
        {
            try
            {
                if (isRecording == false) return;
                durationSeconds++;
                lblTimeCounter.Invoke((Action)(() =>
                    lblTimeCounter.Text = ChangeDurationTime(durationSeconds)));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
        }

        void OnRecognitionTimerStart(bool startStop)
        {
            if (startStop)
            {
                RecognitionTimer.Elapsed += new ElapsedEventHandler(OnRecognitionTimerEvent);
                RecognitionTimer.Interval = 1000; // ~ 1 second
                RecognitionTimer.Enabled = true;
            }
            else
            {
                RecognitionTimer.Stop();
                RecognitionTimer.Elapsed -= OnRecognitionTimerEvent;
            }
        }

        void OnRecognitionTimerEvent(object? source, ElapsedEventArgs e)
        {
            imageToRecognize.InProgress = true;
        }

        private void CameraPage_Load(object sender, EventArgs e)
        {
            // Init UI controls
            ToolTip CameraPageTooltip = new ToolTip();
            CameraPageTooltip.SetToolTip(btnRecord, "Start/Stop Record");
            CameraPageTooltip.SetToolTip(btnScreenShot, "Save ScreenShot");

            object[] modes = [Mode.Regular, Mode.OnMove, Mode.AIMode];
            CmbMode.Items.AddRange(modes);
            CmbMode.SelectedIndex = (int)Properties.Settings.Default["Mode"];
            CameraPageTooltip.SetToolTip(CmbMode, "Record Mode");

            object[] loops = [1, 2, 3, 4, 5];
            CmbLoop.Items.AddRange(loops);
            CmbLoop.SelectedIndex = (int)Properties.Settings.Default["Loop"] - 1; // 1-5 minutes, 0 - unacceptable
            CameraPageTooltip.SetToolTip(CmbLoop, "Loop Recording");

            if (cams.Length > 0)
            {
                foreach (var cam in cams) cmbCameras.Items.Add(cam.Name);
                cmbCameras.SelectedIndex = 0;
                StartCamera();
            }
        }

        private void cmbCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCameraId = cmbCameras.SelectedIndex;
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            try
            {
                isRecording = !isRecording;
                btnRecord.Image = isRecording ? Properties.Resources.movie_off_24 : Properties.Resources.movie_24;
                if (isRecording) // user wants start recording
                {
                    if (CmbMode.SelectedIndex == (int)Mode.Regular)
                    {
                        isRegularModeFlag = true;
                        OnDurationTimerStart(true);
                    }
                }
                else // user wants to stop recordiing
                {
                    if (CmbMode.SelectedIndex == (int)Mode.Regular)
                    {
                        isRegularModeFlag = false;
                        OnDurationTimerStart(false);
                        //writer?.Dispose();
                    }
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
                capture?.Retrieve(mat);
                Bitmap? image = mat.ToImage<Bgr, byte>().Flip(FlipType.Horizontal).ToBitmap();

                if (isAIModeFlag)
                {
                    if (imageToRecognize.InProgress)
                    {
                        imageToRecognize.Image = commonService.OverlayImages(new Bitmap(640, 640), image, new Point(0, 0));// new Bitmap(image);
                        imageToRecognize.InProgress = false;
                        Task.Run(() => GetPredictions(imageToRecognize.Image));
                    }
                    if (yoloPredictions.Count > 0)
                        image = (Bitmap)commonService.DrawBoundingBox(image, yoloPredictions, 0, 0, 1, 1);
                }

                if (isRegularModeFlag && isRecording)
                {
                    // stop starting saving task
                    isRegularModeFlag = false;
                    //run saving task
                    RecordRegularVideo();

                    //run check storage limit

                }

                //if (yoloPredictions.Count > 0)
                //{
                //    var imageToSave = commonService.DrawBoundingBox(image, yoloPredictions, 0, 0, 0, 0);
                //    string screenshotFilename = $"Screen_{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}" +
                //        $"_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}.jpg";
                //    Task.Run(() => imageToSave.Save(Path.Combine(outputFolder, Path.GetFileName(screenshotFilename))))
                //        .ContinueWith(t =>
                //            {
                //                pbVideo.Image = imageToSave;
                //                Task.Delay(frequency);
                //            });
                //    pbVideo.Image = commonService.DrawBoundingBox(image, yoloPredictions, 0, 0, 0, 0);
                //}
                //else
                //{
                //    pbVideo.Image = image;
                //}

                pbVideo.Image = image;

                //if (delay > 0 && !imageToRecognize.InProgress) Thread.Sleep(delay);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ImageGrabbed: " + ex.Message);
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

        private void StartCamera()
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
                    if (capture == null) capture = new VideoCapture(selectedCameraId);
                    capture.ImageGrabbed += VideoCapture_ImageGrabbed;
                    capture.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CameraPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            capture?.Stop();
            capture?.Dispose();
            mat.Dispose();
            //todo: перевірити чи потрібно ліквідовувати writer
            DurationTimer.Stop();
            DurationTimer.Dispose();
            RecognitionTimer.Stop();
            RecognitionTimer.Dispose();
        }

        private void CmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CmbMode.SelectedItem == null) return;
                Properties.Settings.Default["Mode"] = (int)CmbMode.SelectedItem;
                Properties.Settings.Default.Save();

                if (CmbMode.SelectedIndex == (int)Mode.AIMode)
                {
                    OnRecognitionTimerStart(true);
                    isAIModeFlag = true;
                }
                else
                {
                    OnRecognitionTimerStart(false);
                    isAIModeFlag = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mode: " + ex.Message);
            }
        }

        private void CmbLoop_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CmbLoop.SelectedItem == null) return;
                loopRecording = (int)CmbLoop.SelectedItem;
                Properties.Settings.Default["Loop"] = loopRecording;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        string ChangeDurationTime(int durationSeconds)
        {
            int minutesAmount = (int)durationSeconds / 60;
            //if (minutesAmount == loopRecording) durationSeconds = minutesAmount = 0; // starts recording new file
            string minutes = minutesAmount < 10 ? "0" + minutesAmount : minutesAmount.ToString();

            string seconds = (durationSeconds - minutesAmount * 60).ToString();
            if (seconds.Length < 2) seconds = "0" + seconds;
            return $"{minutes}:{seconds}";
        }

        string CreateNewFileName()
        {
            DateTime now = DateTime.Now;
            string minutes = now.Minute < 10 ? "0" + now.Minute : now.Minute.ToString();
            var fileName = now.Year + "_" + now.Month + "_" + now.Day + "_" + now.Hour + "_" + minutes + "_" + now.Second;
            return fileName;
        }

        void RecordRegularVideo()
        {
            try
            {
                if (capture == null) throw new Exception("No available camera.");
                int width = (int)capture.Get(CapProp.FrameWidth);
                int height = (int)capture.Get(CapProp.FrameHeight);
                Size size = new Size(width, height);
                double fps = 15;

                int fourcc = VideoWriter.Fourcc('X', 'V', 'I', 'D');

                Task.Run(() =>
                {
                    using (writer = new(@"D:\Video\" + CreateNewFileName() + ".mp4", fourcc, fps, size, true)) // "output.avi"
                    {
                        durationSeconds = 0;
                        for (int i = 0; i < 150; i++) // Record 10sec * 15fps = 150 frames
                        {
                            if (!isRecording)
                            {
                                writer.Dispose();
                                return;
                            }
                            capture.Read(mat);
                            if (!mat.IsEmpty)
                            {
                                writer.Write(mat); // Write the frame to file
                            }
                        }
                    } // Writer is automatically disposed/closed here
                }).ContinueWith(t =>
                {
                    isRegularModeFlag = true;
                });
            }
            catch
            {
                throw new Exception("Oops! Something went wront while recording the video.");
            }
        }
    }
}
