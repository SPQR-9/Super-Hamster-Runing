using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private bool _isRun = true;
    [SerializeField] private float _speed = 1f;

    private void Update()
    {
        if(_isRun)
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }

    public void Run()
    {
        _isRun = true;
    }

    public void Stop()
    {
        _isRun = false;
    }
}
