using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Chi.Utilities.Graphics
{
    [System.Serializable]
    public struct InstanceData {
        public Mesh mesh;
        public Material material;
        public Transform[] meshTransform;

        public bool IsValid() {
            try {
                CheckVaild();
                return true;
            } catch(Exception e){
                Debug.LogError(e);
                return false;
            }
        }

        private void CheckVaild() {
            if(mesh == null) throw new ArgumentNullException("Mesh");
            if(material == null) throw new ArgumentNullException("Material");
            if(meshTransform == null) throw new ArgumentNullException("MeshTransform");

            if(meshTransform.Length <= 0)
                throw new ArgumentNullException($"MeshTransform is Empty");

            if(meshTransform.Any(x => x == null))
                throw new ArgumentNullException($"Some Element In MeshTransform are Null");
        }

    }

    public interface IMeshInstance
    {
        InstanceData InstanceData { get; set; }
        Matrix4x4[][] Matrices { get; }
        void PrepareTRSMatrices(Transform[] transforms, int batchAmount, in int batchSize);
        void DrawMeshInstance(InstanceData instanceDatas, int batchAmount);
    }

    [ExecuteInEditMode]
    public class SimpleMeshInstance : MonoBehaviour, IMeshInstance
    {
        [SerializeField]
        private InstanceData instanceData;

        private MeshInstanceController _controller;
        private bool _isInitalized = false;


        private void Update() {

            if (!this._isInitalized) {
                this._controller = new MeshInstanceController(this);

                if (this._controller.Initalize())
                    this._isInitalized = true;
                else {
                    Debug.LogError($"{GetObjectNameAndType(this)} Controller Initalize Fail");
                    return;
                }
            }

            this._controller.DrawMeshInstance();
        }

        private void OnDisable() {
            this._isInitalized = false;
            this._controller = null;
        }

        private string GetObjectNameAndType(UnityEngine.Object @object) {
            string gameObjectName = (@object).name;
            string typeName = @object.GetType().Name;

            return $"{gameObjectName} {typeName}";
        }


        #region IMeshInstance Impementation
        InstanceData IMeshInstance.InstanceData {
            get { return instanceData; }
            set { instanceData = value; }
        }
        Matrix4x4[][] IMeshInstance.Matrices => _matrices;

        private Matrix4x4[][] _matrices;

        void IMeshInstance.PrepareTRSMatrices(Transform[] transforms, int batchAmount, in int batchSize) {
            Matrix4x4[][] _matrices = new Matrix4x4[batchAmount][];
            for (int i = 0; i < batchAmount; i++) {

                int count = i == batchAmount - 1 ? transforms.Length % batchSize : batchSize;
                _matrices[i] = new Matrix4x4[count];

                for (int j = 0; j < count; j++) {
                    int index = i * batchSize + j;
                    Transform meshTransform = transforms[index];
                    meshTransform.gameObject.SetActive(false);
                    Matrix4x4 mat = Matrix4x4.TRS(meshTransform.position,
                                                  meshTransform.rotation,
                                                  meshTransform.lossyScale);
                    _matrices[i][j] = mat;
                }

            }
            this._matrices = _matrices;
        }

        void IMeshInstance.DrawMeshInstance(InstanceData instanceDatas, int batchAmount) {
            for (int i = 0; i < batchAmount; i++) {
                UnityEngine.Graphics.DrawMeshInstanced(instanceDatas.mesh, 0,
                                                       instanceDatas.material,
                                                       _matrices[i], _matrices[i].Length);
            }
        }

        #endregion
    }

    public class MeshInstanceController
    {
        private readonly IMeshInstance _meshInstance;
        private InstanceData _instanceDatas => _meshInstance.InstanceData;
        private int _batchAmount;
        private const int BATCH_SIZE = 1023;

        public MeshInstanceController(IMeshInstance meshInstance) {
            this._meshInstance = meshInstance;
        }

        public bool Initalize() {
            bool isInitalize = false;
            if (CheckDataIsValid()) {
                PrepareBatchAmount();
                PrepareTRSMatrices();
                isInitalize = true;
            }
            return isInitalize;
        }

        public void DrawMeshInstance() {
            this._meshInstance.DrawMeshInstance(this._instanceDatas, this._batchAmount); 
        }

        private bool CheckDataIsValid() {
            bool checker = false;
            try {
                checker = this._instanceDatas.IsValid();
            } catch(Exception e) {
                Debug.LogError(e);
                throw;
            }
            return checker;
        }

        private void PrepareBatchAmount() { 
            this._batchAmount = CalculateBatch(this._instanceDatas.meshTransform.Length,
                                              BATCH_SIZE);
        }

        private void PrepareTRSMatrices() {
            this._meshInstance.PrepareTRSMatrices(this._instanceDatas.meshTransform,
                                                  _batchAmount, BATCH_SIZE);
        }

        private int CalculateBatch(int transformCount, in int batchSize) {
            return transformCount / batchSize + 1;
        }

    }

}
