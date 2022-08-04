using UnityEngine;
using UniRx;
using Data;

public class ConfettiController : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particles;

    private void Awake()
    {
        Hub.LevelComplete
            .Subscribe(x =>
            {
                StartParticles();
            }).AddTo(this);
    }

    private void StartParticles()
    {
        foreach(ParticleSystem particle in _particles)
        {
            particle.Play();
        }
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        _particles = GetComponentsInChildren<ParticleSystem>();
    }
#endif
}
