using System;
using UnityEngine;

namespace Chi.Utilities.Graphics
{
    public class MeshInstanceController
    {
        private readonly IMeshInstance _meshInstance;
        private InstanceData _instanceData;
        private int _batchAmount;
        private const int BATCH_SIZE = 1023;

        public MeshInstanceController(IMeshInstance meshInstance, InstanceData instanceData) {
            this._meshInstance = meshInstance;
            this._instanceData = instanceData;
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
            this._meshInstance.DrawMeshInstance(this._instanceData, this._batchAmount);
        }

        private bool CheckDataIsValid() {
            bool checker = false;
            try {
                checker = this._instanceData.IsValid();
            } catch (Exception e) {
                checker = false;
                Debug.LogError(e);
            }
            return checker;
        }

        private void PrepareBatchAmount() {
            this._batchAmount = CalculateBatch(this._instanceData.meshTransform.Length,
                                              BATCH_SIZE);
        }

        private void PrepareTRSMatrices() {
            this._meshInstance.PrepareMatrices(this._instanceData.meshTransform,
                                                  _batchAmount, BATCH_SIZE);
        }

        private int CalculateBatch(int transformCount, in int batchSize) {
            return transformCount / batchSize + 1;
        }

    }
}
