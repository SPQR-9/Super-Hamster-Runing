using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HamsterCollider : MonoBehaviour
{

    private Hamster _hamster;

    public void SetHamster(Hamster hamster)
    {
        _hamster = hamster;
    }

    public Hamster GetHamster()
    {
        return _hamster;
    }

}

