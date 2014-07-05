namespace Zirpl.AppEngine.CodeGeneration.Transformation
{
    //public class LogHelper
    //{
    //    public GeneratedTextTransformation TextTransformation { get; private set; }
    //    public LogHelper(GeneratedTextTransformation textTransformation)
    //    {
    //        this.TextTransformation = textTransformation;
    //    }
    //    /// <summary>
    //    /// Writes a line to the build pane in visual studio and activates it
    //    /// </summary>
    //    /// <param name="message">Text to output - a \n is appended</param>
    //    public void WriteLineToBuildPane(string message)
    //    {
    //        WriteToBuildPane(String.Format("{0}\n", message));
    //    }

    //    /// <summary>
    //    /// Writes a string to the build pane in visual studio and activates it
    //    /// </summary>
    //    /// <param name="message">Text to output</param>
    //    public void WriteToBuildPane(string message)
    //    {
    //        IVsOutputWindow outWindow = (this.TextTransformation.Host as IServiceProvider).GetService(
    // typeof(SVsOutputWindow)) as IVsOutputWindow;
    //        Guid generalPaneGuid =
    // Microsoft.VisualStudio.VSConstants.OutputWindowPaneGuid.BuildOutputPane_guid;
    //        // P.S. There's also the GUID_OutWindowDebugPane available.
    //        IVsOutputWindowPane generalPane;
    //        outWindow.GetPane(ref generalPaneGuid, out generalPane);
    //        generalPane.OutputString(message);
    //        generalPane.Activate(); // Brings this pane into view
    //    }
    //}
}
