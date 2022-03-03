using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private GameObject _waterEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Hamster hamster))
            Instantiate(_waterEffect, hamster.transform.position,new Quaternion());
    }
}
