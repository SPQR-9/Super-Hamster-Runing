using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HamsterAnimationController : MonoBehaviour
{
    private Animator _animator;

    private const string _isRun = "IsRun";
    private const string _isFall = "IsFall";
    private const string _runningSpeed = "RunningSpeed";


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void CheckRunningSpeed(float speed, float maxSpeed)
    {
        if (speed > 0.15f || speed < -0.02f)
        {
            _animator.SetBool(_isRun, true);
            float runningSpeed = speed / maxSpeed;
            if (speed > maxSpeed)
                runningSpeed = 1f;
            _animator.SetFloat(_runningSpeed, runningSpeed);
        }
        else
        {
            _animator.SetBool(_isRun, false);
            _animator.SetFloat(_runningSpeed, 0);
        }
    }

    public void StartFallAnimation()
    {
        _animator.SetBool(_isFall, true);
    }
}
