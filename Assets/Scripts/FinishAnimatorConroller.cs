using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FinishAnimatorConroller : MonoBehaviour
{
    private string _win = "Win";
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartWinAnimation()
    {
        _animator.SetTrigger(_win);
    }
}
