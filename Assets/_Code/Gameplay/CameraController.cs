using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera cinemachineCamera;

    private void Awake()
    {
        Hub.LevelSceneLoaded.Subscribe(x => LevelSceneLoaded());

        Hub.LevelFailed.Subscribe(x => LevelFailed());
    }

    private void LevelSceneLoaded()
    {
        cinemachineCamera.enabled = true;
    }

    private void LevelFailed()
    {
        cinemachineCamera.enabled = false;
    }
}
