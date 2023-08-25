using Chi.Gameplay.Items;
using Chi.Gameplay.Triggers;
using NSubstitute;
using NUnit.Framework;
using System;
using UnityEngine;

namespace Chi.Testing
{

    [TestFixture]
    public class Test_ItemTriggerProcessor
    {
        GameObject otherObj;
        GameObject ItemObj;
        ITrigger trigger;
        ITriggerProcessor itemTriggerProcessor;

        const string TAG = "Player";
        const string OTHER_OBJ_PATH = "Assets/1_Scripts/Testing/TestAssets/Triggers/OtherObj.prefab";
        const string ITEM_PATH = "Assets/1_Scripts/Testing/TestAssets/Items/ItemObj.prefab";

        [SetUp] // Do initalize before Test Run 
        public void SetUp() {
            this.trigger = Substitute.For<ITrigger, IItemTrigger>();
            this.trigger.tags.Returns(new string[1] { TAG });
            this.itemTriggerProcessor
                = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Item, this.trigger);
            this.otherObj = Utilities.TestingUtilities.LoadTestAsset<GameObject>(OTHER_OBJ_PATH);
            this.otherObj.tag = TAG;
            this.ItemObj = Utilities.TestingUtilities.LoadTestAsset<GameObject>(ITEM_PATH);
            
        }

        [Test]
        public void Test_InitalizeTriggerAction() {
            try {
                ITriggerProcessor processor
                    = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Item, this.trigger);

                processor.InitalizeTriggerAction(null, null);

                Assert.AreEqual(true, processor.IsInitalized);

            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test_InitalizeTriggerInteract() {
            try {
                ITriggerProcessor processor
                    = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Item, this.trigger);

                bool result = false;

                Action enterAction = () => { result = true; };
                Action exitAction = () => { result = false; };
                processor.InitalizeTriggerAction(enterAction, exitAction);
                Assert.AreEqual(true, processor.IsInitalized);

                processor.TriggerEnterProcess(otherObj.GetComponent<Collider>());
                Assert.AreEqual(true, result);

                processor.TriggerExitProcess(otherObj.GetComponent<Collider>());
                Assert.AreEqual(false, result);

            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test_InitalizeTriggerProcessor() {

            Assert.Throws<NotImplementedException>(
                () => {
                    ITriggerProcessor processor
                    = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Item, this.trigger);
                    processor.InitalizeTriggerProcessor<object>(null);
                }
            );

        }

        [Test]
        public void Test_TriggerEnterProcess() {
            try {
                ITriggerProcessor processor
                    = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Item, this.trigger);
                processor.TriggerEnterProcess(otherObj.GetComponent<Collider>());

            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }

        }

        [Test]
        public void Test_TriggerExitProcess() {

            try {
                ITriggerProcessor processor
                    = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Item, this.trigger);
                processor.TriggerExitProcess(otherObj.GetComponent<Collider>());

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
