using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerWeapon : MonoBehaviour
{
    private float swingCooldown = 0;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private int swordDamage = 10;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {
            attack();
        }
        manageCooldowns();
    }

    private void attack() {
        if (swingCooldown <= 0) {
            Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hits) {
                if (enemy.tag == "Boss") {
                    Debug.Log(enemy.name + " hit.");
                    enemy.GetComponent<BossController>().damageBoss(swordDamage);
                }
            }
            swingCooldown = 3f;
        }
        return;
    }
    
    void OnDrawGizmosSelected() {
        if (attackPoint == null) {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void manageCooldowns() {
        swingCooldown -= Time.deltaTime;
    }
}
