using UniRx;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/AnimationStates/IdleState")]
public class IdleState : AnimationState
{
    [SerializeField] private float _lookBackInterval = 10f;

    public override void Enable()
    {
        Observable.Timer(System.TimeSpan.FromSeconds(_lookBackInterval))
            .Repeat()
            .Subscribe(_ =>
            {
                SimilarAnimators.SetTrigger(HashAnimatorValues.LookBack);
            }).AddTo(Disposables);
    }

    public override void Disable()
    {
        Disposables.Clear();
    }
}