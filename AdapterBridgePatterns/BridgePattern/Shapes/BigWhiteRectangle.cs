using BridgePattern.Colors;
using BridgePattern.Interfaces;
using BridgePattern.Sizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgePattern.Shapes
{
    public class BigWhiteRectangle : IShape
    {
        public IColor Color { get; set; }
        public ISize Size { get; set; }

        public BigWhiteRectangle()
        {
            Color = new WhiteColor();
            Size = new BigSize();
        }

        public void GetShape()
        {
            Console.WriteLine(Size.GetSize() + " " + Color.GetColor() + " прямоугольник");
        }
    }
}
