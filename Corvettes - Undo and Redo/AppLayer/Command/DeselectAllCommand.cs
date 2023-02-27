using System.Collections.Generic;
using AppLayer.DrawingComponents;

namespace AppLayer.Command
{
    public class DeselectAllCommand : Command
    {
        private List<CorvettePlacement> _selectedCorvettePlacements;
        internal DeselectAllCommand() { }

        public override bool Execute()
        {
            if (TargetDrawing == null) return false;

            _selectedCorvettePlacements = TargetDrawing?.DeselectAll();
            return _selectedCorvettePlacements != null && _selectedCorvettePlacements.Count > 0;
        }

        internal override void Undo()
        {
            if (TargetDrawing == null ||
                _selectedCorvettePlacements == null ||
                _selectedCorvettePlacements.Count == 0) return;

            foreach (var placement in _selectedCorvettePlacements)
                TargetDrawing.ChangeSelection(placement, true);
        }

        internal override void Redo()
        {
            Execute();
        }

    }


}
