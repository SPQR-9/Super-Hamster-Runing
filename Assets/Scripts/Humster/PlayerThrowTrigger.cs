using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out SelectedObject selectedObject))
        {
            selectedObject.Throw();
        }
    }
}
