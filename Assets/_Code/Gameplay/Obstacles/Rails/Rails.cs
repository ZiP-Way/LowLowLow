using Data;
using FluffyUnderware.Curvy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rails : MonoBehaviour
{
    [SerializeField]
    private SideRail[] sideRails;

    [SerializeField]
    private CurvySplineSegment startPoint;

    [SerializeField]
    private CurvySplineSegment endPoint;

    [SerializeField]
    [Range(0f, 1f)]
    private float progress;

    private void Awake()
    {
        startPoint.OnControlPointReached += OnStartPointReached;

        endPoint.OnControlPointReached += OnEndPointReached;
    }

    private void OnStartPointReached()
    {
        Hub.RailsStartReached.Fire(this);
    }

    private void OnEndPointReached()
    {
        Hub.RailsEndReached.Fire();
    }

    public void SetProgress(float progress)
    {
        this.progress = progress;

        UpdateProgress();
    }

    private void UpdateProgress()
    {
        foreach (SideRail rail in sideRails)
        {
            rail.SetProgress(progress);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateProgress();
    }
#endif
}
