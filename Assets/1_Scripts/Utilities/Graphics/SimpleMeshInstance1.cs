using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chi.Utilities.Graphics
{
    [ExecuteInEditMode]
    public class SimpleMeshInstance1 : MonoBehaviour
    {
        [SerializeField] private Mesh mesh;
        [SerializeField] private Material mat;
        [SerializeField] private List<Transform> meshTransform = new List<Transform>();
        [SerializeField] private bool getTransformInChild;

        private Matrix4x4[][] _matrices;
        private int _batchAmount;
        private bool _isInitalized = false;

        private const int BATCH_SIZE = 1023;

        private void Start() {

            Initalize();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateBatch(this._batchAmount, this._matrices);
        }

        private void Initalize() {

            CheckMatGPUInstancing(this.mat);

            this._batchAmount = CalculateBatch();

            this._matrices = CalculateTRSMatrice(this._batchAmount);

            this._isInitalized = true;
        }

        private bool CheckInitalized(bool isInitalized) {
            if (!isInitalized) {
                Debug.LogError($"{GetObjectNameAndType(this)} initalize fail");
            }
            return isInitalized;
        }

        private void UpdateBatch(int batchAmount, Matrix4x4[][] matrices) {
            for (int i = 0; i < batchAmount; i++) {
                UnityEngine.Graphics.DrawMeshInstanced(this.mesh, 0, this.mat, matrices[i], matrices[i].Length);
            }
        }

        private void CheckMatGPUInstancing(Material material) {
            if (!material) {
                Debug.LogError($"{GetObjectNameAndType(this)} material is missing");
                return;
            }
            if (!material.enableInstancing) {
                Debug.LogWarning($"{GetObjectNameAndType(this)} force material enable instancing");
                material.enableInstancing = true;
            }
        }

        private List<Transform> GetTransformInChild(Transform root) {
            return null;
        }

        private int CalculateBatch() {
            return meshTransform.Count / BATCH_SIZE + 1;
        }

        private Matrix4x4[][] CalculateTRSMatrice(int batch) {
            _matrices = new Matrix4x4[batch][];
            for (int i = 0; i < batch; i++) {

                int count = i == batch - 1 ? meshTransform.Count % BATCH_SIZE : BATCH_SIZE;
                _matrices[i] = new Matrix4x4[count];

                for (int j = 0; j < count; j++) {
                    int index = i * BATCH_SIZE + j;
                    Transform meshTransform = this.meshTransform[index];
                    meshTransform.gameObject.SetActive(false);
                    Matrix4x4 mat = Matrix4x4.TRS(meshTransform.position,
                                                  meshTransform.rotation,
                                                  meshTransform.lossyScale);
                    _matrices[i][j] = mat;
                }

            }
            return _matrices;
        }

        private string GetObjectNameAndType(Object @object) {
            string gameObjectName = ((GameObject)@object).name;
            string typeName = @object.GetType().Name;

            return $"{gameObjectName} {typeName}";
        } 
    }

}
