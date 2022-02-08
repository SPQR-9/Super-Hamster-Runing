using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectSelecter : MonoBehaviour
{
    [SerializeField] private Humster _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SelectedObject selectedObject))
        {
            _player.GetSelectedObject(selectedObject.GetObject());
        }
    }
}
