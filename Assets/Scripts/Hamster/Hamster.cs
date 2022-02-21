using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


[RequireComponent(typeof(HamsterMover))]
public class Hamster : MonoBehaviour
{
    public UnityEvent ActivateAfterWin;

    public event UnityAction<Trap> TrapInformationHasBeenTransmitted;
    public event UnityAction FlatVertically;
    public event UnityAction FlatHorizontal;

    [SerializeField] private HamsterType _type;
    [SerializeField] private TMP_Text _nameText;

    private Transform _finishLine;
    private HamsterMover _hamsterMover;
    private bool _isWin = false;
    private bool _isLose = false;
    private string _name;
    private float _distanceToFinishLine = 0f;

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

    public void Stun(float stunTime)
    {
        _hamsterMover.Stun(stunTime);
    }

    public void FlattenVertically()
    {
        FlatVertically?.Invoke();
    }
    
    public void FlattenHorizontal()
    {
        FlatHorizontal?.Invoke();
    }

    public void Win()
    {
        if (_isLose)
            return;
        _isWin = true;
        ActivateAfterWin?.Invoke();
    }

    public void Lose()
    {
        _isLose = true;
        _distanceToFinishLine = Vector3.Distance(transform.position, _finishLine.position);
        _hamsterMover.Lose();
    }
}

public enum HamsterType
{
    Player,
    AI
}


