using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TimerTrap : Trap
{
    [SerializeField] private float _responseTime;

    private Animator _animator;
    private float _timer;
    private const string _response = "Response";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _responseTime)
        {
            _animator.SetTrigger(_response);
            _timer = 0f;
        }
    }
}
