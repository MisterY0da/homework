namespace AdapterPattern
{
    public class Rectangle : IShape
    {
        public double SideOne { get; private set; }
        public double SideTwo { get; private set; }

        public Rectangle(double sideOne, double sideTwo)
        {
            SideOne = sideOne;
            SideTwo = sideTwo;
        }

        public void Draw()
        {
            var area = Math.Round(SideOne * SideTwo);
            var perimeter = Math.Round(2 * (SideOne + SideTwo));
            Console.WriteLine("I'm a rectangle! " + " with area:" + area + " and perimeter:" + perimeter);
        }
    }
}
