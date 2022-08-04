using Data;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Controllers;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerRails : MonoBehaviour
{
    [SerializeField]
    private SplineController splineController;

    private Rails rails;

    private void Awake()
    {
        Hub.RailsStartReached.Subscribe(RailsStartReached);

        Hub.RailsEndReached.Subscribe(x => RailsEndReached());
    }

    private void RailsStartReached(Rails rails)
    {
        this.rails = rails;
    }

    private void RailsEndReached()
    {
        if (rails != null)
        {
            rails.SetProgress(1f);

            rails = null;
        }
    }

    private void Update()
    {
        if (rails != null)
        {
            rails.SetProgress(splineController.RelativePosition);
        }
    }
}
