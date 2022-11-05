using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : StateMachineBehaviour
{
    private bool bossStart = true;
    private AudioSource bossIntroSoundEffect;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossIntroSoundEffect = animator.gameObject.GetComponent<BossController>().getIntroAudioSource();
        if (bossStart) {
            bossIntroSoundEffect.Play();
            animator.gameObject.GetComponent<BossController>().canTakeDamage = false;
        }
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bossStart) {
            if (!bossIntroSoundEffect.isPlaying) {
                bossStart = false;
                animator.SetTrigger("Return To Idle");
                GameObject.Find("musicController").GetComponent<AudioSource>().Play();
            } else {
            }
        } else {
            animator.SetTrigger("Return To Idle");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Return To Idle");
        animator.gameObject.GetComponent<BossController>().canTakeDamage = true;
    }
}
