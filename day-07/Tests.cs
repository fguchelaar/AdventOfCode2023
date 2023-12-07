using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day_07;

[TestClass]
public class Tests
{
    const string example = """
            32T3K 765
            T55J5 684
            KK677 28
            KTJJT 220
            QQQJA 483
            """;

    [TestMethod]
    public void TestOrderWithJokers()
    {
        var hand1 = new Hand("JKKK2 123") { UseJokers = true };
        var hand2 = new Hand("QQQQ2 123") { UseJokers = true };
        Assert.IsTrue(hand1.CompareTo(hand2) < 0);
    }

    [TestMethod]
    public void TestTypes()
    {
        Assert.AreEqual(2, new Hand("32T3K 765").Type);
        Assert.AreEqual(4, new Hand("T55J5 684").Type);
        Assert.AreEqual(3, new Hand("KK677 28").Type);
        Assert.AreEqual(3, new Hand("KTJJT 220").Type);
        Assert.AreEqual(4, new Hand("QQQJA 483").Type);

        Assert.AreEqual(2, new Hand("32T3K 765") { UseJokers = true }.Type);
        Assert.AreEqual(6, new Hand("T55J5 684") { UseJokers = true }.Type);
        Assert.AreEqual(3, new Hand("KK677 28") { UseJokers = true }.Type);
        Assert.AreEqual(6, new Hand("KTJJT 220") { UseJokers = true }.Type);
        Assert.AreEqual(6, new Hand("QQQJA 483") { UseJokers = true }.Type);
        Assert.AreEqual(7, new Hand("JJJJA 483") { UseJokers = true }.Type);
        Assert.AreEqual(7, new Hand("JJJJJ 483") { UseJokers = true }.Type);
    }

    [TestMethod]
    public void TestPart1()
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(6440, puzzle.Part1());
    }

    [TestMethod]
    public void TestPart2()
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(5905, puzzle.Part2());
    }
}
