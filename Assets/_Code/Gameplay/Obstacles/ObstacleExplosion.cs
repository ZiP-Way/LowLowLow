using TapticFeedback;
using UnityEngine;

[RequireComponent(typeof(Obstacle), typeof(ObstacleExplosionFx))]
public class ObstacleExplosion : MonoBehaviour
{
    [SerializeField] private Obstacle _obstacle = default;
    [SerializeField] private ObstacleExplosionFx _explosionFx = default;

    [SerializeField] private float _explosionForce = 500f;
    [SerializeField] private float _explosionRadius = 10f;

    public void DoExplosion()
    {
        Taptic.Medium();

        foreach (ObstaclePiece piece in _obstacle.Pieces)
        {
            piece.AddExplosionForce(_obstacle.MidlePoint, _explosionForce, _explosionRadius);
            _explosionFx.Enable();
        }
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        _obstacle = GetComponent<Obstacle>();
        _explosionFx = GetComponent<ObstacleExplosionFx>();
    }
#endif
}
