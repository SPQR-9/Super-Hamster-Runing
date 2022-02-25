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

    [SerializeField] private HamsterType _type;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Transform _respawnPoint;

    private Transform _finishLine;
    private HamsterMover _hamsterMover;
    private bool _isWin = false;
    private bool _isLose = false;
    private string _name;
    private float _distanceToFinishLine = 0f;
    private Coroutine _respawnCoroutine = null;

    public float DistanceToFinishLine => _distanceToFinishLine;
    public string Name => _name;

    public HamsterType Type => _type;
    public bool IsWin => _isWin;

    private void Awake()
    {
        _hamsterMover = GetComponent<HamsterMover>();
    }

    public void SetName(string name)
    {
        _name = name;
        _nameText.text = name;
    }

    public void SetFinishLinePosition(Transform finishLine)
    {
        _finishLine = finishLine;
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
        if (_isLose)
            return;
        _isWin = true;
        _hamsterMover.Win();
        Won?.Invoke();
    }

    public void Respawn()
    {
        transform.SetPositionAndRotation(_respawnPoint.position,_respawnPoint.rotation);
        _hamsterMover.EnableRigidbodyRestriction();
        SetInfoAboutTrap(null);
        Respauned?.Invoke();
        ActivateAfterRespawn?.Invoke();
        
    }

    public void Lose()
    {
        _isLose = true;
        _distanceToFinishLine = Vector3.Distance(transform.position, _finishLine.position);
        _hamsterMover.Lose();
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


