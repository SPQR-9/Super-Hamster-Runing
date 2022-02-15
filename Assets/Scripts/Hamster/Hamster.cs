using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(HamsterMover))]
public class Hamster : MonoBehaviour
{
    public UnityEvent ActivateAfterFall;
    public UnityEvent ActivateAfterDeath;

    public UnityAction<Trap> TrapInformationHasBeenTransmitted;

    [SerializeField] private HamsterType _type;

    private HamsterCollider[] _hamsterColliders;
    private HamsterMover _hamsterMover;

    public HamsterType Type => _type;

    private void Awake()
    {
        _hamsterColliders = GetComponentsInChildren<HamsterCollider>();
        _hamsterMover = GetComponent<HamsterMover>();
    }

    private void OnEnable()
    {
        foreach (var hamsterCollider in _hamsterColliders)
        {
            hamsterCollider.SetHamster(this);
        }
    }

    public void Fall()
    {
        ActivateAfterFall?.Invoke();
    }

    public void SetInfoAboutTrap(Trap trap)
    {
        TrapInformationHasBeenTransmitted?.Invoke(trap);
    }

    public void ReboundAndStun(float stunTime)
    {
        _hamsterMover.ReboundAndStun(stunTime);
    }

    public void Stun(float stunTime)
    {
        _hamsterMover.Stun(stunTime);
    }

    public void FlattenVertically()
    {
        _hamsterMover.FlattenVertically();
    }
    
    public void FlattenHorizontal()
    {
        _hamsterMover.FlattenHorizontal();
    }
}

public enum HamsterType
{
    Player,
    AI
}


