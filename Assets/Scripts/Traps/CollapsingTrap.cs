using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CollapsingTrap : Trap
{
    [SerializeField] private float _pausedTime = 2.5f;

    private Animator _animator;
    private float _currentTime = 0;

    private const string _hit = "Hit";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime>_pausedTime)
        {
            _currentTime = 0;
            _animator.SetTrigger(_hit);
        }
    }
}
