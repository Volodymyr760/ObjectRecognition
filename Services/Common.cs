using ObjectsRecognition.Models;

namespace ObjectsRecognition.Services
{
    public class CommonService
    {
        Color color;

        public CommonService()
        {
            switch (Properties.Settings.Default["Color"].ToString())
            {
                case "Black":
                    color = Color.Black; break;
                case "Blue":
                    color = Color.Blue; break;
                case "Green":
                    color = Color.Green; break;
                case "Indigo":
                    color = Color.Indigo; break;
                case "Orange":
                    color = Color.Orange; break;
                case "Red":
                    color = Color.Red; break;
                case "Violet":
                    color = Color.Violet; break;
                case "White":
                    color = Color.White; break;
                default: color = Color.Yellow; break;
            }
        }

        public string GetAbsolutePath(string relativePath)
        {
            FileInfo dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = dataRoot.Directory.FullName;
            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }

        //public Image DrawBoundingBox(Image image, List<YoloPrediction> yoloPredictions)
        //{
        //    var imageHeight = image.Height;
        //    var imageWidth = image.Width;

        //    foreach (var prediction in yoloPredictions)
        //    {
        //        int x = (int)prediction.Rectangle.X;
        //        int y = (int)prediction.Rectangle.Y;

        //        using (Graphics thumbnailGraphic = Graphics.FromImage(image))
        //        {
        //            var score = Math.Round(prediction.Score * 100, 1);
        //            string text = $"{prediction.Label.Category} {prediction.Label.Name} - {score}%";

        //            Font drawFont = new Font("Segoe UI", 12, FontStyle.Regular);
        //            SizeF size = thumbnailGraphic.MeasureString(text, drawFont);
        //            SolidBrush fontBrush = new(color);
        //            Point atPoint = new(x - 3, y - 30);

        //            Pen pen = new(color, 2f);

        //            thumbnailGraphic.DrawRectangle(pen, x, y, prediction.Rectangle.Width, prediction.Rectangle.Height);
        //            thumbnailGraphic.DrawString(text, drawFont, fontBrush, atPoint);
        //        }
        //    }

        //    return image;
        //}

        public Image DrawBoundingBox(Image image, List<YoloPrediction> yoloPredictions,
            int paddingLeft, int paddingTop, int row, int column)
        {
            foreach (var prediction in yoloPredictions)
            {
                int x = (int)prediction.Rectangle.X + paddingLeft + (column - 1) * 640;
                int y = (int)prediction.Rectangle.Y + paddingTop + (row - 1) * 640;

                using (Graphics thumbnailGraphic = Graphics.FromImage(image))
                {
                    var score = Math.Round(prediction.Score * 100, 1);
                    string text = $"{prediction.Label.Category} {prediction.Label.Name} - {score}%";

                    Font drawFont = new Font("Segoe UI", 12, FontStyle.Regular);
                    SizeF size = thumbnailGraphic.MeasureString(text, drawFont);
                    SolidBrush fontBrush = new(color);
                    Pen pen = new(color, 2f);
                    Point atPoint = new(x - 3, y - 30);

                    thumbnailGraphic.DrawRectangle(pen, x, y, prediction.Rectangle.Width, prediction.Rectangle.Height);
                    thumbnailGraphic.DrawString(text, drawFont, fontBrush, atPoint);
                }
            }

            return image;
        }

        public Image DrawBoundingBox(Image image, List<PredictionData> predictions)
        {
            foreach (var prediction in predictions)
            {
                int x = (int)prediction.RectangleX;
                int y = (int)prediction.RectangleY;

                using (Graphics thumbnailGraphic = Graphics.FromImage(image))
                {
                    var score = Math.Round(prediction.Score * 100, 1);
                    string text = $"{prediction.Category} {prediction.Name} - {score}%";

                    Font drawFont = new Font("Segoe UI", 12, FontStyle.Regular);
                    SizeF size = thumbnailGraphic.MeasureString(text, drawFont);
                    SolidBrush fontBrush = new(color);
                    Pen pen = new(color, 2f);
                    Point atPoint = new(x - 3, y - 30);

                    thumbnailGraphic.DrawRectangle(pen, x, y, prediction.Width, prediction.Height);
                    thumbnailGraphic.DrawString(text, drawFont, fontBrush, atPoint);
                }
            }

            return image;
        }

        public Bitmap OverlayImages(Bitmap background, Bitmap overlay, Point location)
        {
            Bitmap result = new(background.Width, background.Height);// Clone background to avoid modifying the original

            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(overlay, location);
            }

            return result;
        }
    }
}
