using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapControlledByAnimator : Trap
{
    [SerializeField] public bool MovePermission;

    private void Update()
    {
        _runPermissionForAI = MovePermission;
    }
}
