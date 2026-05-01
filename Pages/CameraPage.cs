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
        #region Private Members

        VideoCapture? capture;
        VideoWriter? writer;

        Bitmap? currentSnapshot;
        Bitmap? previousSnapshot;
        double sensitivity = 0.02d;
        double bitmapsDifference = 0;

        DsDevice[] cams;
        CommonService commonService;
        //int delay;
        //int frequency;
        ImageToRecognize imageToRecognize = new ImageToRecognize();
        bool shouldRecord;
        bool isActuallyRecording;
        int loopRecording;
        int maxStorage;
        Mat mat;
        string outputVideoFolder = string.Empty;
        YoloScorer<YoloSignModel> scorer;
        int selectedCameraId = 0;
        System.Timers.Timer DurationTimer = new System.Timers.Timer();
        System.Timers.Timer RecognitionTimer = new System.Timers.Timer();
        System.Timers.Timer CheckMoveTimer = new System.Timers.Timer();

        int durationSeconds = 0;

        List<YoloPrediction> yoloPredictions = [];

        #endregion

        #region Ctor

        public CameraPage()
        {
            InitializeComponent();

            cams = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            commonService = new CommonService();

            shouldRecord = false;

            mat = new Mat();

            loopRecording = (int)Properties.Settings.Default["Loop"]; // in minutes
            maxStorage = (int)Properties.Settings.Default["MaxStorage"]; // Gb

            outputVideoFolder = Path.Combine(commonService.GetAbsolutePath("Assets"), "Video");
            if (!Directory.Exists(outputVideoFolder)) Directory.CreateDirectory(outputVideoFolder);

            scorer = new YoloScorer<YoloSignModel>("Assets/Models/" +
                            Properties.Settings.Default["CurrentModel"].ToString());
        }

        #endregion

        #region UI Events

        void CameraPage_Load(object sender, EventArgs e)
        {
            // Init UI controls
            ToolTip CameraPageTooltip = new ToolTip();
            CameraPageTooltip.SetToolTip(BtnRecord, "Start/Stop Record");
            CameraPageTooltip.SetToolTip(BtnScreenShot, "Save ScreenShot");

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
                foreach (var cam in cams) CmbCameras.Items.Add(cam.Name);
                CmbCameras.SelectedIndex = selectedCameraId;
                StartCamera();
            }
        }

        void CameraPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            shouldRecord = false;
            capture?.Stop();
            capture?.Dispose();
            mat?.Dispose();
            DurationTimer.Stop();
            DurationTimer.Dispose();
            RecognitionTimer.Stop();
            RecognitionTimer.Dispose();

            shouldRecord = false;
            previousSnapshot = null;
            CheckMoveTimer.Stop();
            CheckMoveTimer.Dispose();
            writer?.Dispose();
        }

        void CameraCapture_ImageGrabbed(object? sender, EventArgs e)
        {
            try
            {
                capture?.Retrieve(mat);
                Bitmap? image = mat.ToImage<Bgr, byte>().Flip(FlipType.Horizontal).ToBitmap();

                if ((int)Properties.Settings.Default["Mode"] == (int)Mode.AIMode)
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

                if (shouldRecord && !isActuallyRecording)
                {
                    if ((int)Properties.Settings.Default["Mode"] == (int)Mode.OnMove)
                    {
                        if (bitmapsDifference < sensitivity) return;
                    }

                    isActuallyRecording = true; // stop starting recording task
                    RecordVideo();

                    CheckStorageUsage();
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

                PbVideo.Image = image;

            }
            catch (Exception ex)
            {
                MessageBox.Show("ImageGrabbed: " + ex.Message);
            }
        }

        void BtnRecord_Click(object sender, EventArgs e)
        {
            if (capture == null) return;
            try
            {
                shouldRecord = !shouldRecord;
                BtnRecord.Image = shouldRecord ? Properties.Resources.movie_off_24 : Properties.Resources.movie_24;
                if (shouldRecord) // user wants start recording
                {
                    if (CmbMode.SelectedIndex == (int)Mode.OnMove) OnCheckMoveTimerStart(true);
                }
                else // user wants to stop recordiing
                {
                    if (CmbMode.SelectedIndex == (int)Mode.OnMove) OnCheckMoveTimerStart(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

                Mat m = new();
                capture?.Retrieve(m);
                var image = m.ToImage<Bgr, byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal).ToBitmap();

                PbVideo.Image = yoloPredictions.Count > 0 ?
                    commonService.DrawBoundingBox(image, yoloPredictions, 0, 0, 0, 0) : image;

                PbVideo.Image.Save(Path.Combine(outputFolder, Path.GetFileName(screenshotFilename)));
                MessageBox.Show("Image has been saved to ./Assets/Output folder.");
            }
            catch
            {
                MessageBox.Show("Oops! Something went wrong.");
            }
        }

        void CmbCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCameraId = CmbCameras.SelectedIndex;
            // todo: перевірити перезапуск відеопотоку з камери і чи потрібно 
            // dispose об’єкти як при CameraPage_FormClosing
        }

        void CmbLoop_SelectedIndexChanged(object sender, EventArgs e)
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

        void CmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CmbMode.SelectedItem == null) return;
                Properties.Settings.Default["Mode"] = (int)CmbMode.SelectedItem;
                Properties.Settings.Default.Save();

                if (CmbMode.SelectedIndex == (int)Mode.AIMode)
                {
                    OnRecognitionTimerStart(true);
                }
                else
                {
                    OnRecognitionTimerStart(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mode: " + ex.Message);
            }
        }

        void OnDurationTimerStart(bool startStop)
        {
            durationSeconds = 0;
            if (startStop)
            {
                DurationTimer.Elapsed += new ElapsedEventHandler(OnDurationTimerEvent);
                DurationTimer.Interval = 1000; // ~ 1 second
                DurationTimer.Enabled = true;
                LblTimeCounter.BackColor = Color.Red;
            }
            else
            {
                DurationTimer.Stop();
                DurationTimer.Elapsed -= OnDurationTimerEvent;
                LblTimeCounter.BackColor = Color.Transparent;
                LblTimeCounter.Invoke((Action)(() => LblTimeCounter.Text = "00:00"));
            }
        }

        void OnDurationTimerEvent(object? source, ElapsedEventArgs e)
        {
            try
            {
                if (!shouldRecord) return;
                durationSeconds++;
                TimeSpan ts = TimeSpan.FromSeconds(durationSeconds);
                LblTimeCounter.Invoke((Action)(() => LblTimeCounter.Text = ts.ToString(@"mm\:ss")));
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

        void OnCheckMoveTimerStart(bool startStop)
        {
            if (startStop)
            {
                CheckMoveTimer.Elapsed += new ElapsedEventHandler(OnCheckMoveEvent);
                CheckMoveTimer.Interval = 1000;
                CheckMoveTimer.Enabled = true;
            }
            else
            {
                CheckMoveTimer.Stop();
                CheckMoveTimer.Elapsed -= OnCheckMoveEvent;
            }
        }

        void OnCheckMoveEvent(object? source, ElapsedEventArgs e)
        {
            if (PbVideo.Image == null) return;
            if (isActuallyRecording) return;

            capture?.Retrieve(mat);
            if (previousSnapshot == null) // Start movement detection
            {
                previousSnapshot = currentSnapshot = new Bitmap(mat.ToImage<Bgr, byte>().Flip(FlipType.Horizontal).ToBitmap(), new Size(16, 16));
            }
            else
            {
                currentSnapshot = new Bitmap(mat.ToImage<Bgr, byte>().Flip(FlipType.Horizontal).ToBitmap(), new Size(16, 16));
                previousSnapshot = currentSnapshot;
            }

            bitmapsDifference = GetBitmapDifference();
            Debug.WriteLine("Bitmaps Difference: " + bitmapsDifference.ToString());
        }

        #endregion

        #region Helpers

        void CheckStorageUsage()
        {
            long usageLimit = maxStorage * 1000000000L; // to bytes

            string? folderPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Assets\\Video";
            if (folderPath == null) return;

            DirectoryInfo directoryInfo = new(folderPath);
            FileInfo[] files = directoryInfo.GetFiles();
            files = [.. files.OrderBy(f => f.CreationTime)];

            long filesUsage = 0;
            foreach (FileInfo file in files) filesUsage += file.Length;

            int counter = 0;

            while (filesUsage > usageLimit)
            {
                string filePath = Path.Combine(folderPath, files[counter].Name);
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                        filesUsage -= files[counter].Length;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error of deleting file: {ex.Message}");
                    }
                    finally
                    {
                        counter++;
                    }
                }
            }
        }

        string CreateNewFileName()
        {
            DateTime now = DateTime.Now;
            string minutes = now.Minute < 10 ? "0" + now.Minute : now.Minute.ToString();
            var fileName = now.Year + "_" + now.Month + "_" + now.Day + "_" + now.Hour + "_" + minutes + "_" + now.Second;
            return fileName;
        }

        double GetBitmapDifference()
        {
            if (currentSnapshot == null || previousSnapshot == null) return 0;
            int summPartialDifferences = 0;
            int maxPossibleDifference = 16 * 16 * 4 * 255;
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    Color pixelOfCurrentBitmap = currentSnapshot.GetPixel(i, j);
                    Color pixelOfPreviousBitmap = previousSnapshot.GetPixel(i, j);
                    summPartialDifferences += CalcPixelDifference(pixelOfCurrentBitmap, pixelOfPreviousBitmap);
                }
            }

            double diff = (double)summPartialDifferences / (double)maxPossibleDifference;
            return diff;
        }

        int CalcPixelDifference(Color firstPixel, Color secondPixel)
        {
            var red = Math.Abs((int)firstPixel.R - (int)secondPixel.R);
            var green = Math.Abs((int)firstPixel.G - (int)secondPixel.G);
            var blue = Math.Abs((int)firstPixel.B - (int)secondPixel.B);
            var alpha = Math.Abs((int)firstPixel.A - (int)secondPixel.A);

            return red + green + blue + alpha;
        }

        void GetPredictions(Bitmap image) => yoloPredictions = scorer.Predict(image);

        void RecordVideo()
        {
            try
            {
                if (capture == null) throw new Exception("No available camera.");
                int width = (int)capture.Get(CapProp.FrameWidth);
                int height = (int)capture.Get(CapProp.FrameHeight);
                Size size = new Size(width, height);
                double fps = 15;

                int fourcc = VideoWriter.Fourcc('X', 'V', 'I', 'D');

                string filePath = Path.Combine(outputVideoFolder, CreateNewFileName() + ".mp4");
                int framesToSave = loopRecording * 60 * (int)fps;

                Task.Run(() =>
                {
                    OnDurationTimerStart(true);
                    using (writer = new(filePath, fourcc, fps, size, true))
                    {
                        durationSeconds = 0;
                        for (int i = 0; i < framesToSave; i++)
                        {
                            if (!shouldRecord)
                            {
                                writer.Dispose();
                                return;
                            }
                            capture.Read(mat);
                            if (!mat.IsEmpty)
                            {
                                writer.Write(mat);
                            }
                        }
                    } // Writer is automatically disposed/closed here
                }).ContinueWith(t =>
                {
                    isActuallyRecording = false;
                    bitmapsDifference = 0;
                    previousSnapshot = null;
                    OnDurationTimerStart(false);
                });
            }
            catch
            {
                throw new Exception("Oops! Something went wront while recording the video.");
            }
        }

        void StartCamera()
        {
            try
            {
                if (cams.Length == 0)
                {
                    throw new Exception("No available cameras.");
                }
                else if (CmbCameras.SelectedItem == null)
                {
                    throw new Exception("Choose the camera.");
                }
                else
                {
                    if (capture == null) capture = new VideoCapture(selectedCameraId);
                    capture.ImageGrabbed += CameraCapture_ImageGrabbed;
                    capture.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}
