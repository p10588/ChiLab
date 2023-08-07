using System;
using System.Linq;
using UnityEngine;

namespace Chi.Utilities.Graphics {

    [System.Serializable]
    public struct InstanceData
    {
        public Mesh mesh;
        public Material material;
        public Transform[] meshTransform;

        public bool IsValid() {
            try {
                CheckAllDataVaild();
                return true;
            } catch (Exception e) {
                throw e;
            }
        }

        private void CheckAllDataVaild() {
            HandleMeshException();
            HandleMaterialException();
            HandleMeshTransException();
        }

        private void HandleMeshException() {
            if (mesh == null) throw new ArgumentNullException("Mesh");
        }

        private void HandleMaterialException() {
            if (material == null) throw new ArgumentNullException("Material");
            if (material.enableInstancing == false)
                throw new InvalidOperationException("Not Instacning Material");
        }

        private void HandleMeshTransException() {
            if (meshTransform?.Any() == false)
                throw new ArgumentNullException($"MeshTransform is Null or Empty");
            if (meshTransform.Any(x => x == null))
                throw new ArgumentNullException($"Some Element In MeshTransform are Null");
        }

    }
}
