using UniRx;
using UnityEngine;

public abstract class AnimationState : ScriptableObject
{
    #region "Fields"

    protected SimilarAnimators SimilarAnimators = default;
    protected HashPlayerAnimatiorValues HashAnimatorValues = default;
    protected CompositeDisposable Disposables = default;

    #endregion

    public virtual void Init(SimilarAnimators similarAnimators, HashPlayerAnimatiorValues animatorValues)
    {
        SimilarAnimators = similarAnimators;
        HashAnimatorValues = animatorValues;

        Disposables = new CompositeDisposable();
    }

    public abstract void Enable();
    public abstract void Disable();
}
