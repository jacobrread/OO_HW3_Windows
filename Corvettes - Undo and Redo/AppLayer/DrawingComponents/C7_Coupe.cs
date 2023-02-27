namespace AppLayer.DrawingComponents
{
    public class C7_Coupe : Corvette
    {
        public static string Name { get; } = "C7 Coupe";

        public bool HasPineCones { get; set; }
        public override string CorvetteName => Name;

        public override string ResourceName => "c7_coupe.png";
    }
}
