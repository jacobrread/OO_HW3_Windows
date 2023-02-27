namespace AppLayer.DrawingComponents
{
    public class CorvettePlacement
    {
        public Corvette Corvette { get; set; }
        public CorvetteExtrinsicState ExtrinsicState { get; set; }
        public CorvettePlacement(Corvette corvette, CorvetteExtrinsicState state)
        {
            Corvette = corvette;
            ExtrinsicState = state;
        }
    }
}
