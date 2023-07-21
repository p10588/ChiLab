using NUnit.Framework;

namespace Chi.Utilities.Testing
{

    [TestFixture]
    public class ChiUnitTest_
    {
        Math_Test _math;

        [SetUp] // Do initalize before Test Run 
        public void SetUp() {
            this._math = new Math_Test();
        }

        [Test]
        public void FirstTest() {
            var result = this._math.Test_Sample_Add(-10, -1);
            Assert.AreEqual(result, -11);
        }

        [TestCase(-1, 4, ExpectedResult = 3)]
        public void TestCase_Assert(int i, int j) {
            var result = this._math.Test_Sample_Add(i, j);
            Assert.AreEqual(result, -11);
        }

        [TestCase(-1, 4, ExpectedResult = 3)]
        [TestCase(-2, 8, ExpectedResult = 6)]
        [TestCase(1, 1, ExpectedResult = 2)]
        public int TestCase_ExpectedResult(int i, int j) {
            return this._math.Test_Sample_Add(i, j);
            
        }

        public void Test_ObjectPool() {
            ObjectPoolExtensions.CreatePool<Math_Test>(10);
        }


        [TearDown] // Do uninitalize after Test run
        public void TearDown() {
            this._math = null;
        }

    }


    public class Math_Test
    {
        public int Test_Sample_Add(int i, int j) {
            return i + j;
        }
    }
}
