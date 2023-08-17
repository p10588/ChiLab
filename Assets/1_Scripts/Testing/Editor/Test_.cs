using NUnit.Framework;
using System;
using UnityEngine;

namespace Chi.Testing
{

    [TestFixture]
    public class Test_
    {
        [SetUp] // Do initalize before Test Run 
        public void SetUp() {

        }

        [Test]
        public void Test() {
            var result = false;
            Assert.AreEqual(false, result);
        }

        [TestCase(-1, 4)]
        public void TestCase_Assert(int i, int j) {
            var result = false;
            Assert.AreEqual(false, result);
        }


        [Test]
        public void Test_VoidMethod() {
            try {


            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test_Exception() {

            //Assert.Throws<ArgumentNullException>(
            //    () => { /* Do Some Test */ }
            //);
        }

        [TearDown] // Do uninitalize after Test run
        public void TearDown() {
        }

    }
}
