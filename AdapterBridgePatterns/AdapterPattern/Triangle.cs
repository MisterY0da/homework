namespace AdapterPattern
{
    public class Triangle : IShape
    {
        public double SideOne { get; private set; }
        public double SideTwo { get; private set; }
        public double SideThree { get; private set; }

        public Triangle(double sideOne, double sideTwo, double sideThree)
        {
            SideOne = sideOne;
            SideTwo = sideTwo;
            SideThree = sideThree;
        }

        public void Draw()
        {
            var perimeter = Math.Round(SideOne + SideTwo + SideThree);
            double sp = perimeter / 2;
            var area = Math.Round(Math.Sqrt(sp * (sp - SideOne) * (sp - SideTwo) * (sp - SideThree)));            
            Console.WriteLine("I'm a triangle " + " with area:" + area + " and perimeter:" + perimeter);
        }
    }
}
