using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class HamsterMover : MonoBehaviour
{
    public event UnityAction<float> SpeedChanged;
    public event UnityAction<float> Stuned;

    [SerializeField] private bool _onGround = true;
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _maxFlyingSpeed = 1f;
    [SerializeField] private float _brakingForce = 1f;
    [SerializeField] private float _accelerationForce = 1f;
    [SerializeField] private float _accelerationFlyingForce = 1f;
    [SerializeField] private float _reboundability = 0.3f;
    [SerializeField] private float _rotationSpeed = 12f;
    [SerializeField] private float _discardForce = 250f;
    [SerializeField] private float _criticalAngular = 65;

    private Rigidbody _rigidbody;
    private bool _isRun = false;
    private float _currentSpeed;
    private Vector3 _direction;
    private bool _isStoped = false;
    private bool _isRestrictionOnCorners = true;
    private float _stunTimer = 0f;
    private RigidbodyConstraints _rigidbodyConstraints;

    public float MaxSpeed => _maxSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbodyConstraints = _rigidbody.constraints;
    }

    private void Update()
    {
        if(transform.rotation.eulerAngles.x > _criticalAngular || transform.rotation.eulerAngles.x < -_criticalAngular)
            _direction = Vector3.right.normalized;
        else
            _direction = transform.forward.normalized;
        if (_stunTimer>0)
            _stunTimer -= Time.deltaTime; 
        if (_onGround && _isRun && _stunTimer <= 0 && !_isStoped)
            _currentSpeed = Mathf.Lerp(_currentSpeed, _maxSpeed, _accelerationForce * Time.deltaTime);
        else if(!_onGround && _isRun && _stunTimer <= 0 && !_isStoped)
        {
            _direction = Vector3.right.normalized;
            _currentSpeed = Mathf.Lerp(_currentSpeed, _maxFlyingSpeed, _accelerationFlyingForce * Time.deltaTime);
        }
        else 
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0, _brakingForce * Time.deltaTime);
        Move();
        if(_isRestrictionOnCorners)
            AngleChecker();

    }

    private void AngleChecker()
    {
        if (transform.rotation.eulerAngles.x > _criticalAngular || transform.rotation.eulerAngles.x < -_criticalAngular)
        {
            Quaternion targetRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.right), _rotationSpeed * Time.deltaTime);
            _rigidbody.MoveRotation(targetRotation);
        }
    }

    private void Move()
    {
        _rigidbody.MovePosition(transform.position + _direction * _currentSpeed);
        SpeedChanged(_currentSpeed);
    }

    public void Run()
    {
        _isRun = true;
    }

    public void Stop()
    {
        _isRun = false;
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
        Stuned?.Invoke(stunTime);
        _stunTimer = stunTime;
        _currentSpeed = -_currentSpeed * _reboundability;
    }

    public void HitStun(float stunTime)
    {
        Daze(stunTime);
        _currentSpeed = 0;
        _rigidbody.isKinematic = true;
        _rigidbody.isKinematic = false;
    }

    public void EnableRigidbodyRestriction()
    {
        _rigidbody.constraints = _rigidbodyConstraints;
        _rigidbody.isKinematic = true;
        _rigidbody.isKinematic = false;
        _isRestrictionOnCorners = true;
    }
    
    public void DisableRigidbodyRestriction()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        _isRestrictionOnCorners = false;
    }

    public void Daze(float stunTime)
    {
        Stuned?.Invoke(stunTime);
        _stunTimer = stunTime;
    }

    public void DiscardHamster(Vector3 direction)
    {
        _rigidbody.AddForce(direction * _discardForce, ForceMode.Impulse);
    }

    public void ProhibitMovement()
    {
        _isStoped = true;
    }
}
