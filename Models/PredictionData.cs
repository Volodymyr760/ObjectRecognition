namespace ObjectsRecognition.Models
{
    public class PredictionData(string name, string category, float x, float y, float w, float h, float score)
    {
        public string Name { get; set; } = name;

        public string Category { get; set; } = category;

        public float RectangleX { get; set; } = x;

        public float RectangleY { get; set; } = y;

        public float Width { get; set; } = w;

        public float Height { get; set; } = h;

        public float Score { get; set; } = score;
    }
}
