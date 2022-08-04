using DG.Tweening;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;
using UniRx;
using Data;
using FluffyUnderware.Curvy;
using Data.Levels;
using TapticFeedback;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private SplineController splineController;

    [SerializeField]
    private Rigidbody thisRigidbody;

    [SerializeField]
    private float defaultSpeed = 10f;

    [SerializeField]
    private float speedWhenFever = 15f;

    #region "Fields"
    private CurvySpline startSpline = default;
    private Sequence _pushBackAnimation = default;
    #endregion

    private void Awake()
    {
        _pushBackAnimation = DOTween.Sequence();
        BuildAnimation();

        Hub.LevelDataLoaded.Subscribe(LevelLoaded).AddTo(this);

        Hub.GameStarted.Subscribe(x => splineController.Speed = defaultSpeed).AddTo(this);

        Hub.LevelFailed.Subscribe(x => LevelFailed()).AddTo(this);

        PlayerObstacleDetection.PlayerCollideWithObstacle
            .Where(x => splineController.MovementDirection == MovementDirection.Forward)
            .Subscribe(x =>
            {
                PlayerFever.ResetFever.Fire();
                splineController.MovementDirection = MovementDirection.Backward;
                _pushBackAnimation.Restart();
                Taptic.Light();
            }).AddTo(this);

        PlayerFever.FeverToggleChanged.Subscribe(state =>
        {
            if (state)
            {
                splineController.Speed = speedWhenFever;
            }
            else
            {
                splineController.Speed = defaultSpeed;
            }
        }).AddTo(this);
    }

    private void BuildAnimation()
    {
        _pushBackAnimation.Pause();
        _pushBackAnimation.SetAutoKill(false);

        _pushBackAnimation.Append(DOTween.To(() => splineController.Speed, x => splineController.Speed = x, 0, 1f));
        _pushBackAnimation.AppendCallback(() => splineController.MovementDirection = MovementDirection.Forward);
        _pushBackAnimation.Append(DOTween.To(() => splineController.Speed, x => splineController.Speed = x, defaultSpeed, 1f));
    }

    private void LevelLoaded(LevelMetaData metaData)
    {
        thisRigidbody.velocity = Vector3.zero;

        splineController.enabled = true;

        startSpline = metaData.StartSpline;

        ResetPosition();
    }

    private void LevelFailed()
    {
        splineController.enabled = false;
    }

    private void ResetPosition()
    {
        splineController.Speed = 0;

        splineController.Spline = startSpline;

        splineController.AbsolutePosition = 0;

        Vector3 startPos = startSpline.FirstSegment.transform.position + Vector3.up;

        transform.position = startPos;
    }
}
