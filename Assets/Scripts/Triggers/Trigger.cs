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
        if (other.TryGetComponent(out Hamster hamster))
        {
            _hamster = hamster;
            ActivateAfterAnyHamsterEntered?.Invoke();
            if (_hamster.Type == HamsterType.Player)
                ActivateAfterPlayerHamsterEntered?.Invoke();
            else
                ActivateAfterAIHamsterEntered?.Invoke();
        }
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
        _hamster.HitStun(stunTime);
    }

    public void Daze(float time)
    {
        _hamster.Daze(time);
    }

    public void InstantiateObject(GameObject gameObject)
    {
        Instantiate(gameObject, _otherCollider.transform);
    }

    public void Finish()
    {
        _hamster.Finish();
    }

    public void SetNewSpawnPointInHamster(Transform transformPoint)
    {
        _hamster.SetNewSpawnPoint(transformPoint);
    }

    public void WaiteRespawn(float pausedBeforeRespawn)
    {
        _hamster.WaiteRespawn(pausedBeforeRespawn);
    }

    public void Fall()
    {
        _hamster.ToFall();
    }

    public void DischargeIntoWater(bool isRightDirection)
    {
        if (isRightDirection)
            _hamster.DischargeIntoWater(_hamster.transform.right.normalized);
        else
            _hamster.DischargeIntoWater(-_hamster.transform.right.normalized);
    }

}



