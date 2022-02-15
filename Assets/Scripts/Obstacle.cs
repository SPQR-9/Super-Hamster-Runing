using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    public UnityEvent ActivateAfterAnyHamsterCollision;
    public UnityEvent ActivateAfterPlayerHamsterCollision;
    public UnityEvent ActivateAfterAIHamsterCollision;

    private Hamster _hamster;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out HamsterCollider hamsterCollider))
        {
            _hamster = hamsterCollider.GetHamster();
            ActivateAfterAnyHamsterCollision?.Invoke();
            if (_hamster.Type == HamsterType.Player)
                ActivateAfterPlayerHamsterCollision?.Invoke();
            else
                ActivateAfterAIHamsterCollision?.Invoke();
        }
    }

    public void HamsterStunAndRebound(float stunTime = 2f)
    {
        _hamster.ReboundAndStun(stunTime);
    }

    public void HamsterStun(float stunTime)
    {
        _hamster.Stun(stunTime);
    }

    public void HamsterFlattenVertically()
    {
        _hamster.FlattenVertically();
    }

    public void HamsterFlattenHorizontal()
    {
        _hamster.FlattenHorizontal();
    }
}
