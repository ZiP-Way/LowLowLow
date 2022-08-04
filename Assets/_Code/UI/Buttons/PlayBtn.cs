using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayBtn : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent PointerDown;

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown.Invoke();
    }
}
