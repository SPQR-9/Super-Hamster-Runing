using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardChecker : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _placesTexts;

    private List<Hamster> _hamstersOnBoard;

    public void PutInTheirPlaces(List<Hamster> hamsters)
    {
        _hamstersOnBoard = hamsters;
        for (int i = 0; i < _placesTexts.Count; i++)
        {
            int minDistanceHamsterIndex = -1;
            for (int j = 0; j < _hamstersOnBoard.Count; j++)
            {
                if (hamsters[j].IsWin)
                {
                    _placesTexts[i].text = $"{i + 1}) {_hamstersOnBoard[j].Name}";
                    hamsters.RemoveAt(j);
                    minDistanceHamsterIndex = -1;
                    break;
                }
                else
                {
                    if (minDistanceHamsterIndex == -1 || hamsters[minDistanceHamsterIndex].DistanceToFinishLine > _hamstersOnBoard[j].DistanceToFinishLine)
                        minDistanceHamsterIndex = j;
                }
            }
            if(minDistanceHamsterIndex>=0)
            {
                _placesTexts[i].text = $"{i + 1}) {hamsters[minDistanceHamsterIndex].Name}";
                _hamstersOnBoard.RemoveAt(minDistanceHamsterIndex);
            }
        }
    }
}
