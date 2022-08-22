using System;

namespace Shapes
{
    public class Triangle : IShape
    {
        private double a;
        private double b;
        private double c;

        public Triangle(double first, double second, double third)
        {
            a = first;
            b = second;
            c = third;
        }

        public double GetArea()
        {
            if(IsRight().Equals(true))
            {
                if(a > b && a > c)
                {
                    return (b * c) / 2;
                }

                else if(b > a && b > c)
                {
                    return (a * c) / 2;
                }

                else if(c > a && c > b)
                {
                    return (a * b) / 2;
                }
            }

            double semiper = (a + b + c) / 2;
            return Math.Sqrt(semiper*(semiper-a)*(semiper-b)*(semiper-c));
        }

        public bool IsRight()
        {
            return (a * a == b * b + c * c) || (b * b == a * a + c * c) || (c * c == a * a + b * b);
        }
    }
}
