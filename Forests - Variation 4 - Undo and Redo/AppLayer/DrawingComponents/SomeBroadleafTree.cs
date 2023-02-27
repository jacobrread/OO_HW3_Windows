namespace AppLayer.DrawingComponents
{
    public class SomeBroadleafTree : Corvette
    {
        public static string Name { get; } = "Some Broadleaf";

        public int NumberOfBirds { get; set; }
        public override string CorvetteName => Name;

        public override string ResourceName => "Some Broadleaf Tree.png";
    }
}
