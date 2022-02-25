using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HamstersObserver : MonoBehaviour
{
    public UnityEvent ActivateAfterPlayerWin;
    public UnityEvent ActivateAfterPlayerLose;
    public UnityEvent ActivateAfterAWhileAfterPlayerFinished;


    [SerializeField] private List<Hamster> _notFinishedHamsters;
    [SerializeField] private string _playerName = "You";
    [SerializeField] private List<string> _aIHamsterNames;
    [SerializeField] private Transform _finishLine;
    [SerializeField] private LeaderboardChecker _leaderboardChecker;
    [SerializeField] private float _activationTime = 1f;

    private bool _isWinnerHamsterFound = false;

    private void Awake()
    {
        foreach (var hamster in _notFinishedHamsters)
        {
            if (hamster.Type == HamsterType.Player)
                hamster.SetName(_playerName);
            else
            {
                int randNameIndex = Random.Range(0, _aIHamsterNames.Count);
                hamster.SetName(_aIHamsterNames[randNameIndex]);
                _aIHamsterNames.RemoveAt(randNameIndex);
            }
        }
    }

    private void OnEnable()
    {
        foreach (var hamster in _notFinishedHamsters)
        {
            hamster.Finished += CheckFinishedHamster;
        }
    }

    private void OnDisable()
    {
        foreach (var hamster in _notFinishedHamsters)
        {
            hamster.Finished -= CheckFinishedHamster;
        }
    }

    private void CheckFinishedHamster(Hamster hamster)
    {
        _leaderboardChecker.SetHamsterInPlace(hamster);
        if (!_isWinnerHamsterFound)
        {
            _isWinnerHamsterFound = true;
            hamster.Win();
            RemoveCurrentHamsterFromTheList(hamster);
            if (hamster.Type == HamsterType.Player)
            {
                StartCoroutine(WaitAfterAWhileAfterPlayerFinishing());
                ActivateAfterPlayerWin?.Invoke();
                DeterminePlacesByDistanceToFinishLine();
            }
        }
        else
        {
            hamster.Lose();
            RemoveCurrentHamsterFromTheList(hamster);
            if (hamster.Type == HamsterType.Player)
            {
                StartCoroutine(WaitAfterAWhileAfterPlayerFinishing());
                ActivateAfterPlayerLose?.Invoke();
                DeterminePlacesByDistanceToFinishLine();
            }
        }
    }

    private void RemoveCurrentHamsterFromTheList(Hamster currentHamster)
    {
        for (int i = 0; i < _notFinishedHamsters.Count; i++)
        {
            if(currentHamster.Name==_notFinishedHamsters[i].Name)
            {
                _notFinishedHamsters.RemoveAt(i);
                break;
            }
        }
    }

    private void DeterminePlacesByDistanceToFinishLine()
    {
        while(_notFinishedHamsters.Count>0)
        {
            int indexHamsterMinDistance = -1;
            for (int i = 0; i < _notFinishedHamsters.Count; i++)
            {
                if (indexHamsterMinDistance == -1)
                    indexHamsterMinDistance = i;
                else if (Vector3.Distance(_notFinishedHamsters[i].transform.position, _finishLine.position) <
                         Vector3.Distance(_notFinishedHamsters[indexHamsterMinDistance].transform.position, _finishLine.position))
                        indexHamsterMinDistance = i;
            }
            if (indexHamsterMinDistance == -1)
                break;
            else
            { 
                _notFinishedHamsters[indexHamsterMinDistance].Lose();
                _leaderboardChecker.SetHamsterInPlace(_notFinishedHamsters[indexHamsterMinDistance]);
                RemoveCurrentHamsterFromTheList(_notFinishedHamsters[indexHamsterMinDistance]);
            }
        }
    }

    private IEnumerator WaitAfterAWhileAfterPlayerFinishing()
    {
        yield return new WaitForSeconds(_activationTime);
        ActivateAfterAWhileAfterPlayerFinished?.Invoke();
    }
}
