using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shapes;

namespace ShapesTests
{
    [TestClass]
    public class CircleTest
    {
        [TestMethod]
        public void ReturnsArea314()
        {
            Assert.AreEqual(new Circle(10).GetArea(), 314);
        }

        [TestMethod]
        public void ReturnsZero()
        {
            Assert.AreEqual(new Circle(0).GetArea(), 0);
        }
    }
}
