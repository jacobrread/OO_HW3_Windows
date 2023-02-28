using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace AppLayer.Command
{
    public class ScreenshotCommand : Command
    {
        private readonly string _filename;
        internal ScreenshotCommand(params object[] commandParameters)
        {
            if (commandParameters.Length > 0)
                _filename = commandParameters[0] as string;
        }

        public override bool Execute()
        {
            if (TargetDrawing == null) return false;

            var targetFilename = _filename;
            if (string.IsNullOrWhiteSpace(targetFilename))
                targetFilename = TargetDrawing.Filename;

            if (string.IsNullOrWhiteSpace(targetFilename))
                return false;

            var writer = new StreamWriter(targetFilename);
            TargetDrawing.SaveScreanshotStream(writer.BaseStream);
            writer.Close();

            return true;
        }

        internal override void Redo()
        {
            throw new System.NotImplementedException();
        }

        internal override void Undo()
        {
            Execute();
        }
    }
}
