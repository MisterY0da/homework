namespace Areas;

public class Triangle : IShape
{
    public double A{ get; private set; }
    public double B{ get; private set; }
    public double C{ get; private set; }

    public Triangle(double first, double second, double third)
    {
        A = first;
        B = second;
        C = third;
    }

    public double GetArea()
    {
        double semiperimeter = (A + B + C) / 2;
        return Math.Sqrt(semiperimeter*(semiperimeter-A)*(semiperimeter-B)*(semiperimeter-C));
    }

    public bool IsRight()
    {
        return (A * A == B * B + C * C) || (B * B == A * A + C * C) || (C * C == A * A + B * B);
    }
}