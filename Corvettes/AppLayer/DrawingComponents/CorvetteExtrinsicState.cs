using System.Drawing;
using System.Runtime.Serialization;

namespace AppLayer.DrawingComponents
{
    [DataContract]
    public class CorvetteExtrinsicState
    {
        [DataMember]
        public string CorvetteType { get; set; }

        [DataMember]
        public Point Location { get; set; }

        [DataMember]
        public Size Size { get; set; }

        [DataMember]
        public bool IsSelected { get; set; }

        public CorvetteExtrinsicState Clone()
        {
            return new CorvetteExtrinsicState()
            {
                CorvetteType = CorvetteType,
                Location = new Point(Location.X, Location.Y),
                Size = new Size(Size.Width, Size.Height),
                IsSelected = IsSelected
            };
        }
    }
}
