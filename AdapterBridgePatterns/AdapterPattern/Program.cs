using AdapterPattern;

List<IShape> shapes = new List<IShape>() { new Triangle(3, 4, 5), new Rectangle(3,5), new Circle(3), new EllipseAdapter(new Ellipse(3,5)) };
foreach (var shape in shapes)
{
    shape.Draw();
}