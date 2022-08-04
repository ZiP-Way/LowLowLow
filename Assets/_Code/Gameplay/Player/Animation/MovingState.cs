using UnityEngine;
using UniRx;

[CreateAssetMenu(menuName = "Player/AnimationStates/MovingState")]
public class MovingState : AnimationState
{
    [SerializeField, Range(0, 1)] private float _deffaulBlendValue = 0.5f;
    [SerializeField, Range(0, 100)] private int _chanceToTurnAround = 10;

    #region "Fields"

    private float _currentBlendValue = 0;

    private bool _isTriedToSwitchState = false;
    private bool _isTriedToTurnAround = false;

    #endregion

    public override void Enable()
    {
        _currentBlendValue = _deffaulBlendValue;
        Swipe.Reset.Fire(_deffaulBlendValue);

        SimilarAnimators.SetBool(HashAnimatorValues.IsMoving, true);
        SimilarAnimators.SetFloat(HashAnimatorValues.BlendValue, _currentBlendValue);

        Swipe.SwipingToggle.Fire(true);
        Swipe.SwipingValueChanged.Subscribe(value =>
        {
            _currentBlendValue = value;
            SimilarAnimators.SetFloat(HashAnimatorValues.BlendValue, _currentBlendValue);

            TryToSwitchAnimationState();
            TryToTurnAround();
        }).AddTo(Disposables);
    }

    public override void Disable()
    {
        SimilarAnimators.SetBool(HashAnimatorValues.IsMoving, false);
        Swipe.SwipingToggle.Fire(false);

        Disposables.Clear();
    }

    private void TryToTurnAround()
    {
        if (_currentBlendValue == 1f && !_isTriedToTurnAround)
        {
            _isTriedToTurnAround = true;

            int randomNum = Random.Range(0, 101);
            if (randomNum <= _chanceToTurnAround)
            {
                SimilarAnimators.SetBool(HashAnimatorValues.IsTurnAround, true);
            }
        }
        else if (_currentBlendValue != 1f && _isTriedToTurnAround)
        {
            _isTriedToTurnAround = false;
            SimilarAnimators.SetBool(HashAnimatorValues.IsTurnAround, false);
        }
    }

    private void TryToSwitchAnimationState()
    {
        if (_currentBlendValue == 0.5f && !_isTriedToSwitchState)
        {
            _isTriedToSwitchState = true;

            int stateId = Random.Range(1, 3);
            SimilarAnimators.SetInteger(HashAnimatorValues.StatesId, stateId);
        }
        else if (_currentBlendValue != 0.5f && _isTriedToSwitchState)
        {
            _isTriedToSwitchState = false;
        }
    }
}
