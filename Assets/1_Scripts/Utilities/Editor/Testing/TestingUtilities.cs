using UnityEngine;

namespace Chi.Utilities.Testing
{
    public static class TestingUtilities
    {
        public static T LoadTestAsset<T>(string path) where T : UnityEngine.Object {
            T obj = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
            if (!obj) Debug.LogError("Load Asset Fail");
            return obj;
        }
    }
}

