using FluffyUnderware.Curvy.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowPlayerController : SplineController
{
    protected override void ComputeTargetPositionAndRotation(out Vector3 targetPosition, out Vector3 targetUp, out Vector3 targetForward)
    {
        Vector3 oldPosition = transform.position;

        base.ComputeTargetPositionAndRotation(out targetPosition, out targetUp, out targetForward);

        targetPosition.y = oldPosition.y;
    }
}
