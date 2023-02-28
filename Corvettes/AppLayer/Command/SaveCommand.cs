using System.IO;

namespace AppLayer.Command
{
    public class SaveCommand : Command
    {
        private readonly string _filename;
        internal SaveCommand(params object[] commandParameters)
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
            TargetDrawing.SaveToStream(writer.BaseStream);
            writer.Close();

            return true;
        }

        internal override void Undo()
        {
            // Do nothing -- we don't want to delete the saved file.
        }

        internal override void Redo()
        {
            Execute();
        }
    }
}
