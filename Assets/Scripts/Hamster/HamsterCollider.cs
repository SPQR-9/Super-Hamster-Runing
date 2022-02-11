using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HamsterCollider : MonoBehaviour
{
    public event UnityAction<Command> AcceptedNewCommand;

    public void AcceptCommand(Command command)
    {
        AcceptedNewCommand?.Invoke(command);
    }
}

