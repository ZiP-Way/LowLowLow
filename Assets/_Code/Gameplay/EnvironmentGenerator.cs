using EasyButtons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform cubePrefab;

    [SerializeField]
    private int rows;

    [SerializeField]
    private int cols;

#if UNITY_EDITOR
    [Button]
    private void Generate()
    {
        if (Application.isPlaying || cubePrefab == null)
        {
            return;
        }

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Vector3 cubePosition = transform.position;

                cubePosition.x = i * 15f;
                cubePosition.z = j * 15f;

                Transform newCube = Instantiate(cubePrefab, cubePosition, Quaternion.identity, transform);
            }
        }
    }
#endif
}
