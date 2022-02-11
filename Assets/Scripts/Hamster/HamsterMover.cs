using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HamsterAnimationController))]
[RequireComponent(typeof(Rigidbody))]
public class HamsterMover : MonoBehaviour
{
    [SerializeField] private bool _runPermission = true;
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _brakingForce = 1f;
    [SerializeField] private float _accelerationForce = 1f;
    [SerializeField] private float _reboundability = 0.3f;
    [SerializeField] private float _timeStun = 2f;


    private Rigidbody _rigidbody;
    private HamsterAnimationController _animationController;
    private bool _isRun = false;
    private float _stunTimer = 0f;
    private float _currentSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animationController = GetComponent<HamsterAnimationController>();
    }

    private void Update()
    {
        _animationController.CheckRunningSpeed(_currentSpeed, _maxSpeed);
        if (_stunTimer > 0)
            _stunTimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0))
            _isRun = true;  
        if (_runPermission && _isRun && _stunTimer <= 0)
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, _maxSpeed, _accelerationForce * Time.deltaTime);
            Move();
        }
        else 
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0, _brakingForce * Time.deltaTime);
            Move();
        }
        _isRun = false;
    }

    private void Move()
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

    public void ReboundAndStun(float reboundForce = 1)
    {
        _stunTimer = _timeStun;
        _currentSpeed = -_currentSpeed * _reboundability * reboundForce;
    }

    public void Fall()
    {
        _animationController.StartFallAnimation();
    }
}
