namespace ObjectsRecognition.Models
{
    public record YoloCocoModel1() : YoloModel(
    640, //Width
    640, //Height
    3, //Depth

    12, //Dimensions

    new[] { 8, 16, 32 }, //Strides

    //Anchors
    new[]
    {
        new[] { new[] { 010, 13 }, new[] { 016, 030 }, new[] { 033, 023 } },
        new[] { new[] { 030, 61 }, new[] { 062, 045 }, new[] { 059, 119 } },
        new[] { new[] { 116, 90 }, new[] { 156, 198 }, new[] { 373, 326 } }
    },

    new[] { 80, 40, 20 }, //Shapes

    0.3f, //Properties.Settings.Default["Confidence"].ToString() == string.Empty ? 0.3f : float.Parse(Properties.Settings.Default["Confidence"].ToString()) / 100,//0.3f, //Confidence
    0.25f, //MulConfidence
    0.45f, //Overlap

    new[] { "output0" }, //Outputs

    new()
    {
        new(1, "Ka-52", "Helicopter"),// aircraft names: ['Ka_52', 'MC_21', 'MiG_29', 'Mi_28', 'Mi_38', 'SSJ_100', 'Su_57']
        new(2, "MC-21", "Aircraft"),
        new(3, "MiG-29", "Fighter"),
        new(4, "Mi-28", "Helicopter"),
        new(5, "Mi-38", "Helicopter"),
        new(6, "SSJ-100", "Aircraft"),
        new(7, "Su-57", "Fighter")
    }, //Labels

    true //UseDetect or false - use Sigmoid
);
}
