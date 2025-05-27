using UnityEngine;

public static class PrefabSpawner
{
    public static GameObject Instantiate(string prefabName, string instantiatedName, Transform parent = null)
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
