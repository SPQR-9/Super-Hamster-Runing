using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HamstersObserver : MonoBehaviour
{
    public UnityEvent ActivateAfterWin;
    public UnityEvent ActivateAfterLose;
    public UnityEvent ActivateAfterAWhileAfterWin;
    public UnityEvent ActivateAfterAWhileAfterLose;


    [SerializeField] private List<Hamster> _hamsters;
    [SerializeField] private string _playerName = "You";
    [SerializeField] private List<string> _aIHamsterNames;
    [SerializeField] private Transform _finishLine;
    [SerializeField] private LeaderboardChecker _leaderboardChecker;
    [SerializeField] private float _activationTime = 1f;

    private void Awake()
    {
        foreach (var hamster in _hamsters)
        {
            if (hamster.Type == HamsterType.Player)
                hamster.SetName(_playerName);
            else
            {
                int randNameIndex = Random.Range(0, _aIHamsterNames.Count);
                hamster.SetName(_aIHamsterNames[randNameIndex]);
                _aIHamsterNames.RemoveAt(randNameIndex);
            }
            hamster.SetFinishLinePosition(_finishLine);
        }
    }

    public void CheckWinnerHamster()
    {
        Hamster winner = null;
        foreach (var hamster in _hamsters)
        {
            if(hamster.IsWin)
                winner = hamster;
            else
                hamster.Lose();
        }
        _leaderboardChecker.PutInTheirPlaces(_hamsters);
        if (winner.Type == HamsterType.Player)
        {
            StartCoroutine(WaitAfterAWhileAfterWinning());
            ActivateAfterWin?.Invoke();
        }
        else
        {
            StartCoroutine(WaitAfterAWhileAfterLosing());
            ActivateAfterLose?.Invoke();
        }
    }


    private IEnumerator WaitAfterAWhileAfterWinning()
    {
        yield return new WaitForSeconds(_activationTime);
        ActivateAfterAWhileAfterWin?.Invoke();
    }
    
    private IEnumerator WaitAfterAWhileAfterLosing()
    {
        yield return new WaitForSeconds(_activationTime);
        ActivateAfterAWhileAfterLose?.Invoke();
    }
}
