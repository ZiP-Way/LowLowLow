using System.Linq;
using UnityEditor;
using UnityEngine;

public class FindGameObjectWithMissingScripts : MonoBehaviour
{
    [MenuItem("Component/Find objects with missing scripts/On scene")]
    public static void FindGameObjectsOnScene()
    {
        GameObject parent = null;

        foreach (GameObject prefab in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            Component[] components = prefab.GetComponents<Component>();

            foreach (Component component in components)
            {
                if (component == null)
                {
                    if (parent == null)
                    {
                        parent = new GameObject("Missing Component Objects On Scene");
                    }

                    GameObject instance = Instantiate(prefab, parent.transform);
                    break;
                }
            }
        }
    }

    [MenuItem("Component/Find objects with missing scripts/On prefabs")]
    public static void FindGameObjectsOnPrefabs()
    {
        string[] prefabPaths = AssetDatabase.GetAllAssetPaths()
            .Where(path => path.EndsWith(".prefab", System.StringComparison.OrdinalIgnoreCase)).ToArray();
        GameObject parent = null;

        foreach (string path in prefabPaths)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            Component[] components = prefab.GetComponents<Component>();

            foreach (Component component in components)
            {
                if (component == null)
                {
                    if (parent == null)
                    {
                        parent = new GameObject("Missing Component Objects On Prefabs");
                    }

                    GameObject instance = Instantiate(prefab, parent.transform);
                    break;
                }
            }
        }
    }
}
