namespace Areas;

public class Circle: IShape
{
    public double Radius { get; private set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public double GetArea()
    {
        return 3.14 * (Radius * Radius);
    }
}