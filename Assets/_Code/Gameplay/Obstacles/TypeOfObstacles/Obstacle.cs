using Data;
using FluffyUnderware.Curvy;
using UniRx;
using UnityEngine;

[RequireComponent (typeof(ObstacleExplosion))]
public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] private ObstaclePiece[] _pieces = default;
    [SerializeField] private CurvySplineSegment controlPoint = default;
    [SerializeField] private Transform _midlePoint = default;
    [SerializeField] private Transform _indicatorPoint = default;
    [SerializeField] private TransperentObstacle _transperentObstacle = default;
    [SerializeField] private GameObject feverIndicator;

    #region "Properties"
    public ObstaclePiece[] Pieces => _pieces;
    public Vector3 MidlePoint => _midlePoint.position;
    #endregion

    private bool passed;

    private void Awake()
    {
        controlPoint.OnControlPointReached += OnControlPointReached;

        controlPoint.OnPrevControlPointReached += OnPrevControlPointReached;

        PlayerFever.FeverToggleChanged.Subscribe(FeverToggleChanged);
    }

    public void Pass()
    {
        passed = true;

        _transperentObstacle.DoBigger();
    }

    private void OnControlPointReached()
    {
        Hub.ObstaclePassed.Fire();

        if (feverIndicator != null)
        {
            feverIndicator.SetActive(false);
        }
    }

    private void OnPrevControlPointReached()
    {
        Hub.ShowObstacleIndicator.Fire(_indicatorPoint);
    }

    private void FeverToggleChanged(bool isFeverActive)
    {
        if (passed)
        {
            return;
        }

        if (feverIndicator != null)
        {
            feverIndicator.SetActive(isFeverActive);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (transform.parent != null && transform.parent.TryGetComponent<CurvySplineSegment>(out var controlPoint))
        {
            this.controlPoint = controlPoint;
        }

        _pieces = GetComponentsInChildren<ObstaclePiece>();
        _transperentObstacle = GetComponentInChildren<TransperentObstacle>();
    }
#endif
}
