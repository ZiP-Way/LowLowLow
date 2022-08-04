using Data;
using Data.Levels;
using EditorExtensions.Attributes;
using UniRx;
using UnityEngine;

namespace UI.Progress {
   /// <summary>
   /// Progress bar for current level (segmented)
   /// </summary>
   public class LevelProgressBarSegmented : MonoBehaviour {
      [SerializeField, RequireInput]
      private ProgressBarMultiSegmentUI _progressBar = default;
      
      #region [Fields]

      private int _totalStages;
      
      #endregion

      private void Awake() {
         Hub.LevelProgressChanged.Subscribe(UpdateProgress).AddTo(this);
         Hub.LevelDataLoaded.Subscribe(SetupProgressBar).AddTo(this);
      }

      private void SetupProgressBar(LevelMetaData data) {
         _totalStages = data.LevelData.StagesPerLevel;
         
         _progressBar.SetSegmentCount(_totalStages);
         _progressBar.FillFirst(0, true, true);
      }

      private void UpdateProgress(float progress) {
         int filledSegments = Mathf.FloorToInt(_totalStages * progress);
         _progressBar.FillFirst(filledSegments);
      }

#if UNITY_EDITOR
      protected virtual void OnValidate() {
         if (_progressBar == null) _progressBar = GetComponentInChildren<ProgressBarMultiSegmentUI>(true);
      }
#endif
   }
}