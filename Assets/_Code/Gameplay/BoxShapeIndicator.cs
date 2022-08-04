using Data;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Controllers;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class BoxShapeIndicator : MonoBehaviour
{
    [SerializeField]
    private SplineController splineController;

    [SerializeField]
    private Transform indicatorPoint;

    [SerializeField]
    private BoxCollider playerShape;

    [SerializeField]
    private Transform indicatorShape;

    private void Awake()
    {
        Hub.ShowObstacleIndicator.Subscribe(ShowObstacleIndicator);
    }

    private void ShowObstacleIndicator(Transform indicatorPoint)
    {
        SetIndicatorPoint(indicatorPoint);
    }

    private void Update()
    {
        if (indicatorPoint != null)
        {
            Vector3 playerShapeSize = playerShape.size;

            indicatorShape.position = indicatorPoint.position;

            indicatorShape.localScale = new Vector3(0.01f, playerShapeSize.y, playerShapeSize.x);
            indicatorShape.rotation = indicatorPoint.rotation;
        }
    }

    private void SetIndicatorPoint(Transform indicatorPoint)
    {
        indicatorShape.gameObject.SetActive(true);

        this.indicatorPoint = indicatorPoint;
    }
}
