using NUnit.Framework;
using System;
using UnityEngine;
using Chi.Utilities.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Chi.Testing
{

    [TestFixture]
    public class Test_Extensions
    {
        [SetUp] // Do initalize before Test Run 
        public void SetUp() {

        }

        [Test]
        public void Test_ListIsNullorEmpty() {
            List<GameObject> testList = null;
            bool result = testList.IsNullorEmpty();
            Debug.Log(result);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Test_ListHaveNullElements() {
            List<GameObject> testList = new List<GameObject>(2) { new GameObject(), null };
            bool result = testList.HaveNullElements();
            Debug.Log(result);
            Assert.AreEqual(true, result);

            testList = new List<GameObject>(1) { new GameObject()};
            result = testList.HaveNullElements();
            Debug.Log(result);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Test_ArrayIsNullorEmpty() {
            GameObject[] testAry = new GameObject[0];
            bool result = testAry.IsNullorEmpty();
            Debug.Log(result);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Test_ArrayHaveNullElements() {
            GameObject[] testAry = new GameObject[2] { new GameObject(), null };
            bool result = testAry.HaveNullElements();
            Debug.Log(result);
            Assert.AreEqual(true, result);

            testAry = new GameObject[1] { new GameObject()};
            result = testAry.HaveNullElements();
            Debug.Log(result);
            Assert.AreEqual(false, result);
        }

        [TestCase(-1, 4)]
        public void TestCase_Assert(int i, int j) {
            var result = false;
            Assert.AreEqual(false, result);
        }


        [Test]
        public void Test_VoidMethod() {
            try {


            } catch (Exception e) {
                Debug.LogError(e);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test_Exception() {

            //Assert.Throws<ArgumentNullException>(
            //    () => { /* Do Some Test */ }
            //);
        }

        [TearDown] // Do uninitalize after Test run
        public void TearDown() {
        }

        private T LoadTestAsset<T>(string path) where T : UnityEngine.Object {
            T obj = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
            if (!obj) Debug.LogError("Load Asset Fail");
            return obj;
        }

    }
}
