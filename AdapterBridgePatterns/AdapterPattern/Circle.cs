namespace AdapterPattern
{
    public class Circle : IShape
    {
        public double Radius { get; private set; } 

        public Circle(double radius)
        {
            Radius = radius;
        }

        public void Draw()
        {
            var area = Math.Round(3.14 * Radius * Radius);
            var perimeter = Math.Round(2 * 3.14 * Radius);
            Console.WriteLine("I'm a circle! " + " with area:" + area + " and perimeter:" + perimeter);
        }
    }
}
