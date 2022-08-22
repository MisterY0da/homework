namespace Shapes
{
    public class Circle : IShape
    {
        private double radius;

        public Circle(double r)
        {
            radius = r;
        }

        public double GetArea()
        {
            return 3.14 * (radius * radius);
        }
    }
}
