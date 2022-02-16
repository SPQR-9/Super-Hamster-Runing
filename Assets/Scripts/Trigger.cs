using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent ActivateAfterAnyHamsterEntered;
    public UnityEvent ActivateAfterPlayerHamsterEntered;
    public UnityEvent ActivateAfterAIHamsterEntered;

    private Hamster _hamster;
    private Collider _otherCollider;

    private void OnTriggerEnter(Collider other)
    {
        _otherCollider = other;
        if (other.TryGetComponent(out HamsterCollider hamsterCollider))
        {
            _hamster = hamsterCollider.GetHamster();
            ActivateAfterAnyHamsterEntered?.Invoke();
            if (_hamster.Type == HamsterType.Player)
                ActivateAfterPlayerHamsterEntered?.Invoke();
            else
                ActivateAfterAIHamsterEntered?.Invoke();
        }
    }

    public void FallOrder()
    {
        _hamster.Fall();
    }
    
    public void TransmitInformationAboutTrap(Trap trap)
    {
        _hamster.SetInfoAboutTrap(trap);
    }

    public void TransmitNullInformationAboutTrap()
    {
        _hamster.SetInfoAboutTrap(null);
    }

    public void Stun(float stunTime)
    {
        _hamster.Stun(stunTime);
    }

    public void InstantiateObject(GameObject gameObject)
    {
        Instantiate(gameObject, _otherCollider.transform);
    }

    public void Win()
    {
        _hamster.Win();
    }
}



