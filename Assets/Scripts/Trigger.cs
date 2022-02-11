using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent ActivateAfterHamsterEntered;

    [SerializeField] private TriggerType _triggerType;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.TryGetComponent(out HamsterCollider hamsterCollider))
        {
            switch (_triggerType)   
            {
                case TriggerType.None:
                    break;
                case TriggerType.Fall:
                    hamsterCollider.AcceptCommand(Command.Fall);
                    break;
                case TriggerType.Death:
                    break;
            }
            ActivateAfterHamsterEntered?.Invoke();
        }
    }
}

[Serializable]

public enum TriggerType
{
    None,
    Fall,
    Death
}

