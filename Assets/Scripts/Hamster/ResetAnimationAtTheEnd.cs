using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimationAtTheEnd : StateMachineBehaviour
{
    private const string _flatVertically = "FlatVertically";
    private const string _flatHorizontal = "FlatHorizontal";

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(_flatVertically, false);
        animator.SetBool(_flatHorizontal, false);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(_flatVertically, false);
        animator.SetBool(_flatHorizontal, false);
    }

}
