using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterFollower : MonoBehaviour
{
    [SerializeField] private Hamster _hamster;

    private bool _isFollowing = true;


    private void OnEnable()
    {
        _hamster.Discharged += StopFollowing;
        _hamster.Respauned += StartFollowing;
    }

    private void OnDisable()
    {
        _hamster.Discharged -= StopFollowing;
        _hamster.Respauned -= StartFollowing;
    }

    private void Update()
    {
        if(_isFollowing)
            transform.position = new Vector3(_hamster.transform.position.x, transform.position.y, _hamster.transform.position.z);
    }

    private void StopFollowing()
    {
        _isFollowing = false;
    }

    private void StartFollowing()
    {
        _isFollowing = true;
    }
}
