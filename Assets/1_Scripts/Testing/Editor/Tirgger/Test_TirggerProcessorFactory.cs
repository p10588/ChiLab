using Chi.Gameplay.Quest;
using Chi.Gameplay.Triggers;
using NSubstitute;
using NUnit.Framework;
using System;
using UnityEngine;

namespace Chi.Testing
{

    [TestFixture]
    public class Test_TriggerProcessorFactory
    {
        [SetUp] // Do initalize before Test Run 
        public void SetUp() {

        }

        [Test]
        public void Test_RequireTriggerProcressor() {
            try {
                ITriggerProcessor processor
                    = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Normal,
                                                                       (ITrigger)null);
                Assert.AreEqual(true, processor != null);
                Assert.AreEqual(TriggerProcessorType.Normal, processor.TriggerProcessorType);

                processor = TriggerProcessorFactory.RequireTriggerProcressor(
                    TriggerProcessorType.Item,
                    (ITrigger)null
                );
                Assert.AreEqual(true, processor != null);
                Assert.AreEqual(TriggerProcessorType.Item, processor.TriggerProcessorType);

                IQuestProcessor questProcessor =
                    Substitute.For<IQuestProcessor>();
                processor = TriggerProcessorFactory.RequireTriggerProcressor(
                        TriggerProcessorType.Quest, (ITrigger)null);
                Assert.AreEqual(true, processor != null);
                Assert.AreEqual(TriggerProcessorType.Quest, processor.TriggerProcessorType);

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
