using Ordering;
using UnityEngine;

namespace Data.Levels {
   /// <summary>
   /// Serializable data storage for level data.
   /// Add level related data here to distribute it across application.
   /// </summary>
   [CreateAssetMenu(menuName = "Levels/Level Data", fileName = "levelDat_", order = SOrder.Levels)]
   public class LevelData : ScriptableObject {
      [SerializeField]
      private int _completionReward = 50;

      [SerializeField]
      private int _stagesPerLevel = 4;
      
      #region [Properties]

      public int CompletionReward => _completionReward;

      // TODO used for LevelProgressBarSegmented. If not using -> remove it and related logic
      public int StagesPerLevel => _stagesPerLevel;
      
      #endregion
   }
}
