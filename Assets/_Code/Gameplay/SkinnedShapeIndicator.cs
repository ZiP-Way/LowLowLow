using Data;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Controllers;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class SkinnedShapeIndicator : MonoBehaviour
{
    [SerializeField]
    private SplineController splineController;

    [SerializeField]
    private Transform indicatorPoint;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private Transform indicatorShape;

    [SerializeField]
    private SkinnedMeshRenderer skinnedMeshRenderer;

    private void Awake()
    {
        Hub.ShowObstacleIndicator.Subscribe(ShowObstacleIndicator);

        Hub.ObstaclePassed.Subscribe(x => HideObstacleIndicator());
    }

    private void ShowObstacleIndicator(Transform indicatorPoint)
    {
        SetIndicatorPoint(indicatorPoint);
    }

    private void HideObstacleIndicator()
    {
        skinnedMeshRenderer.enabled = false;
    }

    private void Update()
    {
        if (indicatorPoint != null)
        {
            indicatorShape.position = indicatorPoint.position;
            indicatorShape.rotation = playerTransform.rotation;
        }
    }

    private void SetIndicatorPoint(Transform indicatorPoint)
    {
        skinnedMeshRenderer.enabled = true;

        this.indicatorPoint = indicatorPoint;
    }
}
