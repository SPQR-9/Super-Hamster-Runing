using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraController : MonoBehaviour
{
    private Animator _animator;

    private const string _cameraNumber = "CameraNumber";
    private const string _kill = "Kill";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeCameraAngle(int number)
    {
        _animator.SetInteger(_cameraNumber, number);
    }

    public void EnableKillCamera()
    {
        _animator.SetBool(_kill, true);
    }

    public void DisableKillCamera()
    {
        _animator.SetBool(_kill, false);
    }
}
