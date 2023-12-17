using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventKit;

[TestClass]
public class Tests
{

    [TestMethod]
    public void TestPoint()
    {
        Assert.AreEqual(new Point(1, 1), new Point(1, 1));
        Assert.AreNotEqual(new Point(1, 2), new Point(1, 1));

        Point[] list = [new Point(1, 1), new Point(1, 2), new Point(1, 1)];
        Assert.IsTrue(list.Contains(new Point(1, 2)));

        ISet<Point> set = new HashSet<Point> { new Point(1, 1), new Point(1, 2), new Point(1, 1) };
        Assert.AreEqual(2, set.Count);
        Assert.IsTrue(set.Contains(new Point(1, 2)));
    }
}
