namespace AppLayer.DrawingComponents
{
    public class OakTree : Corvette
    {
        public static string Name { get; } = "Oak";

        public int NumberOfSquirrels { get; set; }
        public override string CorvetteName => Name;

        public override string ResourceName => "Oak Tree.png";
    }
}
