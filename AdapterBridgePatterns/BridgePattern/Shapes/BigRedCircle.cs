using BridgePattern.Colors;
using BridgePattern.Interfaces;
using BridgePattern.Sizes;

namespace BridgePattern.Shapes
{
    public class BigRedCircle : IShape
    {
        public IColor Color { get; set; }
        public ISize Size { get; set; }

        public BigRedCircle()
        {
            Color = new RedColor();
            Size = new BigSize();
        }
        public void GetShape()
        {
            Console.WriteLine(Size.GetSize() + " " + Color.GetColor() + " круг");
        }
    }
}
