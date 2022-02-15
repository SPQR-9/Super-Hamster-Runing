using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraController : MonoBehaviour
{
    private Animator _animator;

    private const string _cameraNumber = "CameraNumber";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeCameraAngle(int number)
    {
        _animator.SetInteger(_cameraNumber, number);
    }
}
