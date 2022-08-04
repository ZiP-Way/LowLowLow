using UnityEngine;
using UniRx;

public class FeverProgressBar : MonoBehaviour
{
    [SerializeField] private PlayerFever _playerFever = default;
    [SerializeField] private DOTweenProgressBarUI _progressBar = default;
    [SerializeField] private float _doFillSpeed = 3f;

    #region "Fields"
    private float _doEmptySpeed = 5f;
    #endregion

    private void Awake()
    {
        _doEmptySpeed = _playerFever.Time;

        PlayerFever.ResetFever
            .Subscribe(_ =>
            {
                gameObject.SetActive(false);
                _progressBar.ResetProgressBar();
            }).AddTo(this);

        PlayerFever.FeverProgressChanged
            .Subscribe(x =>
            {
            gameObject.SetActive(true);
            _progressBar.ProgressChanged(x, _doFillSpeed);

            if (x == 1)
            {
                _progressBar.ProgressChanged(0, _doEmptySpeed, false);
            }
            }).AddTo(this);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _progressBar = GetComponent<DOTweenProgressBarUI>();
    }
#endif
}
