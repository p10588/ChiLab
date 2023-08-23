using Chi.Gameplay.Quest;
using Chi.Gameplay.Triggers;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chi.Testing
{

    [TestFixture]
    public class Test_QuestTriggerProcessor
    {
        ITrigger trigger;
        GameObject testOtherObj;
        Collider collider;

        const string PATH = "Assets/1_Scripts/Testing/TestAssets/Triggers/OtherObj.prefab";
        const string TAG = "Player";
        const string PATH_CSV
            = "Assets/1_Scripts/Gameplay/QuestSystem/QuestData/SampleData.csv";
        const string PATH_TRIGGER_GROUP
            = "Assets/1_Scripts/Testing/TestAssets/QuestSystem/OOO.prefab";

        [SetUp] // Do initalize before Test Run 
        public void SetUp() {
            this.trigger = Substitute.For<ITrigger,IQuestTrigger>();

            this.testOtherObj = Utilities.TestingUtilities.LoadTestAsset<GameObject>(PATH);
            this.collider = this.testOtherObj.GetComponent<Collider>();
            this.collider.tag = TAG;
            this.trigger.tags.Returns(new string[1] { TAG });
        }

        [Test]
        public void Test_InitalizeTriggerProcessor() {
            try {
                ITriggerProcessor triggerProcessor
                    = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Quest, this.trigger);
                IQuestProcessor processor = Substitute.For<IQuestProcessor>();
                triggerProcessor.InitalizeTriggerProcessor<IQuestProcessor>(processor);

                Assert.AreEqual(true, triggerProcessor.IsInitalized);

            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test_InitalizeTriggerAction() {

            Assert.Throws<NotImplementedException>(
                () => {
                    ITriggerProcessor processor
                    = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Quest, this.trigger);
                    processor.InitalizeTriggerAction(null,null);
                }
            );
            
        }

        [Test]
        public void Test_FullTriggerTest() {
            try {
                ITriggerProcessor triggerProcessor
                    = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Quest, this.trigger);

                TextAsset testTextAsset = Utilities.TestingUtilities.LoadTestAsset<TextAsset>(PATH_CSV);
                GameObject testQuestGroup = Utilities.TestingUtilities.LoadTestAsset<GameObject>(PATH_TRIGGER_GROUP);

                List<GameObject> gameObjects = new List<GameObject>(1) { testQuestGroup };

                IQuestManager _questManager = Substitute.For<IQuestManager>();
                _questManager.questDataSource.Returns(testTextAsset.text);
                _questManager.triggerGroupRoot.Returns(gameObjects);
                QuestManagerProcessor mgrProcessor = new QuestManagerProcessor(_questManager);
                QuestData questData = new QuestData();
                IQuestProcessor processor
                    = QuestProcessorFactory.RequireQuestProcessor(QuestType.Normal,questData, mgrProcessor);

                triggerProcessor.InitalizeTriggerProcessor<IQuestProcessor>(processor);
                triggerProcessor.TriggerEnterProcess(this.collider);

                Assert.AreEqual(QuestProcState.InProgress, processor.CurQuestState);

                triggerProcessor.TriggerExitProcess(this.collider);
                Assert.AreEqual(QuestProcState.Active, processor.CurQuestState);

            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test_TriggerEnterProcess() {
            try {
                ITriggerProcessor triggerProcessor
                    = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Quest, this.trigger);
                IQuestProcessor processor = Substitute.For<IQuestProcessor>();
                triggerProcessor.InitalizeTriggerProcessor<IQuestProcessor>(processor);
                triggerProcessor.TriggerEnterProcess(collider);


            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test_TriggerExitProcess() {
            try {
                ITriggerProcessor triggerProcessor
                    = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Quest, this.trigger);
                IQuestProcessor processor = Substitute.For<IQuestProcessor>();
                triggerProcessor.InitalizeTriggerProcessor<IQuestProcessor>(processor);
                triggerProcessor.TriggerExitProcess(collider);


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
