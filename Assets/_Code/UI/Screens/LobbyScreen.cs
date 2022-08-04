using Data;
using Data.Levels;
using EditorExtensions.Attributes;
using Profile;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI.Screens
{
    /// <summary>
    /// Lobby Screen display logic
    /// </summary>
    public class LobbyScreen : MonoBehaviour
    {
        [SerializeField, RequireInput]
        private GameObject _root = default;

        [SerializeField, RequireInput]
        private PlayBtn _playBtn = default;

        [SerializeField, RequireInput]
        private SoftCurrencyPanel _softCurrencyPanel = default;

        [SerializeField, RequireInput]
        private GameObject _overlayClickBlocker = default;

        [SerializeField]
        private string _currentLevelStr = default;

        [SerializeField]
        private TMP_Text _currentLevelText = default;

        #region [Fields]

        private bool _startGameRequested;
        private bool _isActive = false;

        private LevelMetaData _metaData;

        #endregion

        private void Awake()
        {
            _playBtn.PointerDown.AddListener(OnPlayBtnClicked);

            Hub.RequestLobbyTransition.Subscribe(x =>
            {
                ResetToInitialState();
            }).AddTo(this);

            Hub.GameStarted.Subscribe(x =>
            {
                _overlayClickBlocker.SetActive(true);
                _startGameRequested = true;

                // Could also perform some animation here when game state is loading if necessary
                // after which disable LobbyScreen
                Toggle(false);
            }).AddTo(this);

            Hub.LevelDataLoaded.Subscribe(x =>
            {
                _metaData = x;

                Toggle(true);
            }).AddTo(this);

            _overlayClickBlocker.SetActive(false);
        }

        private void ResetToInitialState()
        {
            _startGameRequested = false;
            _overlayClickBlocker.SetActive(false);

            // Feel free to expand if anything is required
        }

        private void OnPlayBtnClicked()
        {
            if (_startGameRequested) return;
            if (!_isActive) return;

            Hub.GameStarted.Fire();
        }

        private void Toggle(bool state)
        {
            if (_isActive == state) return;

            _root.SetActive(state);
            _isActive = state;

            if (state)
            {
                _softCurrencyPanel.SetValue(PlayerProfile.SoftCurrency);
                _currentLevelText.text = string.Format(_currentLevelStr, (_metaData.VisualLevelIndex + 1).CachedString());
            }
        }
    }
}