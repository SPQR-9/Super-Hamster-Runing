using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HamsterAnimationController))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoneRebinder))]
public class HamsterMover : MonoBehaviour
{
    [SerializeField] private bool _runPermission = true;
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _brakingForce = 1f;
    [SerializeField] private float _accelerationForce = 1f;
    [SerializeField] private float _reboundability = 0.3f;
    [SerializeField] private bool _enableRepaymentZDeviation = true;
    [SerializeField] private float _zLimitDeviation = 0.8f;
    [SerializeField] private GameObject _stunEffect;

    private BoneRebinder _boneRebinder;
    private Rigidbody _rigidbody;
    private HamsterAnimationController _animationController;
    private bool _isRun = false;
    private float _stunTimer = 0f;
    private float _currentSpeed;
    private Vector3 _direction;
    private bool _isWin = false;
    private bool _isLose = false;

    private float _startZPosition;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animationController = GetComponent<HamsterAnimationController>();
        _boneRebinder = GetComponent<BoneRebinder>();
        _startZPosition = transform.position.z;
    }

    private void Update()
    {
        _animationController.CheckRunningSpeed(_currentSpeed, _maxSpeed);
        if (_stunTimer > 0)
        {
            _stunEffect.SetActive(true);
            _stunTimer -= Time.deltaTime;
        }
        else
            _stunEffect.SetActive(false);
        if (_runPermission && _isRun && _stunTimer <= 0 && !_isWin && !_isLose)
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, _maxSpeed, _accelerationForce * Time.deltaTime);
            Move();
        }
        else 
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0, _brakingForce * Time.deltaTime);
            Move();
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

    private void Move()
    {
        _direction = Vector3.right;
        if (_enableRepaymentZDeviation)
            ReturnToStartingZPosition();
        _rigidbody.MovePosition(transform.position + _direction * _currentSpeed);
        
    }

    private void ReturnToStartingZPosition()
    {
        if (transform.position.z > _startZPosition + _zLimitDeviation)
            _direction = new Vector3(1, 0, -0.3f);
        else if (transform.position.z < _startZPosition - _zLimitDeviation)
            _direction = new Vector3(1, 0, 0.3f);
    }

    public void AllowRunning()
    {
        _runPermission = true;
    }

    public void ProhibitRunning()
    {
        _runPermission = false;
    }

    public void ReboundAndStun(float stunTime)
    {
        _stunTimer = stunTime;
        _currentSpeed = -_currentSpeed * _reboundability;
    }

    public void Stun(float stunTime)
    {
        _stunTimer = stunTime;
        _currentSpeed = 0;
        _rigidbody.isKinematic = true;
        _rigidbody.isKinematic = false;
    }

    public void Fall()
    {
        _animationController.StartFallAnimation();
    }

    public void FlattenVertically()
    {
        _animationController.StartFlattenVerticallyAnimation();
    }

    public void FlattenHorizontal()
    {
        _animationController.StartFlattenHorizontalAnimation();
    }

    public void Win()
    {
        _isWin = true;
        _boneRebinder.RebindeBones();
        _animationController.StartWinningAnimation();
    }

    public void Lose()
    {
        _isLose = true;
    }
}
