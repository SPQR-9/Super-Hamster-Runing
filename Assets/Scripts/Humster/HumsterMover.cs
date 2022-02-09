using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumsterMover : MonoBehaviour
{
    [SerializeField] private bool _isRun = true;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Rigidbody _rigidbody;

    private void Update()
    {
        if(_isRun)
        {
            /*_rigidbody.AddTorque(Vector3.forward * _speed);*/
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }

    public void Run()
    {
        _isRun = true;
    }

    public void Stop()
    {
        _isRun = false;
    }
}
