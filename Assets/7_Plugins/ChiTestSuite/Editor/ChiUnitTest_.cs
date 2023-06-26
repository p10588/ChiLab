using NUnit.Framework;

public class ChiUnitTest_
{
    [Test]
    [TestCase(-1, 4, ExpectedResult = 3)]
    [TestCase(-2, 8, ExpectedResult = 6)]
    [TestCase(1, 1, ExpectedResult = 6)]
    public int Test_Sample(int i, int j) {
        return i + j;
    }
}
