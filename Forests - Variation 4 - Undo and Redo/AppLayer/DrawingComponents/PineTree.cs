namespace AppLayer.DrawingComponents
{
    public class PineTree : Corvette
    {
        public static string Name { get; } = "Pine";

        public bool HasPineCones { get; set; }
        public override string CorvetteName => Name;

        public override string ResourceName => "Pine Tree.png";
    }
}
