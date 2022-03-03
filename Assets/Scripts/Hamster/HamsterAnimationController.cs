using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Hamster))]
public class HamsterAnimationController : MonoBehaviour
{
    [SerializeField] private float _minPlaybackLimit = -0.02f;
    [SerializeField] private float _maxPlaybackLimit = 0.05f;

    private float _maxHamsterSpeed;
    private Hamster _hamster;
    private HamsterMover _hamsterMover;
    private Animator _animator;

    private const string _isRun = "IsRun";
    private const string _isFall = "IsFall";
    private const string _isWin = "IsWin";
    private const string _isLose = "IsLose";
    private const string _runningSpeed = "RunningSpeed";
    private const string _flatVertically = "FlatVertically";
    private const string _flatHorizontal = "FlatHorizontal";


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _hamster = GetComponent<Hamster>();
        _hamsterMover = GetComponent<HamsterMover>();
        _maxHamsterSpeed = _hamsterMover.MaxSpeed;
    }

    private void OnEnable()
    {
        _hamster.FlatHorizontal += StartFlattenHorizontalAnimation;
        _hamster.FlatVertically += StartFlattenVerticallyAnimation;
        _hamsterMover.SpeedChanged += CheckRunningSpeed;
        _hamster.Fall += StartFallAnimation;
        _hamster.Won += StartWinningAnimation;
        _hamster.Respauned += DisableFallAnimation;
        _hamster.Losed += StartLoseAnimation;
    }

    private void OnDisable()
    {
        _hamster.FlatHorizontal -= StartFlattenHorizontalAnimation;
        _hamster.FlatVertically -= StartFlattenVerticallyAnimation;
        _hamsterMover.SpeedChanged -= CheckRunningSpeed;
        _hamster.Fall -= StartFallAnimation;
        _hamster.Won -= StartWinningAnimation;
        _hamster.Respauned -= DisableFallAnimation;
        _hamster.Losed -= StartLoseAnimation;
    }

    public void CheckRunningSpeed(float speed)
    {
        if (speed > _maxPlaybackLimit || speed < _minPlaybackLimit)
        {
            _animator.SetBool(_isRun, true);
            float runningSpeed = speed / _maxHamsterSpeed;
            if (speed > _maxHamsterSpeed)
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

    public void StartFlattenVerticallyAnimation()
    {
        _animator.SetBool(_flatVertically,true);
    }

    public void StartLoseAnimation()
    {
        _animator.SetBool(_isLose, true);
    }
    
    public void DisableFallAnimation()
    {
        _animator.SetBool(_isFall, false);
    }

    public void StartFlattenHorizontalAnimation()
    {
        _animator.SetBool(_flatHorizontal,true);
    }

    public void StartWinningAnimation()
    {
        _animator.SetBool(_isWin,true);
    }
}
