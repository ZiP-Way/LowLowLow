using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;
using Data;
using SignalsFramework;
using System;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _loadingImage = default;
    [SerializeField] private float _delayBeforeHiding = 1f;

    #region "Signals"
    public static readonly Signal DoFade = new Signal();
    #endregion

    #region "Fields"
    private Sequence _show = default;
    private Sequence _hide = default;

    private CompositeDisposable _disposable = default;
    #endregion

    private void Awake()
    {
        _loadingImage.gameObject.SetActive(false);
        BuildAnimation();

        LoadingScreen.DoFade.Subscribe(_ =>
            {
                Show();
            }).AddTo(this);
    }

    private void OnEnable()
    {
        _disposable = new CompositeDisposable();
    }

    private void OnDisable()
    {
        _disposable.Dispose();
    }

    private void BuildAnimation()
    {
        _hide = DOTween.Sequence()
            .Pause()
            .SetAutoKill(false);

        _hide.Append(_loadingImage.DOFade(0, 0.25f))
            .OnComplete(() => _loadingImage.gameObject.SetActive(false));

        _show = DOTween.Sequence()
            .Pause()
            .SetAutoKill(false);

        _show.Append(_loadingImage.DOFade(1, 0.25f))
            .OnComplete(() =>
            {
                Hub.LoadLevel.Fire();
                Hub.RequestLobbyTransition.Fire();

                Observable.Timer(TimeSpan.FromSeconds(_delayBeforeHiding))
                .Subscribe(_ =>
                {
                    Hide();
                }).AddTo(_disposable);
            });
    }

    private void Show()
    {
        if (!_show.IsPlaying())
        {
            _loadingImage.gameObject.SetActive(true);
            _show.Restart();
        }
    }

    private void Hide()
    {
        if (!_hide.IsPlaying())
        {
            _hide.Restart();
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _loadingImage = GetComponentInChildren<Image>();
    }
#endif
}
