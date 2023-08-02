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
            bool checker = false;
            try {
                CheckAllDataVaild();
                checker = true;
            } catch (Exception e) {
                throw e;
            }
            return checker;
        }

        private void CheckAllDataVaild() {
            if (mesh == null) throw new ArgumentNullException("Mesh");

            if (material == null) throw new ArgumentNullException("Material");
            if (material.enableInstancing == false)
                throw new InvalidOperationException("Not Instacning Material");

            if (meshTransform == null) throw new ArgumentNullException("MeshTransform");

            if (meshTransform.Length <= 0)
                throw new ArgumentNullException($"MeshTransform is Empty");

            if (meshTransform.Any(x => x == null))
                throw new ArgumentNullException($"Some Element In MeshTransform are Null");
        }

    }
}
