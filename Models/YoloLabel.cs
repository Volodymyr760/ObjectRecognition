namespace ObjectsRecognition.Models
{
    /// <summary>
    /// Label of detected object.
    /// </summary>
    public record YoloLabel(int Id, string Name, string Category, Color Color, YoloLabelKind Kind)
    {
        public YoloLabel(int id, string name, string category) : this(id, name, category, Color.Yellow, YoloLabelKind.Generic) { }
    }

    /// <summary>
    /// Enum to specify type of detected object.
    /// </summary>
    public enum YoloLabelKind
    {
        Generic
    }
}
