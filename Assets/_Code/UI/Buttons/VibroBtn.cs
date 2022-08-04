using Data;
using Profile;
using UI.Buttons;
using UnityEngine;
using UniRx;

public class VibroBtn : MonoBehaviour
{
    [SerializeField] private DisabledStateBtn _vibroBtn = default;

    private void Awake()
    {
        _vibroBtn.OnClick.AddListener(ToggleVibroState);
        _vibroBtn.SetState(!PlayerProfile.VibroDisabled);

        Hub.VibroDisabled.Subscribe(disabled => _vibroBtn.SetState(!disabled)).AddTo(this);
    }

    private void ToggleVibroState()
    {
        PlayerProfile.VibroDisabled = !PlayerProfile.VibroDisabled;
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        TryGetComponent(out _vibroBtn);
    }
#endif
}
