using System;
using Data;
using Data.Levels;
using Data.RateUs;
using EditorExtensions.Attributes;
using FX.CoinFX;
using Profile;
using TMPro;
using Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI.Screens
{
    /// <summary>
    /// Logic for the level completion UI
    /// </summary>
    public class WinScreen : MonoBehaviour
    {
        [SerializeField]
        private float _delayAfterFlightCompletion = 0.5f;

        [Space]
        [SerializeField, RequireInput]
        private GameObject _root = default;

        [SerializeField, RequireInput]
        private ABTweener _fadeTweener = default;

        [SerializeField, RequireInput]
        private SoftCurrencyPanel _softCurrencyPanel = default;

        [SerializeField, RequireInput]
        private FlightCoinFX _coinFX = default;

        [Space]
        [SerializeField, RequireInput]
        private TMP_Text _levelCompleteText = default;

        [SerializeField]
        private string _levelCompleteStr = "Level {0} completed!";

        [SerializeField, RequireInput]
        private TMP_Text _rewardValueText = default;

        [SerializeField, RequireInput]
        private Button _continueBtn = default;

        [SerializeField, RequireInput]
        private Button _backToLobbyBtn = default;

        #region [Fields]

        private int _completionReward;
        private bool _isActive = false;

        private LevelMetaData _metaData;

        #endregion

        private void Awake()
        {
            // Load next level when coin animation is done
            // Could be different, e.g. go to main menu instead
            _softCurrencyPanel.AnimationComplete
                              .Delay(TimeSpan.FromSeconds(_delayAfterFlightCompletion))
                              .Subscribe(x =>
                              {
                                  LoadingScreen.DoFade.Fire();
                              })
                              .AddTo(this);

            Hub.RequestLobbyTransition.Subscribe(x =>
            {
                if (_isActive) Toggle(false);
            }).AddTo(this);

            Hub.LevelDataLoaded.Subscribe(x =>
            {
                _completionReward = x.LevelData.CompletionReward;
                Toggle(false);
            }).AddTo(this);

            PlayerCrystalCollector.EarnedCoins.Subscribe(earnedCoins =>
            {
                _completionReward += earnedCoins;
                ShowAllocateRewards();

            }).AddTo(this);

            Hub.LevelDataLoaded.Subscribe(x => _metaData = x).AddTo(this);
            Hub.ShowRateUs.Subscribe(x => Toggle(false)).AddTo(this);

            _continueBtn.onClick.AddListener(OnContinueClick);
            _backToLobbyBtn.onClick.AddListener(BackToLobbyBtn);

            _root.SetActive(false);
        }

        private void BackToLobbyBtn()
        {
            if (!_isActive) return;

            _isActive = false;
            LoadingScreen.DoFade.Fire();
        }

        private void SetupInitialState()
        {
            _continueBtn.interactable = true;
            LevelData levelData = _metaData.LevelData;

            _levelCompleteText.text = string.Format(_levelCompleteStr, (_metaData.VisualLevelIndex + 1).CachedString());
            _rewardValueText.text = $"+{_completionReward}";
            _softCurrencyPanel.SetValue(PlayerProfile.SoftCurrency - _completionReward); // Pointed
        }

        private void OnContinueClick()
        {
            if (!_isActive) return;

            _continueBtn.interactable = false;
            _isActive = false;

            _coinFX.DoAnimation();
            _softCurrencyPanel.AnimateValuesToProfile(_completionReward); // Pointed
        }

        private void ShowAllocateRewards()
        {
            PlayerProfile.SoftCurrency += _completionReward;
            Toggle(true);
        }

        private void Toggle(bool state)
        {
            if (state)
            {
                // Need to post-pone initialization, 
                // since if data change while tweening out it will be visible for the user

                // Basically, OnEnable
                SetupInitialState();
                _fadeTweener.DoB();
            }
            else
            {
                _fadeTweener.DoA();
            }

            _isActive = state;
        }
    }
}
