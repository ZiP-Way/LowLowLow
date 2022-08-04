using Data;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Hub.LevelComplete.Fire();
    }
}
