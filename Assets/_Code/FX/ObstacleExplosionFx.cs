using UnityEngine;

public class ObstacleExplosionFx : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle = default;

    private void Awake()
    {
        Disable();
    }

    public void Enable()
    {
        _particle.Play();
    }

    public void Disable()
    {
        _particle.Stop();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _particle = GetComponentInChildren<ParticleSystem>();
    }
#endif
}
