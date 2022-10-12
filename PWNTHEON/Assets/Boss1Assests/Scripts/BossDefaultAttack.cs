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

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boss = animator.GetComponent<BossController>();
        remainingAttacks = 3;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
        }
        lastAttack -= Time.deltaTime;
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Return To Idle");
    }
}
