using System.Drawing;
using AppLayer.DrawingComponents;

namespace AppLayer.Command
{
    public class SelectCommand : Command
    {
        private readonly Point _location;
        private CorvettePlacement _placement;
        private bool? _originalState;


        internal SelectCommand(params object[] commandParameters)
        {
            if (commandParameters.Length>0)
            _location = (Point) commandParameters[0];
        }

        public override bool Execute()
        {
            if (TargetDrawing == null) return false;

            _originalState = null;
            _placement = TargetDrawing.FindCorvetteAtPosition(_location);
            if (_placement == null) return false;

            _originalState = _placement.ExtrinsicState.IsSelected;
            TargetDrawing.ChangeSelection(_placement, !_originalState.Value);

            return true;
        }

        internal override void Undo()
        {
            if (_originalState == null) return;

            TargetDrawing.ChangeSelectionAtPosition(_location, _originalState);
        }

        internal override void Redo()
        {
            Execute();
        }

    }
}
