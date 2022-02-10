using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumsterMover : MonoBehaviour
{
    [SerializeField] private bool _runPermission = true;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _timeStun = 2f;
    [SerializeField] private float _brakingForce = 1f;
    [SerializeField] private float _accelerationForce = 1f;
    [SerializeField] private float _reboundForce = 0.5f;

    private bool _isRun = false;
    private float _stunTimer = 0f;
    private float _currentSpeed;

    private void Update()
    {
        if (_stunTimer > 0)
            _stunTimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0))
            _isRun = true;
        else
            _isRun = false;
        if (_runPermission && _isRun && _stunTimer <= 0)
        {
            /*_currentSpeed = _speed;*/
            _currentSpeed = Mathf.Lerp(_currentSpeed, _speed, _accelerationForce * Time.deltaTime);
            Debug.LogWarning(_currentSpeed);
            Run();
        }
        else 
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0, _brakingForce * Time.deltaTime);
            Run();
        }
        Debug.LogWarning(_currentSpeed);
    }

    private void Run()
    {
        _rigidbody.MovePosition(transform.position + Vector3.right.normalized * _currentSpeed);
    }

    public void AllowRunning()
    {
        _runPermission = true;
    }

    public void ProhibitRunning()
    {
        _runPermission = false;
    }

    public void Stun()
    {
        _stunTimer = _timeStun;
        _currentSpeed = -_currentSpeed * _reboundForce;
        Debug.LogWarning("Impulse");
    }
}
