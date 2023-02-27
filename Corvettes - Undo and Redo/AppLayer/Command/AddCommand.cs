using AppLayer.DrawingComponents;
using System;
using System.Drawing;

namespace AppLayer.Command
{
    public class AddCommand : Command
    {
        private const int NormalWidth = 80;
        private const int NormalHeight = 80;

        private readonly string _corvetteType;
        private Point _location;
        private readonly float _scale;
        private CorvettePlacement _placementAdded;

        internal AddCommand() { }

        /// <summary>
        /// Constructor
        /// 
        /// </summary>
        /// <param name="commandParameters">An array of parameters, where
        ///     [1]: string     corvette type
        ///     [2]: Point      center location for the corvette, defaut = top left corner
        ///     [3]: float      scale factor</param>
        internal AddCommand(params object[] commandParameters)
        {
            if (commandParameters.Length>0)
                _corvetteType = commandParameters[0] as string;

            if (commandParameters.Length > 1)
                _location = (Point) commandParameters[1];
            else
                _location = new Point(0, 0);

            if (commandParameters.Length > 2)
                _scale = (float) commandParameters[2];
            else
                _scale = 1.0F;
        }

        public override bool Execute()
        {
            if (string.IsNullOrWhiteSpace(_corvetteType) || TargetDrawing==null) return false;

            var corvetteSize = new Size()
            {
                Width = Convert.ToInt16(Math.Round(NormalWidth * _scale, 0)),
                Height = Convert.ToInt16(Math.Round(NormalHeight * _scale, 0))
            };

            var extrinsicState = new CorvetteExtrinsicState()
            {
                CorvetteType = _corvetteType,
                Location = new Point(_location.X - corvetteSize.Width / 2, _location.Y - corvetteSize.Height / 2),
                Size = corvetteSize
            };
            var corvette = TargetDrawing.CorvetteFactory.CreateCorvette(_corvetteType);
            if (corvette == null) return false;

            _placementAdded = new CorvettePlacement(corvette, extrinsicState);
            TargetDrawing.Add(_placementAdded);

            return true;
        }

        internal override void Undo()
        {
            TargetDrawing.DeleteCorvette(_placementAdded);
        }

        internal override void Redo()
        {
            TargetDrawing.Add(_placementAdded);
        }
    }
}
