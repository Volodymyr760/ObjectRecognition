using ObjectsRecognition.Models;

namespace ObjectsRecognition.Services
{
    public class CommonService
    {
        Color color;

        Queue<Coords> coords = new Queue<Coords>();

        Coords target;

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

        /// <summary>
        /// Use only for Aircraft model
        /// </summary>
        /// <param name="image"></param>
        /// <param name="yoloPredictions"></param>
        /// <returns></returns>
        //public Image DrawBoundingBoxWithSniperCircle(Image image, List<YoloPrediction> yoloPredictions)
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

        //            //thumbnailGraphic.DrawRectangle(pen, x, y, prediction.Rectangle.Width, prediction.Rectangle.Height);
        //            thumbnailGraphic.DrawString(text, drawFont, fontBrush, atPoint);

        //            // Draw sniper scope on screen center
        //            thumbnailGraphic.DrawLine(new(Color.Red, 1f), new PointF(imageWidth / 2, imageHeight / 2 + 300 - 60),
        //                new PointF(imageWidth / 2, imageHeight / 2 + 300 + 60));
        //            thumbnailGraphic.DrawLine(new(Color.Red, 1f), new PointF(imageWidth / 2 - 60, imageHeight / 2 + 300),
        //                new PointF(imageWidth / 2 + 60, imageHeight / 2 + 300));

        //            thumbnailGraphic.DrawEllipse(new(Color.Red, 1f), imageWidth / 2 - 25, imageHeight / 2 + 300 - 25, 50, 50);
        //            thumbnailGraphic.DrawEllipse(new(Color.Red, 1f), imageWidth / 2 - 50, imageHeight / 2 + 300 - 50, 100, 100);

        //            // Draw sniper scope
        //            // Add center of prediction.Rectangle on the image
        //            var current = new Coords(x + prediction.Rectangle.Width / 2, y + prediction.Rectangle.Height / 2);
        //            if (coords.Count == 0)
        //            {
        //                coords.Enqueue(current);
        //            }
        //            else if (coords.Count == 1)
        //            {
        //                var previous = coords.Peek();
        //                if (previous.X != current.X)
        //                {
        //                    coords.Enqueue(current);
        //                    TargetForecast(previous, current);
        //                }
        //            }
        //            else if (coords.Count == 2)
        //            {
        //                var previous = coords.Dequeue();
        //                if (previous.X != current.X)
        //                {
        //                    coords.Enqueue(current);
        //                    TargetForecast(previous, current);
        //                }
        //            }

        //            if (target != null)
        //            {
        //                thumbnailGraphic.DrawLine(new(Color.Orange, 2f), new PointF(target.X, target.Y - 55),
        //                    new PointF(target.X, target.Y + 55));
        //                thumbnailGraphic.DrawLine(new(Color.Orange, 2f), new PointF(target.X - 55, target.Y),
        //                    new PointF(target.X + 55, target.Y));

        //                thumbnailGraphic.DrawLine(new(Color.Red, 1f), new PointF(imageWidth / 2, imageHeight / 2 + 300),
        //                    new PointF(target.X, target.Y));
        //            }
        //        }
        //    }

        //    return image;
        //}

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

        private void TargetForecast(Coords previous, Coords current) =>
             target = new Coords(current.X + (current.X - previous.X), current.Y + (current.Y - previous.Y));
    }
}
