using System.Collections.Generic;
using AppLayer.DrawingComponents;

namespace AppLayer.Command
{
    public class RemoveSelectedCommand : Command
    {
        private List<CorvettePlacement> _deletedPlacements;

        internal RemoveSelectedCommand() { }

        public override bool Execute()
        {
            if (TargetDrawing == null) return false;

            _deletedPlacements = TargetDrawing.DeleteAllSelected();
            return _deletedPlacements != null && _deletedPlacements.Count > 0;
        }

        internal override void Undo()
        {
            if (TargetDrawing == null || _deletedPlacements == null || _deletedPlacements.Count == 0) return;

            foreach (var placement in _deletedPlacements)
                TargetDrawing.Add(placement);
        }

        internal override void Redo()
        {
            if (TargetDrawing == null || _deletedPlacements == null || _deletedPlacements.Count == 0) return;

            foreach (var placement in _deletedPlacements)
                TargetDrawing.DeleteCorvette(placement);
        }

    }
}
