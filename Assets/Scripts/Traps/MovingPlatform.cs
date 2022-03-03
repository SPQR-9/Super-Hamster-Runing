using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : Trap
{
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;
    [SerializeField] private float _travelTime = 2f;
    [SerializeField] private float _minRunTime;
    [SerializeField] private float _maxRunTime;
    private bool _isMove = true;
    private Rigidbody _rigidbody;
    private Vector3 _firstPosition;
    private Vector3 _secondPosition;
    private float _currentTime = 0;
    private float _aiTimer;

    private Vector3 _targetPosition;
    private Vector3 _startPosition;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _firstPosition = _firstPoint.position;
        _secondPosition = _secondPoint.position;
        _startPosition = _firstPosition;
        _targetPosition = _secondPosition;
        _aiTimer = _travelTime;
    }

    private void OnEnable()
    {
        Stoped += Stop;
    }

    private void OnDisable()
    {
        Stoped -= Stop;
    }

    private void Update()
    {
        if (!_isMove)
            return;
        if(_currentTime>=_travelTime)
        {
            ChangeTargetPosition();
            _currentTime = 0;
        }
        _currentTime += Time.deltaTime;
        _aiTimer += Time.deltaTime;
        if (_aiTimer >= _travelTime * 2)
            _aiTimer = 0;
        Vector3 newPosition = Vector3.Lerp(_startPosition, _targetPosition, _currentTime / _travelTime);
        _rigidbody.MovePosition(newPosition);
        if (_aiTimer >= _minRunTime && _aiTimer <= _maxRunTime)
            _runPermissionForAI = true;
        else
            _runPermissionForAI = false;
    }

    private void ChangeTargetPosition()
    {
        if (_targetPosition == _secondPosition)
        {
            _startPosition = _secondPosition;
            _targetPosition = _firstPosition;
        }
        else
        {
            _startPosition = _firstPosition;
            _targetPosition = _secondPosition;
        }
    }

    public void Move()
    {
        _isMove = true;
    }

    public void Stop()
    {
        _isMove = false;
    }
}
