using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryTrigger : MonoBehaviour
{
    [SerializeField] private Direction _directionType;

    private Vector3 _direction;

    private void Awake()
    {
        switch (_directionType)
        {
            case Direction.Forward:
                _direction = Vector3.forward;
                break;
            case Direction.Back:
                _direction = Vector3.back;
                break;
            case Direction.Right:
                _direction = Vector3.right;
                break;
            case Direction.Left:
                _direction = Vector3.left;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out HamsterMover hamsterMover))
        {
            hamsterMover.SetNewDirection(_direction);
        }
    }

    public enum Direction
    {
        Forward,
        Back,
        Right,
        Left
    }
}
