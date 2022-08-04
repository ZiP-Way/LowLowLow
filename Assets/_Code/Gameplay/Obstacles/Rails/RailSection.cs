using UnityEngine;

public class RailSection : MonoBehaviour
{
    [SerializeField]
    private Transform[] progressIndicators;

    private float sectionPartMaxProgress;

    private void Awake()
    {
        sectionPartMaxProgress = 1f / progressIndicators.Length;
    }

    public void SetProgress(float sectionProgress)
    {
        for (int i = 0; i < progressIndicators.Length; i++)
        {
            Vector3 scale = progressIndicators[i].localScale;

            int progressIndex = (int)(sectionProgress / sectionPartMaxProgress);

            if (progressIndex > i)
            {
                scale.x = sectionPartMaxProgress;
            }
            else if (progressIndex < i)
            {
                scale.x = 0f;
            }
            else
            {
                scale.x = (sectionProgress - i * sectionPartMaxProgress);
            }

            progressIndicators[i].gameObject.SetActive(scale.x > 0.001f);

            progressIndicators[i].localScale = scale;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        sectionPartMaxProgress = 1f / progressIndicators.Length;
    }
#endif
}
