using UnityEngine;

public static class ResourceLoader
{
    public static T LoadResource<T>(string resourcePath) where T : Object
    {
        if (string.IsNullOrWhiteSpace(resourcePath))
        {
            Debug.LogError("ResourceLoader: resourcePath is null or empty.");
            return null;
        }

        T resource = Resources.Load<T>(resourcePath);
        if (resource == null)
            Debug.LogError($"ResourceLoader: Could not find resource '{resourcePath}' in any Resources folder.");

        return resource;
    }

    public static GameObject InstantiatePrefab(string prefabName, string instantiatedName, Transform parent = null)
    {
        if (string.IsNullOrWhiteSpace(prefabName))
        {
            Debug.LogError("PrefabSpawner: prefabName is null or empty.");
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
}
