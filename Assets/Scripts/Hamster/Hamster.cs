using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Hamster : MonoBehaviour
{
    public UnityEvent ActivateAfterFall;
    public UnityEvent ActivateAfterDeath;

    [SerializeField] private Type _status;

    private HamsterCollider[] _hamsterColliders;

    public Type HamsterStatus => _status;

    private void Awake()
    {
        _hamsterColliders = GetComponentsInChildren<HamsterCollider>();
    }

    private void OnEnable()
    {
        foreach (var hamsterCollider in _hamsterColliders)
        {
            hamsterCollider.AcceptedNewCommandForHamsterBody += ExecuteCommand;
        }
    }

    private void OnDisable()
    {
        foreach (var hamsterCollider in _hamsterColliders)
        {
            hamsterCollider.AcceptedNewCommandForHamsterBody -= ExecuteCommand;
        }
    }

    public void ExecuteCommand(Command command)
    {
        switch (command)
        {
            case Command.Fall:
                ActivateAfterFall?.Invoke();
                break;
            case Command.Death:
                ActivateAfterDeath?.Invoke();
                break;
            default:
                break;
        }
    }

}

[Serializable]

public enum Command
{
    Fall,
    Death
}

