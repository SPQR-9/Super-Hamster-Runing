using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HummerTrap : MonoBehaviour
{
    [SerializeField] private MovingPlatform _movingPlatform;
    [SerializeField] private float _hitTime = 6.5f;

    private Animator _animator;
    private float _timer = 0f;

    private const string _hit = "Hit";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _hitTime)
        {
            _timer = 0;
            _movingPlatform.Stop();
            _animator.SetTrigger(_hit);
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            _movingPlatform.Move();
        else
            _movingPlatform.Stop();
    }
}
