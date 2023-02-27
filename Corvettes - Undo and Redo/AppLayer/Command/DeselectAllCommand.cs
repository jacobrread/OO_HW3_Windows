using System.Collections.Generic;
using AppLayer.DrawingComponents;

namespace AppLayer.Command
{
    public class DeselectAllCommand : Command
    {
        private List<CorvettePlacement> _selectedTreePlacements;
        internal DeselectAllCommand() { }

        public override bool Execute()
        {
            if (TargetDrawing == null) return false;

            _selectedTreePlacements = TargetDrawing?.DeselectAll();
            return _selectedTreePlacements != null && _selectedTreePlacements.Count > 0;
        }

        internal override void Undo()
        {
            if (TargetDrawing == null ||
                _selectedTreePlacements == null ||
                _selectedTreePlacements.Count == 0) return;

            foreach (var placement in _selectedTreePlacements)
                TargetDrawing.ChangeSelection(placement, true);
        }

        internal override void Redo()
        {
            Execute();
        }

    }


}
