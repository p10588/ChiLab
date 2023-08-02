using System;
using System.Linq;
using UnityEngine;

namespace Chi.Utilities.Graphics
{
    

    public interface IMeshInstance
    {
        Matrix4x4[][] Matrices { get; }
        void PrepareMatrices(Transform[] transforms, int batchAmount, in int batchSize);
        void DrawMeshInstance(InstanceData instanceData, int batchAmount);
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
                this._controller = new MeshInstanceController(this, instanceData);

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

        Matrix4x4[][] IMeshInstance.Matrices => _matrices;

        private Matrix4x4[][] _matrices;

        void IMeshInstance.PrepareMatrices(Transform[] transforms, int batchAmount, in int batchSize) {
            Matrix4x4[][] _matrices = new Matrix4x4[batchAmount][];
            for (int i = 0; i < batchAmount; i++) {
                int count = i == batchAmount - 1 ? transforms.Length % batchSize : batchSize;
                _matrices[i] = new Matrix4x4[count];
                for (int j = 0; j < count; j++) {
                    int index = i * batchSize + j;
                    transforms[index].gameObject.SetActive(false);
                    _matrices[i][j] = TransformToTRSMatrix(transforms[index]);
                }

            }
            this._matrices = _matrices;
        }

        private Matrix4x4 TransformToTRSMatrix(Transform trans) {
            Transform meshTransform = trans;
            Matrix4x4 mat = Matrix4x4.TRS(meshTransform.position,
                                          meshTransform.rotation,
                                          meshTransform.lossyScale);

            return mat;
        }

        void IMeshInstance.DrawMeshInstance(InstanceData instanceData, int batchAmount) {
            for (int i = 0; i < batchAmount; i++) {
                TryDoMeshInstance(instanceData, i);
            }
        }

        private void TryDoMeshInstance(InstanceData instanceData, int index) {
            try {
                UnityEngine.Graphics.DrawMeshInstanced(instanceData.mesh, 0,
                                                       instanceData.material,
                                                       _matrices[index], _matrices[index].Length);
            }catch(Exception e) {
                Debug.LogError(e);
                Reinitalize();
            }
        }

        private void Reinitalize() {
            this._isInitalized = false;
            this.gameObject.SetActive(false);
            this.gameObject.SetActive(true);
        }

        #endregion
    }

 

}
