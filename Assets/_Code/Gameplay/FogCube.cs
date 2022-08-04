using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogCube : MonoBehaviour
{
    [SerializeField]
    private Transform body;

    [SerializeField]
    private Vector3 minLocalPosition;

    [SerializeField]
    private Vector3 maxLocalPosition;

    private float progress;

    private float speed;

    private int direction = 1;

    private void Awake()
    {
        progress = Random.Range(0f, 1f);

        speed = Random.Range(0.05f, 0.1f);

        direction = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    private void Update()
    {
        progress += direction * speed * Time.deltaTime;

        if (progress < 0)
        {
            progress = 0;

            direction = 1;
        }
        else if (progress > 1)
        {
            progress = 1;

            direction = -1;
        }

        body.localPosition = Vector3.Lerp(minLocalPosition, maxLocalPosition, EaseInOutSine(progress));
    }

    private float EaseInOutSine(float x)
    {
        return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
    }
}
