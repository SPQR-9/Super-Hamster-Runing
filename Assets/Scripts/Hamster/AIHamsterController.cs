using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hamster))]
public class AIHamsterController : MonoBehaviour
{
    private Hamster _hamster;
    private HamsterMover _hamsterMover;
    private Trap _trap = null;
    private bool _isRunning = false;

    private void Awake()
    {
        _hamster = GetComponent<Hamster>();
        _hamsterMover = GetComponent<HamsterMover>();
    }

    private void OnEnable()
    {
        _hamster.TrapInformationHasBeenTransmitted += SetTrapInfo;
        _hamster.StartedRunning += StartAI;
        _hamster.StopedRunning += StopAI;
    }

    private void OnDisable()
    {
        _hamster.TrapInformationHasBeenTransmitted -= SetTrapInfo;
        _hamster.StartedRunning -= StartAI;
        _hamster.StopedRunning -= StopAI;
    }

    private void Update()
    {
        if (!_isRunning)
            return;
        if (_trap != null)
        {
            if (_trap.RunPermissionForAI)
                Run();
            else
                Stop();
        }
        else
            Run();
    }

    private void Run()
    {
        _hamsterMover.Run();
    }

    private void Stop()
    {
        _hamsterMover.Stop();
    }

    public void SetTrapInfo(Trap trap)
    {
        _trap = trap;
    }

    public void StartAI()
    {
        _isRunning = true;
    }

    public void StopAI()
    {
        _isRunning = false;
    }
}
