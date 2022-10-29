using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDefaultAttack : StateMachineBehaviour
{
    Transform player;
    BossController boss;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boss = animator.GetComponent<BossController>();
        Vector3 fireballSpawn = boss.transform.position + Vector3.down * 0.75f;
        boss.newBossFireball(player.position, fireballSpawn);
        boss.newBossFireball(player.position + Vector3.left, fireballSpawn + Vector3.left * 0.5f);
        boss.newBossFireball(player.position + Vector3.right, fireballSpawn + Vector3.right * 0.5f);
        animator.SetTrigger("Return To Idle");
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Return To Idle");
    }
}
