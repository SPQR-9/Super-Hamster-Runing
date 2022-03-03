using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HamsterMover))]
public class GroundChecker : MonoBehaviour
{
    private HamsterMover _mover;

    private void Awake()
    {
        _mover = GetComponent<HamsterMover>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _))
            _mover.PutOnGround();
    }

    private void OnCollisionExit(Collision collision)
    {
        _mover.FlyUp();
    }
}
