namespace ObjectsRecognition
{
    // Video capture - https://www.youtube.com/watch?v=NyRRkI8MSb4
    // Yolo v5 scoring - https://github.com/techwingslab/yolov5-net
    // design main idea - https://www.youtube.com/watch?v=BtOEztT1Qzk

    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}