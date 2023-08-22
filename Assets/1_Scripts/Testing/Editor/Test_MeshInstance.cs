using Chi.Utilities.Graphics;
using Chi.Testing;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chi.Testing
{

    [TestFixture]
    public class Test_MeshInstance {

        private GameObject testAsset;
        private MeshInstanceController controller;
        private IMeshInstance meshInstance; //Mock MeshInstance

        private const string TEST_ASSETS_PATH = "Assets/1_Scripts/Testing/TestAssets/MeshInstance/Cube.prefab";

        [SetUp] // Do initalize before Test Run 
        public void SetUp() {
            // Load Test Asset
            testAsset = Utilities.TestingUtilities.LoadTestAsset<GameObject>(TEST_ASSETS_PATH);

            meshInstance = Substitute.For<IMeshInstance>();

            //Mock InstanceData
            InstanceData InstanceData_Mock = new InstanceData {
                mesh = testAsset.GetComponent<MeshFilter>().sharedMesh,
                material = testAsset.GetComponent<MeshRenderer>().sharedMaterial,
                meshTransform = new Transform[1] { testAsset.transform}
            };

            this.controller = new MeshInstanceController(meshInstance, InstanceData_Mock);
        }

        [Test]
        public void Test_Initalize() {
            var result = false;
            Assert.DoesNotThrow(() => { result = this.controller.Initalize(); });
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Test_Initalize_DefaultData() {
            InstanceData InstanceData_Mock = default;

            MeshInstanceController controller_NoAll
                = new MeshInstanceController(meshInstance, InstanceData_Mock);

            Assert.Throws<ArgumentNullException>(
                () => { bool result = controller_NoAll.Initalize(); }
            );
        }

        [Test]
        public void Test_Initalize_AllNull() {
            InstanceData InstanceData_Mock = new InstanceData {
                mesh = null,
                material = null,
                meshTransform = null
            };

            MeshInstanceController controller_AllNull
                = new MeshInstanceController(meshInstance, InstanceData_Mock);

            Assert.Throws<ArgumentNullException>(
                () => { bool result = controller_AllNull.Initalize(); }
            );
        }

        [Test()]
        public void Test_Initalize_NoMesh() {

            InstanceData InstanceData_Mock = new InstanceData {
                mesh = null,
                material = testAsset.GetComponent<MeshRenderer>().sharedMaterial,
                meshTransform = new Transform[1] { testAsset.transform }
            };

            MeshInstanceController controller_NoMesh
                = new MeshInstanceController(meshInstance, InstanceData_Mock);

            Assert.Throws<ArgumentNullException>(
                ()=> { bool result = controller_NoMesh.Initalize(); }
            );
        }

        [Test]
        public void Test_Initalize_NoMaterial() {

            InstanceData InstanceData_Mock = new InstanceData {
                mesh = testAsset.GetComponent<MeshFilter>().sharedMesh,
                material = null,
                meshTransform = new Transform[1] { testAsset.transform }
            };

            MeshInstanceController controller_NoMaterial
                = new MeshInstanceController(meshInstance, InstanceData_Mock);

            Assert.Throws<ArgumentNullException>(
                () => { bool result = controller_NoMaterial.Initalize(); }
            );
        }

        [Test]
        public void Test_Initalize_NoMatMesh() {

            InstanceData InstanceData_Mock = new InstanceData {
                mesh = null,
                material = null,
                meshTransform = new Transform[1] { testAsset.transform }
            };

            MeshInstanceController controller_NoMatMesh
                = new MeshInstanceController(meshInstance, InstanceData_Mock);

            Assert.Throws<ArgumentNullException>(
                () => { bool result = controller_NoMatMesh.Initalize(); }
            );
        }

        [Test]
        public void Test_Initalize_NullTranAry() {
            InstanceData InstanceData_Mock = new InstanceData {
                mesh = testAsset.GetComponent<MeshFilter>().sharedMesh,
                material = testAsset.GetComponent<MeshRenderer>().sharedMaterial,
                meshTransform = null
            };

            MeshInstanceController controller_NullTranAry
                = new MeshInstanceController(meshInstance, InstanceData_Mock);

            Assert.Throws<ArgumentNullException>(
                () => { bool result = controller_NullTranAry.Initalize(); }
            );
        }

        [Test]
        public void Test_Initalize_EmptyTranAry() {
            InstanceData InstanceData_Mock = new InstanceData {
                mesh = testAsset.GetComponent<MeshFilter>().sharedMesh,
                material = testAsset.GetComponent<MeshRenderer>().sharedMaterial,
                meshTransform = new Transform[0]
            };

            MeshInstanceController controller_EmptyTranAry
                = new MeshInstanceController(meshInstance, InstanceData_Mock);


            Assert.Throws<ArgumentNullException>(
                () => { bool result = controller_EmptyTranAry.Initalize(); }
            );
        }

        [Test]
        public void Test_Initalize_AnyNullInTranAry() {
            InstanceData InstanceData_Mock = new InstanceData {
                mesh = testAsset.GetComponent<MeshFilter>().sharedMesh,
                material = testAsset.GetComponent<MeshRenderer>().sharedMaterial,
                meshTransform = new Transform[2] { testAsset.transform, null }
            };

            MeshInstanceController controller_AnyNullInTranAry
                = new MeshInstanceController(meshInstance, InstanceData_Mock);

            Assert.Throws<ArgumentNullException>(
                () => { bool result = controller_AnyNullInTranAry.Initalize(); }
            );
        }


        [Test]
        public void Test_Full() {
            GameObject testAssetObj = Utilities.TestingUtilities.LoadTestAsset<GameObject>(TEST_ASSETS_PATH);

            InstanceData InstanceData_Mock = new InstanceData {
                mesh = testAssetObj.GetComponent<MeshFilter>().sharedMesh,
                material = testAssetObj.GetComponent<MeshRenderer>().sharedMaterial,
                meshTransform = new Transform[1] { testAssetObj.transform }
            };

            MeshInstanceController controller_DrawMeshInstance
                = new MeshInstanceController(meshInstance, InstanceData_Mock);
            try {
                controller_DrawMeshInstance.Initalize();
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            try {
                controller_DrawMeshInstance.DrawMeshInstance();
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        [TearDown] // Do uninitalize after Test run
        public void TearDown() {
            this.meshInstance = null;
            this.controller = null;
        }

    }

}
