using UnityEngine;
using UniRx;
using SignalsFramework;

public class PlayerObstacleDetection : MonoBehaviour
{
    [SerializeField] private float _forceToObstacleParts = 5;

    #region "Signals"
    public static readonly Signal PlayerCollideWithObstacle = new Signal();
    #endregion

    #region "Fields"
    private bool _isNeedForDetection = true;
    #endregion

    private void Awake()
    {
        PlayerFever.FeverToggleChanged.Subscribe(isFevering =>
        {
            _isNeedForDetection = !isFevering;
        }).AddTo(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isNeedForDetection)
        {
            if (collision.gameObject.TryGetComponent(out ObstaclePiece obstaclePiece))
            {
                PlayerObstacleDetection.PlayerCollideWithObstacle.Fire();
                obstaclePiece.AddForce(transform.forward * _forceToObstacleParts);
            }
        }
    }
}
