using UnityEngine;

public class SimilarAnimators : MonoBehaviour
{
    [SerializeField] private Animator[] _animators;

    public void SetFloat(int name, float value)
    {
        foreach(Animator animator in _animators)
        {
            animator.SetFloat(name, value);
        }
    }

    public void SetBool(int name, bool value)
    {
        foreach (Animator animator in _animators)
        {
            animator.SetBool(name, value);
        }
    }
    public void SetInteger(int name, int value)
    {
        foreach (Animator animator in _animators)
        {
            animator.SetInteger(name, value);
        }
    }
    public void SetTrigger(int name)
    {
        foreach (Animator animator in _animators)
        {
            animator.SetTrigger(name);
        }
    }
}
