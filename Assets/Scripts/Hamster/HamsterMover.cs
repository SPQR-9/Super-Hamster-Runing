using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
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
    [SerializeField] private float _criticalAngular = 65f;

    private Rigidbody _rigidbody;
    private bool _isRun = false;
    private float _currentSpeed;
    private Vector3 _currentDirection;
    private Vector3 _targetDirection;
    private bool _isStoped = false;
    private bool _isRestrictionOnCorners = true;
    private bool _isTurnsAround = false;
    private float _stunTimer = 0f;
    private Collider _collider;

    public float MaxSpeed => _maxSpeed;
    public Vector3 Direction => _targetDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _targetDirection = Vector3.right;
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (transform.rotation.eulerAngles.x > _criticalAngular / 2 || transform.rotation.eulerAngles.x < -_criticalAngular / 2)
            _currentDirection = _targetDirection;
        else
            _currentDirection = transform.forward;
        if (_stunTimer>0)
            _stunTimer -= Time.deltaTime; 
        if (_onGround && _isRun && _stunTimer <= 0 && !_isStoped)
            _currentSpeed = Mathf.Lerp(_currentSpeed, _maxSpeed, _accelerationForce * Time.deltaTime);
        else if(!_onGround && _isRun && _stunTimer <= 0 && !_isStoped)
            _currentSpeed = Mathf.Lerp(_currentSpeed, _maxFlyingSpeed, _accelerationFlyingForce * Time.deltaTime);
        else 
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0, _brakingForce * Time.deltaTime);
        Move();
        if (_isTurnsAround)
            Turn();
        if (_isRestrictionOnCorners)
            YAngleChecker();

    }

    private void LateUpdate()
    {
        if (!_isTurnsAround && _isRestrictionOnCorners)
        {
            if (Mathf.Abs(transform.rotation.eulerAngles.y - Quaternion.LookRotation(_targetDirection).eulerAngles.y) > 0.5f)
                transform.rotation = Quaternion.LookRotation(_targetDirection);
        }
    }

    private void YAngleChecker()
    {
        if (transform.rotation.eulerAngles.x > _criticalAngular || transform.rotation.eulerAngles.x < -_criticalAngular)
        {
            Quaternion targetRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.right), _rotationSpeed * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(targetRotation);
        }
    }

    private void Turn()
    {
        Quaternion targetRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_targetDirection), _currentSpeed * _rotationSpeed * Time.fixedDeltaTime);
        _rigidbody.MoveRotation(targetRotation);
        if ((int)transform.rotation.eulerAngles.y+1 == Quaternion.LookRotation(_targetDirection).eulerAngles.y || (int)transform.rotation.eulerAngles.y - 1 == Quaternion.LookRotation(_targetDirection).eulerAngles.y)
        {
            transform.rotation = Quaternion.LookRotation(_targetDirection);
            _isTurnsAround = false;
            EnableRigidbodyRestriction();
        }
    }

    public void SetNewDirection(Vector3 direction)
    {
        _isTurnsAround = true;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
        _targetDirection = direction;
        
    }

    private void Move()
    {
        _rigidbody.MovePosition(transform.position + _currentDirection * _currentSpeed);
        /*transform.Translate(_currentDirection * _currentSpeed);*/
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

    public void DisablePhysics()
    {
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
        _isRestrictionOnCorners = false;
    }

    public void EnableKinematic()
    {
        _rigidbody.isKinematic = false;
    }

    public void DisableKinematic()
    {
        _rigidbody.isKinematic = true;
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
        if (_targetDirection == Vector3.back || _targetDirection == Vector3.forward)
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        else
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
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
