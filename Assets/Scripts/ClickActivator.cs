using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClickActivator : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent ActivateAfterClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        ActivateAfterClick?.Invoke();
    }
}
