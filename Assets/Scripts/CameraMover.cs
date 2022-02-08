using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _correctionX = -0.5f;

    private void Update()
    {
        transform.position = new Vector3(_player.position.x + _correctionX, transform.position.y, transform.position.z);
    }
}
