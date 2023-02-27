namespace AppLayer.DrawingComponents
{
    public class C7_Coupe2 : Corvette
    {
        public static string Name { get; } = "c7 Coupe 2";

        public int NumberOfBirds { get; set; }
        public override string CorvetteName => Name;

        public override string ResourceName => "c7_coupe2.png";
    }
}
