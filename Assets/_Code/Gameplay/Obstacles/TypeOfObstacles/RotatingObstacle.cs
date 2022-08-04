using UnityEngine;
using DG.Tweening;

public class RotatingObstacle : Obstacle
{
    [Header("Rotating Settings")]
    [SerializeField] private Transform _body;
    [SerializeField] private float _angle = 180f;
    [SerializeField] private float _rotationSpeed = 0f;
    [SerializeField] private float _delayTime = 0f;

    #region "Fields"

    private Sequence _rotateAnimation = default;

    #endregion

    private void Start()
    {
        DoRotate();
    }

    private void DoRotate()
    {
        _rotateAnimation = DOTween.Sequence();

        _rotateAnimation.AppendInterval(_delayTime);
        _rotateAnimation.Append(_body.transform.DORotate(new Vector3(0, _angle, 0), _rotationSpeed, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
        _rotateAnimation.OnKill(() =>
        {
            DoRotate();
        });
    }
}