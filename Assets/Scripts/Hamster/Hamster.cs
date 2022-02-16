using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(HamsterMover))]
public class Hamster : MonoBehaviour
{
    public UnityEvent ActivateAfterFall;
    public UnityEvent ActivateAfterWin;

    public UnityAction<Trap> TrapInformationHasBeenTransmitted;

    [SerializeField] private HamsterType _type;

    private HamsterCollider[] _hamsterColliders;
    private HamsterMover _hamsterMover;
    private bool _isWin = false;
    private bool _isLose = false;

    public HamsterType Type => _type;
    public bool IsWin => _isWin;

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

    public void Win()
    {
        if (_isLose)
            return;
        _isWin = true;
        ActivateAfterWin?.Invoke();
        _hamsterMover.Win();
    }

    public void Lose()
    {
        _isLose = true;
        _hamsterMover.Lose();
    }
}

public enum HamsterType
{
    Player,
    AI
}


