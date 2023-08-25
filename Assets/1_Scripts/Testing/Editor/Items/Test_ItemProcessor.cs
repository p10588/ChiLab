using NUnit.Framework;
using System;
using UnityEngine;
using Chi.Gameplay.Items;
using NSubstitute;

namespace Chi.Testing
{

    [TestFixture]
    public class Test_ItemProcessor
    {
        IItem item;

        [SetUp] // Do initalize before Test Run 
        public void SetUp() {
            this.item = Substitute.For<IItem>();
        }

        [Test]
        public void Test_ChangeProc() {
            try {
                IItemProcessor processor = ItemProcessorFactory.RequireItemProcressor(ItemType.Normal, this.item);
                processor.ChangeProc(ItemStateType.Active);
                Assert.AreEqual(ItemStateType.Active, processor.CurItemState);

                processor.ChangeProc(ItemStateType.Interact);
                Assert.AreEqual(ItemStateType.Interact, processor.CurItemState);

                processor.ChangeProc(ItemStateType.Deactive);
                Assert.AreEqual(ItemStateType.Deactive, processor.CurItemState);

                processor.ChangeProc(ItemStateType.Destroy);
                Assert.AreEqual(ItemStateType.Destroy, processor.CurItemState);


            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }


        //[Test]
        public void Test() {
            var result = false;
            Assert.AreEqual(false, result);
        }

        //[TestCase(-1, 4)]
        public void TestCase_Assert(int i, int j) {
            var result = false;
            Assert.AreEqual(false, result);
        }

        //[Test]
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
