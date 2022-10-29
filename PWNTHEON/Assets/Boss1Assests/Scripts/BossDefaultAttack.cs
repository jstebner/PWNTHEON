using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDefaultAttack : StateMachineBehaviour
{
    Transform player;
    BossController boss;
    private float lastAttack = 0f;
    private float timeBetweenAttacks = 1.25f;
    private int remainingAttacks = 3;
    private float timeSinceLastMelee = 0f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boss = animator.GetComponent<BossController>();
        lastAttack = timeBetweenAttacks + .25f;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(player.transform.position, boss.transform.position) <= 2.5f && timeSinceLastMelee <= 0) {
            timeSinceLastMelee = 5f;
            animator.SetTrigger("Melee");
        }
        if (remainingAttacks > 0 && lastAttack <= 0) {
            Vector3 fireballSpawn = boss.transform.position + Vector3.down * 0.75f;
            boss.newBossFireball(player.position, fireballSpawn);
            boss.newBossFireball(player.position + Vector3.left, fireballSpawn + Vector3.left * 0.5f);
            boss.newBossFireball(player.position + Vector3.right, fireballSpawn + Vector3.right * 0.5f);
            remainingAttacks--;
            lastAttack = timeBetweenAttacks;
        }
        if (remainingAttacks <= 0) {
            animator.SetTrigger("Return To Idle");
            remainingAttacks = 3;
        }
        lastAttack -= Time.deltaTime;
        timeSinceLastMelee -= Time.deltaTime;
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Return To Idle");
       animator.ResetTrigger("Melee");
    }
}
