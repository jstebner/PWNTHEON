using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseOne : StateMachineBehaviour
{
    private float timeSinceLastMagicBullets = 0f;
    private int magicBulletVolleys = 3;
    private bool alternateSlamAndSoundBlast = true;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        magicBulletVolleys--;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (magicBulletVolleys <= 0) {
            if (alternateSlamAndSoundBlast) {
                animator.SetTrigger("Physical Slam");
            } else {
                animator.SetTrigger("Sound Blast");
            }
            alternateSlamAndSoundBlast = !alternateSlamAndSoundBlast;
            magicBulletVolleys = 3;
        } else {
            if (timeSinceLastMagicBullets >= 1.5f) {
                timeSinceLastMagicBullets = 0f;
                animator.SetTrigger("Default Attack");
            }
        }
        timeSinceLastMagicBullets += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Default Attack");
       animator.ResetTrigger("Physical Slam");
    }
}
