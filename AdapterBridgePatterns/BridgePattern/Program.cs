using BridgePattern.Interfaces;
using BridgePattern.Shapes;

List<IShape> shapes = new List<IShape>() { new BigRedCircle(), new BigWhiteRectangle(), new SmallWhiteTriangle() };
foreach(var shape in shapes)
{
    shape.GetShape();
}