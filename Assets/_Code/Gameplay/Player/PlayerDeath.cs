using Data;
using UnityEngine;
using UniRx;

public class PlayerDeath : MonoBehaviour
{
    private bool isDied = false;

    private void Awake()
    {
        Hub.LevelDataLoaded.Subscribe(_ => isDied = false).AddTo(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeadZone") && !isDied)
        {
            isDied = true;

            Hub.LevelFailed.Fire();
        }
    }
}
