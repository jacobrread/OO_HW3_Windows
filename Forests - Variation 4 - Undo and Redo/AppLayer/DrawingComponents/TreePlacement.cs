namespace AppLayer.DrawingComponents
{
    public class TreePlacement
    {
        public Corvette Tree { get; set; }
        public CorvetteExtrinsicState ExtrinsicState { get; set; }
        public TreePlacement(Corvette tree, CorvetteExtrinsicState state)
        {
            Tree = tree;
            ExtrinsicState = state;
        }
    }
}
