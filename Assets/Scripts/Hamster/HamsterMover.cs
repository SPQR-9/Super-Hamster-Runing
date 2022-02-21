using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class HamsterMover : MonoBehaviour
{
    public event UnityAction<float> SpeedChanged;

    [SerializeField] private bool _onGround = true;
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _maxFlyingSpeed = 1f;
    [SerializeField] private float _brakingForce = 1f;
    [SerializeField] private float _accelerationForce = 1f;
    [SerializeField] private float _accelerationFlyingForce = 1f;
    [SerializeField] private float _reboundability = 0.3f;
    [SerializeField] private bool _enableRepaymentZDeviation = true;
    [SerializeField] private float _zLimitDeviation = 0.8f;
    [SerializeField] private GameObject _stunEffect;
    [SerializeField] private float _rotationSpeed = 12f;

    private Rigidbody _rigidbody;
    private bool _isRun = false;
    private float _stunTimer = 0f;
    private float _currentSpeed;
    private Vector3 _direction;
    private bool _isWin = false;
    private bool _isLose = false;

    private float _startZPosition;

    public float MaxSpeed => _maxSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startZPosition = transform.position.z;
    }

    private void Update()
    {
        if (_stunTimer > 0)
        {
            _stunEffect.SetActive(true);
            _stunTimer -= Time.deltaTime;
        }
        else
            _stunEffect.SetActive(false);
        if (_onGround && _isRun && _stunTimer <= 0 && !_isWin && !_isLose)
            _currentSpeed = Mathf.Lerp(_currentSpeed, _maxSpeed, _accelerationForce * Time.deltaTime);
        else if(!_onGround && _isRun && _stunTimer <= 0 && !_isWin && !_isLose)
            _currentSpeed = Mathf.Lerp(_currentSpeed, _maxFlyingSpeed, _accelerationFlyingForce * Time.deltaTime);
        else 
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0, _brakingForce * Time.deltaTime);
        Move();
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
        if (transform.rotation.eulerAngles.x > 70 || transform.rotation.eulerAngles.x < -40)
        {
            Quaternion targetRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.right), _rotationSpeed * Time.deltaTime);
            _rigidbody.MoveRotation(targetRotation);
        }
        SpeedChanged(_currentSpeed);
    }

    private void ReturnToStartingZPosition()
    {
        if (transform.position.z > _startZPosition + _zLimitDeviation)
            _direction = new Vector3(1, 0, -0.3f);
        else if (transform.position.z < _startZPosition - _zLimitDeviation)
            _direction = new Vector3(1, 0, 0.3f);
    }

    public void PutOnGround()
    {
        _onGround = true;
    }

    public void FlyUp()
    {
        _onGround = false;
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

    public void Win()
    {
        _isWin = true;
    }

    public void Lose()
    {
        _isLose = true;
    }
}
