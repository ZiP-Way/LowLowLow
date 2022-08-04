using System;
using UnityEngine;
using UniRx.Triggers;
using UniRx;


[CreateAssetMenu(menuName = "Player/AnimationStates/SpeedUpState")]
public class SpeedUpState : AnimationState
{
    [SerializeField] private int _loopCount = 2;
    [SerializeField] private float _animationSpeed = 2f;
    [SerializeField] private AnimationClip _clip = default;

    #region "Events"

    public Action OnAnimationLoopEnd;

    #endregion

    #region "Properties"

    public int LoopCount => _loopCount;

    #endregion

    #region "Fields"

    private float _timeToEndAnimation = 0;

    #endregion

    public override void Init(SimilarAnimators similarAnimators, HashPlayerAnimatiorValues animatorValues)
    {
        base.Init(similarAnimators, animatorValues);

        _timeToEndAnimation = _loopCount * _clip.length / _animationSpeed;
        SimilarAnimators.SetFloat(HashAnimatorValues.SUAnimationSpeed, _animationSpeed);
    }

    public override void Enable()
    {
        SimilarAnimators.SetBool(HashAnimatorValues.IsSpeedUp, true);

        Observable.Timer(TimeSpan.FromSeconds(_timeToEndAnimation)).Subscribe(_ =>
        {
            OnAnimationLoopEnd?.Invoke();
            Disable();
        }).AddTo(Disposables);
    }
    public override void Disable()
    {
        SimilarAnimators.SetBool(HashAnimatorValues.IsSpeedUp, false);
        Disposables.Clear();
    }
}
