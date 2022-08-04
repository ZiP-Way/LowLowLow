using Data;
using FluffyUnderware.Curvy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using UniRx;
using Data.Levels;
using Profile;
using EditorExtensions.Attributes;

public class LevelManager : MonoBehaviour
{
    [SerializeField, RequireInput]
    private LevelList _levelList = default;

    [SerializeField]
    private CurvySpline[] pathSplines;

    private int actualLevelIndex;

    private void Awake()
    {
        Hub.LevelSceneLoaded.Subscribe(x => actualLevelIndex = x);
    }

    private void Start()
    {
        LoadLevelData();
    }

    private float GetPathLength()
    {
        float pathLength = 0;

        foreach (CurvySpline spline in pathSplines)
        {
            pathLength += spline.Length;
        }

        return pathLength;
    }

    private void LoadLevelData()
    {
        LevelData currentData = _levelList.AllLevels[actualLevelIndex];

        // E.g. load current level
        // after that notify that level has loaded
        PlayerFever.ResetFever.Fire();
        Hub.LevelDataLoaded.Fire(new LevelMetaData
        {
            LevelData = currentData,
            ActualLevelIndex = actualLevelIndex,
            VisualLevelIndex = PlayerProfile.CurrentLevel,
            StartSpline = pathSplines[0],
            PathLength = GetPathLength()
        });
    }
}
