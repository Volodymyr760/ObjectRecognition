namespace ObjectsRecognition.Models
{
    public record YoloSignModel() : YoloModel(
    640, //Width
    640, //Height
    3, //Depth

    18, //Dimensions

    new[] { 8, 16, 32 }, //Strides

    //Anchors
    new[]
    {
        new[] { new[] { 010, 13 }, new[] { 016, 030 }, new[] { 033, 023 } },
        new[] { new[] { 030, 61 }, new[] { 062, 045 }, new[] { 059, 119 } },
        new[] { new[] { 116, 90 }, new[] { 156, 198 }, new[] { 373, 326 } }
    },

    new[] { 80, 40, 20 }, //Shapes

    0.25f, //Properties.Settings.Default["Confidence"].ToString() == string.Empty ? 0.3f : float.Parse(Properties.Settings.Default["Confidence"].ToString()) / 100,//0.3f, //Confidence
    0.25f, //MulConfidence
    0.45f, //Overlap

    new[] { "output0" }, //Outputs

    new()
    {
        new(1, "10l", "10"),// signs names: ['10l', '10s', '20l', '20s', etc.]
        new(2, "20l", "20"),
        new(3, "30l", "30"),
        new(4, "40l", "40"),
        new(5, "50l", "50"),
        new(6, "60l", "60"),
        new(7, "70l", "70"),
        new(8, "80l", "80"),
        new(9, "90l", "90"),
        new(10, "100l", "100"),
        new(11, "110l", "110"),
        new(12, "120l", "120"),
        new(13, "130l", "130"),
    }, //Labels

    true //UseDetect or false - use Sigmoid
);
}
