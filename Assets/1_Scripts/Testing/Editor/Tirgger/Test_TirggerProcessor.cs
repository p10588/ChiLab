using Chi.Gameplay.Items;
using Chi.Gameplay.Triggers;
using NSubstitute;
using NUnit.Framework;
using System;
using UnityEngine;

namespace Chi.Testing
{

    [TestFixture]
    public class Test_TriggerProcessor
    {
        ITrigger trigger;
        GameObject testOtherObj;
        Collider collider;

        const string PATH = "Assets/1_Scripts/Testing/TestAssets/Triggers/OtherObj.prefab";
        const string TAG = "Player";

        [SetUp] // Do initalize before Test Run 
        public void SetUp() {
            this.testOtherObj = Utilities.TestingUtilities.LoadTestAsset<GameObject>(PATH);
            this.collider = this.testOtherObj.GetComponent<Collider>();
            this.collider.tag = TAG;
            this.trigger = Substitute.For<ITrigger>();
            this.trigger.tags.Returns(new string[1] { TAG });
        }

        [Test]
        public void Test_InitalizeTriggerProcessor() {

            Assert.Throws<NotImplementedException>(
                () => {
                    ITriggerProcessor processor
                    = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Normal,
                                                                       this.trigger);
                    processor.InitalizeTriggerProcessor<object>(null);
                }
            );
        }

        [Test]
        public void Test_InitalizeTriggerAction() {
            try {
                ITriggerProcessor processor
                    = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Normal,
                                                                       this.trigger);
                processor.InitalizeTriggerAction(null, null);
                Assert.AreEqual(true, processor.IsInitalized);

            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test_UninitalizeTriggerProcessor() {
            try {
                ITriggerProcessor processor
                    = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Normal,
                                                                       this.trigger);
                processor.UninitalizeTriggerProcessor();

            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test_TriggerEnterProcess() {
            try {
                ITriggerProcessor processor
                    = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Normal,
                                                                       this.trigger);
                processor.TriggerEnterProcess(this.collider);

            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test_TriggerExitProcess() {
            try {
                ITriggerProcessor processor
                    = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Normal,
                                                                       this.trigger);
                processor.TriggerExitProcess(this.collider);

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
