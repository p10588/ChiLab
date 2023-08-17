using Chi.Gameplay.Quest;
using NUnit.Framework;
using NSubstitute;
using System;
using UnityEngine;

namespace Chi.Testing
{

    [TestFixture]
    public class Test_QuestProcessorFactory
    {

        [SetUp] // Do initalize before Test Run 
        public void SetUp() {

        }

        [Test]
        public void Test_RequireQuestProcessor() {
            try {
                QuestData questData = new QuestData() {
                    QuestName = "TEST QUEST DATA"
                };
                IQuestProcessor processor
                    = QuestProcessorFactory.RequireQuestProcessor(QuestType.Normal, questData, null);
                processor.ChangeProc(QuestProcState.Active);
                var result = processor.CurQuestState;
                Assert.AreEqual(QuestProcState.Active, result);
                Assert.AreEqual(true, processor.QuestData != null);
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
