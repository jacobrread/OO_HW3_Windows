﻿using System.Collections.Generic;
using System.IO;
using AppLayer.DrawingComponents;

namespace AppLayer.Command
{
    public class LoadCommand : Command
    {
        private readonly string _filename;
        private List<TreePlacement> _previousTreePlacements;

        internal LoadCommand() { }
        internal LoadCommand(params object[] commandParameters)
        {
            if (commandParameters.Length > 0)
                _filename = commandParameters[0] as string;
        }

        public override bool Execute()
        {
            if (string.IsNullOrWhiteSpace(_filename) || TargetDrawing==null)
                return false;

            _previousTreePlacements = TargetDrawing.GetTreePlacements();

            var reader = new StreamReader(_filename);
            TargetDrawing.LoadFromStream(reader.BaseStream);
            reader.Close();

            TargetDrawing.Filename = _filename;

            return true;
        }

        internal override void Undo()
        {
            if (TargetDrawing == null) return;

            TargetDrawing.Clear();

            if (_previousTreePlacements == null || _previousTreePlacements.Count == 0) return;

            foreach (var placement in _previousTreePlacements)
                TargetDrawing.Add(placement);
        }

        internal override void Redo()
        {
            Execute();
        }

    }
}
