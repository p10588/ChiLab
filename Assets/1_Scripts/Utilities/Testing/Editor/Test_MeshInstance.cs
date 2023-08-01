using Chi.Utilities.Graphics;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestSuite
{

    [TestFixture]
    public class Test_MeshInstance {

        private MeshInstanceController controller;
        private const string TEST_ASSETS_PATH = "Assets/1_Scripts/Utilities/Testing/TestAssets/MeshInstance/Cube.prefab";

        [SetUp] // Do initalize before Test Run 
        public void SetUp() {
            GameObject testObj = LoadTestAsset<GameObject>(TEST_ASSETS_PATH);

            //Mock MeshInstance
            IMeshInstance meshInstance = new SimpleMeshInstance();

            //Mock InstanceData
            meshInstance.InstanceData = new InstanceData {
                mesh = testObj.GetComponent<MeshFilter>().sharedMesh,
                material = testObj.GetComponent<MeshRenderer>().sharedMaterial,
                meshTransform = new Transform[1] { testObj.transform}
            };

            this.controller = new MeshInstanceController(meshInstance);
        }

        private T LoadTestAsset<T>(string path) where T : UnityEngine.Object {
            T obj = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(TEST_ASSETS_PATH);
            if (!obj) Debug.LogError("Load Asset Fail");
            return obj;
        }

        [Test]
        public void Test_Initalize() {
            var result = this.controller.Initalize();
            Assert.AreEqual(result, true);
        }

        [Test]
        public void Test_ListorArrayNull() {
            List<Transform> list = new List<Transform>(1) { null };
            var result = list.Any(x => x != null) ;
            Assert.AreEqual(result, true);
        }

        [Test]
        public void Test_DrawMeshInstance() {
            this.controller.DrawMeshInstance();
        }

        /*
                [TestCase(-1, 4)]
                public void TestCase_Assert(int i, int j) {
                    var result = this._math.Test_Sample_Add(i, j);
                    Assert.AreEqual(result, 3);
                }

                [TestCase(-1, 4, ExpectedResult = 3)]
                [TestCase(-2, 8, ExpectedResult = 6)]
                [TestCase(1, 1, ExpectedResult = 2)]
                public int TestCase_ExpectedResult(int i, int j) {

                    return this._math.Test_Sample_Add(i, j);

                }
        */
        [TearDown] // Do uninitalize after Test run
        public void TearDown() {
            this.controller = null;
        }

    }

}
