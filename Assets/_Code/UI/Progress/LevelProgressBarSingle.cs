using Data;
using TMPro;
using UI.ProgressBar;
using UniRx;
using UnityEngine;
using Utility;

namespace UI.Progress
{
    /// <summary>
    /// Display logic for single segment progress bar
    /// </summary>
    public class LevelProgressBarSingle : MonoBehaviour
    {
        [SerializeField]
        private ProgressBarUI _progressBar = default;

        [SerializeField]
        private TMP_Text _currentLevelText = default;

        private void Awake()
        {
            Hub.LevelDataLoaded.Subscribe(x => _progressBar.ProgressChanged(0f, true)).AddTo(this);

            Hub.LevelProgressChanged.Subscribe(x => _progressBar.ProgressChanged(x)).AddTo(this);
            Hub.LevelDataLoaded.Subscribe(x => UpdateLevelText(x.VisualLevelIndex)).AddTo(this);
        }

        private void UpdateLevelText(int currentLevel)
        {
            _currentLevelText.text = string.Format("Level {0}", (currentLevel + 1).CachedString());
        }
    }
}