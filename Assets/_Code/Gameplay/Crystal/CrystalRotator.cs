using UnityEngine;
using UnityEngine.Profiling;
using UpdateSys;

public class CrystalRotator : MonoBehaviour, ILateUpdatable
{
    [SerializeField, HideInInspector] private SimpleCrystal[] _crystals = default;
    [SerializeField] private Vector3 _defaultAngle = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 _axis = new Vector3(0, 1, 0);
    [SerializeField] private float _speed = 50;

    #region "Fields"
    private Quaternion _originRotation = default;
    private float _rotationY = 0f;
    #endregion

    private void OnEnable()
    {
        _rotationY = 0;
        _originRotation = Quaternion.Euler(_defaultAngle);

        this.StartLateUpdate();
    }

    private void OnDisable()
    {
        this.StopLateUpdate();
    }

    public void OnSystemLateUpdate(float deltaTime)
    {
        Profiler.BeginSample("CoinsRotator:: OnSystemLateUpdate");

        RotateAllCoins(deltaTime);

        Profiler.EndSample();
    }

    private void RotateAllCoins(float deltaTime)
    {
        _rotationY += _speed * deltaTime;
        Quaternion rotationY = Quaternion.AngleAxis(-_rotationY, _axis);

        foreach (SimpleCrystal crystal in _crystals)
        {
            crystal.SetRotation(_originRotation * rotationY);
        }
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        _crystals = GetComponentsInChildren<SimpleCrystal>();
    }
#endif
}
