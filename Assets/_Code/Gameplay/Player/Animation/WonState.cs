using UnityEngine;

[CreateAssetMenu(menuName = "Player/AnimationStates/WonState")]
public class WonState : AnimationState
{
    public override void Enable()
    {
        SimilarAnimators.SetBool(HashAnimatorValues.IsWon, true);
    }
    public override void Disable()
    {
        SimilarAnimators.SetBool(HashAnimatorValues.IsWon, false);
    }
}
