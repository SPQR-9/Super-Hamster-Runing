using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private HamsterMover _mover;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _))
            _mover.AllowRunning();
    }

    private void OnCollisionExit(Collision collision)
    {
        _mover.ProhibitRunning();
    }
}
