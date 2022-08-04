using Data;
using Data.Levels;
using Data.RateUs;
using EditorExtensions.Attributes;
using TMPro;
using Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI.Screens
{
    /// <summary>
    /// Logic for the Fail Screen UI
    /// </summary>
    public class FailScreen : MonoBehaviour
    {
        [SerializeField, RequireInput]
        private GameObject _root = default;

        [Space]
        [SerializeField, RequireInput]
        private TMP_Text _levelFailedText = default;

        [SerializeField]
        private string _levelFailedStr = "Level {0} failed!";

        [SerializeField, RequireInput]
        private ABTweener _fadeTweener = default;

        [SerializeField, RequireInput]
        private Button _restartBtn = default;

        [SerializeField, RequireInput]
        private Button _backToLobbyBtn = default;

        #region [Fields]

        private bool _isActive = false;

        #endregion

        private void Awake()
        {
            _restartBtn.onClick.AddListener(ReloadLevel);
            _backToLobbyBtn.onClick.AddListener(GoToLobby);

            Hub.RequestLobbyTransition.Subscribe(x =>
            {
                if(_isActive) Toggle(false);
            }).AddTo(this);

            Hub.ShowRateUs.Subscribe(x =>
            {
                if (_isActive) Toggle(false);
            }).AddTo(this);

            Hub.LevelDataLoaded.Subscribe(x =>
            {
                SetupState(x);
                if (_isActive) Toggle(false);
            }).AddTo(this);

            Hub.LevelFailed.Subscribe(x => Toggle(true)).AddTo(this);

            _root.SetActive(false);
        }

        private void SetupState(LevelMetaData metaData)
        {
            _levelFailedText.text = string.Format(_levelFailedStr, (metaData.VisualLevelIndex + 1).CachedString());
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

        private void ReloadLevel()
        {
            if (!_isActive) return;

            LoadingScreen.DoFade.Fire();

            _fadeTweener.DoA();

            _isActive = false;
        }

        private void GoToLobby()
        {
            if (!_isActive) return;

            LoadingScreen.DoFade.Fire();
        }
    }
}