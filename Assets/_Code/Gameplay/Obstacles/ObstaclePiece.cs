using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class ObstaclePiece : MonoBehaviour
{
    private const int IGNORE_PLAYER_LAYER_ID = 6;

    [SerializeField] private Rigidbody _rigidbody = default;

    #region "Fields"
    private bool _isAlreadyUsed = false;
    #endregion

    public void AddForce(Vector3 direction)
    {
        if (_isAlreadyUsed) return;

        IgnoreCollisionWithPlayer();
        _rigidbody.AddForce(direction, ForceMode.Impulse);
        _rigidbody.AddTorque(RandomDirection(), ForceMode.Impulse);

        transform.parent = null;
    }

    public void AddExplosionForce(Vector3 position, float force, float radius)
    {
        if (_isAlreadyUsed) return;

        IgnoreCollisionWithPlayer();
        _rigidbody.AddExplosionForce(force, position, radius);
        _rigidbody.AddTorque(RandomDirection(), ForceMode.Impulse);

        transform.parent = null;
    }

    private void IgnoreCollisionWithPlayer()
    {
        gameObject.layer = IGNORE_PLAYER_LAYER_ID; // ignore player layer
        _rigidbody.isKinematic = false;
        _isAlreadyUsed = true;
    }

    private Vector3 RandomDirection()
    {
        return Random.insideUnitCircle.normalized;
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }
#endif
}
