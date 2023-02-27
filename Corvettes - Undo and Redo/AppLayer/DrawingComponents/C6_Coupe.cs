namespace AppLayer.DrawingComponents
{
    public class C6_Coupe : Corvette
    {
        public static string Name { get; } = "C6 Coupe";

        public bool HasSwing { get; set; }

        public override string CorvetteName => Name;

        public override string ResourceName => "c6_coupe.png";
    }
}
