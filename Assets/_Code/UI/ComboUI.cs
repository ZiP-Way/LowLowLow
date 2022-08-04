using UnityEngine;
using UniRx;
using TMPro;
using DG.Tweening;

public class ComboUI : MonoBehaviour
{
    [SerializeField] private string[] _comboTexts = default;

    [Header("Components")]
    [SerializeField] private Transform _root = default;
    [SerializeField] private TMP_Text _additionText = default;
    [SerializeField] private TMP_Text _counterText = default;

    [Header("Duration Settings")]
    [SerializeField] private float _doBiggerDuration = 0.3f;
    [SerializeField] private float _doTransparentDuration = 0.8f;

    #region "Fields"
    private Sequence _sequence = default;
    #endregion

    private void Awake()
    {
        _sequence = DOTween.Sequence();
        BuildAnimation();

        CompletedObstaclesCounter.CountOfCompletedObstaclesChanged
            .Subscribe(x =>
            {
                SetTexts(x, _comboTexts[Random.Range(0, _comboTexts.Length)]);
                RunAnimation();
            }).AddTo(this);
    }

    private void BuildAnimation()
    {
        _sequence.Pause();
        _sequence.SetAutoKill(false);

        _sequence.Append(_root.DOScale(1.1f, _doBiggerDuration));
        _sequence.Append(_root.DOScale(1f, _doBiggerDuration / 2));
        _sequence.Append(DOTween.To(() => _additionText.alpha, x => _additionText.alpha = x, 0, _doTransparentDuration));
        _sequence.Join(DOTween.To(() => _counterText.alpha, x => _counterText.alpha = x, 0, _doTransparentDuration));
        _sequence.OnComplete(() => _root.gameObject.SetActive(false));
    }
    private void SetTexts(int count, string text)
    {
        _counterText.text = "X" + count.ToString();
        _additionText.text = text.ToUpper();
    }

    private void ResetToDeffaultValues()
    {
        _root.localScale = Vector3.zero;
        _additionText.alpha = _counterText.alpha = 1;
    }

    private void RunAnimation()
    {
        _root.gameObject.SetActive(true);
        ResetToDeffaultValues();
        _sequence.Restart();
    }
}
