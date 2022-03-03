using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsDisabler : MonoBehaviour
{
    private Trap[] _traps;
 
    private void Awake()
    {
        _traps = GetComponentsInChildren<Trap>();
    }

    public void StopAllTraps()
    {
        foreach (var trap in _traps)
        {
            trap.StopTrapAnimation();
        }
    }
}
