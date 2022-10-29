using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSoundBlastAttack : StateMachineBehaviour
{
    BossController boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossController>();
        boss.newSoundBlast();
        animator.SetTrigger("Return To Idle");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Return To Idle");
    }
}
