using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TemporaryPlatform : Trap
{
    [SerializeField] private float _holdingTime = 1f;

    private bool _isFalled = false;
    private Rigidbody _rigidbody;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _runPermissionForAI = true;
    }


    private void Update()
    {
        if (!_isFalled)
            return;
        _holdingTime -= Time.deltaTime;
        if(_holdingTime<=0)
        {
            _rigidbody.isKinematic = false;
        }
    }

    public void StartFall()
    {
        _isFalled = true;
    }
}
