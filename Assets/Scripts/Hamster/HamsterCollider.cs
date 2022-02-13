using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HamsterCollider : MonoBehaviour
{
    public event UnityAction<Command> AcceptedNewCommandForHamsterBody;
    public event UnityAction<Trap> InformationAboutTrapWasTransmitted;

    public void AcceptCommand(Command command)
    {
        AcceptedNewCommandForHamsterBody?.Invoke(command);
    }

    public void TransmitteInfoAboutTrap(Trap trap)
    {
        InformationAboutTrapWasTransmitted?.Invoke(trap);
    }
}

