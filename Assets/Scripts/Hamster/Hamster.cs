using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


[RequireComponent(typeof(HamsterMover))]
public class Hamster : MonoBehaviour
{
    public UnityEvent ActivateAfterRespawn;

    public event UnityAction<Trap> TrapInformationHasBeenTransmitted;
    public event UnityAction FlatVertically;
    public event UnityAction FlatHorizontal;
    public event UnityAction Fall;
    public event UnityAction Discharged;
    public event UnityAction Respauned;
    public event UnityAction Won;
    public event UnityAction<Hamster> Finished;
    public event UnityAction Losed;

    [SerializeField] private HamsterType _type;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private Transform _pointOfVictoryAnimation;

    private HamsterMover _hamsterMover;
    private string _name;
    private Coroutine _respawnCoroutine = null;


    public string Name => _name;
    public HamsterType Type => _type;

    private void Awake()
    {
        _hamsterMover = GetComponent<HamsterMover>();
    }

    public void SetName(string name)
    {
        _name = name;
        _nameText.text = name;
    }

    public void SetInfoAboutTrap(Trap trap)
    {
        TrapInformationHasBeenTransmitted?.Invoke(trap);
    }

    public void ReboundAndStun(float stunTime)
    {
        _hamsterMover.ReboundAndStun(stunTime);
    }

    public void HitStun(float stunTime)
    {
        _hamsterMover.HitStun(stunTime);
    }

    public void Daze(float stunTime)
    {
        _hamsterMover.Daze(stunTime);
    }

    public void SetNewSpawnPoint(Transform transformPoint)
    {
        _respawnPoint = transformPoint;
    }

    public void FlattenVertically()
    {
        FlatVertically?.Invoke();
    }

    public void DischargeIntoWater(Vector3 direction)
    {
        _hamsterMover.DisableRigidbodyRestriction();
        _hamsterMover.DiscardHamster(direction);
        Discharged?.Invoke();
    }

    public void WaiteRespawn(float pausedTimeBeforeDeath)
    {
        if (_respawnCoroutine != null)
            StopCoroutine(_respawnCoroutine);
        _respawnCoroutine = StartCoroutine(WaitForPauseBeforeRespawn(pausedTimeBeforeDeath));
    }
    
    public void FlattenHorizontal()
    {
        FlatHorizontal?.Invoke();
    }

    public void ToFall()
    {
        _hamsterMover.DisableRigidbodyRestriction();
        Fall?.Invoke();
    }

    public void Win()
    {
        Won?.Invoke();
        _hamsterMover.DisablePhysics();
        transform.parent = _pointOfVictoryAnimation;
    }

    public void Lose()
    {
        _hamsterMover.ProhibitMovement();
        Losed?.Invoke();
    }

    public void Finish()
    {
        _hamsterMover.ProhibitMovement();
        Finished?.Invoke(this);
    }

    public void Respawn()
    {
        _hamsterMover.DisableKinematic();
        transform.position = _respawnPoint.position;
        transform.rotation = _respawnPoint.rotation;
        _hamsterMover.EnableKinematic();
        _hamsterMover.EnableRigidbodyRestriction();
        SetInfoAboutTrap(null);
        Respauned?.Invoke();
        ActivateAfterRespawn?.Invoke();
    }

    private IEnumerator WaitForPauseBeforeRespawn(float time)
    {
        yield return new WaitForSeconds(time);
        Respawn();
    }
}


public enum HamsterType
{
    Player,
    AI
}


