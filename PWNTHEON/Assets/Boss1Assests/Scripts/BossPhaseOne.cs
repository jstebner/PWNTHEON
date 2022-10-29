using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseOne : StateMachineBehaviour
{
    // private int magicBulletVolleys = 3;
    // private bool alternateSlamAndSoundBlast = false;
    // private string nextAttack;

    // // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     nextAttack = "None";
    //     magicBulletVolleys--;
    // }

    // // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    // override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     if (nextAttack != "None") {
    //         animator.SetTrigger(nextAttack);
    //     } else {
    //         if (magicBulletVolleys <= 0) {
    //             alternateSlamAndSoundBlast = !alternateSlamAndSoundBlast;
    //             magicBulletVolleys = 3;
    //             if (alternateSlamAndSoundBlast) {
    //                 nextAttack = "Physical Slam";
    //             } else {
    //                 nextAttack = "Sound Blast";
    //             }
    //         } else {
    //             nextAttack = "Default Attack";
    //         }
    //     }
    // }

    // // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //    animator.ResetTrigger("Default Attack");
    //    animator.ResetTrigger("Physical Slam");
    //    animator.ResetTrigger("Sound Blast");
    // }

    Transform player;
    BossController boss;

    private int magicBulletVolleys = 2;
    private int remainingBulletsInVolley = 3;
    private bool alternateSlamAndSoundBlast = true;
    private string nextAttack;

    private float magicBulletCooldown = 0.25f;
    private float meleeCooldown = 2f;
    private float magicBulletVolleyCooldown = 2f;
    private float tooCloseToBossCooldown = 0.20f;
    private const float maxMagicBulletCooldownTime = 0.25f;
    private const float maxMeleeCooldownTime = 2f;
    private const float maxMagicBulletVolleyCooldownTime = 2f;
    private const float maxTooCloseToBossCooldownTime = 0.20f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boss = animator.GetComponent<BossController>();
        nextAttack = "None";
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (nextAttack != "None") {
            animator.SetTrigger(nextAttack);
        } else {
            if (Vector3.Distance(player.transform.position, boss.transform.position) <= 2.5f) {
                if (meleeCooldown <= 0) {
                    if (tooCloseToBossCooldown <= 0) {
                        nextAttack = "Melee";
                        meleeCooldown = maxMeleeCooldownTime;
                        return;
                    } else {
                        tooCloseToBossCooldown -= Time.deltaTime;
                    }
                } else {
                    meleeCooldown -= Time.deltaTime;
                }
            } else {
                nextAttack = "None";
                tooCloseToBossCooldown = maxTooCloseToBossCooldownTime;
            }
            if (magicBulletVolleys > 0) {
                if (remainingBulletsInVolley > 0 && magicBulletCooldown <= 0) {
                    if (magicBulletVolleyCooldown <= 0) {
                        nextAttack = "Default Attack";
                        remainingBulletsInVolley--;
                        magicBulletCooldown = maxMagicBulletCooldownTime;
                    }
                } 
                if (remainingBulletsInVolley <= 0) {
                    remainingBulletsInVolley = 3;
                    magicBulletVolleyCooldown = maxMagicBulletVolleyCooldownTime;
                    magicBulletVolleys--;
                }
            } else {
                if (alternateSlamAndSoundBlast) {
                    nextAttack = "Physical Slam";
                } else {
                    nextAttack = "Sound Blast";
                }
                magicBulletVolleys = 2;
                alternateSlamAndSoundBlast = !alternateSlamAndSoundBlast;
            }
            magicBulletCooldown -= Time.deltaTime;
            meleeCooldown -= Time.deltaTime;
            magicBulletVolleyCooldown -= Time.deltaTime;
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
