using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterFollower : MonoBehaviour
{
    [SerializeField] private Transform _hamster;

    private void Update()
    {
        transform.position = new Vector3(_hamster.position.x, transform.position.y,transform.position.z);
    }
}
