using UnityEngine;
using DG.Tweening;


public class TransperentObstacle : MonoBehaviour
{
    [SerializeField] private Transform _body = default;
    [SerializeField] private MeshRenderer _meshRenderer = default;

    [Header("Animation Settings")]
    [SerializeField] private Vector3 _targetScale = default;
    [SerializeField] private float _doBiggerDuration = 0.5f;

    #region "Fields"
    private Sequence _sequence = default;
    #endregion

    private void Awake()
    {
        BuildSequence();
    }

    private void BuildSequence()
    {
        _sequence = DOTween.Sequence();

        _sequence
            .Pause()
            .OnStart(() => _body.gameObject.SetActive(true))
            .Append(_body.DOScale(_targetScale, _doBiggerDuration))
            .Join(_meshRenderer.material.DOFade(0, _doBiggerDuration + 1f));
    }

    public void DoBigger()
    {
        if (_sequence == null)
        {
            BuildSequence();
        }

        _sequence.Play();
    }
}
