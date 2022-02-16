using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hamster))]
public class AIHamsterController : MonoBehaviour
{
    private Hamster _hamster;
    private HamsterMover _hamsterMover;
    private Trap _trap = null;

    private void Awake()
    {
        _hamster = GetComponent<Hamster>();
        _hamsterMover = GetComponent<HamsterMover>();
    }

    private void OnEnable()
    {
        _hamster.TrapInformationHasBeenTransmitted += SetTrapInfo;
    }

    private void OnDisable()
    {
        _hamster.TrapInformationHasBeenTransmitted -= SetTrapInfo;
    }

    private void Update()
    {
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
}
