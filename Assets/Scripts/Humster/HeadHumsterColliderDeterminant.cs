using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHumsterColliderDeterminant : MonoBehaviour
{
    [SerializeField] private HumsterMover _mover;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.TryGetComponent(out Wall _))
            _mover.Stun();
    }
}
