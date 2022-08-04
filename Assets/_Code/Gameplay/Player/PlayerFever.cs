using FluffyUnderware.Curvy.Controllers;
using UnityEngine;
using UniRx;
using SignalsFramework;
using Data;

public class PlayerFever : MonoBehaviour
{
    [SerializeField] private int _targetNumberCompletedObstacles = 0;
    [SerializeField] private SplineController _splineController = default;
    [SerializeField] private float _feverTime = 5f;

    #region "Signals"
    public static readonly Signal ResetFever = new Signal();
    public static readonly Signal<float> FeverProgressChanged = new Signal<float>();
    public static readonly Signal<bool> FeverToggleChanged = new Signal<bool>();
    #endregion

    #region "Properties"
    public float Time => _feverTime;
    #endregion

    #region "Fields"
    private CompositeDisposable _disposables = new CompositeDisposable();
    #endregion

    public static bool IsFevering { get; private set; }

    private void Awake()
    {
        _disposables = new CompositeDisposable();
        Toggle(false);

        Hub.LevelComplete.Subscribe(x =>
        {
            StopFever();
        }).AddTo(this);

        Hub.LevelFailed.Subscribe(x =>
        {
            StopFever();
        }).AddTo(this);

        CompletedObstaclesCounter.CountOfCompletedObstaclesChanged
            .Where(x => !IsFevering)
            .Do(x => FeverProgressChanged.Fire(1f / _targetNumberCompletedObstacles * x))
            .Where(x => x >= _targetNumberCompletedObstacles)
            .Subscribe(x =>
            {
                StartFever();
            }).AddTo(this);
    }

    private void OnControlPointReached(CurvySplineMoveEventArgs args)
    {
        ObstacleExplosion obstacleExplosion = args.ControlPoint.GetComponentInChildren<ObstacleExplosion>();

        if (obstacleExplosion)
        {
            obstacleExplosion.DoExplosion();
        }
    }
    private void Toggle(bool state)
    {
        IsFevering = state;
        FeverToggleChanged.Fire(IsFevering);
    }
    private void StartFever()
    {
        Toggle(true);

        gameObject.layer = 8; // set ignore obstacles layer
        _splineController.OnControlPointReached.AddListener(OnControlPointReached);

        Observable.Timer(System.TimeSpan.FromSeconds(_feverTime))
            .Subscribe(_ =>
            {
                StopFever();
            }).AddTo(_disposables);
    }

    private void StopFever()
    {
        Toggle(false);

        gameObject.layer = 3; // set player layer (dafault)
        _splineController.OnControlPointReached.RemoveListener(OnControlPointReached);

        PlayerFever.ResetFever.Fire();

        _disposables.Clear();
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        _splineController = GetComponent<SplineController>();
    }
#endif
}
