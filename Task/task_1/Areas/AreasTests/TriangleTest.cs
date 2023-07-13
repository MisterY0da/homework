using Areas;

namespace AreasTests;

[TestClass]
public class TriangleTest
{
    [TestMethod]
    public void Returns6()
    {
        Assert.AreEqual(new Triangle(3, 4, 5).GetArea(), 6);
    }

    [TestMethod]
    public void IsRight()
    {
        bool result = new Triangle(3, 4, 5).IsRight();
        Assert.IsTrue(result);
    }
}