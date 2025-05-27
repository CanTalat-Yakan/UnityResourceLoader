#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
    public static class ResourceLoaderEditor
    {
        public static void InstantiatePrefab(string prefabName, string instantiatedName = null)
        {
            if (string.IsNullOrWhiteSpace(prefabName))
            {
                Debug.LogError("PrefabSpawnerEditor: prefabName is null or empty.");
                return;
            }

            var guids = AssetDatabase.FindAssets(prefabName + " t:Prefab");
            if (guids.Length == 0)
            {
                Debug.LogError($"{prefabName} prefab not found in project.");
                return;
            }
            
            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null)
            {
                Debug.LogError($"Failed to load prefab at: {path}");
                return;
            }

            var instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            if (Selection.activeGameObject is GameObject parent)
                GameObjectUtility.SetParentAndAlign(instance, parent);
            if(!string.IsNullOrEmpty(instantiatedName))
                instance.name = instantiatedName;

            Undo.RegisterCreatedObjectUndo(instance, "Spawn " + instance.name);

            Selection.activeObject = instance;
        }
    }
}
#endif