using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseOne : StateMachineBehaviour
{
    private int magicBulletVolleys = 3;
    private bool alternateSlamAndSoundBlast = false;
    private string nextAttack;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        nextAttack = "None";
        magicBulletVolleys--;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (nextAttack != "None") {
            animator.SetTrigger(nextAttack);
        } else {
            if (magicBulletVolleys <= 0) {
                alternateSlamAndSoundBlast = !alternateSlamAndSoundBlast;
                magicBulletVolleys = 3;
                if (alternateSlamAndSoundBlast) {
                    nextAttack = "Physical Slam";
                } else {
                    nextAttack = "Sound Blast";
                }
            } else {
                nextAttack = "Default Attack";
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Default Attack");
       animator.ResetTrigger("Physical Slam");
       animator.ResetTrigger("Sound Blast");
    }
}
