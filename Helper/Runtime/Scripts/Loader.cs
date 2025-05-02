using UnityEngine;

namespace Clubhouse.Helper
{
    public static class Loader
    {
        public static GameObject InstantiateFromResources(string a_prefabPath)
        {
            GameObject prefab = Resources.Load<GameObject>(a_prefabPath);
            if (prefab != null)
            {
                GameObject instantiatedObject = Object.Instantiate(prefab);
                // Debug.Log("Prefab instantiated successfully: " + instantiatedObject.name);
            }
            else
            {
                Debug.LogError($"Failed to load prefab from Resources. Ensure '{a_prefabPath}' exists in a 'Resources' folder.");
            }
            return prefab;
        }
    }
}
