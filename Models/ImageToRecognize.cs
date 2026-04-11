namespace ObjectsRecognition.Models
{
    public class ImageToRecognize
    {
        public Bitmap? Image { get; set; }

        public bool InProgress { get; set; }

        public ImageToRecognize()
        {
            Image = null;
            InProgress = false;
        }
    }
}
