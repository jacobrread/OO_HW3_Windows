using System.Collections.Generic;
using AppLayer.DrawingComponents;

namespace AppLayer.Command
{
    public class NewCommand : Command
    {
        private List<CorvettePlacement> _previousPlacements;

        internal NewCommand() {}

        public override bool Execute()
        {
            if (TargetDrawing == null) return false;

            _previousPlacements = TargetDrawing.GetCorvettePlacements();

            TargetDrawing.Clear();

            return true;
        }

        internal override void Undo()
        {
            if (TargetDrawing == null) return;

            TargetDrawing.Clear();

            if (_previousPlacements == null || _previousPlacements.Count == 0) return;

            foreach (var placement in _previousPlacements)
                TargetDrawing.Add(placement);
        }

        internal override void Redo()
        {
            Execute();
        }
    }
}
