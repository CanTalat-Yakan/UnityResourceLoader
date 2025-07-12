using System.Collections.Generic;
using UnityEngine;

namespace UnityEssentials
{
    public static class ResourceLoader
    {
        private static readonly Dictionary<string, Object> _resourceCache = new();
        public static T LoadResource<T>(string resourcePath, bool cacheResource = true) where T : Object
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
            {
                Debug.LogError("ResourceLoader: resourcePath is null or empty.");
                return null;
            }

            if (_resourceCache.TryGetValue(resourcePath, out var cachedObject))
            {
                if (cachedObject is T typedObject)
                    return typedObject;
                Debug.LogWarning($"ResourceLoader: Cached resource at '{resourcePath}' is not of type {typeof(T).Name}.");
            }

            T resource = Resources.Load<T>(resourcePath);
            if (resource == null)
            {
                Debug.LogError($"ResourceLoader: Could not find resource '{resourcePath}' in any Resources folder.");
                return null;
            }

            if (cacheResource)
                _resourceCache[resourcePath] = resource;

            return resource;
        }

        public static GameObject InstantiatePrefab(string prefabName, string instantiatedName, Transform parent = null)
        {
            if (string.IsNullOrWhiteSpace(prefabName))
            {
                Debug.LogWarning("PrefabSpawner: prefabName is null or empty.");
                return null;
            }

            GameObject prefab = Resources.Load<GameObject>(prefabName);
            if (prefab == null)
            {
                Debug.LogError($"PrefabSpawner: Could not find prefab '{prefabName}' in any Resources folder.");
                return null;
            }

            var instance = Object.Instantiate(prefab, parent);
            if (!string.IsNullOrEmpty(instantiatedName))
                instance.name = instantiatedName;

            return instance;
        }

        public static void ClearCache() =>
            _resourceCache.Clear();
    }
}