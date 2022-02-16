using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinHamsterChecker : MonoBehaviour
{
    public UnityEvent ActivateAfterWin;
    public UnityEvent ActivateAfterLose;

    [SerializeField] private List<Hamster> _hamsters;
    
    public void CheckWinnerHamster()
    {
        foreach (var hamster in _hamsters)
        {
            if(hamster.IsWin)
            {
                if (hamster.Type == HamsterType.Player)
                    ActivateAfterWin?.Invoke();
                else
                    ActivateAfterLose?.Invoke();
            }
            else
                hamster.Lose();
        }
    }

}
