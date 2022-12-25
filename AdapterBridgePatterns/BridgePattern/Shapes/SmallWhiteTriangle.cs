using BridgePattern.Colors;
using BridgePattern.Interfaces;
using BridgePattern.Sizes;

namespace BridgePattern.Shapes
{
    public class SmallWhiteTriangle : IShape
    {
        public IColor Color { get; set; }
        public ISize Size { get; set; }

        public SmallWhiteTriangle()
        {
            Color = new WhiteColor();
            Size = new SmallSize();
        }

        public void GetShape()
        {
            Console.WriteLine(Size.GetSize() + " " + Color.GetColor() + " треугольник");
        }
    }
}
