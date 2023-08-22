using Chi.Gameplay.Quest;
using NUnit.Framework;
using NSubstitute;
using System;
using UnityEngine;
using System.Collections.Generic;
using Chi.Testing;
using Chi.Utilities.Extensions;

namespace Chi.Testing
{

    [TestFixture]
    public class Test_QuestManagerProcessor
    {
        IQuestManager _questManager;
        QuestManagerProcessor _processor;
        IQuestTrigger[] _triggers;

        TextAsset testTextAsset;
        GameObject testQuestGroup;
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
            this._processor = new QuestManagerProcessor(this._questManager);
        }

        [Test]
        public void Test_PrepareAndStartQuest() {
            try {                
                this._processor.PrepareAndStartQuest();

                this._triggers = this.testQuestGroup.GetComponentsInChildren<IQuestTrigger>();
                bool result = true;
                int counter = 0;
                for (int i = 0; i < this._triggers.Length; i++) {
                    if (this._triggers[i].questProcessor == null) break;

                    if (this._triggers[i].questProcessor.QuestData.IsRoot) {
                        result &= this._triggers[i].questProcessor.CurQuestState == QuestProcState.Active;
                        counter++;
                    }
                }

                Assert.AreEqual(true, result);
                Assert.AreEqual(1, counter);
            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
            
        }


        [Test]
        public void Test_SpawnQuest() {
            try {
                Test_PrepareAndStartQuest();
                IQuestProcessor result ;
                QuestData questData = new QuestData {QuestName = "YYY" };

                IQuestTrigger testTrigger = this._triggers[3];
                result = this._processor.SpawnQuestObject(testTrigger, questData);
                Assert.AreEqual(false, testTrigger.questProcessor == null);
                Assert.AreEqual(false, testTrigger.questProcessor.QuestData == null);
                Assert.AreEqual(true, testTrigger.questProcessor.QuestData.QuestName == "YYY");

                testTrigger = this._triggers[5];
                result = this._processor.SpawnQuestObject(testTrigger, "6_FFF");
                Assert.AreEqual(false, testTrigger.questProcessor == null);
                Assert.AreEqual(false, testTrigger.questProcessor.QuestData == null);
                Assert.AreEqual(true, testTrigger.questProcessor.QuestData.QuestName == "FFF");


            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test_RemoveQuest() {
            try {
                Test_PrepareAndStartQuest();
                IQuestProcessor result;
                QuestData questData = new QuestData { QuestName = "YYY" };

                IQuestTrigger testTrigger = this._triggers[5];
                result = this._processor.SpawnQuestObject(testTrigger, "6_FFF");
                Assert.AreEqual(false, testTrigger.questProcessor == null);
                Assert.AreEqual(false, testTrigger.questProcessor.QuestData == null);
                Assert.AreEqual(true, testTrigger.questProcessor.QuestData.QuestName == "FFF");

                this._processor.RemoveQuest(testTrigger);
                Assert.AreEqual(true, testTrigger.questProcessor == null);

            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test_RunAllQuest() {
            try {
                Test_PrepareAndStartQuest();
                List<IQuestTrigger> nextTriggers = new List<IQuestTrigger>();
                nextTriggers.Add(this._triggers[0]);
                do {
                    string ids = null;

                    for (int i = 0; i < nextTriggers.Count; i++) {
                        nextTriggers[i].questProcessor.ChangeProc(QuestProcState.Active);
                        nextTriggers[i].questProcessor.ChangeProc(QuestProcState.InProgress);
                        nextTriggers[i].questProcessor.ChangeProc(QuestProcState.Resolve);
                        Assert.AreEqual(false, nextTriggers[i].questProcessor == null);
                        Assert.AreEqual(false, nextTriggers[i].questProcessor.QuestData == null);
                        if (!string.IsNullOrEmpty(nextTriggers[i].questProcessor.QuestData.NextQuestId))
                            ids += "," + nextTriggers[i].questProcessor.QuestData.NextQuestId;
                    }

                    nextTriggers = new List<IQuestTrigger>();

                    if (!string.IsNullOrEmpty(ids)) {
                        ids = ids.Remove(0, 1);
                        string[] splitedId = ids.Split(',');
                        for (int i = 0; i < splitedId.Length; i++) {
                            string[] id = splitedId[i].Split('_');
                            int index = int.Parse(id[0]) - 1;
                            if (!nextTriggers.Contains(this._triggers[index])) {
                                nextTriggers.Add(this._triggers[index]);
                            }
                        }
                    }

                } while (!nextTriggers.IsNullorEmpty());

                Assert.AreEqual(false, this._triggers[7].questProcessor.QuestData == null);

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
            this._processor = null;
        }

        

    }


}
