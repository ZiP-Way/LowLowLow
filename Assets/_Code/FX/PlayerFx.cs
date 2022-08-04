using UnityEngine;
using UniRx;

public class PlayerFx : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particles = default;

    private void Awake()
    {
        PlayerFever.FeverToggleChanged.Subscribe(state =>
        {
            Toggle(state);
        }).AddTo(this);
    }

    private void Toggle(bool state)
    {
        if (state)
        {
            foreach (var particle in _particles)
            {
                particle.Play();
            }
        }
        else
        {
            foreach (var particle in _particles)
            {
                particle.Stop();
            }
        }
    }
}
