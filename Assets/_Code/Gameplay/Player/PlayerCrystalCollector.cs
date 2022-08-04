using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Data;
using SignalsFramework;

public class PlayerCrystalCollector : MonoBehaviour
{
    #region "Signals"

    public static readonly Signal<int> EarnedCoins = new Signal<int>();

    #endregion

    #region "Fields"

    private CompositeDisposable _disposable = default;
    private int _earnedCoins = 0;

    #endregion

    private void Awake()
    {
        _disposable = new CompositeDisposable();

        Hub.LevelComplete.Subscribe(x =>
        {
            PlayerCrystalCollector.EarnedCoins.Fire(_earnedCoins);
        }).AddTo(this);

        Hub.LoadLevel.Subscribe(x =>
        {
            _earnedCoins = 0;
        }).AddTo(this);
    }

    private void Start()
    {
        this.OnTriggerEnterAsObservable().Subscribe(other =>
        {
            if (other.TryGetComponent(out SimpleCrystal crystal))
            {
                crystal.Disable();
                _earnedCoins++;
            }
        }).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }
}
