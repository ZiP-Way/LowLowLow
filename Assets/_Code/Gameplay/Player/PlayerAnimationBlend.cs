using UnityEngine;
using UniRx;
using Data;

public class PlayerAnimationBlend : MonoBehaviour
{
    [Header("States")]
    [SerializeField] private WonState _wonState = default;
    [SerializeField] private IdleState _idleState = default;
    [SerializeField] private SpeedUpState _speedUpState = default;
    [SerializeField] private MovingState _movingState = default;

    [Header("Components")]
    [SerializeField] private SimilarAnimators _similarAnimators = default;

    #region "Fields"

    private CompositeDisposable _disposables = new CompositeDisposable();
    private HashPlayerAnimatiorValues _hashPlayerAnimatior = new HashPlayerAnimatiorValues();
    private AnimationState _currentState = default;

    #endregion

    private void Awake()
    {
        _idleState.Init(_similarAnimators, _hashPlayerAnimatior);
        _wonState.Init(_similarAnimators, _hashPlayerAnimatior);
        _movingState.Init(_similarAnimators, _hashPlayerAnimatior);
        _speedUpState.Init(_similarAnimators, _hashPlayerAnimatior);
        _speedUpState.OnAnimationLoopEnd += SpeedUpAnimationLoopEnd;

        _currentState = _idleState;

        Hub.GameStarted.Subscribe(_ =>
        {
            SwitchToState(_speedUpState);
            Hub.ShowObstacleIndicator.Subscribe(_ =>
            {
                SwitchToState(_movingState);
                _disposables.Clear();
            }).AddTo(_disposables);

        }).AddTo(this);

        Hub.LevelComplete.Subscribe(_ =>
        {
            SwitchToState(_wonState);
        }).AddTo(this);

        Hub.LevelSceneLoaded.Subscribe(_ =>
        {
            SwitchToState(_idleState);
        }).AddTo(this);
    }

    private void SpeedUpAnimationLoopEnd()
    {
        SwitchToState(_movingState);
    }

    private void SwitchToState(AnimationState state)
    {
        if (state == _currentState) return;

        _currentState.Disable();
        _currentState = state;
        _currentState.Enable();
    }
}