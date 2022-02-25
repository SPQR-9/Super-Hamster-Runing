using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardChecker : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _placesTexts;
    [SerializeField] private Color _playerTextColor;
    [SerializeField] private List<Image> _placeImages;
    [SerializeField] private Sprite _playerPlaceSprite;

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
                    SetName(i, j);
                    hamsters.RemoveAt(j);
                    minDistanceHamsterIndex = -1;
                    break;
                }
                else if (minDistanceHamsterIndex == -1 || hamsters[minDistanceHamsterIndex].DistanceToFinishLine > _hamstersOnBoard[j].DistanceToFinishLine)
                    minDistanceHamsterIndex = j;
            }
            if (minDistanceHamsterIndex >= 0)
            {
                SetName(i, minDistanceHamsterIndex);
                hamsters.RemoveAt(minDistanceHamsterIndex);
            }
        }
    }

    private void SetName(int placesTextsindex, int hamsterIndex)
    {
        _placesTexts[placesTextsindex].text = _hamstersOnBoard[hamsterIndex].Name;
        if (_hamstersOnBoard[hamsterIndex].Type == HamsterType.Player)
        {
            _placeImages[placesTextsindex].sprite = _playerPlaceSprite;
            _placesTexts[placesTextsindex].color = _playerTextColor;
        }
    }
}
