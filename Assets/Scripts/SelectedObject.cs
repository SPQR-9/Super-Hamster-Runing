using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedObject : MonoBehaviour
{
    [SerializeField] private Status _status; 
    

    //

    public Status GetObject()
    {
        gameObject.SetActive(false);
        return _status;
    }

    public void Throw()
    {
        //
    }
}

public enum Status
{
    Edible,
    Inedible
}
