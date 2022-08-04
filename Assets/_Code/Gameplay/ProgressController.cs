using Data;
using Data.Levels;
using FluffyUnderware.Curvy.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ProgressController : MonoBehaviour
{
    [SerializeField]
    private SplineController splineController;

    private LevelMetaData levelMetaData;

    private float currentSplineProgress;

    private float totalProgress;

    private void Awake()
    {
        Hub.LevelDataLoaded.Subscribe(x => LevelLoaded(x));

        Hub.LevelComplete.Subscribe(x => enabled = false);

        splineController.OnEndReached.AddListener(SplineSwitched);
    }

    private void Update()
    {
        if (splineController.Spline != null && currentSplineProgress < splineController.AbsolutePosition)
        {
            totalProgress += splineController.AbsolutePosition - currentSplineProgress;

            currentSplineProgress = splineController.AbsolutePosition;

            Hub.LevelProgressChanged.Fire(totalProgress / levelMetaData.PathLength);
        }
    }

    private void LevelLoaded(LevelMetaData levelMetaData)
    {
        currentSplineProgress = 0f;

        totalProgress = 0f;

        enabled = true;

        this.levelMetaData = levelMetaData;
    }

    private void SplineSwitched(CurvySplineMoveEventArgs args)
    {
        currentSplineProgress = 0f;
    }
}
