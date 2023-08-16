using Chi.Gameplay.Quest;
using NUnit.Framework;
using NSubstitute;
using System;
using UnityEngine;
using Chi.Utilities.Testing;

namespace Chi.Testing
{

    [TestFixture]
    public class Test_QuestDataHandler
    {
        private string _testData;
        const string PATH_CSV
            = "Assets/1_Scripts/Gameplay/QuestSystem/QuestData/SampleData.csv";

        [SetUp] // Do initalize before Test Run 
        public void SetUp() {
            _testData = TestingUtilities.LoadTestAsset<TextAsset>(PATH_CSV).text;
        }

        [Test]
        public void Test_GetQuestDataSheet() {
            try {
                QuestDataSheetHandler dataHandler = new QuestDataSheetHandler();
                QuestDataSheet result = dataHandler.GetQuestDataSheet(_testData);
                Assert.AreEqual(8, result.QuestDatas.Count);
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
