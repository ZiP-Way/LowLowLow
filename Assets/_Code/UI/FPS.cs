using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private float deltaBetweenShow = 0.3f;

    private float timer = 0f;
    private float current = 0f;
    private float cash = 0f;
    private int count = 0;

    private void Awake()
    {
        Application.targetFrameRate = 60;

#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
        gameObject.SetActive(false);
#endif
    }

    private void Update()
    {
        timer += Time.deltaTime;
        current = (int)(1f / Time.unscaledDeltaTime);
        cash += current;
        count++;

        if (timer >= deltaBetweenShow)
        {
            timer = 0f;
            float res = cash / count;
            Mathf.RoundToInt(res);
            _text.text = Mathf.RoundToInt(res).ToString();
            cash = 0f;
            count = 0;
        }
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_text == null)
        {
            TryGetComponent(out _text);
        }
    }
#endif
}
