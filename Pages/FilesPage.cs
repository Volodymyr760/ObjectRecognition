using ObjectsRecognition.Models;
using ObjectsRecognition.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ObjectsRecognition.Pages
{
    public partial class FilesPage : Form
    {
        YoloScorer<YoloSignModel> scorer;
        List<YoloPrediction> yoloPredictions = [];
        CommonService commonService;

        public FilesPage()
        {
            InitializeComponent();
        }

        private void FilesPage_Load(object sender, EventArgs e)
        {
            scorer = new YoloScorer<YoloSignModel>("Assets/Models/" +
                            Properties.Settings.Default["CurrentModel"].ToString());
            commonService = new CommonService();
        }

        //private void btnChooseFiles_ClickOld(object sender, EventArgs e)
        //{
        //    RtxtResults.Text = string.Empty;
        //    Image? imageWithPrediction;

        //    try
        //    {
        //        string outputFolder = Path.Combine(commonService.GetAbsolutePath("Assets"), "Output");
        //        if (Directory.Exists(outputFolder)) Directory.Delete(outputFolder, true);
        //        if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

        //        OpenFileDialog openFileDialog = new OpenFileDialog
        //        {
        //            Multiselect = true,
        //            Filter = "Image Files(*.png; *.jpg) | *.png; *.jpg"
        //        };

        //        var sb = new StringBuilder();
        //        if (openFileDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            int i = 2;
        //            int j = 3;
        //            string[,] empty2DArray = new string[i, j];

        //            string[] fileNames = openFileDialog.FileNames;
        //            pBar.Minimum = 1;
        //            pBar.Maximum = fileNames.Length;
        //            pBar.Value = 1;
        //            pBar.Step = 1;
        //            foreach (string fName in fileNames)
        //            {
        //                var image = Image.FromFile(fName);
        //                // Divide image to frames 640x640

        //                // Predict objects on each frame
        //                yoloPredictions = scorer.Predict((Bitmap)image);
        //                // Draw titles and bounding boxes


        //                sb.AppendLine(Path.GetFileName(fName) + ":");
        //                if (yoloPredictions.Count > 0)
        //                {
        //                    imageWithPrediction = commonService.DrawBoundingBox(image, yoloPredictions);
        //                    imageWithPrediction?.Save(Path.Combine(outputFolder, Path.GetFileName(fName)));
        //                    foreach (var prediction in yoloPredictions)
        //                        sb.AppendLine(prediction.Label.Name +
        //                            " - " + Math.Round(prediction.Score * 100, 1).ToString() + "%;");
        //                }
        //                else
        //                {
        //                    sb.AppendLine("Nothing detected.");
        //                }
        //                imageWithPrediction = null;
        //                yoloPredictions.Clear();
        //                sb.AppendLine();
        //                pBar.PerformStep();
        //            }
        //        }

        //        if (sb.Length > 0)
        //        {
        //            RtxtResults.Text = sb.ToString();
        //            var psi = new ProcessStartInfo() { FileName = outputFolder, UseShellExecute = true };
        //            Process.Start(psi);
        //        }
        //        else
        //        {
        //            RtxtResults.Text = "Nothing detected.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Something went wrong." + ex.Message);
        //    }
        //}

        private void btnChooseFiles_Click(object sender, EventArgs e)
        {
            RtxtResults.Text = string.Empty;
            Image? imageWithPrediction;

            try
            {
                string outputFolder = Path.Combine(commonService.GetAbsolutePath("Assets"), "Output");
                if (Directory.Exists(outputFolder)) Directory.Delete(outputFolder, true);
                else Directory.CreateDirectory(outputFolder);

                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Multiselect = true,
                    Filter = "Image Files(*.png; *.jpg) | *.png; *.jpg"
                };

                var sb = new StringBuilder();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] fileNames = openFileDialog.FileNames;
                    SetStartProgressBarSettings(fileNames.Length);

                    foreach (string fName in fileNames)
                    {
                        var image = Image.FromFile(fName);
                        var imageHasPredictions = false;

                        // Divide image to frames 640x640 and predict objects on each frame
                        Bitmap sourceBitmap = (Bitmap)image;
                        int rows = Convert.ToInt32(Math.Floor((decimal)sourceBitmap.Height / 640));
                        int columns = Convert.ToInt32(Math.Floor((decimal)sourceBitmap.Width / 640));
                        int paddingLeft = (sourceBitmap.Width - columns * 640) / 2; // x axle
                        int paddingTop = (sourceBitmap.Height - rows * 640) / 2; // y axle

                        for (int r = 1; r <= rows; r++)
                        {
                            for (int c = 1; c <= columns; c++)
                            {
                                Rectangle cropRectangle = new((c - 1) * 640 + paddingLeft, (r - 1) * 640 + paddingTop, 640, 640);
                                Bitmap? croppedBitmap = sourceBitmap.Clone(cropRectangle, sourceBitmap.PixelFormat);
                                yoloPredictions = scorer.Predict(croppedBitmap);
                                // Draw labels and bounding boxes
                                if (yoloPredictions.Count > 0)
                                {
                                    imageHasPredictions = true;
                                    imageWithPrediction = commonService.DrawBoundingBox(image, yoloPredictions,
                                        paddingLeft, paddingTop, r, c);
                                    foreach (var prediction in yoloPredictions)
                                        sb.AppendLine(prediction.Label.Name +
                                            " - " + Math.Round(prediction.Score * 100, 1).ToString() + "%;");
                                }
                                if (yoloPredictions.Count > 0) yoloPredictions.Clear();
                                croppedBitmap.Dispose();
                            }
                        }
                        //imageWithPrediction = null;
                        if (imageHasPredictions) image.Save(Path.Combine(outputFolder, Path.GetFileName(fName)));
                        
                        imageHasPredictions = false;
                        sb.AppendLine();
                        pBar.PerformStep();
                    }
                }

                if (sb.Length > 0)
                {
                    RtxtResults.Text = sb.ToString();
                    var psi = new ProcessStartInfo() { FileName = outputFolder, UseShellExecute = true };
                    Process.Start(psi);
                }
                else
                {
                    RtxtResults.Text = "Nothing detected.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong." + ex.Message);
            }
        }

        private void SetStartProgressBarSettings(int filesAmount)
        {
            pBar.Minimum = 1;
            pBar.Maximum = filesAmount;
            pBar.Value = 1;
            pBar.Step = 1;
        }
    }
}
