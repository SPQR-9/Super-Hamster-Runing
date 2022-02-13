using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : Trap
{
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;
    [SerializeField] private float _travelTime = 1f;

    [SerializeField] private float _minDesirablePassageLeghth;
    [SerializeField] private float _MaxDesirablePassageLeghth;

    private Rigidbody _rigidbody;
    private Vector3 _firstPosition;
    private Vector3 _secondPosition;
    private float _currentTime = 0;

    private Vector3 _targetPosition;
    private Vector3 _startPosition;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _firstPosition = _firstPoint.position;
        _secondPosition = _secondPoint.position;
        _startPosition = _firstPosition;
        _targetPosition = _secondPosition;
    }

    private void Update()
    {
        if(_currentTime>=_travelTime)
        {
            ChangeTargetPosition();
            _currentTime = 0;
        }
        _currentTime += Time.deltaTime;
        Vector3 newPosition = Vector3.Lerp(_startPosition, _targetPosition, _currentTime / _travelTime);
        _rigidbody.MovePosition(newPosition);
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

        
}
