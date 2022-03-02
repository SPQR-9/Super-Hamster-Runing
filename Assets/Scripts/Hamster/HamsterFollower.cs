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
        _hamster.Fall += StopFollowing;
    }

    private void OnDisable()
    {
        _hamster.Discharged -= StopFollowing;
        _hamster.Respauned -= StartFollowing;
        _hamster.Fall -= StopFollowing;
    }

    private void Update()
    {
        if(_isFollowing)
            transform.position = new Vector3(_hamster.transform.position.x, _hamster.transform.position.y, _hamster.transform.position.z);
    }

    public void StopFollowing()
    {
        _isFollowing = false;
    }

    private void StartFollowing()
    {
        _isFollowing = true;
    }
}
