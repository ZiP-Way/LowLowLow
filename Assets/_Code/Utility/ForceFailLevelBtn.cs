using Data;
using EditorExtensions.Attributes;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Utility {
   /// <summary>
   /// Example how to force a level failed state
   /// </summary>
   public class ForceFailLevelBtn : MonoBehaviour {
      [SerializeField, RequireInput]
      private Button _button = default;

      private void Awake() {
         Hub.LevelDataLoaded.Subscribe(x => _button.interactable = true).AddTo(this);
         _button.onClick.AddListener(OnFailLevelClick);
      }

      private void OnFailLevelClick() {
         _button.interactable = false;
         Hub.LevelFailed.Fire();
      }

#if UNITY_EDITOR
      protected virtual void OnValidate() { _button = GetComponentInChildren<Button>(true); }
#endif
   }
}