using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideRail : MonoBehaviour
{
    [SerializeField]
    private RailSection[] sections;

    public void SetProgress(float progress)
    {
        float sectionMaxProgress = 1f / sections.Length;

        for (int i = 0; i < sections.Length; i++)
        {
            float progressIndex = (int)(progress / sectionMaxProgress);

            if (progressIndex > i)
            {
                sections[i].SetProgress(1f);
            }
            else if (progressIndex < i)
            {
                sections[i].SetProgress(0f);
            }
            else
            {
                sections[i].SetProgress((progress - i * sectionMaxProgress) / sectionMaxProgress);
            }
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        sections = GetComponentsInChildren<RailSection>();
    }
#endif
}
