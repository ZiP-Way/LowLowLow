using Data;
using EditorExtensions.Attributes;
using Profile;
using Tweening;
using UniRx;
using UnityEngine;

namespace UI.Screens
{
    /// <summary>
    /// Game UI screen logic
    /// </summary>
    public class GameScreen : MonoBehaviour
    {
        [SerializeField, RequireInput]
        private GameObject _root = default;

        [SerializeField, RequireInput]
        private ABTweener _fadeTweener = default;

        [SerializeField, RequireInput]
        private SoftCurrencyPanel _softCurrencyPanel = default;

        #region [Fields]

        private bool _isActive = false;

        #endregion

        private void Awake()
        {
            // Ensure that awake on child component runs
            if (!_root.activeSelf) _root.SetActive(true);
            _root.SetActive(false);

            Hub.GameStarted.Subscribe(x =>
            {
                ResetToInitialState();
                Toggle(true);
            }).AddTo(this);

            Hub.LevelComplete.Subscribe(x => Toggle(false)).AddTo(this);
            Hub.LevelFailed.Subscribe(x => Toggle(false)).AddTo(this);

            Hub.RequestLobbyTransition.Subscribe(x =>
            {
                if (_isActive) Toggle(false);
            }).AddTo(this);
        }

        private void Start()
        {
            Toggle(false);
        }

        private void ResetToInitialState()
        {
            _softCurrencyPanel.SetValue(PlayerProfile.SoftCurrency);

            // Can be extended
        }

        private void Toggle(bool state)
        {
            _isActive = state;

            if (state)
            {
                _fadeTweener.DoB();
            }
            else
            {
                _fadeTweener.DoA();
            }
        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (_softCurrencyPanel == null) _softCurrencyPanel = GetComponentInChildren<SoftCurrencyPanel>(true);
        }
#endif
    }
}
