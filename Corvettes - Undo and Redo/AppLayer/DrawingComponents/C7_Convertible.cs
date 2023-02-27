namespace AppLayer.DrawingComponents
{
    public class C7_Convertible : Corvette
    {
        public static string Name { get; } = "C7 Convertible";

        public int NumberOfSquirrels { get; set; }
        public override string CorvetteName => Name;

        public override string ResourceName => "c7_convertible.png";
    }
}
