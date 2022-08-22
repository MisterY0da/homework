using System;

namespace Shapes
{
    public static class Calculator
    {
        public static void Calculate()
        {
            IShape shape;
            Console.Write("Введите название фигуры (круг/треугольник): ");
            string shapeName = Console.ReadLine();
            switch(shapeName)
            {
                case "круг":
                    Console.Write("Введите радиус круга: ");
                    double rad = double.Parse(Console.ReadLine());
                    shape = new Circle(rad);
                    Console.WriteLine("Площадь круга: " + shape.GetArea());
                    break;

                case "треугольник":
                    Console.WriteLine("Введите стороны треугольника: ");
                    double a = double.Parse(Console.ReadLine());
                    double b = double.Parse(Console.ReadLine());
                    double c = double.Parse(Console.ReadLine());
                    shape = new Triangle(a, b, c);
                    Console.WriteLine("Площадь треугольника: " + shape.GetArea());
                    break;

                default:
                    Console.Write("Некорректный ввод!");
                    break;
            }            
        }
    }
}
