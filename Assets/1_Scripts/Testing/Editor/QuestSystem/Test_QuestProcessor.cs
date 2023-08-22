using Chi.Gameplay.Quest;
using NUnit.Framework;
using NSubstitute;
using System;
using UnityEngine;
using Chi.Testing;
using System.Collections.Generic;

namespace Chi.Testing
{

    [TestFixture]
    public class Test_QuestProcessor
    {
        IQuestProcessor _questProcessor;
        IQuestManager _questManager;
        QuestData _questData;
        QuestManagerProcessor questMgrProcessor;

        TextAsset testTextAsset;
        GameObject testQuestGroup;
        IQuestTrigger[] _triggers;

        const string PATH_CSV
            = "Assets/1_Scripts/Gameplay/QuestSystem/QuestData/SampleData.csv";
        const string PATH_TRIGGER_GROUP
            = "Assets/1_Scripts/Testing/TestAssets/QuestSystem/OOO.prefab";

        [SetUp] // Do initalize before Test Run 
        public void SetUp() {

            this.testTextAsset = Utilities.TestingUtilities.LoadTestAsset<TextAsset>(PATH_CSV);
            this.testQuestGroup = Utilities.TestingUtilities.LoadTestAsset<GameObject>(PATH_TRIGGER_GROUP);

            List<GameObject> gameObjects = new List<GameObject>(1) { this.testQuestGroup };

            this._questManager = Substitute.For<IQuestManager>();
            this._questManager.questDataSource.Returns(this.testTextAsset.text);
            this._questManager.triggerGroupRoot.Returns(gameObjects);
            this.questMgrProcessor = new QuestManagerProcessor(this._questManager);

            this.questMgrProcessor.PrepareAndStartQuest();

            this._triggers = this.testQuestGroup.GetComponentsInChildren<IQuestTrigger>();
        }

        [Test]
        public void Test_NewQuestProcessor() {
            try {

                IQuestTrigger testTrigger = this._triggers[1];
                this.questMgrProcessor.SpawnQuestObject(testTrigger, "2_BBB");
                this._questProcessor.ChangeProc(QuestProcState.Active);
                var result = this._questProcessor.CurQuestState;
                Assert.AreEqual(QuestProcState.Active, result);

            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test_ChangeProc() {
            try {

                IQuestTrigger testTrigger = this._triggers[3];
                this._questProcessor = this.questMgrProcessor.SpawnQuestObject(testTrigger, "4_DDD");

                this._questProcessor.ChangeProc(QuestProcState.Active);
                var result = this._questProcessor.CurQuestState;
                Assert.AreEqual(QuestProcState.Active, result);

                this._questProcessor.ChangeProc(QuestProcState.InProgress);
                result = this._questProcessor.CurQuestState;
                Assert.AreEqual(QuestProcState.InProgress, result);

                this._questProcessor.ChangeProc(QuestProcState.Resolve);
                result = this._questProcessor.CurQuestState;
                Assert.AreEqual(QuestProcState.Resolve, result);

                this._questProcessor.ChangeProc(QuestProcState.NonActive);
                result = this._questProcessor.CurQuestState;
                Assert.AreEqual(QuestProcState.NonActive, result);

            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test_ActiveNext() {
            try {

                IQuestTrigger testTrigger = this._triggers[5];
                IQuestProcessor questProcessor
                    = this.questMgrProcessor.SpawnQuestObject(testTrigger, "6_FFF");
                testTrigger.InitalizeTrigger(questProcessor);

                questProcessor.ChangeProc(QuestProcState.Active);
                var result = questProcessor.CurQuestState;
                Assert.AreEqual(QuestProcState.Active, result);

                questProcessor.ChangeProc(QuestProcState.InProgress);
                result = questProcessor.CurQuestState;
                Assert.AreEqual(QuestProcState.InProgress, result);

                questProcessor.ChangeProc(QuestProcState.Resolve);
                result = questProcessor.CurQuestState;
                Assert.AreEqual(QuestProcState.Resolve, result);

                testTrigger = this._triggers[6];
                questProcessor = testTrigger.questProcessor;
                result = questProcessor.CurQuestState;
                Assert.AreEqual(QuestProcState.Active, result);

                questProcessor.ChangeProc(QuestProcState.InProgress);
                result = questProcessor.CurQuestState;
                Assert.AreEqual(QuestProcState.InProgress, result);

                questProcessor.ChangeProc(QuestProcState.Resolve);
                result = questProcessor.CurQuestState;
                Assert.AreEqual(QuestProcState.Resolve, result);

                testTrigger = this._triggers[7];
                questProcessor = testTrigger.questProcessor;
                result = questProcessor.CurQuestState;
                Assert.AreEqual(QuestProcState.Active, result);

                questProcessor.ChangeProc(QuestProcState.InProgress);
                result = questProcessor.CurQuestState;
                Assert.AreEqual(QuestProcState.InProgress, result);

                questProcessor.ChangeProc(QuestProcState.Resolve);
                result = questProcessor.CurQuestState;
                Assert.AreEqual(QuestProcState.Resolve, result);

            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }


        //[TestCase(-1, 4)]
        public void TestCase_Assert(int i, int j) {
            var result = false;
            Assert.AreEqual(false, result);
        }

        //[TestCase(-1, 4, ExpectedResult = 0)]
        //[TestCase(-2, 8, ExpectedResult = 0)]
        //[TestCase(1, 1, ExpectedResult = 0)]
        public int TestCase_ExpectedResult(int i, int j) {

            return 0;
        }

        //[Test]
        public void Test_VoidMethod() {
            try {
                

            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
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
