using FluffyUnderware.Curvy.Controllers;
using UnityEngine;
using UniRx;
using SignalsFramework;
using Data;

public class CompletedObstaclesCounter : MonoBehaviour
{
    [SerializeField] private SplineController _splineController = default;

    #region "Signals/Events"
    public static readonly Signal<int> CountOfCompletedObstaclesChanged = new Signal<int>();
    #endregion

    #region "Fields"
    private int _countOfCompletedObstacles = 0;
    #endregion

    private void Awake()
    {
        PlayerFever.ResetFever.Subscribe(x =>
            {
                _countOfCompletedObstacles = 0;
                Enable();
            }).AddTo(this);

        PlayerFever.FeverToggleChanged.Subscribe(x =>
            {
                Disable();
            }).AddTo(this);
    }

    private void Enable()
    {
        _splineController.OnControlPointReached.AddListenerOnce(OnControlPointReached);
    }

    private void Disable()
    {
        _splineController.OnControlPointReached.RemoveListener(OnControlPointReached);
    }

    private void OnControlPointReached(CurvySplineMoveEventArgs args)
    {
        Obstacle obstacle = args.ControlPoint.GetComponentInChildren<Obstacle>();

        if (obstacle)
        {
            obstacle.Pass();
            _countOfCompletedObstacles++;
            CountOfCompletedObstaclesChanged.Fire(_countOfCompletedObstacles);
        }
    }
}
