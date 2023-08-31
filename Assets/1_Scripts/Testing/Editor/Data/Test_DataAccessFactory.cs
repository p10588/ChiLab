using Chi.Gameplay.Quest;
using Chi.Utilities.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chi.Testing
{

    [TestFixture]
    public class Test_DataAccessorFactory
    {

        const string PATH = "/Volumes/Chain/Unity Project/ChiLab/Assets/1_Scripts/Testing/TestAssets/Data/SampleDataCSV.csv";

        [SetUp] // Do initalize before Test Run 
        public void SetUp() {

        }


        [Test]
        public void Test_RequireDataAccess() {
            try {
                List<QuestData> test = DataAccessFactory.RequireDataAccess<List<QuestData>>(
                        ServiceType.LocalPath,
                        PATH
                      );
                Debug.Log(test.ToString());
                Assert.AreEqual(true, test != null);
                Assert.AreEqual(true, test[0].IsRoot == true);
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

        //[TearDown] // Do uninitalize after Test run
        public void TearDown() {
        }

    }
}