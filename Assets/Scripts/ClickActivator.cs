using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClickActivator : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent ActivateAfterClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        ActivateAfterClick?.Invoke();
    }
}
