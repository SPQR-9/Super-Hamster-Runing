using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TimerTrap : Trap
{
    [SerializeField] private float _responseTime;

    private float _currentTime = 0;
    private const string _response = "Response";
    private const string _idle = "Idle";


    private void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(_idle))
        {
            _runPermissionForAI = true;
            _currentTime += Time.deltaTime;
        }
        else
            _runPermissionForAI = false;
        _animator.SetBool(_response, false);
        if (_currentTime >= _responseTime)
        {
            _animator.SetBool(_response,true);
            _currentTime = 0f;
        }
    }
}
