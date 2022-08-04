using UnityEngine;
using UpdateSys;
using UniRx;
using Data;


public class CameraRotator : MonoBehaviour, ILateUpdatable
{
    [SerializeField] private float _speed = 10f;

    #region "Fields"
    private Quaternion _originRotation = default;
    private float _rotationY = 0f;
    #endregion

    private void Awake()
    {
        Hub.LevelComplete
            .Subscribe(x =>
            {
                ResetValues();
                this.StartLateUpdate();
            }).AddTo(this);

        Hub.RequestLobbyTransition.Subscribe(x =>
            {
                ResetValues();
                this.StopLateUpdate();
            }).AddTo(this);
    }

    public void OnSystemLateUpdate(float deltaTime)
    {
        Rotate(deltaTime);
    }

    private void Rotate(float deltaTime)
    {
        _rotationY += _speed * deltaTime;
        Quaternion rotationY = Quaternion.AngleAxis(-_rotationY, Vector3.up);

        transform.rotation = _originRotation * rotationY;
    }

    private void ResetValues()
    {
        _rotationY = 0;
        _originRotation = transform.parent.rotation;
        transform.rotation = _originRotation;
    }
}
